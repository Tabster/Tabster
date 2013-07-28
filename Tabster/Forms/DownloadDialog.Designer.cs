namespace Tabster.Forms
{
    partial class DownloadDialog
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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.lblprogress = new System.Windows.Forms.Label();
            this.txturls = new System.Windows.Forms.TextBox();
            this.startbtn = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.col_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_url = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.closebtn = new System.Windows.Forms.Button();
            this.addtolibrarybtn = new System.Windows.Forms.Button();
            this.resetbtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // lblprogress
            // 
            this.lblprogress.AutoSize = true;
            this.lblprogress.Location = new System.Drawing.Point(12, 202);
            this.lblprogress.Name = "lblprogress";
            this.lblprogress.Size = new System.Drawing.Size(35, 13);
            this.lblprogress.TabIndex = 2;
            this.lblprogress.Text = "label1";
            this.lblprogress.Visible = false;
            // 
            // txturls
            // 
            this.txturls.Location = new System.Drawing.Point(12, 33);
            this.txturls.Multiline = true;
            this.txturls.Name = "txturls";
            this.txturls.Size = new System.Drawing.Size(588, 166);
            this.txturls.TabIndex = 0;
            this.txturls.TextChanged += new System.EventHandler(this.txturls_TextChanged);
            // 
            // startbtn
            // 
            this.startbtn.Enabled = false;
            this.startbtn.Location = new System.Drawing.Point(473, 205);
            this.startbtn.Name = "startbtn";
            this.startbtn.Size = new System.Drawing.Size(127, 23);
            this.startbtn.TabIndex = 1;
            this.startbtn.Text = "Begin Download";
            this.startbtn.UseVisualStyleBackColor = true;
            this.startbtn.Click += new System.EventHandler(this.startbtn_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 218);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(588, 14);
            this.progressBar1.TabIndex = 3;
            this.progressBar1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(335, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Here you can add Ultimate Guitar links to download. Add one per line.";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.No;
            this.button1.Location = new System.Drawing.Point(525, 238);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col_name,
            this.col_status,
            this.col_url});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(12, 33);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(588, 166);
            this.listView1.TabIndex = 7;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // col_name
            // 
            this.col_name.Text = "Name";
            this.col_name.Width = 149;
            // 
            // col_status
            // 
            this.col_status.Text = "Status";
            this.col_status.Width = 78;
            // 
            // col_url
            // 
            this.col_url.Text = "URL";
            this.col_url.Width = 352;
            // 
            // closebtn
            // 
            this.closebtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closebtn.Location = new System.Drawing.Point(12, 238);
            this.closebtn.Name = "closebtn";
            this.closebtn.Size = new System.Drawing.Size(75, 23);
            this.closebtn.TabIndex = 8;
            this.closebtn.Text = "Close";
            this.closebtn.UseVisualStyleBackColor = true;
            this.closebtn.Click += new System.EventHandler(this.closebtn_Click);
            // 
            // addtolibrarybtn
            // 
            this.addtolibrarybtn.Location = new System.Drawing.Point(473, 238);
            this.addtolibrarybtn.Name = "addtolibrarybtn";
            this.addtolibrarybtn.Size = new System.Drawing.Size(127, 23);
            this.addtolibrarybtn.TabIndex = 9;
            this.addtolibrarybtn.Text = "Add to Library";
            this.addtolibrarybtn.UseVisualStyleBackColor = true;
            this.addtolibrarybtn.Visible = false;
            this.addtolibrarybtn.Click += new System.EventHandler(this.addtolibrarybtn_Click);
            // 
            // resetbtn
            // 
            this.resetbtn.Location = new System.Drawing.Point(473, 238);
            this.resetbtn.Name = "resetbtn";
            this.resetbtn.Size = new System.Drawing.Size(127, 23);
            this.resetbtn.TabIndex = 10;
            this.resetbtn.Text = "Download More";
            this.resetbtn.UseVisualStyleBackColor = true;
            this.resetbtn.Visible = false;
            this.resetbtn.Click += new System.EventHandler(this.resetbtn_Click);
            // 
            // DownloadDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closebtn;
            this.ClientSize = new System.Drawing.Size(612, 273);
            this.Controls.Add(this.addtolibrarybtn);
            this.Controls.Add(this.startbtn);
            this.Controls.Add(this.closebtn);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblprogress);
            this.Controls.Add(this.txturls);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.resetbtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DownloadDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Multi-Downloader";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label lblprogress;
        private System.Windows.Forms.TextBox txturls;
        private System.Windows.Forms.Button startbtn;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader col_name;
        private System.Windows.Forms.ColumnHeader col_status;
        private System.Windows.Forms.ColumnHeader col_url;
        private System.Windows.Forms.Button closebtn;
        private System.Windows.Forms.Button addtolibrarybtn;
        private System.Windows.Forms.Button resetbtn;
    }
}