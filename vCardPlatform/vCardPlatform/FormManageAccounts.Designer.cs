
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
            this.buttonGetAll.Location = new System.Drawing.Point(48, 54);
            this.buttonGetAll.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonGetAll.Name = "buttonGetAll";
            this.buttonGetAll.Size = new System.Drawing.Size(119, 23);
            this.buttonGetAll.TabIndex = 0;
            this.buttonGetAll.Text = "Get All Administrators";
            this.buttonGetAll.UseVisualStyleBackColor = true;
            this.buttonGetAll.Click += new System.EventHandler(this.buttonGetAll_Click);
            // 
            // buttonGet
            // 
            this.buttonGet.Location = new System.Drawing.Point(48, 202);
            this.buttonGet.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonGet.Name = "buttonGet";
            this.buttonGet.Size = new System.Drawing.Size(101, 23);
            this.buttonGet.TabIndex = 1;
            this.buttonGet.Text = "Get Administrator";
            this.buttonGet.UseVisualStyleBackColor = true;
            this.buttonGet.Click += new System.EventHandler(this.buttonGet_Click);
            // 
            // buttonCreate
            // 
            this.buttonCreate.Location = new System.Drawing.Point(107, 162);
            this.buttonCreate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(61, 23);
            this.buttonCreate.TabIndex = 2;
            this.buttonCreate.Text = "Create";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(313, 203);
            this.buttonDelete.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(56, 23);
            this.buttonDelete.TabIndex = 3;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonDisable
            // 
            this.buttonDisable.Location = new System.Drawing.Point(373, 203);
            this.buttonDisable.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonDisable.Name = "buttonDisable";
            this.buttonDisable.Size = new System.Drawing.Size(65, 23);
            this.buttonDisable.TabIndex = 4;
            this.buttonDisable.Text = "Disable";
            this.buttonDisable.UseVisualStyleBackColor = true;
            this.buttonDisable.Click += new System.EventHandler(this.buttonDisable_Click);
            // 
            // richTextBoxGetAll
            // 
            this.richTextBoxGetAll.Location = new System.Drawing.Point(48, 81);
            this.richTextBoxGetAll.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.richTextBoxGetAll.Name = "richTextBoxGetAll";
            this.richTextBoxGetAll.Size = new System.Drawing.Size(390, 97);
            this.richTextBoxGetAll.TabIndex = 5;
            this.richTextBoxGetAll.Text = "";
            // 
            // richTextBoxGet
            // 
            this.richTextBoxGet.Location = new System.Drawing.Point(48, 244);
            this.richTextBoxGet.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.richTextBoxGet.Name = "richTextBoxGet";
            this.richTextBoxGet.Size = new System.Drawing.Size(390, 50);
            this.richTextBoxGet.TabIndex = 6;
            this.richTextBoxGet.Text = "";
            // 
            // textBoxFilterById
            // 
            this.textBoxFilterById.Location = new System.Drawing.Point(165, 206);
            this.textBoxFilterById.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxFilterById.Name = "textBoxFilterById";
            this.textBoxFilterById.Size = new System.Drawing.Size(76, 20);
            this.textBoxFilterById.TabIndex = 7;
            this.textBoxFilterById.Text = "1";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(92, 39);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(76, 20);
            this.textBoxName.TabIndex = 8;
            this.textBoxName.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // textBoxEmail
            // 
            this.textBoxEmail.Location = new System.Drawing.Point(92, 75);
            this.textBoxEmail.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.Size = new System.Drawing.Size(76, 20);
            this.textBoxEmail.TabIndex = 9;
            this.textBoxEmail.TextChanged += new System.EventHandler(this.textBoxEmail_TextChanged);
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(26, 42);
            this.labelName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(44, 15);
            this.labelName.TabIndex = 11;
            this.labelName.Text = "Name:";
            this.labelName.Click += new System.EventHandler(this.labelName_Click);
            // 
            // labelEmail
            // 
            this.labelEmail.AutoSize = true;
            this.labelEmail.Location = new System.Drawing.Point(26, 79);
            this.labelEmail.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(46, 15);
            this.labelEmail.TabIndex = 12;
            this.labelEmail.Text = "E-mail:";
            this.labelEmail.Click += new System.EventHandler(this.labelEmail_Click);
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(26, 118);
            this.labelPassword.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(64, 15);
            this.labelPassword.TabIndex = 13;
            this.labelPassword.Text = "Password:";
            this.labelPassword.Click += new System.EventHandler(this.labelPassword_Click);
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(92, 115);
            this.textBoxPassword.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(76, 20);
            this.textBoxPassword.TabIndex = 10;
            this.textBoxPassword.TextChanged += new System.EventHandler(this.textBoxPassword_TextChanged);
            // 
            // groupBoxCreate
            // 
            this.groupBoxCreate.Controls.Add(this.labelPassword);
            this.groupBoxCreate.Controls.Add(this.buttonCreate);
            this.groupBoxCreate.Controls.Add(this.labelEmail);
            this.groupBoxCreate.Controls.Add(this.labelName);
            this.groupBoxCreate.Controls.Add(this.textBoxName);
            this.groupBoxCreate.Controls.Add(this.textBoxPassword);
            this.groupBoxCreate.Controls.Add(this.textBoxEmail);
            this.groupBoxCreate.Location = new System.Drawing.Point(464, 81);
            this.groupBoxCreate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBoxCreate.Name = "groupBoxCreate";
            this.groupBoxCreate.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBoxCreate.Size = new System.Drawing.Size(190, 212);
            this.groupBoxCreate.TabIndex = 14;
            this.groupBoxCreate.TabStop = false;
            this.groupBoxCreate.Text = "Create Administrator";
            this.groupBoxCreate.Enter += new System.EventHandler(this.groupBoxCreate_Enter);
            // 
            // FormManageAccounts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 341);
            this.Controls.Add(this.textBoxFilterById);
            this.Controls.Add(this.richTextBoxGet);
            this.Controls.Add(this.richTextBoxGetAll);
            this.Controls.Add(this.buttonDisable);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonGet);
            this.Controls.Add(this.buttonGetAll);
            this.Controls.Add(this.groupBoxCreate);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FormManageAccounts";
            this.Text = "FormManageAccounts";
            this.groupBoxCreate.ResumeLayout(false);
            this.groupBoxCreate.PerformLayout();
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