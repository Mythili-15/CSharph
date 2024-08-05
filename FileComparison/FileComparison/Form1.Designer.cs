namespace FileComparison
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            txtFile1 = new RichTextBox();
            txtFile2 = new RichTextBox();
            btnSelectFile1 = new Button();
            btnSelectFile2 = new Button();
            btnCompare = new Button();
            lblStatus = new Label();
            btnClear = new Button();
            SuspendLayout();
            // 
            // txtFile1
            // 
            txtFile1.Location = new Point(15, 62);
            txtFile1.Margin = new Padding(4);
            txtFile1.Name = "txtFile1";
            txtFile1.Size = new Size(705, 675);
            txtFile1.TabIndex = 0;
            txtFile1.Text = "";
            txtFile1.VScroll += txtFile1_VScroll;
            // 
            // txtFile2
            // 
            txtFile2.Location = new Point(728, 62);
            txtFile2.Margin = new Padding(4);
            txtFile2.Name = "txtFile2";
            txtFile2.Size = new Size(691, 675);
            txtFile2.TabIndex = 1;
            txtFile2.Text = "";
            txtFile2.WordWrap = false;
            txtFile2.VScroll += txtFile2_VScroll;
            txtFile2.TextChanged += txtFile2_TextChanged;
            // 
            // btnSelectFile1
            // 
            btnSelectFile1.Location = new Point(15, 16);
            btnSelectFile1.Margin = new Padding(4);
            btnSelectFile1.Name = "btnSelectFile1";
            btnSelectFile1.Size = new Size(125, 38);
            btnSelectFile1.TabIndex = 2;
            btnSelectFile1.Text = "Select File 1";
            btnSelectFile1.UseVisualStyleBackColor = true;
            btnSelectFile1.Click += btnSelectFile1_Click;
            // 
            // btnSelectFile2
            // 
            btnSelectFile2.Location = new Point(728, 16);
            btnSelectFile2.Margin = new Padding(4);
            btnSelectFile2.Name = "btnSelectFile2";
            btnSelectFile2.Size = new Size(125, 38);
            btnSelectFile2.TabIndex = 3;
            btnSelectFile2.Text = "Select File 2";
            btnSelectFile2.UseVisualStyleBackColor = true;
            btnSelectFile2.Click += btnSelectFile2_Click;
            // 
            // btnCompare
            // 
            btnCompare.Location = new Point(595, 745);
            btnCompare.Margin = new Padding(4);
            btnCompare.Name = "btnCompare";
            btnCompare.Size = new Size(125, 38);
            btnCompare.TabIndex = 4;
            btnCompare.Text = "Compare";
            btnCompare.UseVisualStyleBackColor = true;
            btnCompare.Click += btnCompare_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(15, 777);
            lblStatus.Margin = new Padding(4, 0, 4, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(60, 25);
            lblStatus.TabIndex = 5;
            lblStatus.Text = "Status";
            // 
            // btnClear
            // 
            btnClear.Location = new Point(728, 745);
            btnClear.Margin = new Padding(4);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(125, 38);
            btnClear.TabIndex = 7;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1468, 811);
            Controls.Add(btnClear);
            Controls.Add(lblStatus);
            Controls.Add(btnCompare);
            Controls.Add(btnSelectFile2);
            Controls.Add(btnSelectFile1);
            Controls.Add(txtFile2);
            Controls.Add(txtFile1);
            Margin = new Padding(4);
            Name = "Form1";
            Text = "File Comparison";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.RichTextBox txtFile1;
        private System.Windows.Forms.RichTextBox txtFile2;
        private System.Windows.Forms.Button btnSelectFile1;
        private System.Windows.Forms.Button btnSelectFile2;
        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnClear;
    }
}
