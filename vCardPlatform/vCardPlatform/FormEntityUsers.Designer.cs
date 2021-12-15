
namespace vCardPlatform
{
    partial class FormEntityUsers
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEntityUsers));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabEntityUserTable = new System.Windows.Forms.TabPage();
            this.tabEntityUserCreate = new System.Windows.Forms.TabPage();
            this.dataGridViewEntityUser = new System.Windows.Forms.DataGridView();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtConfirmationCode = new System.Windows.Forms.TextBox();
            this.txtPhoto = new System.Windows.Forms.TextBox();
            this.btnChosePhoto = new System.Windows.Forms.Button();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblConfirmationCode = new System.Windows.Forms.Label();
            this.lblPhoto = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.picImage = new System.Windows.Forms.PictureBox();
            this.tabControl1.SuspendLayout();
            this.tabEntityUserTable.SuspendLayout();
            this.tabEntityUserCreate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEntityUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabEntityUserTable);
            this.tabControl1.Controls.Add(this.tabEntityUserCreate);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(944, 525);
            this.tabControl1.TabIndex = 0;
            // 
            // tabEntityUserTable
            // 
            this.tabEntityUserTable.Controls.Add(this.dataGridViewEntityUser);
            this.tabEntityUserTable.Location = new System.Drawing.Point(4, 22);
            this.tabEntityUserTable.Name = "tabEntityUserTable";
            this.tabEntityUserTable.Padding = new System.Windows.Forms.Padding(3);
            this.tabEntityUserTable.Size = new System.Drawing.Size(936, 499);
            this.tabEntityUserTable.TabIndex = 0;
            this.tabEntityUserTable.Text = "Table";
            this.tabEntityUserTable.UseVisualStyleBackColor = true;
            // 
            // tabEntityUserCreate
            // 
            this.tabEntityUserCreate.Controls.Add(this.picImage);
            this.tabEntityUserCreate.Controls.Add(this.btnCreate);
            this.tabEntityUserCreate.Controls.Add(this.lblPhoto);
            this.tabEntityUserCreate.Controls.Add(this.lblConfirmationCode);
            this.tabEntityUserCreate.Controls.Add(this.lblEmail);
            this.tabEntityUserCreate.Controls.Add(this.lblName);
            this.tabEntityUserCreate.Controls.Add(this.lblPassword);
            this.tabEntityUserCreate.Controls.Add(this.lblUsername);
            this.tabEntityUserCreate.Controls.Add(this.btnChosePhoto);
            this.tabEntityUserCreate.Controls.Add(this.txtPhoto);
            this.tabEntityUserCreate.Controls.Add(this.txtConfirmationCode);
            this.tabEntityUserCreate.Controls.Add(this.txtEmail);
            this.tabEntityUserCreate.Controls.Add(this.txtName);
            this.tabEntityUserCreate.Controls.Add(this.txtPassword);
            this.tabEntityUserCreate.Controls.Add(this.txtUsername);
            this.tabEntityUserCreate.Location = new System.Drawing.Point(4, 22);
            this.tabEntityUserCreate.Name = "tabEntityUserCreate";
            this.tabEntityUserCreate.Padding = new System.Windows.Forms.Padding(3);
            this.tabEntityUserCreate.Size = new System.Drawing.Size(936, 499);
            this.tabEntityUserCreate.TabIndex = 1;
            this.tabEntityUserCreate.Text = "Create";
            this.tabEntityUserCreate.UseVisualStyleBackColor = true;
            // 
            // dataGridViewEntityUser
            // 
            this.dataGridViewEntityUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEntityUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewEntityUser.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewEntityUser.Name = "dataGridViewEntityUser";
            this.dataGridViewEntityUser.RowHeadersWidth = 45;
            this.dataGridViewEntityUser.Size = new System.Drawing.Size(930, 493);
            this.dataGridViewEntityUser.TabIndex = 0;
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(104, 92);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(221, 20);
            this.txtUsername.TabIndex = 0;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(104, 145);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(221, 20);
            this.txtPassword.TabIndex = 1;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(104, 198);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(221, 20);
            this.txtName.TabIndex = 2;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(104, 251);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(221, 20);
            this.txtEmail.TabIndex = 3;
            // 
            // txtConfirmationCode
            // 
            this.txtConfirmationCode.Location = new System.Drawing.Point(104, 304);
            this.txtConfirmationCode.Name = "txtConfirmationCode";
            this.txtConfirmationCode.Size = new System.Drawing.Size(221, 20);
            this.txtConfirmationCode.TabIndex = 4;
            // 
            // txtPhoto
            // 
            this.txtPhoto.Enabled = false;
            this.txtPhoto.Location = new System.Drawing.Point(104, 357);
            this.txtPhoto.Name = "txtPhoto";
            this.txtPhoto.Size = new System.Drawing.Size(189, 20);
            this.txtPhoto.TabIndex = 5;
            // 
            // btnChosePhoto
            // 
            this.btnChosePhoto.Image = global::vCardPlatform.Properties.Resources.image;
            this.btnChosePhoto.Location = new System.Drawing.Point(299, 355);
            this.btnChosePhoto.Name = "btnChosePhoto";
            this.btnChosePhoto.Size = new System.Drawing.Size(26, 23);
            this.btnChosePhoto.TabIndex = 6;
            this.btnChosePhoto.UseVisualStyleBackColor = true;
            this.btnChosePhoto.Click += new System.EventHandler(this.btnChosePhoto_Click);
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(101, 74);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(65, 15);
            this.lblUsername.TabIndex = 7;
            this.lblUsername.Text = "Username";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(101, 127);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(61, 15);
            this.lblPassword.TabIndex = 8;
            this.lblPassword.Text = "Password";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(101, 180);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(41, 15);
            this.lblName.TabIndex = 9;
            this.lblName.Text = "Name";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(101, 233);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(39, 15);
            this.lblEmail.TabIndex = 10;
            this.lblEmail.Text = "Email";
            // 
            // lblConfirmationCode
            // 
            this.lblConfirmationCode.AutoSize = true;
            this.lblConfirmationCode.Location = new System.Drawing.Point(101, 286);
            this.lblConfirmationCode.Name = "lblConfirmationCode";
            this.lblConfirmationCode.Size = new System.Drawing.Size(109, 15);
            this.lblConfirmationCode.TabIndex = 11;
            this.lblConfirmationCode.Text = "Confirmation Code";
            // 
            // lblPhoto
            // 
            this.lblPhoto.AutoSize = true;
            this.lblPhoto.Location = new System.Drawing.Point(101, 339);
            this.lblPhoto.Name = "lblPhoto";
            this.lblPhoto.Size = new System.Drawing.Size(39, 15);
            this.lblPhoto.TabIndex = 12;
            this.lblPhoto.Text = "Photo";
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(181, 414);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 13;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            // 
            // picImage
            // 
            this.picImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picImage.Location = new System.Drawing.Point(578, 145);
            this.picImage.Name = "picImage";
            this.picImage.Size = new System.Drawing.Size(160, 160);
            this.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picImage.TabIndex = 14;
            this.picImage.TabStop = false;
            // 
            // FormEntityUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 525);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(960, 566);
            this.Name = "FormEntityUsers";
            this.Text = "Users";
            this.Load += new System.EventHandler(this.FormEntityUsers_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabEntityUserTable.ResumeLayout(false);
            this.tabEntityUserCreate.ResumeLayout(false);
            this.tabEntityUserCreate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEntityUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabEntityUserTable;
        private System.Windows.Forms.DataGridView dataGridViewEntityUser;
        private System.Windows.Forms.TabPage tabEntityUserCreate;
        private System.Windows.Forms.Button btnChosePhoto;
        private System.Windows.Forms.TextBox txtPhoto;
        private System.Windows.Forms.TextBox txtConfirmationCode;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Label lblPhoto;
        private System.Windows.Forms.Label lblConfirmationCode;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.PictureBox picImage;
    }
}