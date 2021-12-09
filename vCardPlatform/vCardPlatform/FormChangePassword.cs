using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using vCardGateway.Models;
using RestSharp;
using RestSharp.Authenticators;
using vCardPlatform.Models;

namespace vCardPlatform
{
    public partial class FormChangePassword : Form
    {
        string baseURI = @"http://localhost:59458/api";
        string adminUsername = "";
        public FormChangePassword(string username)
        {
            InitializeComponent();
            adminUsername = username;
            //MessageBox.Show(name.ToString());
        }

        private void checkBoxOldPassword_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.PasswordChar = !checkBox1.Checked ? '*' : '\0';
        }

        private void checkBoxNewPassword_CheckedChanged(object sender, EventArgs e)
        {
            textBoxNewPassword.PasswordChar = !checkBox2.Checked ? '*' : '\0';
        }
        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                var client = new RestClient(baseURI);
                Administrator administrator = null;

                client.Authenticator = new HttpBasicAuthenticator(adminUsername, textBox1.Text);
                RestRequest request = new RestRequest("me", Method.GET);
                var response = client.Execute<Administrator>(request);

                if (response.IsSuccessful)
                {
                    administrator = response.Data;
                }

                if (administrator == null)
                {
                    MessageBox.Show("Old Password does not match with the current password!");
                    return;
                }

                Secret secret = new Secret()
                {
                    Password = textBox1.Text,
                    NewPassword = textBoxNewPassword.Text
                };

                var request1 = new RestRequest("administrators/" + administrator.Id + "/password", RestSharp.Method.PATCH, DataFormat.Json);

                request1.AddJsonBody(secret);

                IRestResponse response1 = client.Execute(request1);

                MessageBox.Show(response1.StatusCode + " " + response1.ResponseStatus);

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
