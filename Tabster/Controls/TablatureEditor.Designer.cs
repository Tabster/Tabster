namespace Tabster.Controls
{
    partial class TablatureEditor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtContents = new System.Windows.Forms.TextBox();
            this.autoscrollTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // txtContents
            // 
            this.txtContents.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtContents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtContents.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContents.Location = new System.Drawing.Point(0, 0);
            this.txtContents.Multiline = true;
            this.txtContents.Name = "txtContents";
            this.txtContents.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtContents.Size = new System.Drawing.Size(737, 294);
            this.txtContents.TabIndex = 0;
            this.txtContents.TextChanged += new System.EventHandler(this.txtContents_TextChanged);
            // 
            // autoscrollTimer
            // 
            this.autoscrollTimer.Interval = 1000;
            this.autoscrollTimer.Tick += new System.EventHandler(this.autoscrollTimer_Tick);
            // 
            // TablatureEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtContents);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "TablatureEditor";
            this.Size = new System.Drawing.Size(737, 294);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtContents;
        private System.Windows.Forms.Timer autoscrollTimer;



    }
}
