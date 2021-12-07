
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
            this.btnEntitiesCreate = new System.Windows.Forms.Button();
            this.btnEntitiesDelete = new System.Windows.Forms.Button();
            this.btnEntitiesRefresh = new System.Windows.Forms.Button();
            this.dataGridViewEntities = new System.Windows.Forms.DataGridView();
            this.tabEntity = new System.Windows.Forms.TabPage();
            this.btnEntitySave = new System.Windows.Forms.Button();
            this.groupDataEntity = new System.Windows.Forms.GroupBox();
            this.lblEntityID = new System.Windows.Forms.Label();
            this.txtEntityId = new System.Windows.Forms.TextBox();
            this.numEntityMaxLimit = new System.Windows.Forms.NumericUpDown();
            this.btnTestEndpoint = new System.Windows.Forms.Button();
            this.lblEntityMaxLimit = new System.Windows.Forms.Label();
            this.lblEntityEndpoint = new System.Windows.Forms.Label();
            this.txtEntityEndpoint = new System.Windows.Forms.TextBox();
            this.lblEntityName = new System.Windows.Forms.Label();
            this.txtEntityName = new System.Windows.Forms.TextBox();
            this.groupEntityDefaultCategory = new System.Windows.Forms.GroupBox();
            this.btnEntityDCRemoveRow = new System.Windows.Forms.Button();
            this.dataGridViewEntityDefaultCategory = new System.Windows.Forms.DataGridView();
            this.groupEntityStatus = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblEntityStatusName = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
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
            this.btnEntityDefaultCategoriesRefresh = new System.Windows.Forms.Button();
            this.tabCMain.SuspendLayout();
            this.tabProfile.SuspendLayout();
            this.tabEntities.SuspendLayout();
            this.tabCEntities.SuspendLayout();
            this.tabEntityTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEntities)).BeginInit();
            this.tabEntity.SuspendLayout();
            this.groupDataEntity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEntityMaxLimit)).BeginInit();
            this.groupEntityDefaultCategory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEntityDefaultCategory)).BeginInit();
            this.groupEntityStatus.SuspendLayout();
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
            this.tabCEntities.Controls.Add(this.tabEntity);
            this.tabCEntities.Location = new System.Drawing.Point(6, 6);
            this.tabCEntities.Name = "tabCEntities";
            this.tabCEntities.SelectedIndex = 0;
            this.tabCEntities.Size = new System.Drawing.Size(927, 456);
            this.tabCEntities.TabIndex = 30;
            this.tabCEntities.SelectedIndexChanged += new System.EventHandler(this.tabCEntities_SelectedIndexChanged);
            // 
            // tabEntityTable
            // 
            this.tabEntityTable.Controls.Add(this.btnEntitiesCreate);
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
            // btnEntitiesCreate
            // 
            this.btnEntitiesCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEntitiesCreate.Image = global::vCardPlatform.Properties.Resources.add;
            this.btnEntitiesCreate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEntitiesCreate.Location = new System.Drawing.Point(664, 6);
            this.btnEntitiesCreate.Name = "btnEntitiesCreate";
            this.btnEntitiesCreate.Size = new System.Drawing.Size(75, 23);
            this.btnEntitiesCreate.TabIndex = 23;
            this.btnEntitiesCreate.Text = "Create";
            this.btnEntitiesCreate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEntitiesCreate.UseVisualStyleBackColor = true;
            this.btnEntitiesCreate.Click += new System.EventHandler(this.btnEntitiesCreate_Click);
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
            this.btnEntitiesDelete.Click += new System.EventHandler(this.btnEntitiesDelete_Click);
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
            // 
            // tabEntity
            // 
            this.tabEntity.Controls.Add(this.btnEntitySave);
            this.tabEntity.Controls.Add(this.groupDataEntity);
            this.tabEntity.Controls.Add(this.groupEntityDefaultCategory);
            this.tabEntity.Controls.Add(this.groupEntityStatus);
            this.tabEntity.Location = new System.Drawing.Point(4, 22);
            this.tabEntity.Name = "tabEntity";
            this.tabEntity.Size = new System.Drawing.Size(919, 430);
            this.tabEntity.TabIndex = 2;
            this.tabEntity.Text = "Entity";
            this.tabEntity.UseVisualStyleBackColor = true;
            // 
            // btnEntitySave
            // 
            this.btnEntitySave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEntitySave.Enabled = false;
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
            // groupDataEntity
            // 
            this.groupDataEntity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupDataEntity.Controls.Add(this.lblEntityID);
            this.groupDataEntity.Controls.Add(this.txtEntityId);
            this.groupDataEntity.Controls.Add(this.numEntityMaxLimit);
            this.groupDataEntity.Controls.Add(this.btnTestEndpoint);
            this.groupDataEntity.Controls.Add(this.lblEntityMaxLimit);
            this.groupDataEntity.Controls.Add(this.lblEntityEndpoint);
            this.groupDataEntity.Controls.Add(this.txtEntityEndpoint);
            this.groupDataEntity.Controls.Add(this.lblEntityName);
            this.groupDataEntity.Controls.Add(this.txtEntityName);
            this.groupDataEntity.Enabled = false;
            this.groupDataEntity.Location = new System.Drawing.Point(3, 32);
            this.groupDataEntity.Name = "groupDataEntity";
            this.groupDataEntity.Size = new System.Drawing.Size(304, 392);
            this.groupDataEntity.TabIndex = 2;
            this.groupDataEntity.TabStop = false;
            this.groupDataEntity.Text = "Data";
            // 
            // lblEntityID
            // 
            this.lblEntityID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEntityID.AutoSize = true;
            this.lblEntityID.Location = new System.Drawing.Point(52, 96);
            this.lblEntityID.Name = "lblEntityID";
            this.lblEntityID.Size = new System.Drawing.Size(17, 15);
            this.lblEntityID.TabIndex = 9;
            this.lblEntityID.Text = "Id";
            // 
            // txtEntityId
            // 
            this.txtEntityId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEntityId.Enabled = false;
            this.txtEntityId.Location = new System.Drawing.Point(55, 114);
            this.txtEntityId.Name = "txtEntityId";
            this.txtEntityId.Size = new System.Drawing.Size(210, 20);
            this.txtEntityId.TabIndex = 8;
            // 
            // numEntityMaxLimit
            // 
            this.numEntityMaxLimit.DecimalPlaces = 2;
            this.numEntityMaxLimit.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numEntityMaxLimit.Location = new System.Drawing.Point(55, 255);
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
            this.btnTestEndpoint.Location = new System.Drawing.Point(242, 203);
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
            this.lblEntityMaxLimit.Location = new System.Drawing.Point(52, 237);
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
            this.lblEntityEndpoint.Location = new System.Drawing.Point(52, 186);
            this.lblEntityEndpoint.Name = "lblEntityEndpoint";
            this.lblEntityEndpoint.Size = new System.Drawing.Size(56, 15);
            this.lblEntityEndpoint.TabIndex = 3;
            this.lblEntityEndpoint.Text = "Endpoint";
            // 
            // txtEntityEndpoint
            // 
            this.txtEntityEndpoint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEntityEndpoint.Location = new System.Drawing.Point(55, 204);
            this.txtEntityEndpoint.Name = "txtEntityEndpoint";
            this.txtEntityEndpoint.Size = new System.Drawing.Size(181, 20);
            this.txtEntityEndpoint.TabIndex = 2;
            // 
            // lblEntityName
            // 
            this.lblEntityName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEntityName.AutoSize = true;
            this.lblEntityName.Location = new System.Drawing.Point(52, 134);
            this.lblEntityName.Name = "lblEntityName";
            this.lblEntityName.Size = new System.Drawing.Size(41, 15);
            this.lblEntityName.TabIndex = 1;
            this.lblEntityName.Text = "Name";
            // 
            // txtEntityName
            // 
            this.txtEntityName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEntityName.Location = new System.Drawing.Point(55, 152);
            this.txtEntityName.Name = "txtEntityName";
            this.txtEntityName.Size = new System.Drawing.Size(210, 20);
            this.txtEntityName.TabIndex = 0;
            // 
            // groupEntityDefaultCategory
            // 
            this.groupEntityDefaultCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupEntityDefaultCategory.Controls.Add(this.btnEntityDefaultCategoriesRefresh);
            this.groupEntityDefaultCategory.Controls.Add(this.btnEntityDCRemoveRow);
            this.groupEntityDefaultCategory.Controls.Add(this.dataGridViewEntityDefaultCategory);
            this.groupEntityDefaultCategory.Enabled = false;
            this.groupEntityDefaultCategory.Location = new System.Drawing.Point(623, 32);
            this.groupEntityDefaultCategory.Name = "groupEntityDefaultCategory";
            this.groupEntityDefaultCategory.Size = new System.Drawing.Size(304, 392);
            this.groupEntityDefaultCategory.TabIndex = 1;
            this.groupEntityDefaultCategory.TabStop = false;
            this.groupEntityDefaultCategory.Text = "Default Categories";
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
            this.dataGridViewEntityDefaultCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewEntityDefaultCategory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEntityDefaultCategory.Location = new System.Drawing.Point(6, 47);
            this.dataGridViewEntityDefaultCategory.Name = "dataGridViewEntityDefaultCategory";
            this.dataGridViewEntityDefaultCategory.RowHeadersWidth = 45;
            this.dataGridViewEntityDefaultCategory.Size = new System.Drawing.Size(287, 339);
            this.dataGridViewEntityDefaultCategory.TabIndex = 0;
            // 
            // groupEntityStatus
            // 
            this.groupEntityStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupEntityStatus.Controls.Add(this.label6);
            this.groupEntityStatus.Controls.Add(this.label5);
            this.groupEntityStatus.Controls.Add(this.lblEntityStatusName);
            this.groupEntityStatus.Controls.Add(this.label4);
            this.groupEntityStatus.Location = new System.Drawing.Point(313, 32);
            this.groupEntityStatus.Name = "groupEntityStatus";
            this.groupEntityStatus.Size = new System.Drawing.Size(304, 392);
            this.groupEntityStatus.TabIndex = 0;
            this.groupEntityStatus.TabStop = false;
            this.groupEntityStatus.Text = "Status";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 155);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 15);
            this.label6.TabIndex = 5;
            this.label6.Text = "Resources: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 15);
            this.label5.TabIndex = 3;
            this.label5.Text = "Response: ";
            // 
            // lblEntityStatusName
            // 
            this.lblEntityStatusName.AutoSize = true;
            this.lblEntityStatusName.Location = new System.Drawing.Point(99, 96);
            this.lblEntityStatusName.Name = "lblEntityStatusName";
            this.lblEntityStatusName.Size = new System.Drawing.Size(0, 15);
            this.lblEntityStatusName.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(51, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 15);
            this.label4.TabIndex = 1;
            this.label4.Text = "Entity: ";
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
            // btnEntityDefaultCategoriesRefresh
            // 
            this.btnEntityDefaultCategoriesRefresh.Image = global::vCardPlatform.Properties.Resources.refresh;
            this.btnEntityDefaultCategoriesRefresh.Location = new System.Drawing.Point(38, 18);
            this.btnEntityDefaultCategoriesRefresh.Name = "btnEntityDefaultCategoriesRefresh";
            this.btnEntityDefaultCategoriesRefresh.Size = new System.Drawing.Size(26, 23);
            this.btnEntityDefaultCategoriesRefresh.TabIndex = 8;
            this.btnEntityDefaultCategoriesRefresh.UseVisualStyleBackColor = true;
            this.btnEntityDefaultCategoriesRefresh.Click += new System.EventHandler(this.btnEntityDefaultCategoriesRefresh_Click);
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
            this.tabEntity.ResumeLayout(false);
            this.groupDataEntity.ResumeLayout(false);
            this.groupDataEntity.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEntityMaxLimit)).EndInit();
            this.groupEntityDefaultCategory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEntityDefaultCategory)).EndInit();
            this.groupEntityStatus.ResumeLayout(false);
            this.groupEntityStatus.PerformLayout();
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
        private System.Windows.Forms.Button btnEntitiesCreate;
        private System.Windows.Forms.Button btnEntitiesDelete;
        private System.Windows.Forms.Button btnEntitiesRefresh;
        private System.Windows.Forms.DataGridView dataGridViewEntities;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAdministratorName;
        private System.Windows.Forms.TextBox txtAdministratorPassword;
        private System.Windows.Forms.TextBox txtAdministratorEmail;
        private System.Windows.Forms.Button btnCreateAdmin;
        private System.Windows.Forms.TabPage tabEntity;
        private System.Windows.Forms.GroupBox groupDataEntity;
        private System.Windows.Forms.TextBox txtEntityName;
        private System.Windows.Forms.GroupBox groupEntityDefaultCategory;
        private System.Windows.Forms.GroupBox groupEntityStatus;
        private System.Windows.Forms.Button btnEntitySave;
        private System.Windows.Forms.Label lblEntityMaxLimit;
        private System.Windows.Forms.Label lblEntityEndpoint;
        private System.Windows.Forms.TextBox txtEntityEndpoint;
        private System.Windows.Forms.Label lblEntityName;
        private System.Windows.Forms.Button btnTestEndpoint;
        private System.Windows.Forms.DataGridView dataGridViewEntityDefaultCategory;
        private System.Windows.Forms.NumericUpDown numEntityMaxLimit;
        private System.Windows.Forms.Button btnEntityDCRemoveRow;
        private System.Windows.Forms.Label lblEntityID;
        private System.Windows.Forms.TextBox txtEntityId;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblEntityStatusName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnEntityDefaultCategoriesRefresh;
    }
}