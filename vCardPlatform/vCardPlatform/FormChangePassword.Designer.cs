
namespace vCardPlatform
{
    partial class FormChangePassword
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
            this.textBoxOldPassword = new System.Windows.Forms.TextBox();
            this.textBoxNewPassword = new System.Windows.Forms.TextBox();
            this.labelOldPassword = new System.Windows.Forms.Label();
            this.labelNewPassword = new System.Windows.Forms.Label();
            this.buttonSubmit = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.checkBoxOldPassword = new System.Windows.Forms.CheckBox();
            this.checkBoxNewPassword = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // textBoxOldPassword
            // 
            this.textBoxOldPassword.Location = new System.Drawing.Point(166, 41);
            this.textBoxOldPassword.Name = "textBoxOldPassword";
            this.textBoxOldPassword.PasswordChar = '*';
            this.textBoxOldPassword.Size = new System.Drawing.Size(196, 22);
            this.textBoxOldPassword.TabIndex = 0;
            // 
            // textBoxNewPassword
            // 
            this.textBoxNewPassword.Location = new System.Drawing.Point(166, 106);
            this.textBoxNewPassword.Name = "textBoxNewPassword";
            this.textBoxNewPassword.PasswordChar = '*';
            this.textBoxNewPassword.Size = new System.Drawing.Size(196, 22);
            this.textBoxNewPassword.TabIndex = 1;
            // 
            // labelOldPassword
            // 
            this.labelOldPassword.AutoSize = true;
            this.labelOldPassword.Location = new System.Drawing.Point(42, 41);
            this.labelOldPassword.Name = "labelOldPassword";
            this.labelOldPassword.Size = new System.Drawing.Size(95, 17);
            this.labelOldPassword.TabIndex = 2;
            this.labelOldPassword.Text = "Old Password";
            // 
            // labelNewPassword
            // 
            this.labelNewPassword.AutoSize = true;
            this.labelNewPassword.Location = new System.Drawing.Point(42, 106);
            this.labelNewPassword.Name = "labelNewPassword";
            this.labelNewPassword.Size = new System.Drawing.Size(100, 17);
            this.labelNewPassword.TabIndex = 3;
            this.labelNewPassword.Text = "New Password";
            // 
            // buttonSubmit
            // 
            this.buttonSubmit.Location = new System.Drawing.Point(166, 170);
            this.buttonSubmit.Name = "buttonSubmit";
            this.buttonSubmit.Size = new System.Drawing.Size(75, 23);
            this.buttonSubmit.TabIndex = 4;
            this.buttonSubmit.Text = "OK";
            this.buttonSubmit.UseVisualStyleBackColor = true;
            this.buttonSubmit.Click += new System.EventHandler(this.buttonSubmit_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(287, 170);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // checkBoxOldPassword
            // 
            this.checkBoxOldPassword.AutoSize = true;
            this.checkBoxOldPassword.Location = new System.Drawing.Point(379, 41);
            this.checkBoxOldPassword.Name = "checkBoxOldPassword";
            this.checkBoxOldPassword.Size = new System.Drawing.Size(64, 21);
            this.checkBoxOldPassword.TabIndex = 7;
            this.checkBoxOldPassword.Text = "Show";
            this.checkBoxOldPassword.UseVisualStyleBackColor = true;
            this.checkBoxOldPassword.CheckedChanged += new System.EventHandler(this.checkBoxOldPassword_CheckedChanged);
            // 
            // checkBoxNewPassword
            // 
            this.checkBoxNewPassword.AutoSize = true;
            this.checkBoxNewPassword.Location = new System.Drawing.Point(379, 107);
            this.checkBoxNewPassword.Name = "checkBoxNewPassword";
            this.checkBoxNewPassword.Size = new System.Drawing.Size(64, 21);
            this.checkBoxNewPassword.TabIndex = 8;
            this.checkBoxNewPassword.Text = "Show";
            this.checkBoxNewPassword.UseVisualStyleBackColor = true;
            this.checkBoxNewPassword.CheckedChanged += new System.EventHandler(this.checkBoxNewPassword_CheckedChanged);
            // 
            // FormChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 236);
            this.Controls.Add(this.checkBoxNewPassword);
            this.Controls.Add(this.checkBoxOldPassword);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSubmit);
            this.Controls.Add(this.labelNewPassword);
            this.Controls.Add(this.labelOldPassword);
            this.Controls.Add(this.textBoxNewPassword);
            this.Controls.Add(this.textBoxOldPassword);
            this.Name = "FormChangePassword";
            this.Text = "FormChangePassword";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxOldPassword;
        private System.Windows.Forms.TextBox textBoxNewPassword;
        private System.Windows.Forms.Label labelOldPassword;
        private System.Windows.Forms.Label labelNewPassword;
        private System.Windows.Forms.Button buttonSubmit;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkBoxOldPassword;
        private System.Windows.Forms.CheckBox checkBoxNewPassword;
    }
}