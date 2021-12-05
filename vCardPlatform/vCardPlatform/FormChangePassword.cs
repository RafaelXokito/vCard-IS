using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace vCardPlatform
{
    public partial class FormChangePassword : Form
    {
        string baseURI = @"http://localhost:59458/";
        public FormChangePassword()
        {
            InitializeComponent();
        }

        private void checkBoxOldPassword_CheckedChanged(object sender, EventArgs e)
        {
            textBoxOldPassword.PasswordChar = !checkBoxOldPassword.Checked ? '*' : '\0';
        }

        private void checkBoxNewPassword_CheckedChanged(object sender, EventArgs e)
        {
            textBoxNewPassword.PasswordChar = !checkBoxNewPassword.Checked ? '*' : '\0';
        }

        public class Secret
        {
            public string Password { get; set; }
            public string NewPassword { get; set; }
        }
        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
