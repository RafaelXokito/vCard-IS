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
    public partial class FormMainApplication : Form
    {
        //Connection String
        string connStr = Properties.Settings.Default.ConnStr;

        RestClient client = new RestClient("http://localhost:59458/api");
        Administrator administrator = null;

        //Aux Variables
        string entitySelected = "";

        public FormMainApplication(string username, string password)
        {
            InitializeComponent();
            client.Authenticator = new HttpBasicAuthenticator(username, password);

            RestRequest request = new RestRequest("me", Method.GET);
            var response = client.Execute<Administrator>(request);

            if (response.IsSuccessful)
            {
                administrator = response.Data;
                return;
            }

            MessageBox.Show("Error: " + response.ErrorMessage);
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
                loadAdministrators();

                statusProgressBar.PerformStep();

                lblStatus.Text = "Loaded Administrators Table";
                #endregion

                #region Load Entities Table
                loadEntities();

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
            loadAdministrators();
        }
        
        private void loadAdministrators()
        {
            var requestAdmin = new RestRequest("administrators", Method.GET);

            var resultAdmin = client.Execute<List<Administrator>>(requestAdmin).Data;

            dataGridViewAdministrators.DataSource = resultAdmin;
        }


        private void btnEntitiesRefresh_Click(object sender, EventArgs e)
        {
            loadEntities();
        }

        private void loadEntities()
        {
            var request = new RestSharp.RestRequest("entities", RestSharp.Method.GET);

            var result = client.Execute<List<Entity>>(request).Data;

            dataGridViewEntities.DataSource = result;
        }

        private void btnCreateAdmin_Click(object sender, EventArgs e)
        {
            statusProgressBar.Value = 0;

            #region Create Admin Model
            Administrator admin = new Administrator
            {
                Name = txtAdministratorName.Text,
                Email = txtAdministratorEmail.Text,
                Password = txtAdministratorPassword.Text
            };
            #endregion

            #region Create&Populate&Send Request
            var request = new RestSharp.RestRequest("administrators", RestSharp.Method.POST, DataFormat.Json);

            request.AddJsonBody(admin);

            RestSharp.IRestResponse response = client.Execute(request);
            #endregion

            #region Handle Request Response
            if (response.IsSuccessful)
            {
                lblStatus.ForeColor = Color.Green;
                lblStatus.Text = "Admin "+ txtAdministratorName.Text +" created!";
                loadAdministrators();
                tabCAdministrators.SelectedTab = tabCAdministrators.TabPages["tabTable"];
                txtAdministratorName.Text = "";
                txtAdministratorEmail.Text = "";
                txtAdministratorPassword.Text = "";
            }
            else
            {
                lblStatus.ForeColor = Color.Red;
                lblStatus.Text = response.ErrorMessage;
            }
            #endregion
            statusProgressBar.Value = 100;
        }

        private void btnAdministratorsDelete_Click(object sender, EventArgs e)
        {
            Int32 selectedRowCount = dataGridViewAdministrators.Rows.GetRowCount(DataGridViewElementStates.Selected);

            int counteDeleted = 0;
            if (selectedRowCount > 0)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < selectedRowCount; i++)
                {
                    #region Ask User For Confirmation
                    string PK = dataGridViewAdministrators.SelectedRows[i].Cells[0].Value.ToString();
                    string email = dataGridViewAdministrators.SelectedRows[i].Cells[1].Value.ToString();
                    var confirmResult = MessageBox.Show("Are you sure to delete this administrator "+ email + "?",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
                    #region Handle Confirmation Response
                    if (confirmResult == DialogResult.Yes)
                    {
                        #region Create&Send&Handle Request
                        var request = new RestSharp.RestRequest("administrators/" + PK, RestSharp.Method.DELETE);

                        IRestResponse response = client.Execute(request);
                        
                           
                        if (response.IsSuccessful)
                        {
                            lblStatus.Text = "Deleted " + email + " administrator!";
                            lblStatus.ForeColor = Color.Green;
                        }
                        else
                        {
                            lblStatus.Text = "Not deleted " + email + " administrator!";
                            lblStatus.ForeColor = Color.Red;
                        }
                        #endregion

                        counteDeleted++;
                    }
                    #endregion

                    #endregion
                }

                lblStatus.Text = "Deleted "+ counteDeleted + " administrators!";
                lblStatus.ForeColor = Color.Green;

                loadAdministrators();
                   
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormLogin fl = new FormLogin();
            fl.Show();
        }


        private void dataGridViewAdministrators_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewAdministrators.Columns[e.ColumnIndex].HeaderText == "Disabled" && e.RowIndex != -1)
            {
                #region Populate Model & Retrieve Necessary Data
                string PK = dataGridViewAdministrators.Rows[e.RowIndex].Cells[0].Value.ToString();
                Administrator admin = new Administrator
                {
                    Disabled = (bool)dataGridViewAdministrators.Rows[e.RowIndex].Cells[e.ColumnIndex].Value,
                };
                #endregion

                #region Create&Populate&Send Request
                var request = new RestSharp.RestRequest("administrators/" + PK + "/disabled", RestSharp.Method.PATCH, DataFormat.Json);

                request.AddJsonBody(admin);

                IRestResponse response = client.Execute(request);
                #endregion

                #region Handle Request Response
                string msgkey = " disabled ";
                if (!admin.Disabled)
                {
                    msgkey = " abled ";
                }

                if (response.IsSuccessful)
                {
                    string email = dataGridViewAdministrators.Rows[e.RowIndex].Cells[1].Value.ToString();
                    lblStatus.Text = email + msgkey + "administrator!";
                    lblStatus.ForeColor = Color.Green;
                }
                else
                {
                    string email = dataGridViewAdministrators.Rows[e.RowIndex].Cells[1].Value.ToString();
                    lblStatus.Text = "Couldnt" + msgkey + email + " administrator!";
                    lblStatus.ForeColor = Color.Red;
                }
                #endregion
            }
        }

        private void dataGridViewEntities_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void dataGridViewEntities_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0) {
                #region Fill Necessary Fields
                entitySelected = dataGridViewEntities.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtEntityName.Text = dataGridViewEntities.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtEntityEndpoint.Text = dataGridViewEntities.Rows[e.RowIndex].Cells[2].Value.ToString();
                numEntityMaxLimit.Value = Decimal.Parse(dataGridViewEntities.Rows[e.RowIndex].Cells[3].Value.ToString());
                #endregion


                RestClient client = new RestClient(txtEntityEndpoint.Text+"/api");

                RestRequest request = new RestRequest("defaultcategories", Method.GET);

                var responseData = client.Execute<List<DefaultCategory>>(request).Data;
                //dataGridViewEntityDefaultCategory.DataSource = responseData;

                //You can assign the Column types while initializing
                DataGridViewColumn d1 = new DataGridViewTextBoxColumn();
                DataGridViewColumn d2 = new DataGridViewTextBoxColumn();
                DataGridViewColumn d3 = new DataGridViewTextBoxColumn();

                //Add Header Texts to be displayed on the Columns
                d1.HeaderText = "Id";
                d1.Name = "id";
                d2.HeaderText = "Name";
                d2.Name = "name";
                d3.HeaderText = "Type";
                d3.Name = "type";

                //Add the Columns to the DataGridView
                dataGridViewEntityDefaultCategory.Columns.AddRange(d1, d2, d3);

                foreach (DefaultCategory defaultCategory in responseData)
                {
                    int rowId = dataGridViewEntityDefaultCategory.Rows.Add();
                    DataGridViewRow row = dataGridViewEntityDefaultCategory.Rows[rowId];
                    row.Cells["id"].Value = defaultCategory.Id;
                    row.Cells["name"].Value = defaultCategory.Name;
                    row.Cells["type"].Value = (defaultCategory.Type == "D" ? "Debit" : "Credit");
                }

                tabCEntities.SelectedTab = tabCEntities.TabPages["tabEntity"];
            }
        }


        private void btnEntityDCRemoveRow_Click(object sender, EventArgs e)
        {
            Int32 selectedRowCount = dataGridViewEntityDefaultCategory.Rows.GetRowCount(DataGridViewElementStates.Selected);

            if (selectedRowCount > 0)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < selectedRowCount; i++)
                {
                    dataGridViewEntityDefaultCategory.Rows.RemoveAt(i);
                }

            }
        }

        private void btnTestEndpoint_Click(object sender, EventArgs e)
        {
            RestClient client = new RestClient(txtEntityEndpoint.Text);

            RestRequest request = new RestRequest("", Method.GET);

            IRestResponse responseData = client.Execute(request);

            if (responseData.StatusCode != 0)
            {
                txtEntityEndpoint.BackColor = Color.GreenYellow;
            }
            else
            {
                txtEntityEndpoint.BackColor = Color.MediumVioletRed;
            }
        }

        private void btnEntitySave_Click(object sender, EventArgs e)
        {
            statusProgressBar.Value = 0;

            #region Create Entity Model
            Entity entity = new Entity
            {
                Name = txtEntityName.Text,
                Endpoint = txtEntityEndpoint.Text,
                MaxLimit = numEntityMaxLimit.Value
            };
            #endregion

            #region Create&Populate&Send Request
            var request = new RestSharp.RestRequest("entities/"+ entitySelected, RestSharp.Method.PUT, DataFormat.Json);

            request.AddJsonBody(entity);

            RestSharp.IRestResponse response = client.Execute(request);
            #endregion

            #region Handle Request Response
            if (response.IsSuccessful)
            {
                lblStatus.ForeColor = Color.Green;
                lblStatus.Text = "Entity " + txtEntityName.Text + " updated!";
                loadEntities();
                tabCEntities.SelectedTab = tabCEntities.TabPages["tabEntityTable"];
                txtEntityName.Text = "";
                txtEntityEndpoint.Text = "";
                numEntityMaxLimit.Value = 0;
                dataGridViewEntityDefaultCategory.Rows.Clear();
                dataGridViewEntityDefaultCategory.Refresh();
            }
            else
            {
                lblStatus.ForeColor = Color.Red;
                lblStatus.Text = response.ErrorMessage;
            }
            #endregion
            statusProgressBar.Value = 100;
        }

        private void btnEntitiesDelete_Click(object sender, EventArgs e)
        {
            Int32 selectedRowCount = dataGridViewEntities.Rows.GetRowCount(DataGridViewElementStates.Selected);

            int counteDeleted = 0;
            if (selectedRowCount > 0)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < selectedRowCount; i++)
                {
                    #region Ask User For Confirmation
                    string PK = dataGridViewEntities.SelectedRows[i].Cells[0].Value.ToString();
                    string name = dataGridViewEntities.SelectedRows[i].Cells[1].Value.ToString();
                    var confirmResult = MessageBox.Show("Are you sure to delete this entity " + name + "?",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
                    #region Handle Confirmation Response
                    if (confirmResult == DialogResult.Yes)
                    {
                        #region Create&Send&Handle Request
                        var request = new RestSharp.RestRequest("entities/" + PK, RestSharp.Method.DELETE);

                        IRestResponse response = client.Execute(request);


                        if (response.IsSuccessful)
                        {
                            lblStatus.Text = "Deleted " + name + " entity!";
                            lblStatus.ForeColor = Color.Green;
                        }
                        else
                        {
                            lblStatus.Text = "Not deleted " + name + " entity!";
                            lblStatus.ForeColor = Color.Red;
                        }
                        #endregion

                        counteDeleted++;
                    }
                    #endregion

                    #endregion
                }

                lblStatus.Text = "Deleted " + counteDeleted + " entities!";
                lblStatus.ForeColor = Color.Green;

                loadEntities();
            }
        }
    }
}
