using SendGridWrapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SendGridEmailTester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SendEmail();
        }
        internal async void SendEmail()
        {
            EmailSender sender = new EmailSender();
            await sender.SendEmailAsync();
        }
        }
}
