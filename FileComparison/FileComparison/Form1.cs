using System;
using System.IO;
using System.Windows.Forms;
using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;

namespace FileComparison
{
    public partial class Form1 : Form
    {
        private string filePath1 = string.Empty;
        private string filePath2 = string.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnSelectFile1_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        filePath1 = ofd.FileName;
                        txtFile1.Text = File.ReadAllText(filePath1);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while selecting or reading the file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSelectFile2_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        filePath2 = ofd.FileName;
                        txtFile2.Text = File.ReadAllText(filePath2);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while selecting or reading the file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnCompare_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filePath1) || string.IsNullOrWhiteSpace(filePath2))
                {
                    lblStatus.Text = "Please select both files to compare.";
                    return;
                }

                string file1Content = File.ReadAllText(filePath1);
                string file2Content = File.ReadAllText(filePath2);

                var diffBuilder = new InlineDiffBuilder(new Differ());
                var diff = diffBuilder.BuildDiffModel(file1Content, file2Content);

                txtFile1.Clear();
                txtFile2.Clear();

                foreach (var line in diff.Lines)
                {
                    string lineText = line.Text + Environment.NewLine;
                    if (line.Type == ChangeType.Inserted)
                    {
                        txtFile2.SelectionStart = txtFile2.TextLength;
                        txtFile2.SelectionLength = 0;
                        txtFile2.SelectionBackColor = System.Drawing.Color.Green;
                        txtFile2.AppendText(lineText);
                        txtFile2.SelectionBackColor = txtFile2.BackColor;
                    }
                    else if (line.Type == ChangeType.Deleted)
                    {
                        txtFile1.SelectionStart = txtFile1.TextLength;
                        txtFile1.SelectionLength = 0;
                        txtFile1.SelectionBackColor = System.Drawing.Color.Red;
                        txtFile1.AppendText(lineText);
                        txtFile1.SelectionBackColor = txtFile1.BackColor;
                    }
                    else
                    {
                        txtFile1.AppendText(lineText);
                        txtFile2.AppendText(lineText);
                    }
                }

                lblStatus.Text = "Comparison completed.";
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show($"File not found: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException ex)
            {
                MessageBox.Show($"I/O error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtFile1.Clear();
                txtFile2.Clear();
                lblStatus.Text = "Cleared.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while clearing the textboxes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void txtFile1_VScroll(object sender, EventArgs e)
        {
            SyncScroll(txtFile1, txtFile2);
        }

        private void txtFile2_VScroll(object sender, EventArgs e)
        {
            SyncScroll(txtFile2, txtFile1);
        }
        private bool isSyncing = false;

        private void SyncScroll(RichTextBox source, RichTextBox target)
        {
            if (isSyncing)
                return;

            try
            {
                isSyncing = true;

                int sourceFirstCharIndex = source.GetCharIndexFromPosition(new System.Drawing.Point(1, 1));
                int sourceFirstLine = source.GetLineFromCharIndex(sourceFirstCharIndex);
                int sourceLineHeight = source.GetPositionFromCharIndex(sourceFirstCharIndex + source.Lines[sourceFirstLine].Length).Y;

                int targetFirstCharIndex = target.GetCharIndexFromPosition(new System.Drawing.Point(1, 1));
                int targetFirstLine = target.GetLineFromCharIndex(targetFirstCharIndex);
                int targetLineHeight = target.GetPositionFromCharIndex(targetFirstCharIndex + target.Lines[targetFirstLine].Length).Y;

                int sourceScrollOffset = source.GetPositionFromCharIndex(sourceFirstCharIndex).Y % sourceLineHeight;
                int targetScrollOffset = target.GetPositionFromCharIndex(targetFirstCharIndex).Y % targetLineHeight;

                int newTargetLine = sourceFirstLine;
                if (sourceScrollOffset > targetScrollOffset)
                    newTargetLine += 1;

                target.SelectionStart = target.GetFirstCharIndexFromLine(newTargetLine);
                target.ScrollToCaret();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while synchronizing scroll: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                isSyncing = false;
            }
        }

        private void txtFile2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
