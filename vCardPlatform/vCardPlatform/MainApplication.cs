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
using System.Threading;

namespace vCardPlatform
{
    public partial class FormMainApplication : Form
    {
        //Connection String
        string connStr = Properties.Settings.Default.ConnStr;

        RestClient client = new RestClient("http://localhost:59458/api");
        Administrator administrator = null;

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

                #region Setup panelEntityStatusResources for Scroll
                panelEntityStatusResources.AutoScroll = false;
                panelEntityStatusResources.HorizontalScroll.Enabled = false;
                panelEntityStatusResources.HorizontalScroll.Visible = false;
                panelEntityStatusResources.HorizontalScroll.Maximum = 0;
                panelEntityStatusResources.AutoScroll = true;
                #endregion

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

            dataGridViewAdministrators.Columns["password"].Visible = false;
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

        private void dataGridViewEntities_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && e.ColumnIndex >= 0) {
                int c = groupEntityStatus.Controls.Count;
                for (int i = c - 1; i >= 0; i--)
                    if (groupEntityStatus.Controls[i].Name == "")
                        groupEntityStatus.Controls.Remove(groupEntityStatus.Controls[i]);


                c = panelEntityStatusResources.Controls.Count;
                for (int i = c - 1; i >= 0; i--)
                    if (panelEntityStatusResources.Controls[i].Name == "")
                        panelEntityStatusResources.Controls.Remove(panelEntityStatusResources.Controls[i]);
                
                #region Fill Necessary Fields
                txtEntityName.Text = dataGridViewEntities.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtEntityEndpoint.Text = dataGridViewEntities.Rows[e.RowIndex].Cells[2].Value.ToString();
                numEntityMaxLimit.Value = Decimal.Parse(dataGridViewEntities.Rows[e.RowIndex].Cells[3].Value.ToString());
                #endregion

                Thread thread = new Thread(loadEntityDefaultCategories);
                thread.Start();

                txtEntityId.Text = dataGridViewEntities.Rows[e.RowIndex].Cells[0].Value.ToString();
                groupDataEntity.Enabled = true;
                groupEntityDefaultCategory.Enabled = true;
                btnEntitySave.Enabled = true;

                lblEntityStatusName.Text = "";
                btnEntitySave.Text = "Update";
                txtEntityEndpoint.BackColor = SystemColors.Window;

                tabCEntities.SelectedTab = tabCEntities.TabPages["tabEntity"];
            }

        }

        public void loadEntityDefaultCategories()
        {
            
            RestClient client = new RestClient(txtEntityEndpoint.Text + "/api");

            RestRequest request = new RestRequest("defaultcategories", Method.GET);

            var responseData = client.Execute<List<DefaultCategory>>(request).Data;
            //dataGridViewEntityDefaultCategory.DataSource = responseData;

            dataGridViewEntityDefaultCategorySetup(responseData);

        }

        private void dataGridViewEntityDefaultCategorySetup(List<DefaultCategory> responseData)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<List<DefaultCategory>>(dataGridViewEntityDefaultCategorySetup), new object[] { responseData });
                return;
            }
            //You can assign the Column types while initializing
            DataGridViewColumn d2 = new DataGridViewTextBoxColumn();
            DataGridViewColumn d3 = new DataGridViewTextBoxColumn();

            //Add Header Texts to be displayed on the Columns
            d2.HeaderText = "Name";
            d2.Name = "name";
            d3.HeaderText = "Type";
            d3.Name = "type";

            dataGridViewEntityDefaultCategory.Columns.Clear();
            dataGridViewEntityDefaultCategory.Columns.AddRange(d2, d3);

            dataGridViewEntityDefaultCategory.Rows.Clear();

            if (responseData != null)
                foreach (DefaultCategory defaultCategory in responseData)
                {
                    int rowId = dataGridViewEntityDefaultCategory.Rows.Add();
                    DataGridViewRow row = dataGridViewEntityDefaultCategory.Rows[rowId];
                    row.Cells["name"].Value = defaultCategory.Name;
                    row.Cells["type"].Value = (defaultCategory.Type == "D" ? "Debit" : "Credit");
                }
        }

        private void btnEntityDCRemoveRow_Click(object sender, EventArgs e)
        {
            Int32 selectedRowCount = dataGridViewEntityDefaultCategory.Rows.GetRowCount(DataGridViewElementStates.Selected);

            DataGridViewSelectedRowCollection dataGridViewRowCollection = dataGridViewEntityDefaultCategory.SelectedRows;

            if (selectedRowCount > 0)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < selectedRowCount; i++)
                {
                    dataGridViewEntityDefaultCategory.Rows.Remove(dataGridViewRowCollection[i]);
                }

            }
        }

        private void btnTestEndpoint_Click(object sender, EventArgs e)
        {
            int c = groupEntityStatus.Controls.Count;
            for (int i = c - 1; i >= 0; i--)
                if (groupEntityStatus.Controls[i].Name == "")
                    groupEntityStatus.Controls.Remove(groupEntityStatus.Controls[i]);

            c = panelEntityStatusResources.Controls.Count;
            for (int i = c - 1; i >= 0; i--)
                if (panelEntityStatusResources.Controls[i].Name == "")
                    panelEntityStatusResources.Controls.Remove(panelEntityStatusResources.Controls[i]);

            if (txtEntityEndpoint.Text != "")
            {
                Thread thread1 = new Thread(testEntityStatus);
                thread1.Start(txtEntityEndpoint.Text);
            }
        }

        private void btnEntitySave_Click(object sender, EventArgs e)
        {
            try
            {
                RestClient clientTest = new RestClient(txtEntityEndpoint.Text);

                RestRequest request = new RestRequest("", Method.GET);

                IRestResponse responseData = clientTest.Execute(request);
                if (responseData.StatusCode != 0)
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
                    if (btnEntitySave.Text == "Update")
                    {
                        request = new RestSharp.RestRequest("entities/" + txtEntityId.Text, RestSharp.Method.PUT, DataFormat.Json);
                    }
                    else
                    {
                        request = new RestSharp.RestRequest("entities", RestSharp.Method.POST, DataFormat.Json);
                    }
                    request.AddJsonBody(entity);

                    RestSharp.IRestResponse<Entity> response = client.Execute<Entity>(request);
                    #endregion
                    #region Handle Request Response
                    if (response.IsSuccessful)
                    {
                        
                        statusProgressBar.Value = 50;

                        #region Get Default Categories From Endpoint & Prepare To Compare
                        clientTest = new RestClient(response.Data.Endpoint + "/api");
                    
                        request = new RestRequest("defaultcategories", Method.GET);

                        List<DefaultCategory> defaultCategories = clientTest.Execute<List<DefaultCategory>>(request).Data;

                        DataTable dataTable1 = ConvertToDataTable<DefaultCategory>(defaultCategories);
                        dataTable1.Columns.RemoveAt(0);
                        
                        #endregion

                        DataTable dataTable2 = GetDataGridViewAsDataTable(dataGridViewEntityDefaultCategory);

                        foreach (DataRow row in dataTable2.Rows)
                        {
                            if (row["Type"].ToString() == "Credit")
                                row["Type"] = "C";
                            else
                                row["Type"] = "D";
                        }

                        DataTable dt = getDifferentRecords(dataTable2, dataTable1);

                        #region Handle Different Rows
                        foreach (DataRow row in dt.Rows)
                        {
                            DefaultCategory defaultCategory = new DefaultCategory
                            {
                                Name = row["Name"].ToString(),
                                Type = row["Type"].ToString(),
                            };

                            #region Create&Populate&Send Request
                            if (row["Method"].ToString() == "POST") { 
                                request = new RestSharp.RestRequest("entities/"+response.Data.Id+"/defaultcategories", RestSharp.Method.POST, DataFormat.Json);
                                request.AddJsonBody(defaultCategory);
                            }
                            else if (row["Method"].ToString() == "DELETE") { 
                                request = new RestSharp.RestRequest("entities/" + txtEntityId.Text + "/defaultcategories", RestSharp.Method.DELETE);
                                request.AddJsonBody(defaultCategory);

                            }
                            RestSharp.IRestResponse responseDELETE = client.Execute(request);
                            #endregion
                        }
                        #endregion
                        statusProgressBar.Value = 100;
                        groupDataEntity.Enabled = false;
                        btnEntitySave.Enabled = false;
                        groupEntityDefaultCategory.Enabled = false;

                        lblStatus.ForeColor = Color.Green;
                        if (btnEntitySave.Text == "Create")
                            lblStatus.Text = "Entity " + txtEntityName.Text + " created!";
                        else
                            lblStatus.Text = "Entity " + txtEntityName.Text + " updated!";
                        loadEntities();
                        tabCEntities.SelectedTab = tabCEntities.TabPages["tabEntityTable"];
                        txtEntityId.Text = "";
                        txtEntityName.Text = "";
                        txtEntityEndpoint.Text = "";
                        numEntityMaxLimit.Value = 0;
                        dataGridViewEntityDefaultCategory.Rows.Clear();
                        dataGridViewEntityDefaultCategory.Refresh();
                    }
                    else
                    {
                        lblStatus.ForeColor = Color.Red;
                        lblStatus.Text = response.StatusDescription;
                        MessageBox.Show(response.StatusDescription);
                    }
                    #endregion
                }
                else
                {
                    MessageBox.Show("You endpoint is invalid! Use de 'Test End-Point' Button");
                    btnTestEndpoint.Focus();
                }
            }
            catch (Exception ex)
            {
                testEntityStatus(txtEntityEndpoint.Text);
                MessageBox.Show(ex.Message);
            }
        }

        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }

                table.Rows.Add(row);
            }

            return table;
        }

        private DataTable GetDataGridViewAsDataTable(DataGridView _DataGridView)
        {
            try
            {
                if (_DataGridView.ColumnCount == 0) return null;
                DataTable dtSource = new DataTable();
                //////create columns
                foreach (DataGridViewColumn col in _DataGridView.Columns)
                {
                    if (col.ValueType == null) dtSource.Columns.Add(col.Name, typeof(string));
                    else dtSource.Columns.Add(col.Name, col.ValueType);
                    dtSource.Columns[col.Name].Caption = col.HeaderText;
                }
                ///////insert row data
                foreach (DataGridViewRow row in _DataGridView.Rows)
                {
                    if (row.Cells[0].Value == null || row.Cells[0].Value.ToString() == "")
                    {
                        continue;
                    }
                    DataRow drNewRow = dtSource.NewRow();
                    foreach (DataColumn col in dtSource.Columns)
                    {
                        drNewRow[col.ColumnName] = row.Cells[col.ColumnName].Value;
                    }
                    dtSource.Rows.Add(drNewRow);
                }
                return dtSource;
            }
            catch
            {
                return null;
            }
        }

        public DataTable getDifferentRecords(DataTable FirstDataTable, DataTable SecondDataTable)
        {
            FirstDataTable.Columns.Add("Method", typeof(String));
            SecondDataTable.Columns.Add("Method", typeof(String));
            //Create Empty Table 
            DataTable ResultDataTable = new DataTable("ResultDataTable");

            //use a Dataset to make use of a DataRelation object 
            using (DataSet ds = new DataSet())
            {

                //Add tables 
                ds.Tables.AddRange(new DataTable[] { FirstDataTable.Copy(), SecondDataTable.Copy() });

                //Get Columns for DataRelation 
                DataColumn[] firstColumns = new DataColumn[ds.Tables[0].Columns.Count];
                for (int i = 0; i < firstColumns.Length; i++)
                {
                    firstColumns[i] = ds.Tables[0].Columns[i];
                }

                DataColumn[] secondColumns = new DataColumn[ds.Tables[1].Columns.Count];
                for (int i = 0; i < secondColumns.Length; i++)
                {
                    secondColumns[i] = ds.Tables[1].Columns[i];
                }

                //Create DataRelation 
                DataRelation r1 = new DataRelation(string.Empty, firstColumns, secondColumns, false);
                ds.Relations.Add(r1);

                DataRelation r2 = new DataRelation(string.Empty, secondColumns, firstColumns, false);
                ds.Relations.Add(r2);

                //Create columns for return table 
                for (int i = 0; i < FirstDataTable.Columns.Count; i++)
                {
                    ResultDataTable.Columns.Add(FirstDataTable.Columns[i].ColumnName, FirstDataTable.Columns[i].DataType);
                }

                //If FirstDataTable Row not in SecondDataTable, Add to ResultDataTable. 
                ResultDataTable.BeginLoadData();
                foreach (DataRow parentrow in ds.Tables[0].Rows)
                {
                    DataRow[] childrows = parentrow.GetChildRows(r1);
                    if (childrows == null || childrows.Length == 0) {
                        parentrow["Method"] = "POST";
                        ResultDataTable.LoadDataRow(parentrow.ItemArray, true);
                    }
                }

                //If SecondDataTable Row not in FirstDataTable, Add to ResultDataTable. 
                foreach (DataRow parentrow in ds.Tables[1].Rows)
                {
                    DataRow[] childrows = parentrow.GetChildRows(r2);
                    if (childrows == null || childrows.Length == 0) {
                        parentrow["Method"] = "DELETE";
                        ResultDataTable.LoadDataRow(parentrow.ItemArray, true);
                    }
                }
                ResultDataTable.EndLoadData();
            }
            return ResultDataTable;
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

        private void btnEntitiesCreate_Click(object sender, EventArgs e)
        {
            groupDataEntity.Enabled = true;
            groupEntityDefaultCategory.Enabled = true;
            btnEntitySave.Enabled = true;
            txtEntityEndpoint.BackColor = SystemColors.Window;

            btnEntitySave.Text = "Create";

            lblEntityStatusName.Text = "";

            int c = groupEntityStatus.Controls.Count;
            for (int i = c - 1; i >= 0; i--)
                if (groupEntityStatus.Controls[i].Name == "")
                    groupEntityStatus.Controls.Remove(groupEntityStatus.Controls[i]);

            dataGridViewEntityDefaultCategory.Rows.Clear();
            dataGridViewEntityDefaultCategory.Refresh();

            txtEntityId.Text = "";
            txtEntityName.Text = "";
            txtEntityEndpoint.Text = "";
            numEntityMaxLimit.Value = 0;

            tabCEntities.SelectedTab = tabCEntities.TabPages["tabEntity"];
        }

        public void AppendTextBox(Label label, string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<Label,string>(AppendTextBox), new object[] { label, value });
                return;
            }
            label.Text = value;
        }

        public void AppendStatusBar(int value, string message)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<int, string>(AppendStatusBar), new object[] { value, message });
                return;
            }
            statusProgressBar.Value = value;
            lblStatus.Text = message;
            if (value != 100)
            {
                Application.UseWaitCursor = true;
            }
            else
            {
                Application.UseWaitCursor = false;
            }
        }

        private void testEntityStatus(object endpoint)
        {
            try
            {
                #region Check Entity Reachability
                AppendTextBox(lblEntityStatusName, endpoint.ToString());
                RestClient clientTest = new RestClient(endpoint.ToString());

                RestRequest request = new RestRequest("", Method.GET);
                AppendStatusBar(0,"Trying to reach endpoint!");
                IRestResponse responseData = clientTest.Execute(request);
                AppendStatusBar(100, "Done!");

                if (responseData.StatusCode != 0)
                {
                
                    Label lblEntityStatusResponse = new Label();
                    lblEntityStatusResponse.Location = new Point(106, 49);
                    lblEntityStatusResponse.Text = "Success";
                    AppendElemToGroup(groupEntityStatus, lblEntityStatusResponse);
                    lblEntityStatusResponse.BackColor = Color.GreenYellow;

                    request = new RestRequest("endpointssufixs", Method.GET);
                    AppendStatusBar(0, "Getting end-point sufixs!");
                    List<EndpointSufix> endpointSufixes = client.Execute<List<EndpointSufix>>(request).Data;
                    AppendStatusBar(100, "Done!");

                    int i = 0;
                    foreach (EndpointSufix item in endpointSufixes)
                    {
                        AppendStatusBar(100/endpointSufixes.Count, "Trying to reach endpoint sufixs!");

                        Label namelabel = new Label();
                        namelabel.Location = new Point(21, 17 + (i * 31));
                        namelabel.Text = item.Content;
                        AppendElemToPanel(panelEntityStatusResources, namelabel);

                        request = new RestRequest(item.Content, Method.GET);

                        responseData = clientTest.Execute(request);

                        Label responselabel = new Label();
                        responselabel.Location = new Point(185, 17 + (i * 31));
                        responselabel.Text = item.Content;
                        AppendElemToPanel(panelEntityStatusResources, responselabel);

                        if (responseData.StatusCode != 0)
                        {
                            AppendTextBox(responselabel, "Success");
                            responselabel.BackColor = Color.GreenYellow;
                        }
                        else
                        {
                            AppendTextBox(responselabel, "Unreachable");
                            responselabel.BackColor = Color.MediumVioletRed;
                        }
                        i++;
                    }
                }
                else
                {
                    Label lblEntityStatusResponse = new Label();
                    lblEntityStatusResponse.Location = new Point(99, 119);
                    lblEntityStatusResponse.Text = "Unreachable";
                    AppendElemToPanel(panelEntityStatusResources, lblEntityStatusResponse);
                    lblEntityStatusResponse.BackColor = Color.MediumVioletRed;
                }
                AppendStatusBar(100, "Done!");
                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, endpoint.ToString());
            }
        }

        private void AppendElemToGroup(GroupBox group, Label label)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<GroupBox, Label>(AppendElemToGroup), new object[] { group, label });
                return;
            }
            group.Controls.Add(label);
        }

        private void AppendElemToPanel(Panel panel, Label label)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<Panel, Label>(AppendElemToPanel), new object[] { panel, label });
                return;
            }
            panel.Controls.Add(label);
        }

        private void tabCEntities_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnEntityDefaultCategoriesRefresh_Click(object sender, EventArgs e)
        {
            if (txtEntityEndpoint.Text != "")
            {
                Thread thread1 = new Thread(loadEntityDefaultCategories);
                thread1.Start();
            }
        }

        private void btnEndPointsSufixsRefresh_Click(object sender, EventArgs e)
         {
            var request = new RestRequest("endpointssufixs", Method.GET);

            var responseData = client.Execute<List<EndpointSufix>>(request).Data;

            //You can assign the Column types while initializing
            DataGridViewColumn d2 = new DataGridViewTextBoxColumn();

            //Add Header Texts to be displayed on the Columns
            d2.HeaderText = "Content";
            d2.Name = "content";

            dataGridViewEndPointsSufixs.Columns.Clear();
            dataGridViewEndPointsSufixs.Columns.AddRange(d2);

            dataGridViewEndPointsSufixs.Rows.Clear();

            if (responseData != null)
                foreach (EndpointSufix endpointSufix in responseData)
                {
                    int rowId = dataGridViewEndPointsSufixs.Rows.Add();
                    DataGridViewRow row = dataGridViewEndPointsSufixs.Rows[rowId];
                    row.Cells["content"].Value = endpointSufix.Content;
                }
        }



        private void btnEndPointsSufixsSave_Click(object sender, EventArgs e)
        {

        }
    }
}
