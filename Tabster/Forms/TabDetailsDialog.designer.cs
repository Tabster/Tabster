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
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 38);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(640, 290);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBoxLibrary);
            this.tabPage1.Controls.Add(this.groupBoxFile);
            this.tabPage1.Controls.Add(this.groupBoxTablature);
            this.tabPage1.ForeColor = System.Drawing.Color.Black;
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(632, 264);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Basic Info";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBoxLibrary
            // 
            this.groupBoxLibrary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxLibrary.Controls.Add(this.lblLastViewed);
            this.groupBoxLibrary.Controls.Add(this.lblPlaylistCount);
            this.groupBoxLibrary.Controls.Add(this.lblViewCount);
            this.groupBoxLibrary.Controls.Add(this.lblfavorited);
            this.groupBoxLibrary.Location = new System.Drawing.Point(446, 137);
            this.groupBoxLibrary.Name = "groupBoxLibrary";
            this.groupBoxLibrary.Size = new System.Drawing.Size(180, 121);
            this.groupBoxLibrary.TabIndex = 40;
            this.groupBoxLibrary.TabStop = false;
            this.groupBoxLibrary.Text = "Library Information:";
            // 
            // lblLastViewed
            // 
            this.lblLastViewed.AutoSize = true;
            this.lblLastViewed.Location = new System.Drawing.Point(6, 50);
            this.lblLastViewed.Name = "lblLastViewed";
            this.lblLastViewed.Size = new System.Drawing.Size(100, 13);
            this.lblLastViewed.TabIndex = 5;
            this.lblLastViewed.Text = "Last Viewed: Never";
            // 
            // lblPlaylistCount
            // 
            this.lblPlaylistCount.AutoSize = true;
            this.lblPlaylistCount.Location = new System.Drawing.Point(6, 67);
            this.lblPlaylistCount.Name = "lblPlaylistCount";
            this.lblPlaylistCount.Size = new System.Drawing.Size(99, 13);
            this.lblPlaylistCount.TabIndex = 4;
            this.lblPlaylistCount.Text = "Found in 0 playlists.";
            // 
            // lblViewCount
            // 
            this.lblViewCount.AutoSize = true;
            this.lblViewCount.Location = new System.Drawing.Point(6, 33);
            this.lblViewCount.Name = "lblViewCount";
            this.lblViewCount.Size = new System.Drawing.Size(47, 13);
            this.lblViewCount.TabIndex = 3;
            this.lblViewCount.Text = "Views: 0";
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
            // groupBoxFile
            // 
            this.groupBoxFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxFile.Controls.Add(this.lblEncoding);
            this.groupBoxFile.Controls.Add(this.lblCompressed);
            this.groupBoxFile.Controls.Add(this.lblModified);
            this.groupBoxFile.Controls.Add(this.lblCreated);
            this.groupBoxFile.Controls.Add(this.lblLength);
            this.groupBoxFile.Controls.Add(this.lblFormat);
            this.groupBoxFile.Location = new System.Drawing.Point(446, 6);
            this.groupBoxFile.Name = "groupBoxFile";
            this.groupBoxFile.Size = new System.Drawing.Size(180, 125);
            this.groupBoxFile.TabIndex = 39;
            this.groupBoxFile.TabStop = false;
            this.groupBoxFile.Text = "File Information:";
            // 
            // lblEncoding
            // 
            this.lblEncoding.AutoSize = true;
            this.lblEncoding.Location = new System.Drawing.Point(6, 104);
            this.lblEncoding.Name = "lblEncoding";
            this.lblEncoding.Size = new System.Drawing.Size(55, 13);
            this.lblEncoding.TabIndex = 5;
            this.lblEncoding.Text = "Encoding:";
            // 
            // lblCompressed
            // 
            this.lblCompressed.AutoSize = true;
            this.lblCompressed.Location = new System.Drawing.Point(6, 87);
            this.lblCompressed.Name = "lblCompressed";
            this.lblCompressed.Size = new System.Drawing.Size(68, 13);
            this.lblCompressed.TabIndex = 4;
            this.lblCompressed.Text = "Compressed:";
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
            // groupBoxTablature
            // 
            this.groupBoxTablature.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.groupBoxTablature.Location = new System.Drawing.Point(6, 6);
            this.groupBoxTablature.Name = "groupBoxTablature";
            this.groupBoxTablature.Size = new System.Drawing.Size(434, 252);
            this.groupBoxTablature.TabIndex = 38;
            this.groupBoxTablature.TabStop = false;
            this.groupBoxTablature.Text = "Tablature Information:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label9.Location = new System.Drawing.Point(217, 129);
            this.label9.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label9.Name = "label9";
            this.label9.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label9.Size = new System.Drawing.Size(39, 13);
            this.label9.TabIndex = 57;
            this.label9.Text = "Genre:";
            // 
            // txtGenre
            // 
            this.txtGenre.BackColor = System.Drawing.SystemColors.Window;
            this.txtGenre.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtGenre.Location = new System.Drawing.Point(273, 125);
            this.txtGenre.Name = "txtGenre";
            this.txtGenre.Size = new System.Drawing.Size(155, 20);
            this.txtGenre.TabIndex = 9;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label10.Location = new System.Drawing.Point(6, 129);
            this.label10.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label10.Name = "label10";
            this.label10.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label10.Size = new System.Drawing.Size(39, 13);
            this.label10.TabIndex = 56;
            this.label10.Text = "Album:";
            // 
            // txtAlbum
            // 
            this.txtAlbum.BackColor = System.Drawing.SystemColors.Window;
            this.txtAlbum.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtAlbum.Location = new System.Drawing.Point(56, 125);
            this.txtAlbum.Name = "txtAlbum";
            this.txtAlbum.Size = new System.Drawing.Size(155, 20);
            this.txtAlbum.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(217, 102);
            this.label8.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label8.Name = "label8";
            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label8.Size = new System.Drawing.Size(54, 13);
            this.label8.TabIndex = 53;
            this.label8.Text = "Copyright:";
            // 
            // txtCopyright
            // 
            this.txtCopyright.BackColor = System.Drawing.SystemColors.Window;
            this.txtCopyright.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtCopyright.Location = new System.Drawing.Point(273, 98);
            this.txtCopyright.Name = "txtCopyright";
            this.txtCopyright.Size = new System.Drawing.Size(155, 20);
            this.txtCopyright.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(6, 102);
            this.label7.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 51;
            this.label7.Text = "Author:";
            // 
            // txtAuthor
            // 
            this.txtAuthor.BackColor = System.Drawing.SystemColors.Window;
            this.txtAuthor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtAuthor.Location = new System.Drawing.Point(56, 98);
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.Size = new System.Drawing.Size(155, 20);
            this.txtAuthor.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(6, 75);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 49;
            this.label6.Text = "Subtitle:";
            // 
            // txtSubtitle
            // 
            this.txtSubtitle.BackColor = System.Drawing.SystemColors.Window;
            this.txtSubtitle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtSubtitle.Location = new System.Drawing.Point(56, 71);
            this.txtSubtitle.Name = "txtSubtitle";
            this.txtSubtitle.Size = new System.Drawing.Size(155, 20);
            this.txtSubtitle.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(217, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 47;
            this.label4.Text = "Difficulty:";
            // 
            // tablatureDifficultyDropdown1
            // 
            this.tablatureDifficultyDropdown1.DefaultText = " ";
            this.tablatureDifficultyDropdown1.DisplayDefault = true;
            this.tablatureDifficultyDropdown1.Location = new System.Drawing.Point(273, 71);
            this.tablatureDifficultyDropdown1.Name = "tablatureDifficultyDropdown1";
            this.tablatureDifficultyDropdown1.Size = new System.Drawing.Size(155, 21);
            this.tablatureDifficultyDropdown1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(217, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 45;
            this.label2.Text = "Tuning:";
            // 
            // tablatureTuningDropdown1
            // 
            this.tablatureTuningDropdown1.DefaultText = " ";
            this.tablatureTuningDropdown1.DisplayDefault = true;
            this.tablatureTuningDropdown1.Location = new System.Drawing.Point(273, 44);
            this.tablatureTuningDropdown1.Name = "tablatureTuningDropdown1";
            this.tablatureTuningDropdown1.Size = new System.Drawing.Size(155, 21);
            this.tablatureTuningDropdown1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(6, 156);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 43;
            this.label1.Text = "Rating:";
            // 
            // tablatureRatingDropdown1
            // 
            this.tablatureRatingDropdown1.DefaultText = "No Rating";
            this.tablatureRatingDropdown1.DisplayDefault = true;
            this.tablatureRatingDropdown1.Location = new System.Drawing.Point(56, 152);
            this.tablatureRatingDropdown1.Name = "tablatureRatingDropdown1";
            this.tablatureRatingDropdown1.Size = new System.Drawing.Size(155, 21);
            this.tablatureRatingDropdown1.TabIndex = 10;
            // 
            // typeList
            // 
            this.typeList.DefaultText = "";
            this.typeList.DisplayDefault = false;
            this.typeList.Location = new System.Drawing.Point(56, 44);
            this.typeList.Name = "typeList";
            this.typeList.Size = new System.Drawing.Size(155, 21);
            this.typeList.TabIndex = 2;
            this.typeList.UsePluralizedNames = false;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label16.Location = new System.Drawing.Point(6, 21);
            this.label16.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label16.Name = "label16";
            this.label16.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label16.Size = new System.Drawing.Size(33, 13);
            this.label16.TabIndex = 36;
            this.label16.Text = "Artist:";
            // 
            // txtComment
            // 
            this.txtComment.BackColor = System.Drawing.SystemColors.Window;
            this.txtComment.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtComment.Location = new System.Drawing.Point(9, 199);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(419, 47);
            this.txtComment.TabIndex = 11;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label17.Location = new System.Drawing.Point(217, 21);
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
            this.label5.Location = new System.Drawing.Point(6, 183);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Comment:";
            // 
            // txtArtist
            // 
            this.txtArtist.BackColor = System.Drawing.SystemColors.Window;
            this.txtArtist.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtArtist.Location = new System.Drawing.Point(56, 17);
            this.txtArtist.Name = "txtArtist";
            this.txtArtist.Size = new System.Drawing.Size(155, 20);
            this.txtArtist.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(6, 48);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Type:";
            // 
            // txtTitle
            // 
            this.txtTitle.BackColor = System.Drawing.SystemColors.Window;
            this.txtTitle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtTitle.Location = new System.Drawing.Point(273, 17);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(155, 20);
            this.txtTitle.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txtLyrics);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(632, 264);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Lyrics";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtLyrics
            // 
            this.txtLyrics.BackColor = System.Drawing.SystemColors.Window;
            this.txtLyrics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLyrics.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtLyrics.Location = new System.Drawing.Point(3, 3);
            this.txtLyrics.Multiline = true;
            this.txtLyrics.Name = "txtLyrics";
            this.txtLyrics.PlaceholderForecolor = System.Drawing.SystemColors.GrayText;
            this.txtLyrics.PlaceholderText = null;
            this.txtLyrics.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLyrics.SelectOnFocus = false;
            this.txtLyrics.Size = new System.Drawing.Size(626, 258);
            this.txtLyrics.TabIndex = 1;
            this.txtLyrics.TextChangedDelay = 0;
            // 
            // okbtn
            // 
            this.okbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okbtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okbtn.Location = new System.Drawing.Point(496, 334);
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
            this.cancelbtn.Location = new System.Drawing.Point(577, 334);
            this.cancelbtn.Name = "cancelbtn";
            this.cancelbtn.Size = new System.Drawing.Size(75, 23);
            this.cancelbtn.TabIndex = 24;
            this.cancelbtn.Text = "Cancel";
            this.cancelbtn.UseVisualStyleBackColor = true;
            // 
            // txtlocation
            // 
            this.txtlocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtlocation.BackColor = System.Drawing.SystemColors.Control;
            this.txtlocation.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtlocation.Location = new System.Drawing.Point(12, 12);
            this.txtlocation.Name = "txtlocation";
            this.txtlocation.ReadOnly = true;
            this.txtlocation.Size = new System.Drawing.Size(640, 20);
            this.txtlocation.TabIndex = 41;
            // 
            // TabDetailsDialog
            // 
            this.AcceptButton = this.okbtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelbtn;
            this.ClientSize = new System.Drawing.Size(664, 369);
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
            this.Text = "Tablature Details";
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