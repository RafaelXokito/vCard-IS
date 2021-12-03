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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        public static string username = "";
        //Connection String
        //string cs = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Desktop\Integração de Sistemas\vCard-IS\vCardPlatform\vCardGateway\App_Data\DBGateway.mdf;Integrated Security = True";
        string cs = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\DBGateway.mdf;Integrated Security=True;";
        //button Login Click event
        private void buttonLoginAdmin_Click(object sender, EventArgs e)
        {
            if (textBoxUserNameAdmin.Text == "" || textBoxPasswordAdmin.Text == "")
            {
                MessageBox.Show("Please provide UserName and Password");
                return;
            }

            try
            {
                //Create SqlConnection
                SqlConnection con = new SqlConnection(cs);
                con.Open();

                SqlCommand cmd = new SqlCommand("Select * from Administrators where Email=@username and Password=@password", con);
                cmd.Parameters.AddWithValue("@username", textBoxUserNameAdmin.Text);
                cmd.Parameters.AddWithValue("@password", textBoxPasswordAdmin.Text);

                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapt.Fill(ds);
                DataTable dt = new DataTable();
                adapt.Fill(dt);
                con.Close();

                int count = ds.Tables[0].Rows.Count;
                bool enable = (bool)dt.Rows[0]["Disabled"];
                if (!enable)
                {
                    //If count is equal to 1, than show Main Applocation form
                    if (count == 1)
                    {
                        username = textBoxUserNameAdmin.Text;
                        MessageBox.Show("Login Successful!");
                        this.Hide();
                        FormMainApplication fm = new FormMainApplication();
                        fm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Login Failed!");
                    }
                }
                else
                {
                    MessageBox.Show("Account Disabled!");
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
