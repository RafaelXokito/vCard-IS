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
using RestSharp;
using RestSharp.Authenticators;
using vCardPlatform.Models;

namespace vCardPlatform
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        //button Login Click event
        private void buttonLoginAdmin_Click(object sender, EventArgs e)
        {
            string email = textBoxUserNameAdmin.Text.Trim();
            string password = textBoxPasswordAdmin.Text;
            if (email == "" || password == "")
            {
                MessageBox.Show("The username/e-mail and password are required");
                return;
            }

            try
            {
                Credentials credentials = new Credentials()
                {
                    Email = email,
                    Password = password
                };

                RestClient client = new RestClient("http://localhost:59458/api");

                RestRequest request = new RestRequest("login", Method.POST, DataFormat.Json);

                request.AddJsonBody(credentials);

                var response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    FormMainApplication fm = new FormMainApplication(email, password);
                    fm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Error: " + response.StatusCode + " - " + response.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            pictureLogo.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}
