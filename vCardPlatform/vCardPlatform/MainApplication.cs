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
    public partial class FormMainApplication : Form
    {
        public static string password = "";
        //string cs = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Desktop\Integração de Sistemas\vCard-IS\vCardPlatform\vCardGateway\App_Data\DBGateway.mdf;Integrated Security = True";
        string cs = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\DBGateway.mdf;Integrated Security=True;";
        public FormMainApplication()
        {
            InitializeComponent();
            SqlConnection con = new SqlConnection(cs);
            con.Open();

            SqlCommand cmd = new SqlCommand("Select * from Administrators where Email=@username", con);
            cmd.Parameters.AddWithValue("@username", FormLogin.username);
            SqlDataAdapter adapt = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            adapt.Fill(ds);
            labelAdministratorName.Text = (string)ds.Rows[0]["Name"];
            password = (string)ds.Rows[0]["Password"];

            con.Close();
        }

        //button Logout Click Event
        private void buttonLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormLogin fl = new FormLogin();
            fl.Show();
        }

        private void FormMainApplication_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void buttonChangePassword_Click(object sender, EventArgs e)
        {
            FormChangePassword fm = new FormChangePassword();
            fm.Show();
        }

        private void FormMainApplication_Load(object sender, EventArgs e)
        {

        }

        private void buttonManageAccounts_Click(object sender, EventArgs e)
        {
            FormManageAccounts fm = new FormManageAccounts();
            fm.Show();
        }
    }
}
