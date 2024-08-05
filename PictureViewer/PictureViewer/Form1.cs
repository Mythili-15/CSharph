using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace PictureViewer
{
    public partial class Form1 : Form
    {
        private float _rotationAngle = 0f;

        public Form1()
        {
            InitializeComponent();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void backgroundButton_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                pictureBox1.BackColor = colorDialog1.Color;
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        private void showButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(openFileDialog1.FileName);
                _rotationAngle = 0f; 
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            else
                pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
        }

        private void rotateButton_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                _rotationAngle += 90f;
                if (_rotationAngle >= 360f)
                    _rotationAngle = 0f;

                pictureBox1.Image = RotateImage(pictureBox1.Image, _rotationAngle);
            }
        }


        private Image RotateImage(Image image, float angle)
        {
            if (image == null) return null;

            // Calculate new dimensions based on 90-degree rotation
            int newWidth = image.Width;
            int newHeight = image.Height;
            if (angle % 180 != 0) // For 90 and 270 degrees
            {
                newWidth = image.Height;
                newHeight = image.Width;
            }

            Bitmap rotatedBmp = new Bitmap(newWidth, newHeight);

            using (Graphics g = Graphics.FromImage(rotatedBmp))
            {
                g.Clear(Color.Transparent);

                // Move the origin to the center of the new image
                g.TranslateTransform(newWidth / 2f, newHeight / 2f); 
                g.RotateTransform(angle); // Rotation angle in degrees

                // Move the origin back and draw the image
                g.TranslateTransform(-image.Width / 2f, -image.Height / 2f); 
                g.DrawImage(image, new Point(0, 0));
            }

            return rotatedBmp;
        }


        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            
        }

        private void cropButton_Click(object sender, EventArgs e)
        {

        }
    }
}