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
    public partial class FormMainApplication : Form
    {
        public FormMainApplication()
        {
            InitializeComponent();
        }

        //button Logout Click Event
        private void buttonLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormLoginAdmin fl = new FormLoginAdmin();
            fl.Show();
        }

        private void FormMainApplication_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
