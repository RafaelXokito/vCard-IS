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

namespace vCardPlatform
{
    public partial class FormChangePassword : Form
    {
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

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            if (FormMainApplication.password != textBoxOldPassword.Text)
            {
                MessageBox.Show("Old Password does not match with the current password!");
                //textBoxOldPassword.Text = textBoxNewPassword.Text = "";
                return;
            }

            try {
                //FormMainApplication.password = textBoxNewPassword.Text;

                //string cs = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Desktop\Integração de Sistemas\vCard-IS\vCardPlatform\vCardGateway\App_Data\DBGateway.mdf;Integrated Security = True";
                string cs = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\DBGateway.mdf;Integrated Security=True;";

                SqlConnection con = new SqlConnection(cs);
                con.Open();

                SqlCommand cmd = new SqlCommand("Update Administrators SET Password=@password where Email=@username", con);
                cmd.Parameters.AddWithValue("@username", FormLogin.username);
                cmd.Parameters.AddWithValue("@password", textBoxNewPassword.Text);

                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                adapt.Fill(ds);
                con.Close();

                FormMainApplication.password = textBoxNewPassword.Text;
                MessageBox.Show("Password Changed!");
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
