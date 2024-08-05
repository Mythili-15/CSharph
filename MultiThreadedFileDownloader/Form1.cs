using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiThreadedFileDownloader
{
    public partial class Form1 : Form
    {
        private Dictionary<string, DownloadTask> downloadTasks;
        public Form1()
        {
            InitializeComponent();
            downloadTasks = new Dictionary<string, DownloadTask>();
            addurl.Click += ButtonDownload_Click;
        }

        private async void ButtonDownload_Click(object sender, EventArgs e)
        {
            string url = urlText.Text.Trim(); // Get the URL from the text box
            urlText.Clear(); // Clear the text box content after pressing download button
            if (!string.IsNullOrEmpty(url))
            {
                var downloadTask = new DownloadTask(url);
                downloadTasks[url] = downloadTask;
                await DownloadFileAsync(downloadTask); // Pass the DownloadTask to the method
            }
            else
            {
                MessageBox.Show("Please enter a valid URL.");
            }
        }

        private async Task DownloadFileAsync(DownloadTask downloadTask)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    // Create progress bar, label, open button, pause button, resume button, and delete button
                    var progressBar = new ProgressBar { Width = 300 };
                    var label = new Label { Text = $"Downloading {downloadTask.Url}", AutoSize = true };
                    var openButton = new Button { Text = "Open", Enabled = false, Width = 80, Height = 30 };
                    var deleteButton = new Button { Text = "Delete", Enabled = false, Width = 80, Height = 30 };
                    

                    var panel = new FlowLayoutPanel { Width = flowLayoutPanelDownloads.Width - 30, Height = 50, AutoSize = true };
                    panel.Controls.Add(label);
                    panel.Controls.Add(progressBar);
                    panel.Controls.Add(openButton);
                    
                    panel.Controls.Add(deleteButton);

                    flowLayoutPanelDownloads.Invoke(new Action(() => flowLayoutPanelDownloads.Controls.Add(panel)));

                    var response = await httpClient.GetAsync(downloadTask.Url, HttpCompletionOption.ResponseHeadersRead, downloadTask.CancellationTokenSource.Token);
                    response.EnsureSuccessStatusCode();

                    var totalBytes = response.Content.Headers.ContentLength ?? -1L; // Get file length or store -1
                    var canReportProgress = totalBytes != -1;
                    var readBytes = 0L;

                    var filePath = GetFileNameFromUrl(downloadTask.Url);
                    using (var contentStream = await response.Content.ReadAsStreamAsync()) // Asynchronously retrieves the content stream from the HTTP response
                    using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true)) // Creates a file stream for writing to a file specified
                    {
                        var buffer = new byte[8192];
                        int bytesRead;

                        while ((bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length, downloadTask.CancellationTokenSource.Token)) > 0) // Reads data from the content stream into the buffer
                        {
                            await fileStream.WriteAsync(buffer, 0, bytesRead, downloadTask.CancellationTokenSource.Token); // Writes the data read from the content stream into the file stream
                            readBytes += bytesRead;

                            if (canReportProgress)
                            {
                                var progress = (int)((double)readBytes / totalBytes * 100);
                                progressBar.Invoke(new Action(() => progressBar.Value = progress));
                                label.Invoke(new Action(() => label.Text = $"Downloading {FormatBytes(readBytes)} / {FormatBytes(totalBytes)}"));
                            }
                        }
                    }

                    label.Invoke(new Action(() => label.Text = $"Downloaded {downloadTask.Url} ({FormatBytes(totalBytes)})"));
                    openButton.Invoke(new Action(() => openButton.Enabled = true));
                    
                    deleteButton.Invoke(new Action(() => deleteButton.Enabled = true));

                    openButton.Click += (s, e) =>
                    {
                        try
                        {
                            if (File.Exists(filePath))
                            {
                                Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
                            }
                            else
                            {
                                MessageBox.Show("File does not exist or cannot be found.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"An error occurred while trying to open the file: {ex.Message}");
                        }
                    };

                    deleteButton.Click += (s, e) =>
                    {
                        if (downloadTasks.ContainsKey(downloadTask.Url))
                        {
                            downloadTask.CancellationTokenSource.Cancel();
                            File.Delete(filePath);
                            panel.Dispose();
                            downloadTasks.Remove(downloadTask.Url);
                        }
                    };
       
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error downloading {downloadTask.Url}: {ex.Message}");
                }
            }
        }

        private string GetFileNameFromUrl(string url)
        {
            return Path.GetFileName(new Uri(url).LocalPath);
        }

        private string FormatBytes(long bytes)
        {
            string[] suffixes = { "B", "KB", "MB", "GB", "TB" };
            int suffixIndex = 0;
            double byteCount = bytes;

            while (byteCount >= 1024 && suffixIndex < suffixes.Length - 1)
            {
                byteCount /= 1024;
                suffixIndex++;
            }

            return $"{byteCount:0.##} {suffixes[suffixIndex]}";
        }

        private class DownloadTask
        {
            public string Url { get; set; }
            public CancellationTokenSource CancellationTokenSource { get; set; }

            public DownloadTask(string url)
            {
                Url = url;
                CancellationTokenSource = new CancellationTokenSource();
            }
        }
    }
}
