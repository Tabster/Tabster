using Tabster.WinForms;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DownloadDialog));
            this.txtUrls = new System.Windows.Forms.TextBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.listDownloads = new System.Windows.Forms.ListView();
            this.colUrl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.DownloadBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnLibrary = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.typeList = new Tabster.WinForms.TabTypeDropdown();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtArtist = new System.Windows.Forms.TextBox();
            this.btnUrls = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtUrls
            // 
            resources.ApplyResources(this.txtUrls, "txtUrls");
            this.txtUrls.Name = "txtUrls";
            this.txtUrls.TextChanged += new System.EventHandler(this.txtUrls_TextChanged);
            this.txtUrls.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUrls_KeyDown);
            // 
            // btnDownload
            // 
            resources.ApplyResources(this.btnDownload, "btnDownload");
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // listDownloads
            // 
            resources.ApplyResources(this.listDownloads, "listDownloads");
            this.listDownloads.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colUrl,
            this.colStatus});
            this.listDownloads.FullRowSelect = true;
            this.listDownloads.GridLines = true;
            this.listDownloads.MultiSelect = false;
            this.listDownloads.Name = "listDownloads";
            this.listDownloads.UseCompatibleStateImageBehavior = false;
            this.listDownloads.View = System.Windows.Forms.View.Details;
            this.listDownloads.SelectedIndexChanged += new System.EventHandler(this.listDownloads_SelectedIndexChanged);
            // 
            // colUrl
            // 
            resources.ApplyResources(this.colUrl, "colUrl");
            // 
            // colStatus
            // 
            resources.ApplyResources(this.colStatus, "colStatus");
            // 
            // progressBar1
            // 
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.Name = "progressBar1";
            // 
            // DownloadBackgroundWorker
            // 
            this.DownloadBackgroundWorker.WorkerReportsProgress = true;
            this.DownloadBackgroundWorker.WorkerSupportsCancellation = true;
            this.DownloadBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.DownloadBackgroundWorker_DoWork);
            this.DownloadBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.DownloadBackgroundWorker_ProgressChanged);
            this.DownloadBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.DownloadBackgroundWorker_RunWorkerCompleted);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnLibrary
            // 
            resources.ApplyResources(this.btnLibrary, "btnLibrary");
            this.btnLibrary.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnLibrary.Name = "btnLibrary";
            this.btnLibrary.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnUpdate);
            this.groupBox1.Controls.Add(this.typeList);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtTitle);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtArtist);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // btnUpdate
            // 
            resources.ApplyResources(this.btnUpdate, "btnUpdate");
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // typeList
            // 
            this.typeList.DefaultText = "";
            this.typeList.DisplayDefault = false;
            resources.ApplyResources(this.typeList, "typeList");
            this.typeList.Name = "typeList";
            this.typeList.UsePluralizedNames = false;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Name = "label3";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Name = "label1";
            // 
            // txtTitle
            // 
            this.txtTitle.BackColor = System.Drawing.SystemColors.Window;
            this.txtTitle.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.txtTitle, "txtTitle");
            this.txtTitle.Name = "txtTitle";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Name = "label2";
            // 
            // txtArtist
            // 
            this.txtArtist.BackColor = System.Drawing.SystemColors.Window;
            this.txtArtist.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.txtArtist, "txtArtist");
            this.txtArtist.Name = "txtArtist";
            // 
            // btnUrls
            // 
            resources.ApplyResources(this.btnUrls, "btnUrls");
            this.btnUrls.Name = "btnUrls";
            this.btnUrls.UseVisualStyleBackColor = true;
            this.btnUrls.Click += new System.EventHandler(this.btnUrls_Click);
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.progressBar1);
            this.splitContainer1.Panel1.Controls.Add(this.btnCancel);
            this.splitContainer1.Panel1.Controls.Add(this.btnUrls);
            this.splitContainer1.Panel1.Controls.Add(this.txtUrls);
            this.splitContainer1.Panel1.Controls.Add(this.listDownloads);
            this.splitContainer1.Panel1.Controls.Add(this.btnDownload);
            this.splitContainer1.Panel1.Controls.Add(this.btnLibrary);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            // 
            // DownloadDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DownloadDialog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DownloadDialog_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtUrls;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView listDownloads;
        private System.Windows.Forms.ColumnHeader colUrl;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker DownloadBackgroundWorker;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnLibrary;
        private System.Windows.Forms.GroupBox groupBox1;
        private TabTypeDropdown typeList;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtTitle;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtArtist;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnUrls;
        private System.Windows.Forms.SplitContainer splitContainer1;

    }
}