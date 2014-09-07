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
            this.label2 = new System.Windows.Forms.Label();
            this.txtartist = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txttitle = new System.Windows.Forms.TextBox();
            this.okbtn = new System.Windows.Forms.Button();
            this.cancelbtn = new System.Windows.Forms.Button();
            this.browsebtn = new System.Windows.Forms.Button();
            this.lblfile = new System.Windows.Forms.Label();
            this.txtimportfile = new System.Windows.Forms.TextBox();
            this.chkusertab = new System.Windows.Forms.CheckBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.typeList = new Tabster.Controls.TabTypeDropdown();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(4, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Type:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(5, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Artist:";
            // 
            // txtartist
            // 
            this.txtartist.BackColor = System.Drawing.SystemColors.Window;
            this.txtartist.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtartist.Location = new System.Drawing.Point(44, 38);
            this.txtartist.Name = "txtartist";
            this.txtartist.Size = new System.Drawing.Size(206, 20);
            this.txtartist.TabIndex = 0;
            this.txtartist.TextChanged += new System.EventHandler(this.ValidateData);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(8, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Title:";
            // 
            // txttitle
            // 
            this.txttitle.BackColor = System.Drawing.SystemColors.Window;
            this.txttitle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txttitle.Location = new System.Drawing.Point(44, 64);
            this.txttitle.Name = "txttitle";
            this.txttitle.Size = new System.Drawing.Size(206, 20);
            this.txttitle.TabIndex = 1;
            this.txttitle.TextChanged += new System.EventHandler(this.ValidateData);
            // 
            // okbtn
            // 
            this.okbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okbtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okbtn.Enabled = false;
            this.okbtn.Location = new System.Drawing.Point(247, 115);
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
            this.cancelbtn.Location = new System.Drawing.Point(328, 115);
            this.cancelbtn.Name = "cancelbtn";
            this.cancelbtn.Size = new System.Drawing.Size(75, 23);
            this.cancelbtn.TabIndex = 5;
            this.cancelbtn.Text = "Cancel";
            this.cancelbtn.UseVisualStyleBackColor = true;
            // 
            // browsebtn
            // 
            this.browsebtn.Location = new System.Drawing.Point(328, 10);
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
            this.lblfile.Location = new System.Drawing.Point(12, 15);
            this.lblfile.Name = "lblfile";
            this.lblfile.Size = new System.Drawing.Size(26, 13);
            this.lblfile.TabIndex = 34;
            this.lblfile.Text = "File:";
            // 
            // txtimportfile
            // 
            this.txtimportfile.BackColor = System.Drawing.SystemColors.Window;
            this.txtimportfile.Enabled = false;
            this.txtimportfile.ForeColor = System.Drawing.Color.Black;
            this.txtimportfile.Location = new System.Drawing.Point(44, 12);
            this.txtimportfile.Name = "txtimportfile";
            this.txtimportfile.ReadOnly = true;
            this.txtimportfile.Size = new System.Drawing.Size(278, 20);
            this.txtimportfile.TabIndex = 33;
            this.txtimportfile.TextChanged += new System.EventHandler(this.ValidateData);
            // 
            // chkusertab
            // 
            this.chkusertab.AutoSize = true;
            this.chkusertab.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkusertab.Location = new System.Drawing.Point(281, 53);
            this.chkusertab.Name = "chkusertab";
            this.chkusertab.Size = new System.Drawing.Size(105, 17);
            this.chkusertab.TabIndex = 36;
            this.chkusertab.Text = "I created this tab";
            this.chkusertab.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Title = "Import File - Tabster";
            // 
            // typeList
            // 
            this.typeList.DefaultText = "";
            this.typeList.DisplayDefault = false;
            this.typeList.Location = new System.Drawing.Point(44, 90);
            this.typeList.Name = "typeList";
            this.typeList.Size = new System.Drawing.Size(119, 21);
            this.typeList.TabIndex = 37;
            this.typeList.UsePluralizedNames = false;
            this.typeList.TypeChanged += new System.EventHandler(this.ValidateData);
            // 
            // ImportDialog
            // 
            this.AcceptButton = this.okbtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.cancelbtn;
            this.ClientSize = new System.Drawing.Size(415, 154);
            this.Controls.Add(this.typeList);
            this.Controls.Add(this.chkusertab);
            this.Controls.Add(this.browsebtn);
            this.Controls.Add(this.lblfile);
            this.Controls.Add(this.txtimportfile);
            this.Controls.Add(this.okbtn);
            this.Controls.Add(this.cancelbtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txttitle);
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
            this.Load += new System.EventHandler(this.ImportDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtartist;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txttitle;
        private System.Windows.Forms.Button okbtn;
        private System.Windows.Forms.Button cancelbtn;
        public System.Windows.Forms.Button browsebtn;
        private System.Windows.Forms.Label lblfile;
        public System.Windows.Forms.TextBox txtimportfile;
        public System.Windows.Forms.CheckBox chkusertab;
        private Controls.TabTypeDropdown typeList;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}