namespace Tabster.Forms
{
    partial class PreferencesDialog
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
            this.chkUpdates = new System.Windows.Forms.CheckBox();
            this.cancelbtn = new System.Windows.Forms.Button();
            this.okbtn = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chkStripVersionedNames = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.printColorPreview = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.chkPrintTimestamp = new System.Windows.Forms.CheckBox();
            this.chkPrintPageNumbers = new System.Windows.Forms.CheckBox();
            this.tabPlugins = new System.Windows.Forms.TabPage();
            this.listPlugins = new System.Windows.Forms.ListView();
            this.colpluginName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colpluginEnabled = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblPluginFilename = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblPluginDescription = new System.Windows.Forms.Label();
            this.lblPluginHomepage = new System.Windows.Forms.LinkLabel();
            this.label7 = new System.Windows.Forms.Label();
            this.lblPluginAuthor = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblPluginVersion = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnEditSystemProxy = new System.Windows.Forms.Button();
            this.radioNoProxy = new System.Windows.Forms.RadioButton();
            this.radioManualProxy = new System.Windows.Forms.RadioButton();
            this.radioSystemProxy = new System.Windows.Forms.RadioButton();
            this.customProxyPanel = new System.Windows.Forms.Panel();
            this.chkShowProxyPassword = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtProxyUsername = new System.Windows.Forms.TextBox();
            this.chkProxyAuthentication = new System.Windows.Forms.CheckBox();
            this.txtProxyPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numProxyPort = new System.Windows.Forms.NumericUpDown();
            this.txtProxyAddress = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.listSearchEngines = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.lblSearchEngineSupportsRatings = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblSearchEngineHomepage = new System.Windows.Forms.LinkLabel();
            this.label15 = new System.Windows.Forms.Label();
            this.pluginsDirectorybtn = new System.Windows.Forms.Button();
            this.printColorDialog = new System.Windows.Forms.ColorDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPlugins.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.customProxyPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numProxyPort)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkUpdates
            // 
            this.chkUpdates.AutoSize = true;
            this.chkUpdates.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkUpdates.Location = new System.Drawing.Point(6, 6);
            this.chkUpdates.Name = "chkUpdates";
            this.chkUpdates.Size = new System.Drawing.Size(163, 17);
            this.chkUpdates.TabIndex = 18;
            this.chkUpdates.Text = "Check for updates on startup";
            this.chkUpdates.UseVisualStyleBackColor = true;
            // 
            // cancelbtn
            // 
            this.cancelbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelbtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelbtn.Location = new System.Drawing.Point(503, 251);
            this.cancelbtn.Name = "cancelbtn";
            this.cancelbtn.Size = new System.Drawing.Size(75, 23);
            this.cancelbtn.TabIndex = 22;
            this.cancelbtn.Text = "Cancel";
            this.cancelbtn.UseVisualStyleBackColor = true;
            // 
            // okbtn
            // 
            this.okbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okbtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okbtn.Location = new System.Drawing.Point(422, 251);
            this.okbtn.Name = "okbtn";
            this.okbtn.Size = new System.Drawing.Size(75, 23);
            this.okbtn.TabIndex = 23;
            this.okbtn.Text = "OK";
            this.okbtn.UseVisualStyleBackColor = true;
            this.okbtn.Click += new System.EventHandler(this.okbtn_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPlugins);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(13, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(563, 232);
            this.tabControl1.TabIndex = 24;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chkStripVersionedNames);
            this.tabPage1.Controls.Add(this.chkUpdates);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(555, 206);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // chkStripVersionedNames
            // 
            this.chkStripVersionedNames.AutoSize = true;
            this.chkStripVersionedNames.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkStripVersionedNames.Location = new System.Drawing.Point(6, 29);
            this.chkStripVersionedNames.Name = "chkStripVersionedNames";
            this.chkStripVersionedNames.Size = new System.Drawing.Size(271, 17);
            this.chkStripVersionedNames.TabIndex = 19;
            this.chkStripVersionedNames.Text = "Strip versioned text from saved tablature (ex: (ver 3))";
            this.chkStripVersionedNames.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.printColorPreview);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.chkPrintTimestamp);
            this.tabPage2.Controls.Add(this.chkPrintPageNumbers);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(555, 206);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "Printing";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // printColorPreview
            // 
            this.printColorPreview.BackColor = System.Drawing.Color.Black;
            this.printColorPreview.Location = new System.Drawing.Point(70, 55);
            this.printColorPreview.Name = "printColorPreview";
            this.printColorPreview.Size = new System.Drawing.Size(24, 16);
            this.printColorPreview.TabIndex = 24;
            this.printColorPreview.Click += new System.EventHandler(this.printColorPreview_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Print Color:";
            // 
            // chkPrintTimestamp
            // 
            this.chkPrintTimestamp.AutoSize = true;
            this.chkPrintTimestamp.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkPrintTimestamp.Location = new System.Drawing.Point(6, 29);
            this.chkPrintTimestamp.Name = "chkPrintTimestamp";
            this.chkPrintTimestamp.Size = new System.Drawing.Size(105, 17);
            this.chkPrintTimestamp.TabIndex = 20;
            this.chkPrintTimestamp.Text = "Display print time";
            this.chkPrintTimestamp.UseVisualStyleBackColor = true;
            // 
            // chkPrintPageNumbers
            // 
            this.chkPrintPageNumbers.AutoSize = true;
            this.chkPrintPageNumbers.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkPrintPageNumbers.Location = new System.Drawing.Point(6, 6);
            this.chkPrintPageNumbers.Name = "chkPrintPageNumbers";
            this.chkPrintPageNumbers.Size = new System.Drawing.Size(130, 17);
            this.chkPrintPageNumbers.TabIndex = 19;
            this.chkPrintPageNumbers.Text = "Display page numbers";
            this.chkPrintPageNumbers.UseVisualStyleBackColor = true;
            // 
            // tabPlugins
            // 
            this.tabPlugins.Controls.Add(this.listPlugins);
            this.tabPlugins.Controls.Add(this.groupBox1);
            this.tabPlugins.Location = new System.Drawing.Point(4, 22);
            this.tabPlugins.Name = "tabPlugins";
            this.tabPlugins.Padding = new System.Windows.Forms.Padding(3);
            this.tabPlugins.Size = new System.Drawing.Size(555, 206);
            this.tabPlugins.TabIndex = 1;
            this.tabPlugins.Text = "Plugins";
            this.tabPlugins.UseVisualStyleBackColor = true;
            // 
            // listPlugins
            // 
            this.listPlugins.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colpluginName,
            this.colpluginEnabled});
            this.listPlugins.Dock = System.Windows.Forms.DockStyle.Left;
            this.listPlugins.FullRowSelect = true;
            this.listPlugins.GridLines = true;
            this.listPlugins.Location = new System.Drawing.Point(3, 3);
            this.listPlugins.Name = "listPlugins";
            this.listPlugins.ShowItemToolTips = true;
            this.listPlugins.Size = new System.Drawing.Size(262, 200);
            this.listPlugins.TabIndex = 0;
            this.listPlugins.UseCompatibleStateImageBehavior = false;
            this.listPlugins.View = System.Windows.Forms.View.Details;
            this.listPlugins.SelectedIndexChanged += new System.EventHandler(this.listPlugins_SelectedIndexChanged);
            this.listPlugins.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listPlugins_MouseDoubleClick);
            // 
            // colpluginName
            // 
            this.colpluginName.Text = "Name";
            this.colpluginName.Width = 175;
            // 
            // colpluginEnabled
            // 
            this.colpluginEnabled.Text = "Enabled";
            this.colpluginEnabled.Width = 80;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblPluginFilename);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.lblPluginDescription);
            this.groupBox1.Controls.Add(this.lblPluginHomepage);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.lblPluginAuthor);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lblPluginVersion);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(271, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(278, 194);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Plugin Details";
            // 
            // lblPluginFilename
            // 
            this.lblPluginFilename.AutoSize = true;
            this.lblPluginFilename.Location = new System.Drawing.Point(74, 19);
            this.lblPluginFilename.Name = "lblPluginFilename";
            this.lblPluginFilename.Size = new System.Drawing.Size(88, 13);
            this.lblPluginFilename.TabIndex = 15;
            this.lblPluginFilename.Text = "lblPluginFilename";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "Filename:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 114);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Description:";
            // 
            // lblPluginDescription
            // 
            this.lblPluginDescription.AutoSize = true;
            this.lblPluginDescription.Location = new System.Drawing.Point(6, 141);
            this.lblPluginDescription.MaximumSize = new System.Drawing.Size(266, 0);
            this.lblPluginDescription.Name = "lblPluginDescription";
            this.lblPluginDescription.Size = new System.Drawing.Size(99, 13);
            this.lblPluginDescription.TabIndex = 12;
            this.lblPluginDescription.Text = "lblPluginDescription";
            // 
            // lblPluginHomepage
            // 
            this.lblPluginHomepage.ActiveLinkColor = System.Drawing.Color.Blue;
            this.lblPluginHomepage.AutoSize = true;
            this.lblPluginHomepage.Location = new System.Drawing.Point(74, 88);
            this.lblPluginHomepage.Name = "lblPluginHomepage";
            this.lblPluginHomepage.Size = new System.Drawing.Size(55, 13);
            this.lblPluginHomepage.TabIndex = 11;
            this.lblPluginHomepage.TabStop = true;
            this.lblPluginHomepage.Text = "linkLabel1";
            this.lblPluginHomepage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel_LinkClicked);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 88);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Homepage:";
            // 
            // lblPluginAuthor
            // 
            this.lblPluginAuthor.AutoSize = true;
            this.lblPluginAuthor.Location = new System.Drawing.Point(74, 65);
            this.lblPluginAuthor.Name = "lblPluginAuthor";
            this.lblPluginAuthor.Size = new System.Drawing.Size(77, 13);
            this.lblPluginAuthor.TabIndex = 8;
            this.lblPluginAuthor.Text = "lblPluginAuthor";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 65);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Author:";
            // 
            // lblPluginVersion
            // 
            this.lblPluginVersion.AutoSize = true;
            this.lblPluginVersion.Location = new System.Drawing.Point(74, 42);
            this.lblPluginVersion.Name = "lblPluginVersion";
            this.lblPluginVersion.Size = new System.Drawing.Size(81, 13);
            this.lblPluginVersion.TabIndex = 6;
            this.lblPluginVersion.Text = "lblPluginVersion";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Version: ";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnEditSystemProxy);
            this.tabPage3.Controls.Add(this.radioNoProxy);
            this.tabPage3.Controls.Add(this.radioManualProxy);
            this.tabPage3.Controls.Add(this.radioSystemProxy);
            this.tabPage3.Controls.Add(this.customProxyPanel);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(555, 206);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Network";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnEditSystemProxy
            // 
            this.btnEditSystemProxy.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnEditSystemProxy.Location = new System.Drawing.Point(172, 26);
            this.btnEditSystemProxy.Name = "btnEditSystemProxy";
            this.btnEditSystemProxy.Size = new System.Drawing.Size(149, 23);
            this.btnEditSystemProxy.TabIndex = 25;
            this.btnEditSystemProxy.Text = "Edit System Proxy Settings";
            this.btnEditSystemProxy.UseVisualStyleBackColor = true;
            this.btnEditSystemProxy.Click += new System.EventHandler(this.btnEditSystemProxy_Click);
            // 
            // radioNoProxy
            // 
            this.radioNoProxy.AutoSize = true;
            this.radioNoProxy.Checked = true;
            this.radioNoProxy.Location = new System.Drawing.Point(6, 6);
            this.radioNoProxy.Name = "radioNoProxy";
            this.radioNoProxy.Size = new System.Drawing.Size(139, 17);
            this.radioNoProxy.TabIndex = 1;
            this.radioNoProxy.TabStop = true;
            this.radioNoProxy.Text = "Don\'t use a proxy server";
            this.radioNoProxy.UseVisualStyleBackColor = true;
            // 
            // radioManualProxy
            // 
            this.radioManualProxy.AutoSize = true;
            this.radioManualProxy.Location = new System.Drawing.Point(6, 52);
            this.radioManualProxy.Name = "radioManualProxy";
            this.radioManualProxy.Size = new System.Drawing.Size(127, 17);
            this.radioManualProxy.TabIndex = 3;
            this.radioManualProxy.TabStop = true;
            this.radioManualProxy.Text = "Custom proxy settings";
            this.radioManualProxy.UseVisualStyleBackColor = true;
            this.radioManualProxy.CheckedChanged += new System.EventHandler(this.radioCustomProxy_CheckedChanged);
            // 
            // radioSystemProxy
            // 
            this.radioSystemProxy.AutoSize = true;
            this.radioSystemProxy.Location = new System.Drawing.Point(6, 29);
            this.radioSystemProxy.Name = "radioSystemProxy";
            this.radioSystemProxy.Size = new System.Drawing.Size(146, 17);
            this.radioSystemProxy.TabIndex = 2;
            this.radioSystemProxy.TabStop = true;
            this.radioSystemProxy.Text = "Use system proxy settings";
            this.radioSystemProxy.UseVisualStyleBackColor = true;
            // 
            // customProxyPanel
            // 
            this.customProxyPanel.Controls.Add(this.chkShowProxyPassword);
            this.customProxyPanel.Controls.Add(this.label1);
            this.customProxyPanel.Controls.Add(this.label3);
            this.customProxyPanel.Controls.Add(this.label2);
            this.customProxyPanel.Controls.Add(this.txtProxyUsername);
            this.customProxyPanel.Controls.Add(this.chkProxyAuthentication);
            this.customProxyPanel.Controls.Add(this.txtProxyPassword);
            this.customProxyPanel.Controls.Add(this.label4);
            this.customProxyPanel.Controls.Add(this.numProxyPort);
            this.customProxyPanel.Controls.Add(this.txtProxyAddress);
            this.customProxyPanel.Enabled = false;
            this.customProxyPanel.Location = new System.Drawing.Point(6, 75);
            this.customProxyPanel.Name = "customProxyPanel";
            this.customProxyPanel.Size = new System.Drawing.Size(354, 125);
            this.customProxyPanel.TabIndex = 9;
            // 
            // chkShowProxyPassword
            // 
            this.chkShowProxyPassword.AutoSize = true;
            this.chkShowProxyPassword.Enabled = false;
            this.chkShowProxyPassword.Location = new System.Drawing.Point(218, 82);
            this.chkShowProxyPassword.Name = "chkShowProxyPassword";
            this.chkShowProxyPassword.Size = new System.Drawing.Size(101, 17);
            this.chkShowProxyPassword.TabIndex = 9;
            this.chkShowProxyPassword.Text = "Show password";
            this.chkShowProxyPassword.UseVisualStyleBackColor = true;
            this.chkShowProxyPassword.CheckedChanged += new System.EventHandler(this.chkShowProxyPassword_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Address:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Username:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(208, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port:";
            // 
            // txtProxyUsername
            // 
            this.txtProxyUsername.Enabled = false;
            this.txtProxyUsername.Location = new System.Drawing.Point(96, 54);
            this.txtProxyUsername.Name = "txtProxyUsername";
            this.txtProxyUsername.Size = new System.Drawing.Size(116, 20);
            this.txtProxyUsername.TabIndex = 6;
            // 
            // chkProxyAuthentication
            // 
            this.chkProxyAuthentication.AutoSize = true;
            this.chkProxyAuthentication.Location = new System.Drawing.Point(16, 31);
            this.chkProxyAuthentication.Name = "chkProxyAuthentication";
            this.chkProxyAuthentication.Size = new System.Drawing.Size(162, 17);
            this.chkProxyAuthentication.TabIndex = 5;
            this.chkProxyAuthentication.Text = "Proxy requires authentication";
            this.chkProxyAuthentication.UseVisualStyleBackColor = true;
            this.chkProxyAuthentication.CheckedChanged += new System.EventHandler(this.chkProxyAuthentication_CheckedChanged);
            // 
            // txtProxyPassword
            // 
            this.txtProxyPassword.Enabled = false;
            this.txtProxyPassword.Location = new System.Drawing.Point(96, 80);
            this.txtProxyPassword.Name = "txtProxyPassword";
            this.txtProxyPassword.PasswordChar = '*';
            this.txtProxyPassword.Size = new System.Drawing.Size(116, 20);
            this.txtProxyPassword.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Password:";
            // 
            // numProxyPort
            // 
            this.numProxyPort.Location = new System.Drawing.Point(243, 6);
            this.numProxyPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numProxyPort.Name = "numProxyPort";
            this.numProxyPort.Size = new System.Drawing.Size(71, 20);
            this.numProxyPort.TabIndex = 2;
            // 
            // txtProxyAddress
            // 
            this.txtProxyAddress.Location = new System.Drawing.Point(57, 5);
            this.txtProxyAddress.Name = "txtProxyAddress";
            this.txtProxyAddress.Size = new System.Drawing.Size(145, 20);
            this.txtProxyAddress.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.listSearchEngines);
            this.tabPage4.Controls.Add(this.groupBox2);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(555, 206);
            this.tabPage4.TabIndex = 4;
            this.tabPage4.Text = "Searching";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // listSearchEngines
            // 
            this.listSearchEngines.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listSearchEngines.Dock = System.Windows.Forms.DockStyle.Left;
            this.listSearchEngines.FullRowSelect = true;
            this.listSearchEngines.GridLines = true;
            this.listSearchEngines.Location = new System.Drawing.Point(3, 3);
            this.listSearchEngines.Name = "listSearchEngines";
            this.listSearchEngines.ShowItemToolTips = true;
            this.listSearchEngines.Size = new System.Drawing.Size(262, 200);
            this.listSearchEngines.TabIndex = 1;
            this.listSearchEngines.UseCompatibleStateImageBehavior = false;
            this.listSearchEngines.View = System.Windows.Forms.View.Details;
            this.listSearchEngines.SelectedIndexChanged += new System.EventHandler(this.listSearchEngines_SelectedIndexChanged);
            this.listSearchEngines.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listSearchEngines_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 175;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Enabled";
            this.columnHeader2.Width = 80;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listBox1);
            this.groupBox2.Controls.Add(this.lblSearchEngineSupportsRatings);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.lblSearchEngineHomepage);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Location = new System.Drawing.Point(271, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(278, 194);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Search Engine Details";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(9, 89);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(157, 56);
            this.listBox1.TabIndex = 15;
            // 
            // lblSearchEngineSupportsRatings
            // 
            this.lblSearchEngineSupportsRatings.AutoSize = true;
            this.lblSearchEngineSupportsRatings.Location = new System.Drawing.Point(102, 48);
            this.lblSearchEngineSupportsRatings.Name = "lblSearchEngineSupportsRatings";
            this.lblSearchEngineSupportsRatings.Size = new System.Drawing.Size(162, 13);
            this.lblSearchEngineSupportsRatings.TabIndex = 14;
            this.lblSearchEngineSupportsRatings.Text = "lblSearchEngineSupportsRatings";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 73);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(139, 13);
            this.label12.TabIndex = 13;
            this.label12.Text = "Supported Tablature Types:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 48);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(91, 13);
            this.label11.TabIndex = 12;
            this.label11.Text = "Supports Ratings:";
            // 
            // lblSearchEngineHomepage
            // 
            this.lblSearchEngineHomepage.ActiveLinkColor = System.Drawing.Color.Blue;
            this.lblSearchEngineHomepage.AutoSize = true;
            this.lblSearchEngineHomepage.Location = new System.Drawing.Point(74, 19);
            this.lblSearchEngineHomepage.Name = "lblSearchEngineHomepage";
            this.lblSearchEngineHomepage.Size = new System.Drawing.Size(136, 13);
            this.lblSearchEngineHomepage.TabIndex = 11;
            this.lblSearchEngineHomepage.TabStop = true;
            this.lblSearchEngineHomepage.Text = "lblSearchEngineHomepage";
            this.lblSearchEngineHomepage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel_LinkClicked);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 19);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(62, 13);
            this.label15.TabIndex = 9;
            this.label15.Text = "Homepage:";
            // 
            // pluginsDirectorybtn
            // 
            this.pluginsDirectorybtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pluginsDirectorybtn.Location = new System.Drawing.Point(12, 251);
            this.pluginsDirectorybtn.Name = "pluginsDirectorybtn";
            this.pluginsDirectorybtn.Size = new System.Drawing.Size(150, 23);
            this.pluginsDirectorybtn.TabIndex = 25;
            this.pluginsDirectorybtn.Text = "Browse Plugins Directory";
            this.pluginsDirectorybtn.UseVisualStyleBackColor = true;
            this.pluginsDirectorybtn.Visible = false;
            this.pluginsDirectorybtn.Click += new System.EventHandler(this.pluginsDirectorybtn_Click);
            // 
            // printColorDialog
            // 
            this.printColorDialog.SolidColorOnly = true;
            // 
            // PreferencesDialog
            // 
            this.AcceptButton = this.okbtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.cancelbtn;
            this.ClientSize = new System.Drawing.Size(588, 284);
            this.Controls.Add(this.pluginsDirectorybtn);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.okbtn);
            this.Controls.Add(this.cancelbtn);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PreferencesDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Preferences";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPlugins.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.customProxyPanel.ResumeLayout(false);
            this.customProxyPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numProxyPort)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkUpdates;
        private System.Windows.Forms.Button cancelbtn;
        private System.Windows.Forms.Button okbtn;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPlugins;
        private System.Windows.Forms.ListView listPlugins;
        private System.Windows.Forms.ColumnHeader colpluginName;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.RadioButton radioManualProxy;
        private System.Windows.Forms.RadioButton radioSystemProxy;
        private System.Windows.Forms.RadioButton radioNoProxy;
        private System.Windows.Forms.NumericUpDown numProxyPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtProxyAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtProxyPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtProxyUsername;
        private System.Windows.Forms.CheckBox chkProxyAuthentication;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel customProxyPanel;
        private System.Windows.Forms.CheckBox chkShowProxyPassword;
        private System.Windows.Forms.Button btnEditSystemProxy;
        private System.Windows.Forms.ColumnHeader colpluginEnabled;
        private System.Windows.Forms.Button pluginsDirectorybtn;
        private System.Windows.Forms.CheckBox chkPrintPageNumbers;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckBox chkPrintTimestamp;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ColorDialog printColorDialog;
        private System.Windows.Forms.Panel printColorPreview;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblPluginAuthor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblPluginVersion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.LinkLabel lblPluginHomepage;
        private System.Windows.Forms.Label lblPluginDescription;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblPluginFilename;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ListView listSearchEngines;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.LinkLabel lblSearchEngineHomepage;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblSearchEngineSupportsRatings;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.CheckBox chkStripVersionedNames;
    }
}