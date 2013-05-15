using Tabster.Controls;

namespace Tabster.Forms
{
    partial class Splash
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Splash));
            this.lbltitle = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblversion = new System.Windows.Forms.Label();
            this.lbldisclaimer = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblloading = new System.Windows.Forms.Label();
            this.lblname = new System.Windows.Forms.Label();
            this.simpleProgressBar1 = new Tabster.Controls.SimpleProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbltitle
            // 
            this.lbltitle.AutoSize = true;
            this.lbltitle.BackColor = System.Drawing.Color.Transparent;
            this.lbltitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltitle.ForeColor = System.Drawing.Color.Black;
            this.lbltitle.Location = new System.Drawing.Point(153, 64);
            this.lbltitle.Name = "lbltitle";
            this.lbltitle.Size = new System.Drawing.Size(331, 16);
            this.lbltitle.TabIndex = 51;
            this.lbltitle.Text = "Guitar Tabs • Guitar Chords • Bass Tabs • Drum Tabs";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 128);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 50;
            this.pictureBox1.TabStop = false;
            // 
            // lblversion
            // 
            this.lblversion.AutoSize = true;
            this.lblversion.BackColor = System.Drawing.Color.Transparent;
            this.lblversion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblversion.ForeColor = System.Drawing.Color.Gray;
            this.lblversion.Location = new System.Drawing.Point(454, 192);
            this.lblversion.Name = "lblversion";
            this.lblversion.Size = new System.Drawing.Size(52, 16);
            this.lblversion.TabIndex = 49;
            this.lblversion.Text = "v0.0.0.0";
            // 
            // lbldisclaimer
            // 
            this.lbldisclaimer.AutoSize = true;
            this.lbldisclaimer.BackColor = System.Drawing.Color.Transparent;
            this.lbldisclaimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldisclaimer.ForeColor = System.Drawing.Color.Gray;
            this.lbldisclaimer.Location = new System.Drawing.Point(9, 165);
            this.lbldisclaimer.Name = "lbldisclaimer";
            this.lbldisclaimer.Size = new System.Drawing.Size(288, 16);
            this.lbldisclaimer.TabIndex = 34;
            this.lbldisclaimer.Text = "Tabster is not endorsed by Ultimate-Guitar.com";
            this.lbldisclaimer.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(9, 192);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(223, 16);
            this.label2.TabIndex = 48;
            this.label2.Text = "Copyright © 2010-2013 Nate Shoffner";
            // 
            // lblloading
            // 
            this.lblloading.AutoSize = true;
            this.lblloading.BackColor = System.Drawing.Color.Transparent;
            this.lblloading.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblloading.ForeColor = System.Drawing.Color.Black;
            this.lblloading.Location = new System.Drawing.Point(153, 94);
            this.lblloading.Name = "lblloading";
            this.lblloading.Size = new System.Drawing.Size(216, 16);
            this.lblloading.TabIndex = 10;
            this.lblloading.Text = "Scanning tabs and loading library...";
            // 
            // lblname
            // 
            this.lblname.AutoSize = true;
            this.lblname.BackColor = System.Drawing.Color.Transparent;
            this.lblname.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblname.ForeColor = System.Drawing.Color.Black;
            this.lblname.Location = new System.Drawing.Point(146, 9);
            this.lblname.Name = "lblname";
            this.lblname.Size = new System.Drawing.Size(194, 55);
            this.lblname.TabIndex = 4;
            this.lblname.Text = "Tabster";
            // 
            // simpleProgressBar1
            // 
            this.simpleProgressBar1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.simpleProgressBar1.Location = new System.Drawing.Point(156, 126);
            this.simpleProgressBar1.Maximum = 140;
            this.simpleProgressBar1.Name = "simpleProgressBar1";
            this.simpleProgressBar1.Size = new System.Drawing.Size(328, 14);
            this.simpleProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.simpleProgressBar1.TabIndex = 52;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Splash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(518, 217);
            this.Controls.Add(this.simpleProgressBar1);
            this.Controls.Add(this.lbltitle);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblversion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbldisclaimer);
            this.Controls.Add(this.lblloading);
            this.Controls.Add(this.lblname);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Splash";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Splash";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbltitle;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblversion;
        private System.Windows.Forms.Label lbldisclaimer;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label lblloading;
        private System.Windows.Forms.Label lblname;
        private SimpleProgressBar simpleProgressBar1;
        private System.Windows.Forms.Timer timer1;

    }
}