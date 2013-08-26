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
            this.SuspendLayout();
            // 
            // chkupdatestartup
            // 
            this.chkupdatestartup.AutoSize = true;
            this.chkupdatestartup.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkupdatestartup.Location = new System.Drawing.Point(12, 12);
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
            this.cancelbtn.Location = new System.Drawing.Point(258, 125);
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
            this.okbtn.Location = new System.Drawing.Point(177, 125);
            this.okbtn.Name = "okbtn";
            this.okbtn.Size = new System.Drawing.Size(75, 23);
            this.okbtn.TabIndex = 23;
            this.okbtn.Text = "OK";
            this.okbtn.UseVisualStyleBackColor = true;
            this.okbtn.Click += new System.EventHandler(this.okbtn_Click);
            // 
            // PreferencesDialog
            // 
            this.AcceptButton = this.okbtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.cancelbtn;
            this.ClientSize = new System.Drawing.Size(343, 158);
            this.Controls.Add(this.okbtn);
            this.Controls.Add(this.cancelbtn);
            this.Controls.Add(this.chkupdatestartup);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PreferencesDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Preferences";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkupdatestartup;
        private System.Windows.Forms.Button cancelbtn;
        private System.Windows.Forms.Button okbtn;
    }
}