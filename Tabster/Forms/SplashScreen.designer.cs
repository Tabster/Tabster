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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
            this.lblCopyright = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            this.lblPortable = new System.Windows.Forms.Label();
            this.lblSafeMode = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblBuild = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblCopyright
            // 
            resources.ApplyResources(this.lblCopyright, "lblCopyright");
            this.lblCopyright.BackColor = System.Drawing.Color.Transparent;
            this.lblCopyright.ForeColor = System.Drawing.Color.Gray;
            this.lblCopyright.Name = "lblCopyright";
            // 
            // lblProgress
            // 
            resources.ApplyResources(this.lblProgress, "lblProgress");
            this.lblProgress.BackColor = System.Drawing.Color.Transparent;
            this.lblProgress.ForeColor = System.Drawing.Color.Black;
            this.lblProgress.Name = "lblProgress";
            // 
            // lblPortable
            // 
            resources.ApplyResources(this.lblPortable, "lblPortable");
            this.lblPortable.BackColor = System.Drawing.Color.Transparent;
            this.lblPortable.ForeColor = System.Drawing.Color.Gray;
            this.lblPortable.Name = "lblPortable";
            // 
            // lblSafeMode
            // 
            resources.ApplyResources(this.lblSafeMode, "lblSafeMode");
            this.lblSafeMode.BackColor = System.Drawing.Color.Transparent;
            this.lblSafeMode.ForeColor = System.Drawing.Color.Gray;
            this.lblSafeMode.Name = "lblSafeMode";
            // 
            // lblVersion
            // 
            resources.ApplyResources(this.lblVersion, "lblVersion");
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.ForeColor = System.Drawing.Color.Gray;
            this.lblVersion.Name = "lblVersion";
            // 
            // lblBuild
            // 
            resources.ApplyResources(this.lblBuild, "lblBuild");
            this.lblBuild.BackColor = System.Drawing.Color.Transparent;
            this.lblBuild.ForeColor = System.Drawing.Color.Gray;
            this.lblBuild.Name = "lblBuild";
            // 
            // SplashScreen
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::Tabster.Properties.Resources.splash;
            this.Controls.Add(this.lblBuild);
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
        private System.Windows.Forms.Label lblBuild;

    }
}