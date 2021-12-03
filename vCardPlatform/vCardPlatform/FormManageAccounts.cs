using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vCardPlatform
{
    public partial class FormManageAccounts : Form
    {
        string baseURI = @"http://localhost:59458/";
        public FormManageAccounts()
        {
            InitializeComponent();
        }

        private void buttonGetAll_Click(object sender, EventArgs e)
        {
            var client = new RestSharp.RestClient(baseURI);

            var request = new RestSharp.RestRequest("api/administrators", RestSharp.Method.GET);

            var result = client.Execute<List<Administrator>>(request).Data;
            
            richTextBoxGetAll.Clear();
            foreach (Administrator item in result)
            {
                richTextBoxGetAll.AppendText($"Id: {item.Id}\tName: {item.Name}\tE-mail: {item.Email}\tDisabled: {item.Disabled}\n");
            }
        }

        private void buttonGet_Click(object sender, EventArgs e)
        {
            var client = new RestSharp.RestClient(baseURI);

            var request = new RestSharp.RestRequest("api/administrators/{id}", RestSharp.Method.GET);
            request.AddUrlSegment("id", textBoxFilterById.Text);

            var response = client.Execute<Administrator>(request).Data;

            if (response != null)
            {
                richTextBoxGet.Text = $"Id: {response.Id}\tName: {response.Name}\tE-mail: {response.Email}\tDisabled: {response.Disabled}\t";
            }
            else {
                richTextBoxGet.Clear();
            }
            //textBoxName.Text = response.Name;
            //textBoxEmail.Text = response.Email;
        }

        private void buttonDisable_Click(object sender, EventArgs e)
        {
            buttonDisable.Text = buttonDisable.Text.Contains("Disable") ? "Enable" : "Disable";

            //richTextBoxGet.Text = $"Disabled: {buttonDisable.Text}\t";

            var client = new RestSharp.RestClient(baseURI);

            var request = new RestSharp.RestRequest("api/administrators/{id}", RestSharp.Method.GET);
            request.AddUrlSegment("id", textBoxFilterById.Text);

            var response = client.Execute<Administrator>(request).Data;

            if (response != null)
            {
                richTextBoxGet.Text = $"Id: {response.Id}\tName: {response.Name}\tE-mail: {response.Email}\tDisabled: {response.Disabled}\t";
            }
            else
            {
                richTextBoxGet.Clear();
            }

            Administrator admin = new Administrator
            {
                Name = response.Name,
                Email = response.Email,
                Password = response.Password
            };

            var request1 = new RestSharp.RestRequest("api/administrators/{id}/disabled", RestSharp.Method.PATCH);
            request1.AddUrlSegment("id", textBoxFilterById.Text);

            request1.AddJsonBody(admin);

            RestSharp.IRestResponse response1 = client.Execute(request1);

            MessageBox.Show(response1.StatusCode + " " + response1.ResponseStatus);
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            Administrator admin = new Administrator
            {
                Name = textBoxName.Text,
                Email = textBoxEmail.Text,
                Password = textBoxPassword.Text
            };

            var client = new RestSharp.RestClient(baseURI);

            var request = new RestSharp.RestRequest("api/administrators", RestSharp.Method.POST);

            request.AddJsonBody(admin);

            RestSharp.IRestResponse response = client.Execute(request);
            MessageBox.Show(response.StatusCode + " " + response.ResponseStatus);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            var client = new RestSharp.RestClient(baseURI);

            var request = new RestSharp.RestRequest("api/administrators/{id}", RestSharp.Method.DELETE);
            request.AddUrlSegment("id", textBoxFilterById.Text);

            RestSharp.IRestResponse response = client.Execute(request);

            MessageBox.Show(response.StatusCode + " " + response.ResponseStatus);
            richTextBoxGet.Clear();
        }
    }
}
