using Tabster.Controls;

namespace Tabster.Forms
{
    partial class SplashScreen
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
            this.lblCopyright = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            this.lblPortable = new System.Windows.Forms.Label();
            this.lblSafeMode = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblCopyright
            // 
            this.lblCopyright.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.BackColor = System.Drawing.Color.Transparent;
            this.lblCopyright.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopyright.ForeColor = System.Drawing.Color.Gray;
            this.lblCopyright.Location = new System.Drawing.Point(144, 286);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(65, 16);
            this.lblCopyright.TabIndex = 48;
            this.lblCopyright.Text = "Copyright";
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.BackColor = System.Drawing.Color.Transparent;
            this.lblProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgress.ForeColor = System.Drawing.Color.Black;
            this.lblProgress.Location = new System.Drawing.Point(187, 200);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(77, 16);
            this.lblProgress.TabIndex = 10;
            this.lblProgress.Text = "lblProgress";
            // 
            // lblPortable
            // 
            this.lblPortable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPortable.AutoSize = true;
            this.lblPortable.BackColor = System.Drawing.Color.Transparent;
            this.lblPortable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPortable.ForeColor = System.Drawing.Color.Gray;
            this.lblPortable.Location = new System.Drawing.Point(423, 9);
            this.lblPortable.Name = "lblPortable";
            this.lblPortable.Size = new System.Drawing.Size(97, 16);
            this.lblPortable.TabIndex = 52;
            this.lblPortable.Text = "Portable Mode";
            this.lblPortable.Visible = false;
            // 
            // lblSafeMode
            // 
            this.lblSafeMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSafeMode.AutoSize = true;
            this.lblSafeMode.BackColor = System.Drawing.Color.Transparent;
            this.lblSafeMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSafeMode.ForeColor = System.Drawing.Color.Gray;
            this.lblSafeMode.Location = new System.Drawing.Point(446, 29);
            this.lblSafeMode.Name = "lblSafeMode";
            this.lblSafeMode.Size = new System.Drawing.Size(74, 16);
            this.lblSafeMode.TabIndex = 53;
            this.lblSafeMode.Text = "Safe Mode";
            this.lblSafeMode.Visible = false;
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = System.Drawing.Color.Gray;
            this.lblVersion.Location = new System.Drawing.Point(468, 286);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(52, 16);
            this.lblVersion.TabIndex = 54;
            this.lblVersion.Text = "v0.0.0.0";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // SplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::Tabster.Properties.Resources.splash;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(532, 314);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblSafeMode);
            this.Controls.Add(this.lblPortable);
            this.Controls.Add(this.lblCopyright);
            this.Controls.Add(this.lblProgress);
            this.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SplashScreen";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Splash";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCopyright;
        public System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label lblPortable;
        private System.Windows.Forms.Label lblSafeMode;
        private System.Windows.Forms.Label lblVersion;

    }
}