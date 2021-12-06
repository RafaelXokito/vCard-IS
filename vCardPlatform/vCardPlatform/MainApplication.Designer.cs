
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
            this.tabCEntities = new System.Windows.Forms.TabControl();
            this.tabEntityTable = new System.Windows.Forms.TabPage();
            this.btnEntitiesBlock = new System.Windows.Forms.Button();
            this.btnEntitiesDelete = new System.Windows.Forms.Button();
            this.btnEntitiesRefresh = new System.Windows.Forms.Button();
            this.dataGridViewEntities = new System.Windows.Forms.DataGridView();
            this.tabEntityCreate = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.tabEntity = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnEntitySave = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.numEntityMaxLimit = new System.Windows.Forms.NumericUpDown();
            this.btnTestEndpoint = new System.Windows.Forms.Button();
            this.lblEntityMaxLimit = new System.Windows.Forms.Label();
            this.lblEntityEndpoint = new System.Windows.Forms.Label();
            this.txtEntityEndpoint = new System.Windows.Forms.TextBox();
            this.lblEntityName = new System.Windows.Forms.Label();
            this.txtEntityName = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnEntityDCRemoveRow = new System.Windows.Forms.Button();
            this.dataGridViewEntityDefaultCategory = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
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
            this.txtAdministratorName = new System.Windows.Forms.TextBox();
            this.txtAdministratorPassword = new System.Windows.Forms.TextBox();
            this.txtAdministratorEmail = new System.Windows.Forms.TextBox();
            this.btnCreateAdmin = new System.Windows.Forms.Button();
            this.tabOperations = new System.Windows.Forms.TabPage();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.btnLogout = new System.Windows.Forms.Button();
            this.tabCMain.SuspendLayout();
            this.tabProfile.SuspendLayout();
            this.tabEntities.SuspendLayout();
            this.tabCEntities.SuspendLayout();
            this.tabEntityTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEntities)).BeginInit();
            this.tabEntityCreate.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabEntity.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEntityMaxLimit)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEntityDefaultCategory)).BeginInit();
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
            this.tabEntities.Controls.Add(this.tabCEntities);
            this.tabEntities.Location = new System.Drawing.Point(4, 22);
            this.tabEntities.Name = "tabEntities";
            this.tabEntities.Padding = new System.Windows.Forms.Padding(3);
            this.tabEntities.Size = new System.Drawing.Size(938, 473);
            this.tabEntities.TabIndex = 1;
            this.tabEntities.Text = "Entities";
            this.tabEntities.UseVisualStyleBackColor = true;
            // 
            // tabCEntities
            // 
            this.tabCEntities.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabCEntities.Controls.Add(this.tabEntityTable);
            this.tabCEntities.Controls.Add(this.tabEntityCreate);
            this.tabCEntities.Controls.Add(this.tabEntity);
            this.tabCEntities.Location = new System.Drawing.Point(6, 8);
            this.tabCEntities.Name = "tabCEntities";
            this.tabCEntities.SelectedIndex = 0;
            this.tabCEntities.Size = new System.Drawing.Size(927, 456);
            this.tabCEntities.TabIndex = 30;
            // 
            // tabEntityTable
            // 
            this.tabEntityTable.Controls.Add(this.btnEntitiesBlock);
            this.tabEntityTable.Controls.Add(this.btnEntitiesDelete);
            this.tabEntityTable.Controls.Add(this.btnEntitiesRefresh);
            this.tabEntityTable.Controls.Add(this.dataGridViewEntities);
            this.tabEntityTable.Location = new System.Drawing.Point(4, 22);
            this.tabEntityTable.Name = "tabEntityTable";
            this.tabEntityTable.Padding = new System.Windows.Forms.Padding(3);
            this.tabEntityTable.Size = new System.Drawing.Size(919, 430);
            this.tabEntityTable.TabIndex = 0;
            this.tabEntityTable.Text = "Table";
            this.tabEntityTable.UseVisualStyleBackColor = true;
            // 
            // btnEntitiesBlock
            // 
            this.btnEntitiesBlock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.btnEntitiesDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.btnEntitiesRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.dataGridViewEntities.ReadOnly = true;
            this.dataGridViewEntities.RowHeadersWidth = 45;
            this.dataGridViewEntities.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewEntities.Size = new System.Drawing.Size(910, 392);
            this.dataGridViewEntities.TabIndex = 20;
            this.dataGridViewEntities.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewEntities_CellDoubleClick);
            this.dataGridViewEntities.DoubleClick += new System.EventHandler(this.dataGridViewEntities_DoubleClick);
            // 
            // tabEntityCreate
            // 
            this.tabEntityCreate.Controls.Add(this.groupBox2);
            this.tabEntityCreate.Location = new System.Drawing.Point(4, 22);
            this.tabEntityCreate.Name = "tabEntityCreate";
            this.tabEntityCreate.Padding = new System.Windows.Forms.Padding(3);
            this.tabEntityCreate.Size = new System.Drawing.Size(919, 430);
            this.tabEntityCreate.TabIndex = 1;
            this.tabEntityCreate.Text = "Create";
            this.tabEntityCreate.UseVisualStyleBackColor = true;
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
            // tabEntity
            // 
            this.tabEntity.Controls.Add(this.button3);
            this.tabEntity.Controls.Add(this.button2);
            this.tabEntity.Controls.Add(this.btnEntitySave);
            this.tabEntity.Controls.Add(this.groupBox4);
            this.tabEntity.Controls.Add(this.groupBox3);
            this.tabEntity.Controls.Add(this.groupBox1);
            this.tabEntity.Location = new System.Drawing.Point(4, 22);
            this.tabEntity.Name = "tabEntity";
            this.tabEntity.Size = new System.Drawing.Size(919, 430);
            this.tabEntity.TabIndex = 2;
            this.tabEntity.Text = "Entity";
            this.tabEntity.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(679, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(760, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btnEntitySave
            // 
            this.btnEntitySave.Image = global::vCardPlatform.Properties.Resources.save;
            this.btnEntitySave.Location = new System.Drawing.Point(841, 3);
            this.btnEntitySave.Name = "btnEntitySave";
            this.btnEntitySave.Size = new System.Drawing.Size(75, 23);
            this.btnEntitySave.TabIndex = 3;
            this.btnEntitySave.Text = "Save";
            this.btnEntitySave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEntitySave.UseVisualStyleBackColor = true;
            this.btnEntitySave.Click += new System.EventHandler(this.btnEntitySave_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox4.Controls.Add(this.numEntityMaxLimit);
            this.groupBox4.Controls.Add(this.btnTestEndpoint);
            this.groupBox4.Controls.Add(this.lblEntityMaxLimit);
            this.groupBox4.Controls.Add(this.lblEntityEndpoint);
            this.groupBox4.Controls.Add(this.txtEntityEndpoint);
            this.groupBox4.Controls.Add(this.lblEntityName);
            this.groupBox4.Controls.Add(this.txtEntityName);
            this.groupBox4.Location = new System.Drawing.Point(315, 32);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(304, 392);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Data";
            // 
            // numEntityMaxLimit
            // 
            this.numEntityMaxLimit.DecimalPlaces = 2;
            this.numEntityMaxLimit.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numEntityMaxLimit.Location = new System.Drawing.Point(48, 188);
            this.numEntityMaxLimit.Maximum = new decimal(new int[] {
            -559939585,
            902409669,
            54,
            0});
            this.numEntityMaxLimit.Name = "numEntityMaxLimit";
            this.numEntityMaxLimit.Size = new System.Drawing.Size(210, 20);
            this.numEntityMaxLimit.TabIndex = 7;
            // 
            // btnTestEndpoint
            // 
            this.btnTestEndpoint.Image = global::vCardPlatform.Properties.Resources.connect;
            this.btnTestEndpoint.Location = new System.Drawing.Point(235, 136);
            this.btnTestEndpoint.Name = "btnTestEndpoint";
            this.btnTestEndpoint.Size = new System.Drawing.Size(23, 23);
            this.btnTestEndpoint.TabIndex = 6;
            this.btnTestEndpoint.UseVisualStyleBackColor = true;
            this.btnTestEndpoint.Click += new System.EventHandler(this.btnTestEndpoint_Click);
            // 
            // lblEntityMaxLimit
            // 
            this.lblEntityMaxLimit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEntityMaxLimit.AutoSize = true;
            this.lblEntityMaxLimit.Location = new System.Drawing.Point(45, 170);
            this.lblEntityMaxLimit.Name = "lblEntityMaxLimit";
            this.lblEntityMaxLimit.Size = new System.Drawing.Size(61, 15);
            this.lblEntityMaxLimit.TabIndex = 5;
            this.lblEntityMaxLimit.Text = "Max Limit";
            // 
            // lblEntityEndpoint
            // 
            this.lblEntityEndpoint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEntityEndpoint.AutoSize = true;
            this.lblEntityEndpoint.Location = new System.Drawing.Point(45, 119);
            this.lblEntityEndpoint.Name = "lblEntityEndpoint";
            this.lblEntityEndpoint.Size = new System.Drawing.Size(56, 15);
            this.lblEntityEndpoint.TabIndex = 3;
            this.lblEntityEndpoint.Text = "Endpoint";
            // 
            // txtEntityEndpoint
            // 
            this.txtEntityEndpoint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEntityEndpoint.Location = new System.Drawing.Point(48, 137);
            this.txtEntityEndpoint.Name = "txtEntityEndpoint";
            this.txtEntityEndpoint.Size = new System.Drawing.Size(181, 20);
            this.txtEntityEndpoint.TabIndex = 2;
            // 
            // lblEntityName
            // 
            this.lblEntityName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEntityName.AutoSize = true;
            this.lblEntityName.Location = new System.Drawing.Point(45, 67);
            this.lblEntityName.Name = "lblEntityName";
            this.lblEntityName.Size = new System.Drawing.Size(41, 15);
            this.lblEntityName.TabIndex = 1;
            this.lblEntityName.Text = "Name";
            // 
            // txtEntityName
            // 
            this.txtEntityName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEntityName.Location = new System.Drawing.Point(48, 85);
            this.txtEntityName.Name = "txtEntityName";
            this.txtEntityName.Size = new System.Drawing.Size(210, 20);
            this.txtEntityName.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.btnEntityDCRemoveRow);
            this.groupBox3.Controls.Add(this.dataGridViewEntityDefaultCategory);
            this.groupBox3.Location = new System.Drawing.Point(623, 32);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(304, 392);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Default Categories";
            // 
            // btnEntityDCRemoveRow
            // 
            this.btnEntityDCRemoveRow.Image = global::vCardPlatform.Properties.Resources.delete;
            this.btnEntityDCRemoveRow.Location = new System.Drawing.Point(6, 18);
            this.btnEntityDCRemoveRow.Name = "btnEntityDCRemoveRow";
            this.btnEntityDCRemoveRow.Size = new System.Drawing.Size(26, 23);
            this.btnEntityDCRemoveRow.TabIndex = 7;
            this.btnEntityDCRemoveRow.UseVisualStyleBackColor = true;
            this.btnEntityDCRemoveRow.Click += new System.EventHandler(this.btnEntityDCRemoveRow_Click);
            // 
            // dataGridViewEntityDefaultCategory
            // 
            this.dataGridViewEntityDefaultCategory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEntityDefaultCategory.Location = new System.Drawing.Point(6, 47);
            this.dataGridViewEntityDefaultCategory.Name = "dataGridViewEntityDefaultCategory";
            this.dataGridViewEntityDefaultCategory.RowHeadersWidth = 45;
            this.dataGridViewEntityDefaultCategory.Size = new System.Drawing.Size(287, 339);
            this.dataGridViewEntityDefaultCategory.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Location = new System.Drawing.Point(5, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(304, 392);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Status";
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
            this.tabCAdministrators.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.btnAdministratorsDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdministratorsDelete.Image = global::vCardPlatform.Properties.Resources.delete;
            this.btnAdministratorsDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdministratorsDelete.Location = new System.Drawing.Point(745, 6);
            this.btnAdministratorsDelete.Name = "btnAdministratorsDelete";
            this.btnAdministratorsDelete.Size = new System.Drawing.Size(75, 23);
            this.btnAdministratorsDelete.TabIndex = 22;
            this.btnAdministratorsDelete.Text = "Delete";
            this.btnAdministratorsDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAdministratorsDelete.UseVisualStyleBackColor = true;
            this.btnAdministratorsDelete.Click += new System.EventHandler(this.btnAdministratorsDelete_Click);
            // 
            // btnAdministratorsRefresh
            // 
            this.btnAdministratorsRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.dataGridViewAdministrators.Size = new System.Drawing.Size(907, 392);
            this.dataGridViewAdministrators.TabIndex = 20;
            this.dataGridViewAdministrators.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewAdministrators_CellValueChanged);
            // 
            // tabCreate
            // 
            this.tabCreate.Controls.Add(this.label1);
            this.tabCreate.Controls.Add(this.label2);
            this.tabCreate.Controls.Add(this.label3);
            this.tabCreate.Controls.Add(this.txtAdministratorName);
            this.tabCreate.Controls.Add(this.txtAdministratorPassword);
            this.tabCreate.Controls.Add(this.txtAdministratorEmail);
            this.tabCreate.Controls.Add(this.btnCreateAdmin);
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
            // txtAdministratorName
            // 
            this.txtAdministratorName.Location = new System.Drawing.Point(317, 138);
            this.txtAdministratorName.Margin = new System.Windows.Forms.Padding(2);
            this.txtAdministratorName.Name = "txtAdministratorName";
            this.txtAdministratorName.Size = new System.Drawing.Size(350, 20);
            this.txtAdministratorName.TabIndex = 21;
            // 
            // txtAdministratorPassword
            // 
            this.txtAdministratorPassword.Location = new System.Drawing.Point(317, 214);
            this.txtAdministratorPassword.Margin = new System.Windows.Forms.Padding(2);
            this.txtAdministratorPassword.Name = "txtAdministratorPassword";
            this.txtAdministratorPassword.PasswordChar = '*';
            this.txtAdministratorPassword.Size = new System.Drawing.Size(350, 20);
            this.txtAdministratorPassword.TabIndex = 23;
            // 
            // txtAdministratorEmail
            // 
            this.txtAdministratorEmail.Location = new System.Drawing.Point(317, 174);
            this.txtAdministratorEmail.Margin = new System.Windows.Forms.Padding(2);
            this.txtAdministratorEmail.Name = "txtAdministratorEmail";
            this.txtAdministratorEmail.Size = new System.Drawing.Size(350, 20);
            this.txtAdministratorEmail.TabIndex = 22;
            // 
            // btnCreateAdmin
            // 
            this.btnCreateAdmin.Location = new System.Drawing.Point(422, 269);
            this.btnCreateAdmin.Margin = new System.Windows.Forms.Padding(2);
            this.btnCreateAdmin.Name = "btnCreateAdmin";
            this.btnCreateAdmin.Size = new System.Drawing.Size(61, 23);
            this.btnCreateAdmin.TabIndex = 20;
            this.btnCreateAdmin.Text = "Create";
            this.btnCreateAdmin.UseVisualStyleBackColor = true;
            this.btnCreateAdmin.Click += new System.EventHandler(this.btnCreateAdmin_Click);
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
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // FormMainApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 528);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabCMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormMainApplication";
            this.Text = "MainApplication";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMainApplication_FormClosing);
            this.Load += new System.EventHandler(this.FormMainApplication_Load);
            this.tabCMain.ResumeLayout(false);
            this.tabProfile.ResumeLayout(false);
            this.tabProfile.PerformLayout();
            this.tabEntities.ResumeLayout(false);
            this.tabCEntities.ResumeLayout(false);
            this.tabEntityTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEntities)).EndInit();
            this.tabEntityCreate.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabEntity.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEntityMaxLimit)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEntityDefaultCategory)).EndInit();
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
        private System.Windows.Forms.TabControl tabCEntities;
        private System.Windows.Forms.TabPage tabEntityTable;
        private System.Windows.Forms.Button btnEntitiesBlock;
        private System.Windows.Forms.Button btnEntitiesDelete;
        private System.Windows.Forms.Button btnEntitiesRefresh;
        private System.Windows.Forms.DataGridView dataGridViewEntities;
        private System.Windows.Forms.TabPage tabEntityCreate;
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
        private System.Windows.Forms.TextBox txtAdministratorName;
        private System.Windows.Forms.TextBox txtAdministratorPassword;
        private System.Windows.Forms.TextBox txtAdministratorEmail;
        private System.Windows.Forms.Button btnCreateAdmin;
        private System.Windows.Forms.TabPage tabEntity;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtEntityName;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnEntitySave;
        private System.Windows.Forms.Label lblEntityMaxLimit;
        private System.Windows.Forms.Label lblEntityEndpoint;
        private System.Windows.Forms.TextBox txtEntityEndpoint;
        private System.Windows.Forms.Label lblEntityName;
        private System.Windows.Forms.Button btnTestEndpoint;
        private System.Windows.Forms.DataGridView dataGridViewEntityDefaultCategory;
        private System.Windows.Forms.NumericUpDown numEntityMaxLimit;
        private System.Windows.Forms.Button btnEntityDCRemoveRow;
    }
}