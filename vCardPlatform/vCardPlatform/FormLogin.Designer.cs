
namespace vCardPlatform
{
    partial class FormLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogin));
            this.labelUsernameLogin = new System.Windows.Forms.Label();
            this.labelPasswordLogin = new System.Windows.Forms.Label();
            this.textBoxUserNameAdmin = new System.Windows.Forms.TextBox();
            this.textBoxPasswordAdmin = new System.Windows.Forms.TextBox();
            this.buttonLoginAdmin = new System.Windows.Forms.Button();
            this.pictureLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // labelUsernameLogin
            // 
            this.labelUsernameLogin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelUsernameLogin.AutoSize = true;
            this.labelUsernameLogin.Location = new System.Drawing.Point(70, 253);
            this.labelUsernameLogin.Name = "labelUsernameLogin";
            this.labelUsernameLogin.Size = new System.Drawing.Size(75, 17);
            this.labelUsernameLogin.TabIndex = 0;
            this.labelUsernameLogin.Text = "UserName";
            // 
            // labelPasswordLogin
            // 
            this.labelPasswordLogin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPasswordLogin.AutoSize = true;
            this.labelPasswordLogin.Location = new System.Drawing.Point(70, 298);
            this.labelPasswordLogin.Name = "labelPasswordLogin";
            this.labelPasswordLogin.Size = new System.Drawing.Size(69, 17);
            this.labelPasswordLogin.TabIndex = 1;
            this.labelPasswordLogin.Text = "Password";
            // 
            // textBoxUserNameAdmin
            // 
            this.textBoxUserNameAdmin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxUserNameAdmin.Location = new System.Drawing.Point(73, 273);
            this.textBoxUserNameAdmin.Name = "textBoxUserNameAdmin";
            this.textBoxUserNameAdmin.Size = new System.Drawing.Size(503, 22);
            this.textBoxUserNameAdmin.TabIndex = 2;
            // 
            // textBoxPasswordAdmin
            // 
            this.textBoxPasswordAdmin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPasswordAdmin.Location = new System.Drawing.Point(73, 318);
            this.textBoxPasswordAdmin.Name = "textBoxPasswordAdmin";
            this.textBoxPasswordAdmin.Size = new System.Drawing.Size(503, 22);
            this.textBoxPasswordAdmin.TabIndex = 3;
            // 
            // buttonLoginAdmin
            // 
            this.buttonLoginAdmin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLoginAdmin.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.buttonLoginAdmin.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonLoginAdmin.Location = new System.Drawing.Point(284, 371);
            this.buttonLoginAdmin.Name = "buttonLoginAdmin";
            this.buttonLoginAdmin.Size = new System.Drawing.Size(75, 28);
            this.buttonLoginAdmin.TabIndex = 4;
            this.buttonLoginAdmin.Text = "Login";
            this.buttonLoginAdmin.UseVisualStyleBackColor = false;
            this.buttonLoginAdmin.Click += new System.EventHandler(this.buttonLoginAdmin_Click);
            // 
            // pictureLogo
            // 
            this.pictureLogo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pictureLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureLogo.Image = ((System.Drawing.Image)(resources.GetObject("pictureLogo.Image")));
            this.pictureLogo.InitialImage = null;
            this.pictureLogo.Location = new System.Drawing.Point(135, 12);
            this.pictureLogo.Name = "pictureLogo";
            this.pictureLogo.Size = new System.Drawing.Size(375, 202);
            this.pictureLogo.TabIndex = 5;
            this.pictureLogo.TabStop = false;
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 413);
            this.Controls.Add(this.pictureLogo);
            this.Controls.Add(this.buttonLoginAdmin);
            this.Controls.Add(this.textBoxPasswordAdmin);
            this.Controls.Add(this.textBoxUserNameAdmin);
            this.Controls.Add(this.labelPasswordLogin);
            this.Controls.Add(this.labelUsernameLogin);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(671, 460);
            this.MinimumSize = new System.Drawing.Size(671, 460);
            this.Name = "FormLogin";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.FormLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelUsernameLogin;
        private System.Windows.Forms.Label labelPasswordLogin;
        private System.Windows.Forms.TextBox textBoxUserNameAdmin;
        private System.Windows.Forms.TextBox textBoxPasswordAdmin;
        private System.Windows.Forms.Button buttonLoginAdmin;
        private System.Windows.Forms.PictureBox pictureLogo;
    }
}