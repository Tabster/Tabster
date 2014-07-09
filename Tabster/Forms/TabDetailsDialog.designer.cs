namespace Tabster.Forms
{
    partial class TabDetailsDialog
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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblPlaylistCount = new System.Windows.Forms.Label();
            this.lblfavorited = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblModified = new System.Windows.Forms.Label();
            this.lblCreated = new System.Windows.Forms.Label();
            this.lblLength = new System.Windows.Forms.Label();
            this.lblFormat = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtcomment = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtartist = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txttype = new System.Windows.Forms.ComboBox();
            this.txtsong = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtlyrics = new System.Windows.Forms.TextBox();
            this.okbtn = new System.Windows.Forms.Button();
            this.cancelbtn = new System.Windows.Forms.Button();
            this.txtlocation = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(488, 201);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.ForeColor = System.Drawing.Color.Black;
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(480, 175);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Summary";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.lblPlaylistCount);
            this.groupBox3.Controls.Add(this.lblfavorited);
            this.groupBox3.Location = new System.Drawing.Point(277, 104);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(197, 65);
            this.groupBox3.TabIndex = 40;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Library Information:";
            this.groupBox3.Enter += new System.EventHandler(this.groupBox3_Enter);
            // 
            // lblPlaylistCount
            // 
            this.lblPlaylistCount.AutoSize = true;
            this.lblPlaylistCount.Location = new System.Drawing.Point(6, 33);
            this.lblPlaylistCount.Name = "lblPlaylistCount";
            this.lblPlaylistCount.Size = new System.Drawing.Size(99, 13);
            this.lblPlaylistCount.TabIndex = 3;
            this.lblPlaylistCount.Text = "Found in 0 playlists.";
            // 
            // lblfavorited
            // 
            this.lblfavorited.AutoSize = true;
            this.lblfavorited.Location = new System.Drawing.Point(6, 16);
            this.lblfavorited.Name = "lblfavorited";
            this.lblfavorited.Size = new System.Drawing.Size(71, 13);
            this.lblfavorited.TabIndex = 2;
            this.lblfavorited.Text = "Favorited: No";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lblModified);
            this.groupBox2.Controls.Add(this.lblCreated);
            this.groupBox2.Controls.Add(this.lblLength);
            this.groupBox2.Controls.Add(this.lblFormat);
            this.groupBox2.Location = new System.Drawing.Point(277, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(197, 92);
            this.groupBox2.TabIndex = 39;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "File Information:";
            // 
            // lblModified
            // 
            this.lblModified.AutoSize = true;
            this.lblModified.Location = new System.Drawing.Point(6, 70);
            this.lblModified.Name = "lblModified";
            this.lblModified.Size = new System.Drawing.Size(50, 13);
            this.lblModified.TabIndex = 3;
            this.lblModified.Text = "Modified:";
            // 
            // lblCreated
            // 
            this.lblCreated.AutoSize = true;
            this.lblCreated.Location = new System.Drawing.Point(6, 53);
            this.lblCreated.Name = "lblCreated";
            this.lblCreated.Size = new System.Drawing.Size(47, 13);
            this.lblCreated.TabIndex = 2;
            this.lblCreated.Text = "Created:";
            // 
            // lblLength
            // 
            this.lblLength.AutoSize = true;
            this.lblLength.Location = new System.Drawing.Point(6, 36);
            this.lblLength.Name = "lblLength";
            this.lblLength.Size = new System.Drawing.Size(43, 13);
            this.lblLength.TabIndex = 1;
            this.lblLength.Text = "Length:";
            // 
            // lblFormat
            // 
            this.lblFormat.AutoSize = true;
            this.lblFormat.Location = new System.Drawing.Point(6, 19);
            this.lblFormat.Name = "lblFormat";
            this.lblFormat.Size = new System.Drawing.Size(61, 13);
            this.lblFormat.TabIndex = 0;
            this.lblFormat.Text = "File Format:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.txtcomment);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtartist);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txttype);
            this.groupBox1.Controls.Add(this.txtsong);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(265, 163);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Metadata:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label16.Location = new System.Drawing.Point(6, 19);
            this.label16.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label16.Name = "label16";
            this.label16.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label16.Size = new System.Drawing.Size(33, 13);
            this.label16.TabIndex = 36;
            this.label16.Text = "Artist:";
            // 
            // txtcomment
            // 
            this.txtcomment.BackColor = System.Drawing.SystemColors.Window;
            this.txtcomment.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtcomment.Location = new System.Drawing.Point(66, 95);
            this.txtcomment.Multiline = true;
            this.txtcomment.Name = "txtcomment";
            this.txtcomment.Size = new System.Drawing.Size(193, 59);
            this.txtcomment.TabIndex = 4;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label17.Location = new System.Drawing.Point(6, 45);
            this.label17.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label17.Name = "label17";
            this.label17.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label17.Size = new System.Drawing.Size(30, 13);
            this.label17.TabIndex = 37;
            this.label17.Text = "Title:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(6, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Comment:";
            // 
            // txtartist
            // 
            this.txtartist.BackColor = System.Drawing.SystemColors.Window;
            this.txtartist.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtartist.Location = new System.Drawing.Point(66, 16);
            this.txtartist.Name = "txtartist";
            this.txtartist.Size = new System.Drawing.Size(193, 20);
            this.txtartist.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(6, 71);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Type:";
            // 
            // txttype
            // 
            this.txttype.BackColor = System.Drawing.SystemColors.Window;
            this.txttype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txttype.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txttype.FormattingEnabled = true;
            this.txttype.ItemHeight = 13;
            this.txttype.Location = new System.Drawing.Point(66, 68);
            this.txttype.Name = "txttype";
            this.txttype.Size = new System.Drawing.Size(136, 21);
            this.txttype.TabIndex = 2;
            // 
            // txtsong
            // 
            this.txtsong.BackColor = System.Drawing.SystemColors.Window;
            this.txtsong.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtsong.Location = new System.Drawing.Point(66, 42);
            this.txtsong.Name = "txtsong";
            this.txtsong.Size = new System.Drawing.Size(193, 20);
            this.txtsong.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txtlyrics);
            this.tabPage3.ForeColor = System.Drawing.Color.Black;
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(480, 175);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Lyrics";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtlyrics
            // 
            this.txtlyrics.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtlyrics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtlyrics.ForeColor = System.Drawing.Color.Black;
            this.txtlyrics.Location = new System.Drawing.Point(3, 3);
            this.txtlyrics.Multiline = true;
            this.txtlyrics.Name = "txtlyrics";
            this.txtlyrics.Size = new System.Drawing.Size(474, 169);
            this.txtlyrics.TabIndex = 17;
            // 
            // okbtn
            // 
            this.okbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okbtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okbtn.Location = new System.Drawing.Point(342, 245);
            this.okbtn.Name = "okbtn";
            this.okbtn.Size = new System.Drawing.Size(75, 23);
            this.okbtn.TabIndex = 25;
            this.okbtn.Text = "OK";
            this.okbtn.UseVisualStyleBackColor = true;
            this.okbtn.Click += new System.EventHandler(this.okbtn_Click);
            // 
            // cancelbtn
            // 
            this.cancelbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelbtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelbtn.Location = new System.Drawing.Point(425, 245);
            this.cancelbtn.Name = "cancelbtn";
            this.cancelbtn.Size = new System.Drawing.Size(75, 23);
            this.cancelbtn.TabIndex = 24;
            this.cancelbtn.Text = "Cancel";
            this.cancelbtn.UseVisualStyleBackColor = true;
            this.cancelbtn.Click += new System.EventHandler(this.cancelbtn_Click);
            // 
            // txtlocation
            // 
            this.txtlocation.BackColor = System.Drawing.SystemColors.Control;
            this.txtlocation.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtlocation.Location = new System.Drawing.Point(12, 219);
            this.txtlocation.Name = "txtlocation";
            this.txtlocation.ReadOnly = true;
            this.txtlocation.Size = new System.Drawing.Size(488, 20);
            this.txtlocation.TabIndex = 41;
            // 
            // TabDetailsDialog
            // 
            this.AcceptButton = this.okbtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelbtn;
            this.ClientSize = new System.Drawing.Size(512, 280);
            this.Controls.Add(this.txtlocation);
            this.Controls.Add(this.okbtn);
            this.Controls.Add(this.cancelbtn);
            this.Controls.Add(this.tabControl1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TabDetailsDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tab Details";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtcomment;
        public System.Windows.Forms.ComboBox txttype;
        public System.Windows.Forms.TextBox txtartist;
        public System.Windows.Forms.TextBox txtsong;
        private System.Windows.Forms.TabPage tabPage3;
        public System.Windows.Forms.TextBox txtlyrics;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        public System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button okbtn;
        private System.Windows.Forms.Button cancelbtn;
        public System.Windows.Forms.TextBox txtlocation;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblFormat;
        private System.Windows.Forms.Label lblLength;
        private System.Windows.Forms.Label lblModified;
        private System.Windows.Forms.Label lblCreated;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblPlaylistCount;
        private System.Windows.Forms.Label lblfavorited;
    }
}