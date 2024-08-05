using System;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Email
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pass.PasswordChar = '*';
        }

        private async void send_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(from.Text) ||
                    string.IsNullOrWhiteSpace(to.Text) ||
                    string.IsNullOrWhiteSpace(subj.Text) ||
                    string.IsNullOrWhiteSpace(content.Text) ||
                    string.IsNullOrWhiteSpace(pass.Text))
                {
                    MessageBox.Show("Please fill in all the fields.");
                    return;
                }

                MailMessage o = new MailMessage(from.Text, to.Text, subj.Text, content.Text);
                NetworkCredential netCred = new NetworkCredential(from.Text, pass.Text);
                SmtpClient smtpobj = new SmtpClient("smtp.office365.com", 587)
                {
                    EnableSsl = true,
                    Credentials = netCred,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Timeout = 20000 // Set a reasonable timeout period
                };

                await smtpobj.SendMailAsync(o);

                // Clear the fields after successful email send
                from.Text = string.Empty;
                to.Text = string.Empty;
                subj.Text = string.Empty;
                content.Text = string.Empty;
                pass.Text = string.Empty;

                MessageBox.Show("Email has been sent.");
            }
            catch (SmtpException smtpEx)
            {
                MessageBox.Show($"SMTP Error: {smtpEx.Message}");
            }
            catch (SocketException sockEx)
            {
                MessageBox.Show($"Socket Error: {sockEx.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"General Error: {ex.Message}");
            }
        }
    }
}
