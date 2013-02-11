namespace Tabster.Forms
{
    partial class Preferences
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
            this.internetpropertiesbtn = new System.Windows.Forms.Button();
            this.cancelbtn = new System.Windows.Forms.Button();
            this.okbtn = new System.Windows.Forms.Button();
            this.chksplash = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chkupdatestartup
            // 
            this.chkupdatestartup.AutoSize = true;
            this.chkupdatestartup.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkupdatestartup.Location = new System.Drawing.Point(12, 35);
            this.chkupdatestartup.Name = "chkupdatestartup";
            this.chkupdatestartup.Size = new System.Drawing.Size(163, 17);
            this.chkupdatestartup.TabIndex = 18;
            this.chkupdatestartup.Text = "Check for updates on startup";
            this.chkupdatestartup.UseVisualStyleBackColor = true;
            // 
            // internetpropertiesbtn
            // 
            this.internetpropertiesbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.internetpropertiesbtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.internetpropertiesbtn.Location = new System.Drawing.Point(12, 123);
            this.internetpropertiesbtn.Name = "internetpropertiesbtn";
            this.internetpropertiesbtn.Size = new System.Drawing.Size(115, 23);
            this.internetpropertiesbtn.TabIndex = 19;
            this.internetpropertiesbtn.Text = "Internet Properties";
            this.internetpropertiesbtn.UseVisualStyleBackColor = true;
            this.internetpropertiesbtn.Click += new System.EventHandler(this.internetpropertiesbtn_Click);
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
            // chksplash
            // 
            this.chksplash.AutoSize = true;
            this.chksplash.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chksplash.Location = new System.Drawing.Point(12, 12);
            this.chksplash.Name = "chksplash";
            this.chksplash.Size = new System.Drawing.Size(171, 17);
            this.chksplash.TabIndex = 51;
            this.chksplash.Text = "Show splash screen on startup";
            this.chksplash.UseVisualStyleBackColor = true;
            // 
            // Preferences
            // 
            this.AcceptButton = this.okbtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.cancelbtn;
            this.ClientSize = new System.Drawing.Size(343, 158);
            this.Controls.Add(this.chksplash);
            this.Controls.Add(this.internetpropertiesbtn);
            this.Controls.Add(this.okbtn);
            this.Controls.Add(this.cancelbtn);
            this.Controls.Add(this.chkupdatestartup);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Preferences";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Preferences";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button internetpropertiesbtn;
        private System.Windows.Forms.CheckBox chkupdatestartup;
        private System.Windows.Forms.Button cancelbtn;
        private System.Windows.Forms.Button okbtn;
        private System.Windows.Forms.CheckBox chksplash;
    }
}