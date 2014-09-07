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
            this.listPlugins = new System.Windows.Forms.ListView();
            this.colPluginEnabled = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colpluginName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colpluginVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colpluginFilename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colpluginDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
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
            this.cancelbtn.Location = new System.Drawing.Point(529, 251);
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
            this.okbtn.Location = new System.Drawing.Point(448, 251);
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
            this.tabControl1.Location = new System.Drawing.Point(13, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(589, 232);
            this.tabControl1.TabIndex = 24;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chkupdatestartup);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(624, 206);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.listPlugins);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(581, 206);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Plugins";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // listPlugins
            // 
            this.listPlugins.CheckBoxes = true;
            this.listPlugins.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colPluginEnabled,
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
            this.listPlugins.Size = new System.Drawing.Size(575, 200);
            this.listPlugins.TabIndex = 0;
            this.listPlugins.UseCompatibleStateImageBehavior = false;
            this.listPlugins.View = System.Windows.Forms.View.Details;
            this.listPlugins.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listPlugins_ItemChecked);
            // 
            // colPluginEnabled
            // 
            this.colPluginEnabled.Text = "Enabled";
            this.colPluginEnabled.Width = 55;
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
            // PreferencesDialog
            // 
            this.AcceptButton = this.okbtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.cancelbtn;
            this.ClientSize = new System.Drawing.Size(614, 284);
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkupdatestartup;
        private System.Windows.Forms.Button cancelbtn;
        private System.Windows.Forms.Button okbtn;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView listPlugins;
        private System.Windows.Forms.ColumnHeader colpluginName;
        private System.Windows.Forms.ColumnHeader colpluginFilename;
        private System.Windows.Forms.ColumnHeader colpluginVersion;
        private System.Windows.Forms.ColumnHeader colPluginEnabled;
        private System.Windows.Forms.ColumnHeader colpluginDescription;
    }
}