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
        private Label lblOldPassword;
        private Label lblNewPassword;
        private TextBox txtOldBalance;
        private TextBox txtNewBalance;
        private Button button1;
        private Button button2;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        string adminUsername = "";
        public FormChangePassword(string username)
        {
            InitializeComponent();
            adminUsername = username;
            //MessageBox.Show(name.ToString());
        }

        private void checkBoxOldPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtOldBalance.PasswordChar = !checkBox1.Checked ? '*' : '\0';
        }

        private void checkBoxNewPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtNewBalance.PasswordChar = !checkBox2.Checked ? '*' : '\0';
        }
        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                var client = new RestClient(baseURI);
                Administrator administrator = null;

                client.Authenticator = new HttpBasicAuthenticator(adminUsername, txtOldBalance.Text);
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
                    Password = txtOldBalance.Text,
                    NewPassword = txtNewBalance.Text
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

        private void InitializeComponent()
        {
            this.lblOldPassword = new System.Windows.Forms.Label();
            this.lblNewPassword = new System.Windows.Forms.Label();
            this.txtOldBalance = new System.Windows.Forms.TextBox();
            this.txtNewBalance = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblOldPassword
            // 
            this.lblOldPassword.AutoSize = true;
            this.lblOldPassword.Location = new System.Drawing.Point(35, 45);
            this.lblOldPassword.Name = "lblOldPassword";
            this.lblOldPassword.Size = new System.Drawing.Size(83, 15);
            this.lblOldPassword.TabIndex = 0;
            this.lblOldPassword.Text = "Old Password";
            // 
            // lblNewPassword
            // 
            this.lblNewPassword.AutoSize = true;
            this.lblNewPassword.Location = new System.Drawing.Point(29, 100);
            this.lblNewPassword.Name = "lblNewPassword";
            this.lblNewPassword.Size = new System.Drawing.Size(89, 15);
            this.lblNewPassword.TabIndex = 1;
            this.lblNewPassword.Text = "New Password";
            // 
            // txtOldBalance
            // 
            this.txtOldBalance.Location = new System.Drawing.Point(124, 42);
            this.txtOldBalance.Name = "txtOldBalance";
            this.txtOldBalance.Size = new System.Drawing.Size(144, 20);
            this.txtOldBalance.TabIndex = 2;
            // 
            // txtNewBalance
            // 
            this.txtNewBalance.Location = new System.Drawing.Point(124, 97);
            this.txtNewBalance.Name = "txtNewBalance";
            this.txtNewBalance.Size = new System.Drawing.Size(144, 20);
            this.txtNewBalance.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(78, 149);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(65, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(169, 149);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(65, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(290, 46);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(57, 19);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "Show";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(290, 100);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(57, 19);
            this.checkBox2.TabIndex = 7;
            this.checkBox2.Text = "Show";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // FormChangePassword
            // 
            this.ClientSize = new System.Drawing.Size(355, 204);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtNewBalance);
            this.Controls.Add(this.txtOldBalance);
            this.Controls.Add(this.lblNewPassword);
            this.Controls.Add(this.lblOldPassword);
            this.Name = "FormChangePassword";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

    }
}
