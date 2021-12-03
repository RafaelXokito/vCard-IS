
namespace vCardPlatform
{
    partial class FormManageAccounts
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
            this.buttonGetAll = new System.Windows.Forms.Button();
            this.buttonGet = new System.Windows.Forms.Button();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonDisable = new System.Windows.Forms.Button();
            this.richTextBoxGetAll = new System.Windows.Forms.RichTextBox();
            this.richTextBoxGet = new System.Windows.Forms.RichTextBox();
            this.textBoxFilterById = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxEmail = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.labelEmail = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.groupBoxCreate = new System.Windows.Forms.GroupBox();
            this.groupBoxCreate.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonGetAll
            // 
            this.buttonGetAll.Location = new System.Drawing.Point(64, 54);
            this.buttonGetAll.Name = "buttonGetAll";
            this.buttonGetAll.Size = new System.Drawing.Size(159, 25);
            this.buttonGetAll.TabIndex = 0;
            this.buttonGetAll.Text = "Get All Administrators";
            this.buttonGetAll.UseVisualStyleBackColor = true;
            this.buttonGetAll.Click += new System.EventHandler(this.buttonGetAll_Click);
            // 
            // buttonGet
            // 
            this.buttonGet.Location = new System.Drawing.Point(64, 248);
            this.buttonGet.Name = "buttonGet";
            this.buttonGet.Size = new System.Drawing.Size(135, 28);
            this.buttonGet.TabIndex = 1;
            this.buttonGet.Text = "Get Administrator";
            this.buttonGet.UseVisualStyleBackColor = true;
            this.buttonGet.Click += new System.EventHandler(this.buttonGet_Click);
            // 
            // buttonCreate
            // 
            this.buttonCreate.Location = new System.Drawing.Point(143, 199);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(75, 24);
            this.buttonCreate.TabIndex = 2;
            this.buttonCreate.Text = "Create";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(420, 253);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonDelete.TabIndex = 3;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonDisable
            // 
            this.buttonDisable.Location = new System.Drawing.Point(508, 253);
            this.buttonDisable.Name = "buttonDisable";
            this.buttonDisable.Size = new System.Drawing.Size(75, 23);
            this.buttonDisable.TabIndex = 4;
            this.buttonDisable.Text = "Disable";
            this.buttonDisable.UseVisualStyleBackColor = true;
            this.buttonDisable.Click += new System.EventHandler(this.buttonDisable_Click);
            // 
            // richTextBoxGetAll
            // 
            this.richTextBoxGetAll.Location = new System.Drawing.Point(64, 100);
            this.richTextBoxGetAll.Name = "richTextBoxGetAll";
            this.richTextBoxGetAll.Size = new System.Drawing.Size(519, 119);
            this.richTextBoxGetAll.TabIndex = 5;
            this.richTextBoxGetAll.Text = "";
            // 
            // richTextBoxGet
            // 
            this.richTextBoxGet.Location = new System.Drawing.Point(64, 300);
            this.richTextBoxGet.Name = "richTextBoxGet";
            this.richTextBoxGet.Size = new System.Drawing.Size(519, 61);
            this.richTextBoxGet.TabIndex = 6;
            this.richTextBoxGet.Text = "";
            // 
            // textBoxFilterById
            // 
            this.textBoxFilterById.Location = new System.Drawing.Point(220, 254);
            this.textBoxFilterById.Name = "textBoxFilterById";
            this.textBoxFilterById.Size = new System.Drawing.Size(100, 22);
            this.textBoxFilterById.TabIndex = 7;
            this.textBoxFilterById.Text = "1";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(736, 144);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(100, 22);
            this.textBoxName.TabIndex = 8;
            // 
            // textBoxEmail
            // 
            this.textBoxEmail.Location = new System.Drawing.Point(736, 188);
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.Size = new System.Drawing.Size(100, 22);
            this.textBoxEmail.TabIndex = 9;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(653, 148);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(49, 17);
            this.labelName.TabIndex = 11;
            this.labelName.Text = "Name:";
            // 
            // labelEmail
            // 
            this.labelEmail.AutoSize = true;
            this.labelEmail.Location = new System.Drawing.Point(653, 193);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(51, 17);
            this.labelEmail.TabIndex = 12;
            this.labelEmail.Text = "E-mail:";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(653, 241);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(73, 17);
            this.labelPassword.TabIndex = 13;
            this.labelPassword.Text = "Password:";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(736, 238);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(100, 22);
            this.textBoxPassword.TabIndex = 10;
            // 
            // groupBoxCreate
            // 
            this.groupBoxCreate.Controls.Add(this.buttonCreate);
            this.groupBoxCreate.Location = new System.Drawing.Point(618, 100);
            this.groupBoxCreate.Name = "groupBoxCreate";
            this.groupBoxCreate.Size = new System.Drawing.Size(254, 261);
            this.groupBoxCreate.TabIndex = 14;
            this.groupBoxCreate.TabStop = false;
            this.groupBoxCreate.Text = "Create Administrator";
            // 
            // FormManageAccounts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 420);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.labelEmail);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxEmail);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.textBoxFilterById);
            this.Controls.Add(this.richTextBoxGet);
            this.Controls.Add(this.richTextBoxGetAll);
            this.Controls.Add(this.buttonDisable);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonGet);
            this.Controls.Add(this.buttonGetAll);
            this.Controls.Add(this.groupBoxCreate);
            this.Name = "FormManageAccounts";
            this.Text = "FormManageAccounts";
            this.groupBoxCreate.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonGetAll;
        private System.Windows.Forms.Button buttonGet;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonDisable;
        private System.Windows.Forms.RichTextBox richTextBoxGetAll;
        private System.Windows.Forms.RichTextBox richTextBoxGet;
        private System.Windows.Forms.TextBox textBoxFilterById;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxEmail;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.GroupBox groupBoxCreate;
    }
}