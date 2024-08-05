namespace MultiThreadedFileDownloader
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.urlText = new System.Windows.Forms.TextBox();
            this.addurl = new System.Windows.Forms.Button();
            this.flowLayoutPanelDownloads = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // urlText
            // 
            this.urlText.Location = new System.Drawing.Point(51, 46);
            this.urlText.Multiline = true;
            this.urlText.Name = "urlText";
            this.urlText.Size = new System.Drawing.Size(624, 55);
            this.urlText.TabIndex = 0;
            // 
            // addurl
            // 
            this.addurl.Location = new System.Drawing.Point(706, 43);
            this.addurl.Name = "addurl";
            this.addurl.Size = new System.Drawing.Size(218, 58);
            this.addurl.TabIndex = 1;
            this.addurl.Text = "Download";
            this.addurl.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanelDownloads
            // 
            this.flowLayoutPanelDownloads.Location = new System.Drawing.Point(51, 120);
            this.flowLayoutPanelDownloads.Name = "flowLayoutPanelDownloads";
            this.flowLayoutPanelDownloads.Size = new System.Drawing.Size(873, 500);
            this.flowLayoutPanelDownloads.TabIndex = 2;
            this.flowLayoutPanelDownloads.AutoScroll = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(993, 672);
            this.Controls.Add(this.flowLayoutPanelDownloads);
            this.Controls.Add(this.addurl);
            this.Controls.Add(this.urlText);
            this.Name = "Form1";
            this.Text = "MultiThreaded File Downloader";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox urlText;
        private System.Windows.Forms.Button addurl;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelDownloads;
    }
}