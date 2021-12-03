
namespace vCardPlatform
{
    partial class FormMainApplication
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
            this.buttonLogout = new System.Windows.Forms.Button();
            this.labelAdministratorName = new System.Windows.Forms.Label();
            this.buttonChangePassword = new System.Windows.Forms.Button();
            this.buttonManageAccounts = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonLogout
            // 
            this.buttonLogout.Location = new System.Drawing.Point(698, 13);
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.Size = new System.Drawing.Size(71, 32);
            this.buttonLogout.TabIndex = 0;
            this.buttonLogout.Text = "Log out";
            this.buttonLogout.UseVisualStyleBackColor = true;
            this.buttonLogout.Click += new System.EventHandler(this.buttonLogout_Click);
            // 
            // labelAdministratorName
            // 
            this.labelAdministratorName.AutoSize = true;
            this.labelAdministratorName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAdministratorName.Location = new System.Drawing.Point(35, 20);
            this.labelAdministratorName.Name = "labelAdministratorName";
            this.labelAdministratorName.Size = new System.Drawing.Size(197, 25);
            this.labelAdministratorName.TabIndex = 1;
            this.labelAdministratorName.Text = "Administrator\'s Name";
            // 
            // buttonChangePassword
            // 
            this.buttonChangePassword.Location = new System.Drawing.Point(40, 59);
            this.buttonChangePassword.Name = "buttonChangePassword";
            this.buttonChangePassword.Size = new System.Drawing.Size(136, 29);
            this.buttonChangePassword.TabIndex = 2;
            this.buttonChangePassword.Text = "Change Password";
            this.buttonChangePassword.UseVisualStyleBackColor = true;
            this.buttonChangePassword.Click += new System.EventHandler(this.buttonChangePassword_Click);
            // 
            // buttonManageAccounts
            // 
            this.buttonManageAccounts.Location = new System.Drawing.Point(328, 59);
            this.buttonManageAccounts.Name = "buttonManageAccounts";
            this.buttonManageAccounts.Size = new System.Drawing.Size(228, 29);
            this.buttonManageAccounts.TabIndex = 3;
            this.buttonManageAccounts.Text = "Manage Administrator Accounts";
            this.buttonManageAccounts.UseVisualStyleBackColor = true;
            this.buttonManageAccounts.Click += new System.EventHandler(this.buttonManageAccounts_Click);
            // 
            // FormMainApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonManageAccounts);
            this.Controls.Add(this.buttonChangePassword);
            this.Controls.Add(this.labelAdministratorName);
            this.Controls.Add(this.buttonLogout);
            this.Name = "FormMainApplication";
            this.Text = "MainApplication";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMainApplication_FormClosing);
            this.Load += new System.EventHandler(this.FormMainApplication_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonLogout;
        private System.Windows.Forms.Label labelAdministratorName;
        private System.Windows.Forms.Button buttonChangePassword;
        private System.Windows.Forms.Button buttonManageAccounts;
    }
}