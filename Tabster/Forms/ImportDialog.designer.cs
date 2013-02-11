namespace Tabster.Forms
{
    partial class ImportDialog
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
            this.label3 = new System.Windows.Forms.Label();
            this.txttype = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtartist = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtsong = new System.Windows.Forms.TextBox();
            this.okbtn = new System.Windows.Forms.Button();
            this.cancelbtn = new System.Windows.Forms.Button();
            this.browsebtn = new System.Windows.Forms.Button();
            this.lblfile = new System.Windows.Forms.Label();
            this.txtimportfile = new System.Windows.Forms.TextBox();
            this.chkusertab = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(11, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Type:";
            // 
            // txttype
            // 
            this.txttype.BackColor = System.Drawing.SystemColors.Window;
            this.txttype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txttype.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txttype.FormattingEnabled = true;
            this.txttype.ItemHeight = 13;
            this.txttype.Location = new System.Drawing.Point(53, 59);
            this.txttype.Name = "txttype";
            this.txttype.Size = new System.Drawing.Size(119, 21);
            this.txttype.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Artist:";
            // 
            // txtartist
            // 
            this.txtartist.BackColor = System.Drawing.SystemColors.Window;
            this.txtartist.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtartist.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtartist.Location = new System.Drawing.Point(53, 7);
            this.txtartist.Name = "txtartist";
            this.txtartist.Size = new System.Drawing.Size(206, 20);
            this.txtartist.TabIndex = 0;
            this.txtartist.TextChanged += new System.EventHandler(this.txtartist_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(10, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Song:";
            // 
            // txtsong
            // 
            this.txtsong.BackColor = System.Drawing.SystemColors.Window;
            this.txtsong.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtsong.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtsong.Location = new System.Drawing.Point(53, 33);
            this.txtsong.Name = "txtsong";
            this.txtsong.Size = new System.Drawing.Size(206, 20);
            this.txtsong.TabIndex = 1;
            this.txtsong.TextChanged += new System.EventHandler(this.txtsong_TextChanged);
            // 
            // okbtn
            // 
            this.okbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okbtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okbtn.Enabled = false;
            this.okbtn.Location = new System.Drawing.Point(103, 188);
            this.okbtn.Name = "okbtn";
            this.okbtn.Size = new System.Drawing.Size(75, 23);
            this.okbtn.TabIndex = 4;
            this.okbtn.Text = "OK";
            this.okbtn.UseVisualStyleBackColor = true;
            this.okbtn.Click += new System.EventHandler(this.okbtn_Click);
            // 
            // cancelbtn
            // 
            this.cancelbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelbtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelbtn.Location = new System.Drawing.Point(184, 188);
            this.cancelbtn.Name = "cancelbtn";
            this.cancelbtn.Size = new System.Drawing.Size(75, 23);
            this.cancelbtn.TabIndex = 5;
            this.cancelbtn.Text = "Cancel";
            this.cancelbtn.UseVisualStyleBackColor = true;
            // 
            // browsebtn
            // 
            this.browsebtn.Location = new System.Drawing.Point(53, 112);
            this.browsebtn.Name = "browsebtn";
            this.browsebtn.Size = new System.Drawing.Size(75, 23);
            this.browsebtn.TabIndex = 35;
            this.browsebtn.Text = "Browse...";
            this.browsebtn.UseVisualStyleBackColor = true;
            this.browsebtn.Click += new System.EventHandler(this.browsebtn_Click);
            // 
            // lblfile
            // 
            this.lblfile.AutoSize = true;
            this.lblfile.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblfile.Location = new System.Drawing.Point(19, 88);
            this.lblfile.Name = "lblfile";
            this.lblfile.Size = new System.Drawing.Size(26, 13);
            this.lblfile.TabIndex = 34;
            this.lblfile.Text = "File:";
            // 
            // txtimportfile
            // 
            this.txtimportfile.BackColor = System.Drawing.SystemColors.Window;
            this.txtimportfile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtimportfile.Enabled = false;
            this.txtimportfile.ForeColor = System.Drawing.Color.Black;
            this.txtimportfile.Location = new System.Drawing.Point(54, 86);
            this.txtimportfile.Name = "txtimportfile";
            this.txtimportfile.ReadOnly = true;
            this.txtimportfile.Size = new System.Drawing.Size(205, 20);
            this.txtimportfile.TabIndex = 33;
            this.txtimportfile.TextChanged += new System.EventHandler(this.txtimportfile_TextChanged);
            // 
            // chkusertab
            // 
            this.chkusertab.AutoSize = true;
            this.chkusertab.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkusertab.Location = new System.Drawing.Point(53, 153);
            this.chkusertab.Name = "chkusertab";
            this.chkusertab.Size = new System.Drawing.Size(105, 17);
            this.chkusertab.TabIndex = 36;
            this.chkusertab.Text = "I created this tab";
            this.chkusertab.UseVisualStyleBackColor = true;
            // 
            // ImportDialog
            // 
            this.AcceptButton = this.okbtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.cancelbtn;
            this.ClientSize = new System.Drawing.Size(271, 223);
            this.Controls.Add(this.chkusertab);
            this.Controls.Add(this.browsebtn);
            this.Controls.Add(this.lblfile);
            this.Controls.Add(this.txtimportfile);
            this.Controls.Add(this.okbtn);
            this.Controls.Add(this.cancelbtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txttype);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtsong);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtartist);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import Tab";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox txttype;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtartist;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtsong;
        private System.Windows.Forms.Button okbtn;
        private System.Windows.Forms.Button cancelbtn;
        public System.Windows.Forms.Button browsebtn;
        private System.Windows.Forms.Label lblfile;
        public System.Windows.Forms.TextBox txtimportfile;
        public System.Windows.Forms.CheckBox chkusertab;
    }
}