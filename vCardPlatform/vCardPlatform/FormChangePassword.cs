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
            if (FormMainApplication.password != textBoxOldPassword.Text)
            {
                MessageBox.Show("Old Password does not match with the current password!");
                return;
            }

            try {
                var client = new RestSharp.RestClient(baseURI);

                Secret secret = new Secret
                {
                    Password = textBoxOldPassword.Text,
                    NewPassword = textBoxNewPassword.Text
                };

                var request = new RestSharp.RestRequest("api/administrators/{id}/password", RestSharp.Method.PATCH);
                request.AddUrlSegment("id", FormMainApplication.id);

                request.AddJsonBody(secret);

                RestSharp.IRestResponse response = client.Execute(request);

                MessageBox.Show(response.StatusCode + " " + response.ResponseStatus);
                FormMainApplication.password = textBoxNewPassword.Text;
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
