
namespace vCardPlatform
{
    partial class FormLoginAdmin
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
            this.labelUsernameLogin = new System.Windows.Forms.Label();
            this.labelPasswordLogin = new System.Windows.Forms.Label();
            this.textBoxUserNameAdmin = new System.Windows.Forms.TextBox();
            this.textBoxPasswordAdmin = new System.Windows.Forms.TextBox();
            this.buttonLoginAdmin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelUsernameLogin
            // 
            this.labelUsernameLogin.AutoSize = true;
            this.labelUsernameLogin.Location = new System.Drawing.Point(36, 50);
            this.labelUsernameLogin.Name = "labelUsernameLogin";
            this.labelUsernameLogin.Size = new System.Drawing.Size(75, 17);
            this.labelUsernameLogin.TabIndex = 0;
            this.labelUsernameLogin.Text = "UserName";
            // 
            // labelPasswordLogin
            // 
            this.labelPasswordLogin.AutoSize = true;
            this.labelPasswordLogin.Location = new System.Drawing.Point(42, 87);
            this.labelPasswordLogin.Name = "labelPasswordLogin";
            this.labelPasswordLogin.Size = new System.Drawing.Size(69, 17);
            this.labelPasswordLogin.TabIndex = 1;
            this.labelPasswordLogin.Text = "Password";
            // 
            // textBoxUserNameAdmin
            // 
            this.textBoxUserNameAdmin.Location = new System.Drawing.Point(139, 47);
            this.textBoxUserNameAdmin.Name = "textBoxUserNameAdmin";
            this.textBoxUserNameAdmin.Size = new System.Drawing.Size(187, 22);
            this.textBoxUserNameAdmin.TabIndex = 2;
            // 
            // textBoxPasswordAdmin
            // 
            this.textBoxPasswordAdmin.Location = new System.Drawing.Point(139, 84);
            this.textBoxPasswordAdmin.Name = "textBoxPasswordAdmin";
            this.textBoxPasswordAdmin.Size = new System.Drawing.Size(187, 22);
            this.textBoxPasswordAdmin.TabIndex = 3;
            // 
            // buttonLoginAdmin
            // 
            this.buttonLoginAdmin.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.buttonLoginAdmin.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonLoginAdmin.Location = new System.Drawing.Point(251, 131);
            this.buttonLoginAdmin.Name = "buttonLoginAdmin";
            this.buttonLoginAdmin.Size = new System.Drawing.Size(75, 28);
            this.buttonLoginAdmin.TabIndex = 4;
            this.buttonLoginAdmin.Text = "Login";
            this.buttonLoginAdmin.UseVisualStyleBackColor = false;
            this.buttonLoginAdmin.Click += new System.EventHandler(this.buttonLoginAdmin_Click);
            // 
            // FormLoginAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 197);
            this.Controls.Add(this.buttonLoginAdmin);
            this.Controls.Add(this.textBoxPasswordAdmin);
            this.Controls.Add(this.textBoxUserNameAdmin);
            this.Controls.Add(this.labelPasswordLogin);
            this.Controls.Add(this.labelUsernameLogin);
            this.Name = "FormLoginAdmin";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelUsernameLogin;
        private System.Windows.Forms.Label labelPasswordLogin;
        private System.Windows.Forms.TextBox textBoxUserNameAdmin;
        private System.Windows.Forms.TextBox textBoxPasswordAdmin;
        private System.Windows.Forms.Button buttonLoginAdmin;
    }
}