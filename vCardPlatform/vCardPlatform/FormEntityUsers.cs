using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Windows.Forms;
using vCardPlatform.Models;

namespace vCardPlatform
{
    public partial class FormEntityUsers : Form
    {
        Entity entity;
        RestClient client;
        string imageB64 = "";
        public FormEntityUsers(Entity entity, RestClient client)
        {
            this.entity = entity;
            this.client = client;
            InitializeComponent();
        }

        private void FormEntityUsers_Load(object sender, EventArgs e)
        {
            try
            {
                loadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, $"Error Loading {entity.Name} Entity Users", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void loadUsers()
        {
            try
            {
                RestRequest request = new RestRequest($"entities/{entity.Id}/users", Method.GET);
                request.AddHeader("Authorization", entity.Authentication.Token);
                IRestResponse responseData = client.Execute(request);
                if (responseData.IsSuccessful)
                {
                    if (dataGridViewEntityUser.Columns["cImage"] == null)
                    {
                        DataGridViewImageColumn ic = new DataGridViewImageColumn();
                        ic.HeaderText = "Image";
                        ic.Image = null;
                        ic.Name = "cImage";
                        ic.Width = 35;
                        dataGridViewEntityUser.Columns.Add(ic);
                    }

                    string content = responseData.Content.Replace("\\", "");
                    dynamic dataEntityUsers = Json.Decode(content);
                    List<User> auxList = new List<User>();
                    if (dataEntityUsers.data != null)
                        dataEntityUsers = dataEntityUsers.data;
                    foreach (var item in dataEntityUsers)
                    {
                        if ((item.user_type ?? "USER") != "A") {
                            item.max_debit = item.max_debit ?? item.maximumlimit.ToString();

                            auxList.Add(new User
                            {
                                Username = item.id ?? item.username ?? item.PhoneNumber,
                                Name = item.name,
                                Email = item.email,
                                Photo = item.photo_url ?? item.photo,

                                MaximumLimit = decimal.Parse(item.max_debit.ToString().Replace(".", ",") ?? item.maximumlimit.ToString()),
                                Balance = decimal.Parse(item.balance.ToString().Replace(".", ","))
                            });
                        }
                    }

                    dataGridViewEntityUser.DataSource = auxList;
                    dataGridViewEntityUser.Columns["Password"].Visible = false;
                    dataGridViewEntityUser.Columns["Photo"].Visible = false;
                    dataGridViewEntityUser.Columns["ConfirmationCode"].Visible = false;

                    Thread thread1 = new Thread(loadUsersPhotos);
                    thread1.Start();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void loadUsersPhotos()
        {
            try
            {

                foreach (DataGridViewRow row in dataGridViewEntityUser.Rows)
                {
                    if (row.Cells["Photo"].Value.ToString() != "" && row.Cells["Photo"].Value.ToString().StartsWith("/"))
                    {
                        DataGridViewImageCell cell = row.Cells["cImage"] as DataGridViewImageCell;
                        string newUrl1 = null;
                        newUrl1 = entity.Endpoint + row.Cells["Photo"].Value;
                        var request1 = WebRequest.Create(newUrl1);

                        using (var response = request1.GetResponse())
                        using (var stream = response.GetResponseStream())
                        {
                            SetImageCell(cell, Image.FromStream(stream));
                        }
                    }
                }

            }
            catch (Exception)
            {

            }
        }

        public void SetImageCell(DataGridViewImageCell cell, Image img)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<DataGridViewImageCell, Image>(SetImageCell), new object[] { cell, img });
                return;
            }
            cell.Value = img;
            cell.ImageLayout = DataGridViewImageCellLayout.Stretch;
        }

        private void btnChosePhoto_Click(object sender, EventArgs e)
        {
            // open file dialog   
            OpenFileDialog open = new OpenFileDialog();
            // image filters  
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                byte[] imageBytes;
                imageBytes = File.ReadAllBytes(open.FileName);

                imageB64 = Convert.ToBase64String(imageBytes);
                // display image in picture box  
                picImage.Image = new Bitmap(open.FileName);
                // image file path  
                txtPhoto.Text = open.FileName;
                
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                User user = new User()
                {
                    Username = txtUsername.Text,
                    Password = txtPassword.Text,
                    Name = txtName.Text,
                    Email = txtEmail.Text,
                    ConfirmationCode = txtConfirmationCode.Text,
                };

                RestRequest request = new RestRequest($"entities/{entity.Id}/users", Method.POST, DataFormat.Json);

                request.AddJsonBody(user);
                request.AddHeader("Authorization", entity.Authentication.Token);
                IRestResponse<User> responseData = client.Execute<User>(request);
                if (responseData.IsSuccessful)
                {

                    IRestRequest requestPhoto = new RestRequest($"entities/{entity.Id}/users/{user.Username}/photo", Method.POST, DataFormat.Json);
                    user.Photo = imageB64;
                    requestPhoto.AddJsonBody(user);
                    requestPhoto.AddHeader("Authorization", entity.Authentication.Token);
                    IRestResponse response = client.Execute(requestPhoto);
                    if (response.IsSuccessful)
                    {
                        txtUsername.Text = "";
                        txtPassword.Text = "";
                        txtName.Text = "";
                        txtEmail.Text = "";
                        txtConfirmationCode.Text = "";
                        txtPhoto.Text = "";

                        tabControlEntityUser.SelectedTab = tabControlEntityUser.TabPages["tabEntityUserTable"];
                        loadUsers();
                    }
                    else
                    {
                        MessageBox.Show(response.StatusCode.ToString(), $"Error Storing Photo {user.Name}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(responseData.Content.ToString(), $"Error Creating {entity.Name} Entity User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, $"Error Creating {entity.Name} Entity User", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        string MyDictionaryToJson(Dictionary<string, object> dict)
        {
            var entries = dict.Select(d =>
                string.Format("\"{0}\": [{1}]", d.Key, string.Join(",", d.Value)));
            return "{" + string.Join(",", entries) + "}";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            loadUsers();
        }
    }

}
