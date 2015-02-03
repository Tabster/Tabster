using Tabster.Controls;

namespace Tabster.Forms
{
    partial class ExternalViewerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExternalViewerForm));
            this.TabControlMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.closeTabContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlsToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripDropDownButton();
            this.offToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savebtn = new System.Windows.Forms.ToolStripButton();
            this.fullscreenbtn = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.documentStateImageLIst = new System.Windows.Forms.ImageList(this.components);
            this.printbtn = new System.Windows.Forms.ToolStripSplitButton();
            this.TabControlMenu.SuspendLayout();
            this.controlsToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControlMenu
            // 
            this.TabControlMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeTabContextMenuItem});
            this.TabControlMenu.Name = "contextMenuStrip1";
            this.TabControlMenu.Size = new System.Drawing.Size(127, 26);
            // 
            // closeTabContextMenuItem
            // 
            this.closeTabContextMenuItem.Name = "closeTabContextMenuItem";
            this.closeTabContextMenuItem.Size = new System.Drawing.Size(126, 22);
            this.closeTabContextMenuItem.Text = "Close Tab";
            this.closeTabContextMenuItem.Click += new System.EventHandler(this.closeTabToolStripMenuItem_Click);
            // 
            // controlsToolStrip
            // 
            this.controlsToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.controlsToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printbtn,
            this.toolStripSeparator8,
            this.toolStripButton3,
            this.savebtn,
            this.fullscreenbtn});
            this.controlsToolStrip.Location = new System.Drawing.Point(0, 0);
            this.controlsToolStrip.Name = "controlsToolStrip";
            this.controlsToolStrip.Padding = new System.Windows.Forms.Padding(0);
            this.controlsToolStrip.Size = new System.Drawing.Size(738, 25);
            this.controlsToolStrip.TabIndex = 24;
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.offToolStripMenuItem,
            this.onToolStripMenuItem});
            this.toolStripButton3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toolStripButton3.Image = global::Tabster.Properties.Resources.cursor;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(96, 22);
            this.toolStripButton3.Text = "Auto-Scroll";
            // 
            // offToolStripMenuItem
            // 
            this.offToolStripMenuItem.Checked = true;
            this.offToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.offToolStripMenuItem.Name = "offToolStripMenuItem";
            this.offToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.offToolStripMenuItem.Text = "Off";
            this.offToolStripMenuItem.Click += new System.EventHandler(this.AutoScrollChange);
            // 
            // onToolStripMenuItem
            // 
            this.onToolStripMenuItem.Name = "onToolStripMenuItem";
            this.onToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.onToolStripMenuItem.Text = "On";
            this.onToolStripMenuItem.Click += new System.EventHandler(this.AutoScrollChange);
            // 
            // savebtn
            // 
            this.savebtn.Enabled = false;
            this.savebtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.savebtn.Image = global::Tabster.Properties.Resources.page_save;
            this.savebtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.savebtn.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
            this.savebtn.Name = "savebtn";
            this.savebtn.Size = new System.Drawing.Size(51, 22);
            this.savebtn.Text = "Save";
            this.savebtn.Click += new System.EventHandler(this.SaveTab);
            // 
            // fullscreenbtn
            // 
            this.fullscreenbtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.fullscreenbtn.Image = global::Tabster.Properties.Resources.arrow_out;
            this.fullscreenbtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fullscreenbtn.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
            this.fullscreenbtn.Name = "fullscreenbtn";
            this.fullscreenbtn.Size = new System.Drawing.Size(84, 22);
            this.fullscreenbtn.Text = "Full Screen";
            this.fullscreenbtn.Click += new System.EventHandler(this.ToggleFullscreen);
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.documentStateImageLIst;
            this.tabControl1.ItemSize = new System.Drawing.Size(220, 23);
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.ShowToolTips = true;
            this.tabControl1.Size = new System.Drawing.Size(738, 488);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            this.tabControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseUp);
            // 
            // documentStateImageLIst
            // 
            this.documentStateImageLIst.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("documentStateImageLIst.ImageStream")));
            this.documentStateImageLIst.TransparentColor = System.Drawing.Color.Transparent;
            this.documentStateImageLIst.Images.SetKeyName(0, "bullet_green.png");
            this.documentStateImageLIst.Images.SetKeyName(1, "bullet_red.png");
            // 
            // printbtn
            // 
            this.printbtn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.printbtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.printbtn.Image = global::Tabster.Properties.Resources.printer;
            this.printbtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printbtn.Name = "printbtn";
            this.printbtn.Size = new System.Drawing.Size(64, 22);
            this.printbtn.Text = "Print";
            this.printbtn.Click += new System.EventHandler(this.PrintTab);
            // 
            // TabbedViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 513);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.controlsToolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "TabbedViewer";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tabster";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TabbedViewer_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TabbedViewer_KeyDown);
            this.TabControlMenu.ResumeLayout(false);
            this.controlsToolStrip.ResumeLayout(false);
            this.controlsToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ContextMenuStrip TabControlMenu;
        private System.Windows.Forms.ToolStripMenuItem closeTabContextMenuItem;
        private System.Windows.Forms.ToolStrip controlsToolStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButton3;
        private System.Windows.Forms.ToolStripMenuItem offToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton savebtn;
        private System.Windows.Forms.ToolStripButton fullscreenbtn;
        private System.Windows.Forms.ImageList documentStateImageLIst;
        private System.Windows.Forms.ToolStripSplitButton printbtn;

    }
}