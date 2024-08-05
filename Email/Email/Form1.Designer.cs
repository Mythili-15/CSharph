namespace Email
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            from = new TextBox();
            to = new TextBox();
            subj = new TextBox();
            content = new TextBox();
            pass = new TextBox();
            send = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(45, 48);
            label1.Name = "label1";
            label1.Size = new Size(101, 25);
            label1.TabIndex = 0;
            label1.Text = "From Email";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(45, 172);
            label2.Name = "label2";
            label2.Size = new Size(77, 25);
            label2.TabIndex = 1;
            label2.Text = "To Email";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(45, 274);
            label3.Name = "label3";
            label3.Size = new Size(70, 25);
            label3.TabIndex = 2;
            label3.Text = "Subject";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(45, 387);
            label4.Name = "label4";
            label4.Size = new Size(75, 25);
            label4.TabIndex = 3;
            label4.Text = "Content";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(521, 48);
            label5.Name = "label5";
            label5.Size = new Size(87, 25);
            label5.TabIndex = 4;
            label5.Text = "Password";
            // 
            // from
            // 
            from.Location = new Point(79, 99);
            from.Name = "from";
            from.Size = new Size(327, 31);
            from.TabIndex = 5;
            // 
            // to
            // 
            to.Location = new Point(79, 222);
            to.Name = "to";
            to.Size = new Size(327, 31);
            to.TabIndex = 6;
           
            // 
            // subj
            // 
            subj.Location = new Point(79, 330);
            subj.Name = "subj";
            subj.Size = new Size(841, 31);
            subj.TabIndex = 7;
            // 
            // content
            // 
            content.Location = new Point(79, 425);
            content.Multiline = true;
            content.Name = "content";
            content.Size = new Size(841, 249);
            content.TabIndex = 8;
            // 
            // pass
            // 
            pass.Location = new Point(560, 99);
            pass.Name = "pass";
            pass.Size = new Size(327, 31);
            pass.TabIndex = 9;
            // 
            // send
            // 
            send.Location = new Point(560, 219);
            send.Name = "send";
            send.Size = new Size(327, 34);
            send.TabIndex = 10;
            send.Text = "Send";
            send.UseVisualStyleBackColor = true;
            send.Click += send_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(998, 704);
            Controls.Add(send);
            Controls.Add(pass);
            Controls.Add(content);
            Controls.Add(subj);
            Controls.Add(to);
            Controls.Add(from);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox from;
        private TextBox to;
        private TextBox subj;
        private TextBox content;
        private TextBox pass;
        private Button send;
    }
}
