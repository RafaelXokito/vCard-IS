using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Windows.Forms;
using vCardPlatform.Models;

namespace vCardPlatform
{
    public partial class FormChangeProfile : Form
    {
        private FormMainApplication parent;
        private RestClient client;
        private int admin_id;
        public FormChangeProfile(FormMainApplication parent, RestClient client, int admin_id)
        {
            this.parent = parent;
            this.client = client;
            this.admin_id = admin_id;
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "")
            {

                RestRequest request = new RestRequest("administrators/"+admin_id, Method.PUT, DataFormat.Json);
                Administrator administrator = new Administrator
                {
                    Name = txtName.Text
                };
                request.AddJsonBody(administrator);
                IRestResponse response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    parent.CurrentAdminName = txtName.Text;
                    this.Close();
                    return;
                }

                MessageBox.Show("Something went wrong");
            }
        }
    }
}
