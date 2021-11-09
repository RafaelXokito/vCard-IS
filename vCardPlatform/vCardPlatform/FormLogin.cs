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
    public partial class FormLoginAdmin : Form
    {
        public FormLoginAdmin()
        {
            InitializeComponent();
        }

        //Connection String
        string cs = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\DatabaseVCardPlatform.mdf;Integrated Security=True;";
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
                SqlCommand cmd = new SqlCommand("Select * from tbl_Login where UserName=@username and Password=@password", con);
                cmd.Parameters.AddWithValue("@username", textBoxUserNameAdmin.Text);
                cmd.Parameters.AddWithValue("@password", textBoxPasswordAdmin.Text);
                con.Open();

                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapt.Fill(ds);
                con.Close();

                int count = ds.Tables[0].Rows.Count;
                //If count is equal to 1, than show Main Applocation form
                if (count == 1)
                {
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
