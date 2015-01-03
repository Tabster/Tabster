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
            this.chkupdatestartup = new System.Windows.Forms.CheckBox();
            this.cancelbtn = new System.Windows.Forms.Button();
            this.okbtn = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.printColorPreview = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.chkPrintTimestamp = new System.Windows.Forms.CheckBox();
            this.chkPrintPageNumbers = new System.Windows.Forms.CheckBox();
            this.tabPlugins = new System.Windows.Forms.TabPage();
            this.listPlugins = new System.Windows.Forms.ListView();
            this.colpluginEnabled = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colpluginName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colpluginVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colpluginFilename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colpluginDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            this.pluginsDirectorybtn = new System.Windows.Forms.Button();
            this.printColorDialog = new System.Windows.Forms.ColorDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPlugins.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.customProxyPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numProxyPort)).BeginInit();
            this.SuspendLayout();
            // 
            // chkupdatestartup
            // 
            this.chkupdatestartup.AutoSize = true;
            this.chkupdatestartup.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkupdatestartup.Location = new System.Drawing.Point(6, 6);
            this.chkupdatestartup.Name = "chkupdatestartup";
            this.chkupdatestartup.Size = new System.Drawing.Size(163, 17);
            this.chkupdatestartup.TabIndex = 18;
            this.chkupdatestartup.Text = "Check for updates on startup";
            this.chkupdatestartup.UseVisualStyleBackColor = true;
            // 
            // cancelbtn
            // 
            this.cancelbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelbtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelbtn.Location = new System.Drawing.Point(474, 251);
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
            this.okbtn.Location = new System.Drawing.Point(393, 251);
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
            this.tabControl1.Location = new System.Drawing.Point(13, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(534, 232);
            this.tabControl1.TabIndex = 24;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chkupdatestartup);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(526, 206);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
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
            this.tabPage2.Size = new System.Drawing.Size(526, 206);
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
            this.tabPlugins.Location = new System.Drawing.Point(4, 22);
            this.tabPlugins.Name = "tabPlugins";
            this.tabPlugins.Padding = new System.Windows.Forms.Padding(3);
            this.tabPlugins.Size = new System.Drawing.Size(526, 206);
            this.tabPlugins.TabIndex = 1;
            this.tabPlugins.Text = "Plugins";
            this.tabPlugins.UseVisualStyleBackColor = true;
            // 
            // listPlugins
            // 
            this.listPlugins.CheckBoxes = true;
            this.listPlugins.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colpluginEnabled,
            this.colpluginName,
            this.colpluginVersion,
            this.colpluginFilename,
            this.colpluginDescription});
            this.listPlugins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listPlugins.FullRowSelect = true;
            this.listPlugins.GridLines = true;
            this.listPlugins.Location = new System.Drawing.Point(3, 3);
            this.listPlugins.Name = "listPlugins";
            this.listPlugins.ShowItemToolTips = true;
            this.listPlugins.Size = new System.Drawing.Size(520, 200);
            this.listPlugins.TabIndex = 0;
            this.listPlugins.UseCompatibleStateImageBehavior = false;
            this.listPlugins.View = System.Windows.Forms.View.Details;
            this.listPlugins.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listPlugins_ItemChecked);
            // 
            // colpluginEnabled
            // 
            this.colpluginEnabled.Text = "";
            this.colpluginEnabled.Width = 20;
            // 
            // colpluginName
            // 
            this.colpluginName.Text = "Name";
            this.colpluginName.Width = 122;
            // 
            // colpluginVersion
            // 
            this.colpluginVersion.Text = "Version";
            this.colpluginVersion.Width = 75;
            // 
            // colpluginFilename
            // 
            this.colpluginFilename.Text = "Filename";
            this.colpluginFilename.Width = 135;
            // 
            // colpluginDescription
            // 
            this.colpluginDescription.Text = "Description";
            this.colpluginDescription.Width = 180;
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
            this.tabPage3.Size = new System.Drawing.Size(526, 206);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Network";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnEditSystemProxy
            // 
            this.btnEditSystemProxy.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnEditSystemProxy.Location = new System.Drawing.Point(158, 26);
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
            // pluginsDirectorybtn
            // 
            this.pluginsDirectorybtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pluginsDirectorybtn.Location = new System.Drawing.Point(13, 251);
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
            this.ClientSize = new System.Drawing.Size(559, 284);
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
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.customProxyPanel.ResumeLayout(false);
            this.customProxyPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numProxyPort)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkupdatestartup;
        private System.Windows.Forms.Button cancelbtn;
        private System.Windows.Forms.Button okbtn;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPlugins;
        private System.Windows.Forms.ListView listPlugins;
        private System.Windows.Forms.ColumnHeader colpluginName;
        private System.Windows.Forms.ColumnHeader colpluginFilename;
        private System.Windows.Forms.ColumnHeader colpluginVersion;
        private System.Windows.Forms.ColumnHeader colpluginDescription;
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
    }
}