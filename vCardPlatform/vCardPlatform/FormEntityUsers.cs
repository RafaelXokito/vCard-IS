using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
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
        public FormEntityUsers(Entity entity, RestClient client)
        {
            this.entity = entity;
            this.client = client;
            InitializeComponent();
        }

        private void FormEntityUsers_Load(object sender, EventArgs e)
        {
            RestRequest request = new RestRequest($"entities/{entity.Id}/users", Method.GET);

            IRestResponse responseData = client.Execute(request);
            if (responseData.IsSuccessful)
            {
                DataGridViewImageColumn ic = new DataGridViewImageColumn();
                ic.HeaderText = "Image";
                ic.Image = null;
                ic.Name = "cImage";
                ic.Width = 35;
                dataGridViewEntityUser.Columns.Add(ic);

                string content = responseData.Content.Replace("\\", "");
                dynamic dataEntityUsers = Json.Decode(content.Substring(1, content.Length - 2));
                List<User> auxList = new List<User>();
                if (dataEntityUsers.data != null)
                    dataEntityUsers = dataEntityUsers.data;
                foreach (var item in dataEntityUsers)
                {
                    if ((item.user_type ?? "USER") != "A")
                    auxList.Add(new User
                    {
                        Username = item.id ?? item.username ?? item.PhoneNumber,
                        Name = item.name,
                        Email = item.email,
                        Photo = item.photo_url ?? item.photo,

                        MaximumLimit = decimal.Parse(item.max_debit ?? item.maximumlimit.ToString()),
                        Balance = decimal.Parse(item.balance.ToString())
                    });
                }

                dataGridViewEntityUser.DataSource = auxList;
                dataGridViewEntityUser.Columns["Password"].Visible = false;
                dataGridViewEntityUser.Columns["Photo"].Visible = false;
                dataGridViewEntityUser.Columns["ConfirmationCode"].Visible = false;
                foreach (DataGridViewRow row in dataGridViewEntityUser.Rows)
                {
                    if (row.Cells["Photo"].Value != "" && row.Cells["Photo"].Value.ToString().StartsWith("/"))
                    {
                        DataGridViewImageCell cell = row.Cells["cImage"] as DataGridViewImageCell;
                        string newUrl1 = null;
                        newUrl1 = entity.Endpoint+ row.Cells["Photo"].Value;
                        var request1 = WebRequest.Create(newUrl1);

                        using (var response = request1.GetResponse())
                        using (var stream = response.GetResponseStream())
                        {
                            cell.Value = Image.FromStream(stream);
                            cell.ImageLayout = DataGridViewImageCellLayout.Stretch;
                        }
                    }
                }
            }
        }

        private void btnChosePhoto_Click(object sender, EventArgs e)
        {
            // open file dialog   
            OpenFileDialog open = new OpenFileDialog();
            // image filters  
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box  
                picImage.Image = new Bitmap(open.FileName);
                // image file path  
                txtPhoto.Text = open.FileName;
            }
        }
    }
}
