namespace Tabster.Forms
{
    partial class AboutDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutDialog));
            this.lblName = new System.Windows.Forms.Label();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.closebtn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.txtLicense = new System.Windows.Forms.TextBox();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listPlugins = new System.Windows.Forms.ListView();
            this.colpluginName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colpluginVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colpluginAuthor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colpluginFilename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtFontLicense = new System.Windows.Forms.TextBox();
            this.btnHomepage = new System.Windows.Forms.Button();
            this.lblVersion = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblName
            // 
            resources.ApplyResources(this.lblName, "lblName");
            this.lblName.BackColor = System.Drawing.Color.Transparent;
            this.lblName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblName.Name = "lblName";
            // 
            // lblCopyright
            // 
            resources.ApplyResources(this.lblCopyright, "lblCopyright");
            this.lblCopyright.BackColor = System.Drawing.Color.Transparent;
            this.lblCopyright.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCopyright.Name = "lblCopyright";
            // 
            // closebtn
            // 
            resources.ApplyResources(this.closebtn, "closebtn");
            this.closebtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closebtn.Name = "closebtn";
            this.closebtn.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Tabster.Properties.Resources.Guitar98;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // linkLabel3
            // 
            this.linkLabel3.ActiveLinkColor = System.Drawing.Color.Blue;
            resources.ApplyResources(this.linkLabel3, "linkLabel3");
            this.linkLabel3.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel3.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.TabStop = true;
            this.linkLabel3.UseCompatibleTextRendering = true;
            this.linkLabel3.VisitedLinkColor = System.Drawing.Color.Blue;
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkClicked);
            // 
            // txtLicense
            // 
            resources.ApplyResources(this.txtLicense, "txtLicense");
            this.txtLicense.Name = "txtLicense";
            this.txtLicense.ReadOnly = true;
            // 
            // linkLabel2
            // 
            this.linkLabel2.ActiveLinkColor = System.Drawing.Color.Blue;
            resources.ApplyResources(this.linkLabel2, "linkLabel2");
            this.linkLabel2.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel2.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.TabStop = true;
            this.linkLabel2.UseCompatibleTextRendering = true;
            this.linkLabel2.VisitedLinkColor = System.Drawing.Color.Blue;
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtLicense);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.listPlugins);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // listPlugins
            // 
            this.listPlugins.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colpluginName,
            this.colpluginVersion,
            this.colpluginAuthor,
            this.colpluginFilename});
            resources.ApplyResources(this.listPlugins, "listPlugins");
            this.listPlugins.FullRowSelect = true;
            this.listPlugins.GridLines = true;
            this.listPlugins.Name = "listPlugins";
            this.listPlugins.ShowItemToolTips = true;
            this.listPlugins.UseCompatibleStateImageBehavior = false;
            this.listPlugins.View = System.Windows.Forms.View.Details;
            // 
            // colpluginName
            // 
            resources.ApplyResources(this.colpluginName, "colpluginName");
            // 
            // colpluginVersion
            // 
            resources.ApplyResources(this.colpluginVersion, "colpluginVersion");
            // 
            // colpluginAuthor
            // 
            resources.ApplyResources(this.colpluginAuthor, "colpluginAuthor");
            // 
            // colpluginFilename
            // 
            resources.ApplyResources(this.colpluginFilename, "colpluginFilename");
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txtFontLicense);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtFontLicense
            // 
            resources.ApplyResources(this.txtFontLicense, "txtFontLicense");
            this.txtFontLicense.Name = "txtFontLicense";
            this.txtFontLicense.ReadOnly = true;
            // 
            // btnHomepage
            // 
            resources.ApplyResources(this.btnHomepage, "btnHomepage");
            this.btnHomepage.Name = "btnHomepage";
            this.btnHomepage.UseVisualStyleBackColor = true;
            this.btnHomepage.Click += new System.EventHandler(this.btnHomepage_Click);
            // 
            // lblVersion
            // 
            this.lblVersion.ActiveLinkColor = System.Drawing.Color.Blue;
            resources.ApplyResources(this.lblVersion, "lblVersion");
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.UseCompatibleTextRendering = true;
            this.lblVersion.VisitedLinkColor = System.Drawing.Color.Blue;
            this.lblVersion.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblVersion_LinkClicked);
            // 
            // AboutDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.closebtn;
            this.ControlBox = false;
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.btnHomepage);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.linkLabel3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.closebtn);
            this.Controls.Add(this.lblCopyright);
            this.Controls.Add(this.lblName);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.Button closebtn;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.TextBox txtLicense;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView listPlugins;
        private System.Windows.Forms.ColumnHeader colpluginName;
        private System.Windows.Forms.ColumnHeader colpluginVersion;
        private System.Windows.Forms.ColumnHeader colpluginFilename;
        private System.Windows.Forms.ColumnHeader colpluginAuthor;
        private System.Windows.Forms.Button btnHomepage;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox txtFontLicense;
        private System.Windows.Forms.LinkLabel lblVersion;
    }
}