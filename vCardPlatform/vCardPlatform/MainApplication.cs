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
        public static Int64 id = 0;
        //string cs = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Desktop\Integração de Sistemas\vCard-IS\vCardPlatform\vCardGateway\App_Data\DBGateway.mdf;Integrated Security = True";
        string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\Desktop\escola\vCard-IS\vCardPlatform\vCardGateway\App_Data\DBGateway.mdf;Integrated Security=True";
        string baseURI = @"http://localhost:59458/";
        public FormMainApplication()
        {
            InitializeComponent();
            SqlConnection con = new SqlConnection(cs);
            con.Open();

            SqlCommand cmd = new SqlCommand("Select * from Administrators where Email=@username", con);
            cmd.Parameters.AddWithValue("@username", "a1@mail.pt");
            SqlDataAdapter adapt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            labelAdministratorName.Text = (string)dt.Rows[0]["Name"];
            password = (string)dt.Rows[0]["Password"];
            id = (Int64)dt.Rows[0]["Id"];

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
            panelProfile.Controls.Clear();
            fm.TopLevel = false;
            fm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            panelProfile.Controls.Add(fm);
            fm.Show();
        }

        private void FormMainApplication_Load(object sender, EventArgs e)
        {
            try
            {
                statusProgressBar.Value = 0;
                statusProgressBar.Step = 25;
                lblStatus.ForeColor = Color.Gold;
                lblStatus.Text = "Loading tables...";

                #region Load Administrators Table
                var clientAdmin = new RestSharp.RestClient(baseURI);

                var requestAdmin = new RestSharp.RestRequest("api/administrators", RestSharp.Method.GET);

                var resultAdmin = clientAdmin.Execute<List<Administrator>>(requestAdmin).Data;

                dataGridViewAdministrators.DataSource = resultAdmin;

                statusProgressBar.PerformStep();

                lblStatus.Text = "Loaded Administrators Table";
                #endregion

                #region Load Entities Table
                var clientEntity = new RestSharp.RestClient(baseURI);

                var requestEntity = new RestSharp.RestRequest("api/entities", RestSharp.Method.GET);

                var resultEntity = clientEntity.Execute<List<Entity>>(requestEntity).Data;

                dataGridViewEntities.DataSource = resultEntity;

                statusProgressBar.PerformStep();

                lblStatus.Text = "Loaded Entities Table";
                #endregion

                lblStatus.Text = "Tables loaded";

                lblStatus.ForeColor = Color.Green;
                lblStatus.Text = "Everything is up to go!";

                statusProgressBar.Value = 100;
            }
            catch (Exception ex)
            {
                lblStatus.ForeColor = Color.Red;
                lblStatus.Text = ex.Message;
                statusProgressBar.Value = 100;
            }
        }


        private void buttonGetAll_Click(object sender, EventArgs e)
        {

        }

        private void groupBoxCreate_Enter(object sender, EventArgs e)
        {

        }

        private void btnAdministratorsRefresh_Click(object sender, EventArgs e)
        {
            var client = new RestSharp.RestClient(baseURI);

            var request = new RestSharp.RestRequest("api/administrators", RestSharp.Method.GET);

            var result = client.Execute<List<Administrator>>(request).Data;

            dataGridViewAdministrators.DataSource = result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Administrator admin = new Administrator
            {
                Name = txtName.Text,
                Email = txtEmail.Text,
                Password = txtPassword.Text
            };

            var client = new RestSharp.RestClient(baseURI);

            var request = new RestSharp.RestRequest("api/administrators", RestSharp.Method.POST);

            request.AddJsonBody(admin);

            RestSharp.IRestResponse response = client.Execute(request);
            MessageBox.Show(response.StatusCode + " " + response.ResponseStatus);
        }

        private void btnEntitiesRefresh_Click(object sender, EventArgs e)
        {

            var client = new RestSharp.RestClient(baseURI);

            var request = new RestSharp.RestRequest("api/entities", RestSharp.Method.GET);

            var result = client.Execute<List<Entity>>(request).Data;

            dataGridViewEntities.DataSource = result;
        }
    }
}
