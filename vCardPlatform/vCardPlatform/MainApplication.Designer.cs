
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMainApplication));
            this.labelAdministratorName = new System.Windows.Forms.Label();
            this.buttonChangePassword = new System.Windows.Forms.Button();
            this.tabCMain = new System.Windows.Forms.TabControl();
            this.tabProfile = new System.Windows.Forms.TabPage();
            this.panelProfile = new System.Windows.Forms.Panel();
            this.tabEntities = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnEntitiesBlock = new System.Windows.Forms.Button();
            this.btnEntitiesDelete = new System.Windows.Forms.Button();
            this.btnEntitiesRefresh = new System.Windows.Forms.Button();
            this.dataGridViewEntities = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.tabAdmistrators = new System.Windows.Forms.TabPage();
            this.tabCAdministrators = new System.Windows.Forms.TabControl();
            this.tabTable = new System.Windows.Forms.TabPage();
            this.btnAdministratorsDelete = new System.Windows.Forms.Button();
            this.btnAdministratorsRefresh = new System.Windows.Forms.Button();
            this.dataGridViewAdministrators = new System.Windows.Forms.DataGridView();
            this.tabCreate = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tabOperations = new System.Windows.Forms.TabPage();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.btnLogout = new System.Windows.Forms.Button();
            this.tabCMain.SuspendLayout();
            this.tabProfile.SuspendLayout();
            this.tabEntities.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEntities)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabAdmistrators.SuspendLayout();
            this.tabCAdministrators.SuspendLayout();
            this.tabTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAdministrators)).BeginInit();
            this.tabCreate.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelAdministratorName
            // 
            this.labelAdministratorName.AutoSize = true;
            this.labelAdministratorName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAdministratorName.Location = new System.Drawing.Point(10, 9);
            this.labelAdministratorName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelAdministratorName.Name = "labelAdministratorName";
            this.labelAdministratorName.Size = new System.Drawing.Size(180, 22);
            this.labelAdministratorName.TabIndex = 1;
            this.labelAdministratorName.Text = "Administrator\'s Name";
            // 
            // buttonChangePassword
            // 
            this.buttonChangePassword.Location = new System.Drawing.Point(227, 7);
            this.buttonChangePassword.Margin = new System.Windows.Forms.Padding(2);
            this.buttonChangePassword.Name = "buttonChangePassword";
            this.buttonChangePassword.Size = new System.Drawing.Size(102, 24);
            this.buttonChangePassword.TabIndex = 2;
            this.buttonChangePassword.Text = "Change Password";
            this.buttonChangePassword.UseVisualStyleBackColor = true;
            this.buttonChangePassword.Click += new System.EventHandler(this.buttonChangePassword_Click);
            // 
            // tabCMain
            // 
            this.tabCMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabCMain.Controls.Add(this.tabProfile);
            this.tabCMain.Controls.Add(this.tabEntities);
            this.tabCMain.Controls.Add(this.tabAdmistrators);
            this.tabCMain.Controls.Add(this.tabOperations);
            this.tabCMain.Location = new System.Drawing.Point(0, 1);
            this.tabCMain.Name = "tabCMain";
            this.tabCMain.SelectedIndex = 0;
            this.tabCMain.Size = new System.Drawing.Size(946, 499);
            this.tabCMain.TabIndex = 4;
            // 
            // tabProfile
            // 
            this.tabProfile.Controls.Add(this.panelProfile);
            this.tabProfile.Controls.Add(this.buttonChangePassword);
            this.tabProfile.Controls.Add(this.labelAdministratorName);
            this.tabProfile.Location = new System.Drawing.Point(4, 22);
            this.tabProfile.Name = "tabProfile";
            this.tabProfile.Padding = new System.Windows.Forms.Padding(3);
            this.tabProfile.Size = new System.Drawing.Size(938, 473);
            this.tabProfile.TabIndex = 0;
            this.tabProfile.Text = "Profile";
            this.tabProfile.UseVisualStyleBackColor = true;
            // 
            // panelProfile
            // 
            this.panelProfile.BackColor = System.Drawing.SystemColors.Control;
            this.panelProfile.Location = new System.Drawing.Point(9, 57);
            this.panelProfile.Name = "panelProfile";
            this.panelProfile.Size = new System.Drawing.Size(911, 354);
            this.panelProfile.TabIndex = 4;
            // 
            // tabEntities
            // 
            this.tabEntities.Controls.Add(this.tabControl1);
            this.tabEntities.Location = new System.Drawing.Point(4, 22);
            this.tabEntities.Name = "tabEntities";
            this.tabEntities.Padding = new System.Windows.Forms.Padding(3);
            this.tabEntities.Size = new System.Drawing.Size(938, 473);
            this.tabEntities.TabIndex = 1;
            this.tabEntities.Text = "Entities";
            this.tabEntities.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(6, 8);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(927, 456);
            this.tabControl1.TabIndex = 30;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnEntitiesBlock);
            this.tabPage1.Controls.Add(this.btnEntitiesDelete);
            this.tabPage1.Controls.Add(this.btnEntitiesRefresh);
            this.tabPage1.Controls.Add(this.dataGridViewEntities);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(919, 430);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Table";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnEntitiesBlock
            // 
            this.btnEntitiesBlock.Image = global::vCardPlatform.Properties.Resources.block;
            this.btnEntitiesBlock.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEntitiesBlock.Location = new System.Drawing.Point(664, 6);
            this.btnEntitiesBlock.Name = "btnEntitiesBlock";
            this.btnEntitiesBlock.Size = new System.Drawing.Size(75, 23);
            this.btnEntitiesBlock.TabIndex = 23;
            this.btnEntitiesBlock.Text = "Block";
            this.btnEntitiesBlock.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEntitiesBlock.UseVisualStyleBackColor = true;
            // 
            // btnEntitiesDelete
            // 
            this.btnEntitiesDelete.Image = global::vCardPlatform.Properties.Resources.delete;
            this.btnEntitiesDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEntitiesDelete.Location = new System.Drawing.Point(745, 6);
            this.btnEntitiesDelete.Name = "btnEntitiesDelete";
            this.btnEntitiesDelete.Size = new System.Drawing.Size(75, 23);
            this.btnEntitiesDelete.TabIndex = 22;
            this.btnEntitiesDelete.Text = "Delete";
            this.btnEntitiesDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEntitiesDelete.UseVisualStyleBackColor = true;
            // 
            // btnEntitiesRefresh
            // 
            this.btnEntitiesRefresh.Image = global::vCardPlatform.Properties.Resources.refresh;
            this.btnEntitiesRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEntitiesRefresh.Location = new System.Drawing.Point(826, 6);
            this.btnEntitiesRefresh.Name = "btnEntitiesRefresh";
            this.btnEntitiesRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnEntitiesRefresh.TabIndex = 21;
            this.btnEntitiesRefresh.Text = "Refresh";
            this.btnEntitiesRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEntitiesRefresh.UseVisualStyleBackColor = true;
            this.btnEntitiesRefresh.Click += new System.EventHandler(this.btnEntitiesRefresh_Click);
            // 
            // dataGridViewEntities
            // 
            this.dataGridViewEntities.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewEntities.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEntities.Location = new System.Drawing.Point(6, 35);
            this.dataGridViewEntities.Name = "dataGridViewEntities";
            this.dataGridViewEntities.RowHeadersWidth = 45;
            this.dataGridViewEntities.Size = new System.Drawing.Size(910, 392);
            this.dataGridViewEntities.TabIndex = 20;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(919, 430);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Create";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.textBox4);
            this.groupBox2.Controls.Add(this.textBox5);
            this.groupBox2.Controls.Add(this.textBox6);
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Location = new System.Drawing.Point(7, 5);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(892, 420);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Create Entity";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(256, 202);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 15);
            this.label4.TabIndex = 19;
            this.label4.Text = "Password:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(256, 163);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 15);
            this.label5.TabIndex = 18;
            this.label5.Text = "E-mail:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(256, 126);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 15);
            this.label6.TabIndex = 17;
            this.label6.Text = "Name:";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(322, 123);
            this.textBox4.Margin = new System.Windows.Forms.Padding(2);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(350, 20);
            this.textBox4.TabIndex = 14;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(322, 199);
            this.textBox5.Margin = new System.Windows.Forms.Padding(2);
            this.textBox5.Name = "textBox5";
            this.textBox5.PasswordChar = '*';
            this.textBox5.Size = new System.Drawing.Size(350, 20);
            this.textBox5.TabIndex = 16;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(322, 159);
            this.textBox6.Margin = new System.Windows.Forms.Padding(2);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(350, 20);
            this.textBox6.TabIndex = 15;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(427, 254);
            this.button5.Margin = new System.Windows.Forms.Padding(2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(61, 23);
            this.button5.TabIndex = 2;
            this.button5.Text = "Create";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // tabAdmistrators
            // 
            this.tabAdmistrators.Controls.Add(this.tabCAdministrators);
            this.tabAdmistrators.Location = new System.Drawing.Point(4, 22);
            this.tabAdmistrators.Name = "tabAdmistrators";
            this.tabAdmistrators.Size = new System.Drawing.Size(938, 473);
            this.tabAdmistrators.TabIndex = 2;
            this.tabAdmistrators.Text = "Administrators";
            this.tabAdmistrators.UseVisualStyleBackColor = true;
            // 
            // tabCAdministrators
            // 
            this.tabCAdministrators.Controls.Add(this.tabTable);
            this.tabCAdministrators.Controls.Add(this.tabCreate);
            this.tabCAdministrators.Location = new System.Drawing.Point(8, 3);
            this.tabCAdministrators.Name = "tabCAdministrators";
            this.tabCAdministrators.SelectedIndex = 0;
            this.tabCAdministrators.Size = new System.Drawing.Size(927, 456);
            this.tabCAdministrators.TabIndex = 29;
            // 
            // tabTable
            // 
            this.tabTable.Controls.Add(this.btnAdministratorsDelete);
            this.tabTable.Controls.Add(this.btnAdministratorsRefresh);
            this.tabTable.Controls.Add(this.dataGridViewAdministrators);
            this.tabTable.Location = new System.Drawing.Point(4, 22);
            this.tabTable.Name = "tabTable";
            this.tabTable.Padding = new System.Windows.Forms.Padding(3);
            this.tabTable.Size = new System.Drawing.Size(919, 430);
            this.tabTable.TabIndex = 0;
            this.tabTable.Text = "Table";
            this.tabTable.UseVisualStyleBackColor = true;
            // 
            // btnAdministratorsDelete
            // 
            this.btnAdministratorsDelete.Image = global::vCardPlatform.Properties.Resources.delete;
            this.btnAdministratorsDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdministratorsDelete.Location = new System.Drawing.Point(745, 6);
            this.btnAdministratorsDelete.Name = "btnAdministratorsDelete";
            this.btnAdministratorsDelete.Size = new System.Drawing.Size(75, 23);
            this.btnAdministratorsDelete.TabIndex = 22;
            this.btnAdministratorsDelete.Text = "Delete";
            this.btnAdministratorsDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAdministratorsDelete.UseVisualStyleBackColor = true;
            // 
            // btnAdministratorsRefresh
            // 
            this.btnAdministratorsRefresh.Image = global::vCardPlatform.Properties.Resources.refresh;
            this.btnAdministratorsRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdministratorsRefresh.Location = new System.Drawing.Point(826, 6);
            this.btnAdministratorsRefresh.Name = "btnAdministratorsRefresh";
            this.btnAdministratorsRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnAdministratorsRefresh.TabIndex = 21;
            this.btnAdministratorsRefresh.Text = "Refresh";
            this.btnAdministratorsRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAdministratorsRefresh.UseVisualStyleBackColor = true;
            this.btnAdministratorsRefresh.Click += new System.EventHandler(this.btnAdministratorsRefresh_Click);
            // 
            // dataGridViewAdministrators
            // 
            this.dataGridViewAdministrators.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewAdministrators.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAdministrators.Location = new System.Drawing.Point(6, 35);
            this.dataGridViewAdministrators.Name = "dataGridViewAdministrators";
            this.dataGridViewAdministrators.RowHeadersWidth = 45;
            this.dataGridViewAdministrators.Size = new System.Drawing.Size(910, 392);
            this.dataGridViewAdministrators.TabIndex = 20;
            // 
            // tabCreate
            // 
            this.tabCreate.Controls.Add(this.label1);
            this.tabCreate.Controls.Add(this.label2);
            this.tabCreate.Controls.Add(this.label3);
            this.tabCreate.Controls.Add(this.txtName);
            this.tabCreate.Controls.Add(this.txtPassword);
            this.tabCreate.Controls.Add(this.txtEmail);
            this.tabCreate.Controls.Add(this.button1);
            this.tabCreate.Location = new System.Drawing.Point(4, 22);
            this.tabCreate.Name = "tabCreate";
            this.tabCreate.Padding = new System.Windows.Forms.Padding(3);
            this.tabCreate.Size = new System.Drawing.Size(919, 430);
            this.tabCreate.TabIndex = 1;
            this.tabCreate.Text = "Create";
            this.tabCreate.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(251, 217);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 15);
            this.label1.TabIndex = 26;
            this.label1.Text = "Password:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(251, 178);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 15);
            this.label2.TabIndex = 25;
            this.label2.Text = "E-mail:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(251, 141);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 15);
            this.label3.TabIndex = 24;
            this.label3.Text = "Name:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(317, 138);
            this.txtName.Margin = new System.Windows.Forms.Padding(2);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(350, 20);
            this.txtName.TabIndex = 21;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(317, 214);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(2);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(350, 20);
            this.txtPassword.TabIndex = 23;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(317, 174);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(2);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(350, 20);
            this.txtEmail.TabIndex = 22;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(422, 269);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(61, 23);
            this.button1.TabIndex = 20;
            this.button1.Text = "Create";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // tabOperations
            // 
            this.tabOperations.Location = new System.Drawing.Point(4, 22);
            this.tabOperations.Name = "tabOperations";
            this.tabOperations.Size = new System.Drawing.Size(938, 473);
            this.tabOperations.TabIndex = 3;
            this.tabOperations.Text = "Operations";
            this.tabOperations.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.statusProgressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 504);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(944, 24);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(79, 19);
            this.lblStatus.Text = "StatusLabel";
            // 
            // statusProgressBar
            // 
            this.statusProgressBar.Name = "statusProgressBar";
            this.statusProgressBar.Size = new System.Drawing.Size(100, 18);
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.Image = ((System.Drawing.Image)(resources.GetObject("btnLogout.Image")));
            this.btnLogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.Location = new System.Drawing.Point(873, 503);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(2);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(69, 25);
            this.btnLogout.TabIndex = 6;
            this.btnLogout.Text = "Logout";
            this.btnLogout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLogout.UseVisualStyleBackColor = true;
            // 
            // FormMainApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 528);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabCMain);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormMainApplication";
            this.Text = "MainApplication";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMainApplication_FormClosing);
            this.Load += new System.EventHandler(this.FormMainApplication_Load);
            this.tabCMain.ResumeLayout(false);
            this.tabProfile.ResumeLayout(false);
            this.tabProfile.PerformLayout();
            this.tabEntities.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEntities)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabAdmistrators.ResumeLayout(false);
            this.tabCAdministrators.ResumeLayout(false);
            this.tabTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAdministrators)).EndInit();
            this.tabCreate.ResumeLayout(false);
            this.tabCreate.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelAdministratorName;
        private System.Windows.Forms.Button buttonChangePassword;
        private System.Windows.Forms.TabControl tabCMain;
        private System.Windows.Forms.TabPage tabProfile;
        private System.Windows.Forms.TabPage tabEntities;
        private System.Windows.Forms.Panel panelProfile;
        private System.Windows.Forms.TabPage tabAdmistrators;
        private System.Windows.Forms.TabPage tabOperations;
        private System.Windows.Forms.TabControl tabCAdministrators;
        private System.Windows.Forms.TabPage tabTable;
        private System.Windows.Forms.TabPage tabCreate;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripProgressBar statusProgressBar;
        private System.Windows.Forms.Button btnAdministratorsRefresh;
        private System.Windows.Forms.DataGridView dataGridViewAdministrators;
        private System.Windows.Forms.Button btnAdministratorsDelete;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnEntitiesBlock;
        private System.Windows.Forms.Button btnEntitiesDelete;
        private System.Windows.Forms.Button btnEntitiesRefresh;
        private System.Windows.Forms.DataGridView dataGridViewEntities;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Button button1;
    }
}