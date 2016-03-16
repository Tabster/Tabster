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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreferencesDialog));
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
            resources.ApplyResources(this.cancelbtn, "cancelbtn");
            this.cancelbtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelbtn.Name = "cancelbtn";
            this.cancelbtn.UseVisualStyleBackColor = true;
            // 
            // okbtn
            // 
            resources.ApplyResources(this.okbtn, "okbtn");
            this.okbtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okbtn.Name = "okbtn";
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
            resources.ApplyResources(this.tabPage4, "tabPage4");
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // listSearchEngines
            // 
            this.listSearchEngines.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colSearchEngineName,
            this.colSearchEngineEnabled});
            resources.ApplyResources(this.listSearchEngines, "listSearchEngines");
            this.listSearchEngines.FullRowSelect = true;
            this.listSearchEngines.GridLines = true;
            this.listSearchEngines.Name = "listSearchEngines";
            this.listSearchEngines.ShowItemToolTips = true;
            this.listSearchEngines.UseCompatibleStateImageBehavior = false;
            this.listSearchEngines.View = System.Windows.Forms.View.Details;
            this.listSearchEngines.SelectedIndexChanged += new System.EventHandler(this.listSearchEngines_SelectedIndexChanged);
            this.listSearchEngines.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listSearchEngines_MouseDoubleClick);
            // 
            // colSearchEngineName
            // 
            resources.ApplyResources(this.colSearchEngineName, "colSearchEngineName");
            // 
            // colSearchEngineEnabled
            // 
            resources.ApplyResources(this.colSearchEngineEnabled, "colSearchEngineEnabled");
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listBox1);
            this.groupBox2.Controls.Add(this.lblSearchEngineSupportsRatings);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.lblSearchEngineHomepage);
            this.groupBox2.Controls.Add(this.label15);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            resources.ApplyResources(this.listBox1, "listBox1");
            this.listBox1.Name = "listBox1";
            // 
            // lblSearchEngineSupportsRatings
            // 
            resources.ApplyResources(this.lblSearchEngineSupportsRatings, "lblSearchEngineSupportsRatings");
            this.lblSearchEngineSupportsRatings.Name = "lblSearchEngineSupportsRatings";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // lblSearchEngineHomepage
            // 
            this.lblSearchEngineHomepage.ActiveLinkColor = System.Drawing.Color.Blue;
            resources.ApplyResources(this.lblSearchEngineHomepage, "lblSearchEngineHomepage");
            this.lblSearchEngineHomepage.Name = "lblSearchEngineHomepage";
            this.lblSearchEngineHomepage.TabStop = true;
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnEditSystemProxy);
            this.tabPage3.Controls.Add(this.radioNoProxy);
            this.tabPage3.Controls.Add(this.radioManualProxy);
            this.tabPage3.Controls.Add(this.radioSystemProxy);
            this.tabPage3.Controls.Add(this.customProxyPanel);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnEditSystemProxy
            // 
            resources.ApplyResources(this.btnEditSystemProxy, "btnEditSystemProxy");
            this.btnEditSystemProxy.Name = "btnEditSystemProxy";
            this.btnEditSystemProxy.UseVisualStyleBackColor = true;
            this.btnEditSystemProxy.Click += new System.EventHandler(this.btnEditSystemProxy_Click);
            // 
            // radioNoProxy
            // 
            resources.ApplyResources(this.radioNoProxy, "radioNoProxy");
            this.radioNoProxy.Checked = true;
            this.radioNoProxy.Name = "radioNoProxy";
            this.radioNoProxy.TabStop = true;
            this.radioNoProxy.UseVisualStyleBackColor = true;
            // 
            // radioManualProxy
            // 
            resources.ApplyResources(this.radioManualProxy, "radioManualProxy");
            this.radioManualProxy.Name = "radioManualProxy";
            this.radioManualProxy.TabStop = true;
            this.radioManualProxy.UseVisualStyleBackColor = true;
            this.radioManualProxy.CheckedChanged += new System.EventHandler(this.radioCustomProxy_CheckedChanged);
            // 
            // radioSystemProxy
            // 
            resources.ApplyResources(this.radioSystemProxy, "radioSystemProxy");
            this.radioSystemProxy.Name = "radioSystemProxy";
            this.radioSystemProxy.TabStop = true;
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
            resources.ApplyResources(this.customProxyPanel, "customProxyPanel");
            this.customProxyPanel.Name = "customProxyPanel";
            // 
            // chkShowProxyPassword
            // 
            resources.ApplyResources(this.chkShowProxyPassword, "chkShowProxyPassword");
            this.chkShowProxyPassword.Name = "chkShowProxyPassword";
            this.chkShowProxyPassword.UseVisualStyleBackColor = true;
            this.chkShowProxyPassword.CheckedChanged += new System.EventHandler(this.chkShowProxyPassword_CheckedChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtProxyUsername
            // 
            resources.ApplyResources(this.txtProxyUsername, "txtProxyUsername");
            this.txtProxyUsername.Name = "txtProxyUsername";
            // 
            // chkProxyAuthentication
            // 
            resources.ApplyResources(this.chkProxyAuthentication, "chkProxyAuthentication");
            this.chkProxyAuthentication.Name = "chkProxyAuthentication";
            this.chkProxyAuthentication.UseVisualStyleBackColor = true;
            this.chkProxyAuthentication.CheckedChanged += new System.EventHandler(this.chkProxyAuthentication_CheckedChanged);
            // 
            // txtProxyPassword
            // 
            resources.ApplyResources(this.txtProxyPassword, "txtProxyPassword");
            this.txtProxyPassword.Name = "txtProxyPassword";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // numProxyPort
            // 
            resources.ApplyResources(this.numProxyPort, "numProxyPort");
            this.numProxyPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numProxyPort.Name = "numProxyPort";
            // 
            // txtProxyAddress
            // 
            resources.ApplyResources(this.txtProxyAddress, "txtProxyAddress");
            this.txtProxyAddress.Name = "txtProxyAddress";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.printColorPreview);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.chkPrintTimestamp);
            this.tabPage2.Controls.Add(this.chkPrintPageNumbers);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // printColorPreview
            // 
            this.printColorPreview.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.printColorPreview, "printColorPreview");
            this.printColorPreview.Name = "printColorPreview";
            this.printColorPreview.Click += new System.EventHandler(this.printColorPreview_Click);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // chkPrintTimestamp
            // 
            resources.ApplyResources(this.chkPrintTimestamp, "chkPrintTimestamp");
            this.chkPrintTimestamp.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkPrintTimestamp.Name = "chkPrintTimestamp";
            this.chkPrintTimestamp.UseVisualStyleBackColor = true;
            // 
            // chkPrintPageNumbers
            // 
            resources.ApplyResources(this.chkPrintPageNumbers, "chkPrintPageNumbers");
            this.chkPrintPageNumbers.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkPrintPageNumbers.Name = "chkPrintPageNumbers";
            this.chkPrintPageNumbers.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnClearRecentItems);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.numMaxRecentItems);
            this.tabPage1.Controls.Add(this.chkStripVersionedNames);
            this.tabPage1.Controls.Add(this.chkUpdates);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnClearRecentItems
            // 
            resources.ApplyResources(this.btnClearRecentItems, "btnClearRecentItems");
            this.btnClearRecentItems.Name = "btnClearRecentItems";
            this.btnClearRecentItems.UseVisualStyleBackColor = true;
            this.btnClearRecentItems.Click += new System.EventHandler(this.btnClearRecentItems_Click);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // numMaxRecentItems
            // 
            resources.ApplyResources(this.numMaxRecentItems, "numMaxRecentItems");
            this.numMaxRecentItems.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numMaxRecentItems.Name = "numMaxRecentItems";
            // 
            // chkStripVersionedNames
            // 
            resources.ApplyResources(this.chkStripVersionedNames, "chkStripVersionedNames");
            this.chkStripVersionedNames.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkStripVersionedNames.Name = "chkStripVersionedNames";
            this.chkStripVersionedNames.UseVisualStyleBackColor = true;
            // 
            // chkUpdates
            // 
            resources.ApplyResources(this.chkUpdates, "chkUpdates");
            this.chkUpdates.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkUpdates.Name = "chkUpdates";
            this.chkUpdates.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // PreferencesDialog
            // 
            this.AcceptButton = this.okbtn;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.cancelbtn;
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