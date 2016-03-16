using Tabster.Controls;
using Tabster.WinForms;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TabDetailsDialog));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBoxLibrary = new System.Windows.Forms.GroupBox();
            this.lblLastViewed = new System.Windows.Forms.Label();
            this.lblPlaylistCount = new System.Windows.Forms.Label();
            this.lblViewCount = new System.Windows.Forms.Label();
            this.lblfavorited = new System.Windows.Forms.Label();
            this.groupBoxFile = new System.Windows.Forms.GroupBox();
            this.lblEncoding = new System.Windows.Forms.Label();
            this.lblCompressed = new System.Windows.Forms.Label();
            this.lblModified = new System.Windows.Forms.Label();
            this.lblCreated = new System.Windows.Forms.Label();
            this.lblLength = new System.Windows.Forms.Label();
            this.lblFormat = new System.Windows.Forms.Label();
            this.groupBoxTablature = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtGenre = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtAlbum = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCopyright = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSubtitle = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tablatureDifficultyDropdown1 = new Tabster.WinForms.TablatureDifficultyDropdown();
            this.label2 = new System.Windows.Forms.Label();
            this.tablatureTuningDropdown1 = new Tabster.WinForms.TablatureTuningDropdown();
            this.label1 = new System.Windows.Forms.Label();
            this.tablatureRatingDropdown1 = new Tabster.WinForms.TablatureRatingDropdown();
            this.typeList = new Tabster.WinForms.TabTypeDropdown();
            this.label16 = new System.Windows.Forms.Label();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtArtist = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtLyrics = new Tabster.Controls.TextBoxExtended();
            this.okbtn = new System.Windows.Forms.Button();
            this.cancelbtn = new System.Windows.Forms.Button();
            this.txtlocation = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBoxLibrary.SuspendLayout();
            this.groupBoxFile.SuspendLayout();
            this.groupBoxTablature.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBoxLibrary);
            this.tabPage1.Controls.Add(this.groupBoxFile);
            this.tabPage1.Controls.Add(this.groupBoxTablature);
            this.tabPage1.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBoxLibrary
            // 
            resources.ApplyResources(this.groupBoxLibrary, "groupBoxLibrary");
            this.groupBoxLibrary.Controls.Add(this.lblLastViewed);
            this.groupBoxLibrary.Controls.Add(this.lblPlaylistCount);
            this.groupBoxLibrary.Controls.Add(this.lblViewCount);
            this.groupBoxLibrary.Controls.Add(this.lblfavorited);
            this.groupBoxLibrary.Name = "groupBoxLibrary";
            this.groupBoxLibrary.TabStop = false;
            // 
            // lblLastViewed
            // 
            resources.ApplyResources(this.lblLastViewed, "lblLastViewed");
            this.lblLastViewed.Name = "lblLastViewed";
            // 
            // lblPlaylistCount
            // 
            resources.ApplyResources(this.lblPlaylistCount, "lblPlaylistCount");
            this.lblPlaylistCount.Name = "lblPlaylistCount";
            // 
            // lblViewCount
            // 
            resources.ApplyResources(this.lblViewCount, "lblViewCount");
            this.lblViewCount.Name = "lblViewCount";
            // 
            // lblfavorited
            // 
            resources.ApplyResources(this.lblfavorited, "lblfavorited");
            this.lblfavorited.Name = "lblfavorited";
            // 
            // groupBoxFile
            // 
            resources.ApplyResources(this.groupBoxFile, "groupBoxFile");
            this.groupBoxFile.Controls.Add(this.lblEncoding);
            this.groupBoxFile.Controls.Add(this.lblCompressed);
            this.groupBoxFile.Controls.Add(this.lblModified);
            this.groupBoxFile.Controls.Add(this.lblCreated);
            this.groupBoxFile.Controls.Add(this.lblLength);
            this.groupBoxFile.Controls.Add(this.lblFormat);
            this.groupBoxFile.Name = "groupBoxFile";
            this.groupBoxFile.TabStop = false;
            // 
            // lblEncoding
            // 
            resources.ApplyResources(this.lblEncoding, "lblEncoding");
            this.lblEncoding.Name = "lblEncoding";
            // 
            // lblCompressed
            // 
            resources.ApplyResources(this.lblCompressed, "lblCompressed");
            this.lblCompressed.Name = "lblCompressed";
            // 
            // lblModified
            // 
            resources.ApplyResources(this.lblModified, "lblModified");
            this.lblModified.Name = "lblModified";
            // 
            // lblCreated
            // 
            resources.ApplyResources(this.lblCreated, "lblCreated");
            this.lblCreated.Name = "lblCreated";
            // 
            // lblLength
            // 
            resources.ApplyResources(this.lblLength, "lblLength");
            this.lblLength.Name = "lblLength";
            // 
            // lblFormat
            // 
            resources.ApplyResources(this.lblFormat, "lblFormat");
            this.lblFormat.Name = "lblFormat";
            // 
            // groupBoxTablature
            // 
            resources.ApplyResources(this.groupBoxTablature, "groupBoxTablature");
            this.groupBoxTablature.Controls.Add(this.label9);
            this.groupBoxTablature.Controls.Add(this.txtGenre);
            this.groupBoxTablature.Controls.Add(this.label10);
            this.groupBoxTablature.Controls.Add(this.txtAlbum);
            this.groupBoxTablature.Controls.Add(this.label8);
            this.groupBoxTablature.Controls.Add(this.txtCopyright);
            this.groupBoxTablature.Controls.Add(this.label7);
            this.groupBoxTablature.Controls.Add(this.txtAuthor);
            this.groupBoxTablature.Controls.Add(this.label6);
            this.groupBoxTablature.Controls.Add(this.txtSubtitle);
            this.groupBoxTablature.Controls.Add(this.label4);
            this.groupBoxTablature.Controls.Add(this.tablatureDifficultyDropdown1);
            this.groupBoxTablature.Controls.Add(this.label2);
            this.groupBoxTablature.Controls.Add(this.tablatureTuningDropdown1);
            this.groupBoxTablature.Controls.Add(this.label1);
            this.groupBoxTablature.Controls.Add(this.tablatureRatingDropdown1);
            this.groupBoxTablature.Controls.Add(this.typeList);
            this.groupBoxTablature.Controls.Add(this.label16);
            this.groupBoxTablature.Controls.Add(this.txtComment);
            this.groupBoxTablature.Controls.Add(this.label17);
            this.groupBoxTablature.Controls.Add(this.label5);
            this.groupBoxTablature.Controls.Add(this.txtArtist);
            this.groupBoxTablature.Controls.Add(this.label3);
            this.groupBoxTablature.Controls.Add(this.txtTitle);
            this.groupBoxTablature.Name = "groupBoxTablature";
            this.groupBoxTablature.TabStop = false;
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label9.Name = "label9";
            // 
            // txtGenre
            // 
            this.txtGenre.BackColor = System.Drawing.SystemColors.Window;
            this.txtGenre.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.txtGenre, "txtGenre");
            this.txtGenre.Name = "txtGenre";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label10.Name = "label10";
            // 
            // txtAlbum
            // 
            this.txtAlbum.BackColor = System.Drawing.SystemColors.Window;
            this.txtAlbum.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.txtAlbum, "txtAlbum");
            this.txtAlbum.Name = "txtAlbum";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Name = "label8";
            // 
            // txtCopyright
            // 
            this.txtCopyright.BackColor = System.Drawing.SystemColors.Window;
            this.txtCopyright.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.txtCopyright, "txtCopyright");
            this.txtCopyright.Name = "txtCopyright";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Name = "label7";
            // 
            // txtAuthor
            // 
            this.txtAuthor.BackColor = System.Drawing.SystemColors.Window;
            this.txtAuthor.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.txtAuthor, "txtAuthor");
            this.txtAuthor.Name = "txtAuthor";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Name = "label6";
            // 
            // txtSubtitle
            // 
            this.txtSubtitle.BackColor = System.Drawing.SystemColors.Window;
            this.txtSubtitle.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.txtSubtitle, "txtSubtitle");
            this.txtSubtitle.Name = "txtSubtitle";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Name = "label4";
            // 
            // tablatureDifficultyDropdown1
            // 
            this.tablatureDifficultyDropdown1.DefaultText = " ";
            this.tablatureDifficultyDropdown1.DisplayDefault = true;
            resources.ApplyResources(this.tablatureDifficultyDropdown1, "tablatureDifficultyDropdown1");
            this.tablatureDifficultyDropdown1.Name = "tablatureDifficultyDropdown1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Name = "label2";
            // 
            // tablatureTuningDropdown1
            // 
            this.tablatureTuningDropdown1.DefaultText = " ";
            this.tablatureTuningDropdown1.DisplayDefault = true;
            resources.ApplyResources(this.tablatureTuningDropdown1, "tablatureTuningDropdown1");
            this.tablatureTuningDropdown1.Name = "tablatureTuningDropdown1";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Name = "label1";
            // 
            // tablatureRatingDropdown1
            // 
            this.tablatureRatingDropdown1.DefaultText = "No Rating";
            this.tablatureRatingDropdown1.DisplayDefault = true;
            resources.ApplyResources(this.tablatureRatingDropdown1, "tablatureRatingDropdown1");
            this.tablatureRatingDropdown1.Name = "tablatureRatingDropdown1";
            // 
            // typeList
            // 
            this.typeList.DefaultText = "";
            this.typeList.DisplayDefault = false;
            resources.ApplyResources(this.typeList, "typeList");
            this.typeList.Name = "typeList";
            this.typeList.UsePluralizedNames = false;
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label16.Name = "label16";
            // 
            // txtComment
            // 
            this.txtComment.BackColor = System.Drawing.SystemColors.Window;
            this.txtComment.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.txtComment, "txtComment");
            this.txtComment.Name = "txtComment";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label17.Name = "label17";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Name = "label5";
            // 
            // txtArtist
            // 
            this.txtArtist.BackColor = System.Drawing.SystemColors.Window;
            this.txtArtist.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.txtArtist, "txtArtist");
            this.txtArtist.Name = "txtArtist";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Name = "label3";
            // 
            // txtTitle
            // 
            this.txtTitle.BackColor = System.Drawing.SystemColors.Window;
            this.txtTitle.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.txtTitle, "txtTitle");
            this.txtTitle.Name = "txtTitle";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txtLyrics);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtLyrics
            // 
            this.txtLyrics.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.txtLyrics, "txtLyrics");
            this.txtLyrics.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtLyrics.Name = "txtLyrics";
            this.txtLyrics.PlaceholderForecolor = System.Drawing.SystemColors.GrayText;
            this.txtLyrics.PlaceholderText = null;
            this.txtLyrics.SelectOnFocus = false;
            this.txtLyrics.TextChangedDelay = 0;
            // 
            // okbtn
            // 
            resources.ApplyResources(this.okbtn, "okbtn");
            this.okbtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okbtn.Name = "okbtn";
            this.okbtn.UseVisualStyleBackColor = true;
            this.okbtn.Click += new System.EventHandler(this.okbtn_Click);
            // 
            // cancelbtn
            // 
            resources.ApplyResources(this.cancelbtn, "cancelbtn");
            this.cancelbtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelbtn.Name = "cancelbtn";
            this.cancelbtn.UseVisualStyleBackColor = true;
            // 
            // txtlocation
            // 
            resources.ApplyResources(this.txtlocation, "txtlocation");
            this.txtlocation.BackColor = System.Drawing.SystemColors.Control;
            this.txtlocation.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtlocation.Name = "txtlocation";
            this.txtlocation.ReadOnly = true;
            // 
            // TabDetailsDialog
            // 
            this.AcceptButton = this.okbtn;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelbtn;
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
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBoxLibrary.ResumeLayout(false);
            this.groupBoxLibrary.PerformLayout();
            this.groupBoxFile.ResumeLayout(false);
            this.groupBoxFile.PerformLayout();
            this.groupBoxTablature.ResumeLayout(false);
            this.groupBoxTablature.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtComment;
        public System.Windows.Forms.TextBox txtArtist;
        public System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        public System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button okbtn;
        private System.Windows.Forms.Button cancelbtn;
        public System.Windows.Forms.TextBox txtlocation;
        private System.Windows.Forms.GroupBox groupBoxTablature;
        private System.Windows.Forms.GroupBox groupBoxFile;
        private System.Windows.Forms.Label lblFormat;
        private System.Windows.Forms.Label lblLength;
        private System.Windows.Forms.Label lblModified;
        private System.Windows.Forms.Label lblCreated;
        private System.Windows.Forms.GroupBox groupBoxLibrary;
        private System.Windows.Forms.Label lblfavorited;
        private System.Windows.Forms.Label lblPlaylistCount;
        private System.Windows.Forms.Label lblViewCount;
        private System.Windows.Forms.Label lblLastViewed;
        private TabTypeDropdown typeList;
        private TablatureRatingDropdown tablatureRatingDropdown1;
        private System.Windows.Forms.Label label1;
        private TablatureTuningDropdown tablatureTuningDropdown1;
        private System.Windows.Forms.Label lblCompressed;
        private System.Windows.Forms.Label lblEncoding;
        private System.Windows.Forms.TabPage tabPage3;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label4;
        private TablatureDifficultyDropdown tablatureDifficultyDropdown1;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox txtSubtitle;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox txtCopyright;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox txtGenre;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox txtAlbum;
        public TextBoxExtended txtLyrics;
    }
}