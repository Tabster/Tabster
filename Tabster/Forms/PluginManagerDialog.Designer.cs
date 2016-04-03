namespace Tabster.Forms
{
    partial class PluginManagerDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginManagerDialog));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabInstalled = new System.Windows.Forms.TabPage();
            this.listPlugins = new System.Windows.Forms.ListView();
            this.colpluginName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colpluginEnabled = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colpluginVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabFeatured = new System.Windows.Forms.TabPage();
            this.listFeatured = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblPlaceholder = new System.Windows.Forms.Label();
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
            this.pluginsDirectorybtn = new System.Windows.Forms.Button();
            this.okbtn = new System.Windows.Forms.Button();
            this.cancelbtn = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabInstalled.SuspendLayout();
            this.tabFeatured.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabInstalled);
            this.tabControl1.Controls.Add(this.tabFeatured);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabInstalled
            // 
            this.tabInstalled.Controls.Add(this.listPlugins);
            resources.ApplyResources(this.tabInstalled, "tabInstalled");
            this.tabInstalled.Name = "tabInstalled";
            this.tabInstalled.UseVisualStyleBackColor = true;
            // 
            // listPlugins
            // 
            this.listPlugins.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colpluginName,
            this.colpluginEnabled,
            this.colpluginVersion});
            resources.ApplyResources(this.listPlugins, "listPlugins");
            this.listPlugins.FullRowSelect = true;
            this.listPlugins.GridLines = true;
            this.listPlugins.Name = "listPlugins";
            this.listPlugins.ShowItemToolTips = true;
            this.listPlugins.UseCompatibleStateImageBehavior = false;
            this.listPlugins.View = System.Windows.Forms.View.Details;
            this.listPlugins.SelectedIndexChanged += new System.EventHandler(this.listPlugins_SelectedIndexChanged);
            this.listPlugins.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listPlugins_MouseDoubleClick);
            // 
            // colpluginName
            // 
            resources.ApplyResources(this.colpluginName, "colpluginName");
            // 
            // colpluginEnabled
            // 
            resources.ApplyResources(this.colpluginEnabled, "colpluginEnabled");
            // 
            // colpluginVersion
            // 
            resources.ApplyResources(this.colpluginVersion, "colpluginVersion");
            // 
            // tabFeatured
            // 
            this.tabFeatured.Controls.Add(this.listFeatured);
            resources.ApplyResources(this.tabFeatured, "tabFeatured");
            this.tabFeatured.Name = "tabFeatured";
            this.tabFeatured.UseVisualStyleBackColor = true;
            // 
            // listFeatured
            // 
            this.listFeatured.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            resources.ApplyResources(this.listFeatured, "listFeatured");
            this.listFeatured.FullRowSelect = true;
            this.listFeatured.GridLines = true;
            this.listFeatured.Name = "listFeatured";
            this.listFeatured.ShowItemToolTips = true;
            this.listFeatured.UseCompatibleStateImageBehavior = false;
            this.listFeatured.View = System.Windows.Forms.View.Details;
            this.listFeatured.SelectedIndexChanged += new System.EventHandler(this.listFeatured_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // columnHeader2
            // 
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            // 
            // columnHeader3
            // 
            resources.ApplyResources(this.columnHeader3, "columnHeader3");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblPlaceholder);
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
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // lblPlaceholder
            // 
            resources.ApplyResources(this.lblPlaceholder, "lblPlaceholder");
            this.lblPlaceholder.Name = "lblPlaceholder";
            // 
            // lblPluginFilename
            // 
            resources.ApplyResources(this.lblPluginFilename, "lblPluginFilename");
            this.lblPluginFilename.Name = "lblPluginFilename";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // lblPluginDescription
            // 
            resources.ApplyResources(this.lblPluginDescription, "lblPluginDescription");
            this.lblPluginDescription.Name = "lblPluginDescription";
            // 
            // lblPluginHomepage
            // 
            this.lblPluginHomepage.ActiveLinkColor = System.Drawing.Color.Blue;
            resources.ApplyResources(this.lblPluginHomepage, "lblPluginHomepage");
            this.lblPluginHomepage.Name = "lblPluginHomepage";
            this.lblPluginHomepage.TabStop = true;
            this.lblPluginHomepage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel_LinkClicked);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // lblPluginAuthor
            // 
            resources.ApplyResources(this.lblPluginAuthor, "lblPluginAuthor");
            this.lblPluginAuthor.Name = "lblPluginAuthor";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // lblPluginVersion
            // 
            resources.ApplyResources(this.lblPluginVersion, "lblPluginVersion");
            this.lblPluginVersion.Name = "lblPluginVersion";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // pluginsDirectorybtn
            // 
            resources.ApplyResources(this.pluginsDirectorybtn, "pluginsDirectorybtn");
            this.pluginsDirectorybtn.Name = "pluginsDirectorybtn";
            this.pluginsDirectorybtn.UseVisualStyleBackColor = true;
            this.pluginsDirectorybtn.Click += new System.EventHandler(this.pluginsDirectorybtn_Click);
            // 
            // okbtn
            // 
            resources.ApplyResources(this.okbtn, "okbtn");
            this.okbtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okbtn.Name = "okbtn";
            this.okbtn.UseVisualStyleBackColor = true;
            this.okbtn.Click += new System.EventHandler(this.okbtn_Click);
            // 
            // cancelbtn
            // 
            resources.ApplyResources(this.cancelbtn, "cancelbtn");
            this.cancelbtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelbtn.Name = "cancelbtn";
            this.cancelbtn.UseVisualStyleBackColor = true;
            // 
            // PluginManagerDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.okbtn);
            this.Controls.Add(this.cancelbtn);
            this.Controls.Add(this.pluginsDirectorybtn);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PluginManagerDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.tabControl1.ResumeLayout(false);
            this.tabInstalled.ResumeLayout(false);
            this.tabFeatured.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabFeatured;
        private System.Windows.Forms.TabPage tabInstalled;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblPluginFilename;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblPluginDescription;
        private System.Windows.Forms.LinkLabel lblPluginHomepage;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblPluginAuthor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblPluginVersion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListView listPlugins;
        private System.Windows.Forms.ColumnHeader colpluginName;
        private System.Windows.Forms.ColumnHeader colpluginEnabled;
        private System.Windows.Forms.ColumnHeader colpluginVersion;
        private System.Windows.Forms.Button pluginsDirectorybtn;
        private System.Windows.Forms.Button okbtn;
        private System.Windows.Forms.Button cancelbtn;
        private System.Windows.Forms.Label lblPlaceholder;
        private System.Windows.Forms.ListView listFeatured;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}