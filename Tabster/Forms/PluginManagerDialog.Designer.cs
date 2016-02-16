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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabInstalled = new System.Windows.Forms.TabPage();
            this.listPlugins = new System.Windows.Forms.ListView();
            this.colpluginName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colpluginEnabled = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colpluginVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabUpdates = new System.Windows.Forms.TabPage();
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
            this.pluginsDirectorybtn = new System.Windows.Forms.Button();
            this.okbtn = new System.Windows.Forms.Button();
            this.cancelbtn = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabInstalled.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabInstalled);
            this.tabControl1.Controls.Add(this.tabUpdates);
            this.tabControl1.Location = new System.Drawing.Point(13, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(413, 207);
            this.tabControl1.TabIndex = 0;
            // 
            // tabInstalled
            // 
            this.tabInstalled.Controls.Add(this.listPlugins);
            this.tabInstalled.Location = new System.Drawing.Point(4, 22);
            this.tabInstalled.Name = "tabInstalled";
            this.tabInstalled.Size = new System.Drawing.Size(405, 181);
            this.tabInstalled.TabIndex = 2;
            this.tabInstalled.Text = "Installed";
            this.tabInstalled.UseVisualStyleBackColor = true;
            // 
            // listPlugins
            // 
            this.listPlugins.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colpluginName,
            this.colpluginEnabled,
            this.colpluginVersion});
            this.listPlugins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listPlugins.FullRowSelect = true;
            this.listPlugins.GridLines = true;
            this.listPlugins.Location = new System.Drawing.Point(0, 0);
            this.listPlugins.Name = "listPlugins";
            this.listPlugins.ShowItemToolTips = true;
            this.listPlugins.Size = new System.Drawing.Size(405, 181);
            this.listPlugins.TabIndex = 1;
            this.listPlugins.UseCompatibleStateImageBehavior = false;
            this.listPlugins.View = System.Windows.Forms.View.Details;
            this.listPlugins.SelectedIndexChanged += new System.EventHandler(this.listPlugins_SelectedIndexChanged);
            this.listPlugins.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listPlugins_MouseDoubleClick);
            // 
            // colpluginName
            // 
            this.colpluginName.Text = "Name";
            this.colpluginName.Width = 189;
            // 
            // colpluginEnabled
            // 
            this.colpluginEnabled.Text = "Enabled";
            this.colpluginEnabled.Width = 80;
            // 
            // colpluginVersion
            // 
            this.colpluginVersion.Text = "Version";
            this.colpluginVersion.Width = 94;
            // 
            // tabUpdates
            // 
            this.tabUpdates.Location = new System.Drawing.Point(4, 22);
            this.tabUpdates.Name = "tabUpdates";
            this.tabUpdates.Padding = new System.Windows.Forms.Padding(3);
            this.tabUpdates.Size = new System.Drawing.Size(405, 168);
            this.tabUpdates.TabIndex = 1;
            this.tabUpdates.Text = "Updates";
            this.tabUpdates.UseVisualStyleBackColor = true;
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
            this.groupBox1.Location = new System.Drawing.Point(432, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(278, 208);
            this.groupBox1.TabIndex = 2;
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
            // pluginsDirectorybtn
            // 
            this.pluginsDirectorybtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pluginsDirectorybtn.Location = new System.Drawing.Point(13, 226);
            this.pluginsDirectorybtn.Name = "pluginsDirectorybtn";
            this.pluginsDirectorybtn.Size = new System.Drawing.Size(150, 23);
            this.pluginsDirectorybtn.TabIndex = 26;
            this.pluginsDirectorybtn.Text = "Browse Plugins Directory";
            this.pluginsDirectorybtn.UseVisualStyleBackColor = true;
            this.pluginsDirectorybtn.Visible = false;
            this.pluginsDirectorybtn.Click += new System.EventHandler(this.pluginsDirectorybtn_Click);
            // 
            // okbtn
            // 
            this.okbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okbtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okbtn.Location = new System.Drawing.Point(566, 226);
            this.okbtn.Name = "okbtn";
            this.okbtn.Size = new System.Drawing.Size(75, 23);
            this.okbtn.TabIndex = 28;
            this.okbtn.Text = "OK";
            this.okbtn.UseVisualStyleBackColor = true;
            this.okbtn.Click += new System.EventHandler(this.okbtn_Click);
            // 
            // cancelbtn
            // 
            this.cancelbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelbtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelbtn.Location = new System.Drawing.Point(647, 226);
            this.cancelbtn.Name = "cancelbtn";
            this.cancelbtn.Size = new System.Drawing.Size(75, 23);
            this.cancelbtn.TabIndex = 27;
            this.cancelbtn.Text = "Cancel";
            this.cancelbtn.UseVisualStyleBackColor = true;
            // 
            // PluginManagerDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 261);
            this.Controls.Add(this.okbtn);
            this.Controls.Add(this.cancelbtn);
            this.Controls.Add(this.pluginsDirectorybtn);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControl1);
            this.Name = "PluginManagerDialog";
            this.Text = "PluginManagerForm";
            this.tabControl1.ResumeLayout(false);
            this.tabInstalled.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabUpdates;
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
    }
}