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
            this.cancelbtn = new System.Windows.Forms.Button();
            this.okbtn = new System.Windows.Forms.Button();
            this.printColorDialog = new System.Windows.Forms.ColorDialog();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.listSearchEngines = new System.Windows.Forms.ListView();
            this.colSearchEngineName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSearchEngineEnabled = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.lblSearchEngineSupportsRatings = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblSearchEngineHomepage = new System.Windows.Forms.LinkLabel();
            this.label15 = new System.Windows.Forms.Label();
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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.printColorPreview = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.chkPrintTimestamp = new System.Windows.Forms.CheckBox();
            this.chkPrintPageNumbers = new System.Windows.Forms.CheckBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnClearRecentItems = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.numMaxRecentItems = new System.Windows.Forms.NumericUpDown();
            this.chkStripVersionedNames = new System.Windows.Forms.CheckBox();
            this.chkUpdates = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.customProxyPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numProxyPort)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxRecentItems)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
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
            // printColorDialog
            // 
            this.printColorDialog.SolidColorOnly = true;
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
            this.colSearchEngineName,
            this.colSearchEngineEnabled});
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
            // colSearchEngineName
            // 
            this.colSearchEngineName.Text = "Name";
            this.colSearchEngineName.Width = 175;
            // 
            // colSearchEngineEnabled
            // 
            this.colSearchEngineEnabled.Text = "Enabled";
            this.colSearchEngineEnabled.Width = 80;
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
            this.btnEditSystemProxy.Location = new System.Drawing.Point(158, 166);
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
            this.radioManualProxy.Location = new System.Drawing.Point(6, 29);
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
            this.radioSystemProxy.Location = new System.Drawing.Point(6, 169);
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
            this.customProxyPanel.Location = new System.Drawing.Point(6, 52);
            this.customProxyPanel.Name = "customProxyPanel";
            this.customProxyPanel.Size = new System.Drawing.Size(354, 111);
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
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnClearRecentItems);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.numMaxRecentItems);
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
            // btnClearRecentItems
            // 
            this.btnClearRecentItems.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnClearRecentItems.Location = new System.Drawing.Point(175, 50);
            this.btnClearRecentItems.Name = "btnClearRecentItems";
            this.btnClearRecentItems.Size = new System.Drawing.Size(88, 23);
            this.btnClearRecentItems.TabIndex = 26;
            this.btnClearRecentItems.Text = "Clear";
            this.btnClearRecentItems.UseVisualStyleBackColor = true;
            this.btnClearRecentItems.Click += new System.EventHandler(this.btnClearRecentItems_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Max recent items:";
            // 
            // numMaxRecentItems
            // 
            this.numMaxRecentItems.Location = new System.Drawing.Point(98, 53);
            this.numMaxRecentItems.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numMaxRecentItems.Name = "numMaxRecentItems";
            this.numMaxRecentItems.Size = new System.Drawing.Size(71, 20);
            this.numMaxRecentItems.TabIndex = 20;
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
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(13, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(563, 232);
            this.tabControl1.TabIndex = 24;
            // 
            // PreferencesDialog
            // 
            this.AcceptButton = this.okbtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.cancelbtn;
            this.ClientSize = new System.Drawing.Size(588, 284);
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
            this.tabPage4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.customProxyPanel.ResumeLayout(false);
            this.customProxyPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numProxyPort)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxRecentItems)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cancelbtn;
        private System.Windows.Forms.Button okbtn;
        private System.Windows.Forms.ColorDialog printColorDialog;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ListView listSearchEngines;
        private System.Windows.Forms.ColumnHeader colSearchEngineName;
        private System.Windows.Forms.ColumnHeader colSearchEngineEnabled;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label lblSearchEngineSupportsRatings;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.LinkLabel lblSearchEngineHomepage;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnEditSystemProxy;
        private System.Windows.Forms.RadioButton radioNoProxy;
        private System.Windows.Forms.RadioButton radioManualProxy;
        private System.Windows.Forms.RadioButton radioSystemProxy;
        private System.Windows.Forms.Panel customProxyPanel;
        private System.Windows.Forms.CheckBox chkShowProxyPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtProxyUsername;
        private System.Windows.Forms.CheckBox chkProxyAuthentication;
        private System.Windows.Forms.TextBox txtProxyPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numProxyPort;
        private System.Windows.Forms.TextBox txtProxyAddress;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel printColorPreview;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkPrintTimestamp;
        private System.Windows.Forms.CheckBox chkPrintPageNumbers;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckBox chkStripVersionedNames;
        private System.Windows.Forms.CheckBox chkUpdates;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button btnClearRecentItems;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numMaxRecentItems;
    }
}