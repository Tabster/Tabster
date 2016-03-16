using Tabster.Controls;

namespace Tabster.Forms
{
    partial class TablatureViewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TablatureViewForm));
            this.TabControlMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.closeTabContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlsToolStrip = new System.Windows.Forms.ToolStrip();
            this.printbtn = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripDropDownButton();
            this.offToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savebtn = new System.Windows.Forms.ToolStripButton();
            this.fullscreenbtn = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.documentStateImageLIst = new System.Windows.Forms.ImageList(this.components);
            this.TabControlMenu.SuspendLayout();
            this.controlsToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControlMenu
            // 
            this.TabControlMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeTabContextMenuItem});
            this.TabControlMenu.Name = "contextMenuStrip1";
            resources.ApplyResources(this.TabControlMenu, "TabControlMenu");
            // 
            // closeTabContextMenuItem
            // 
            this.closeTabContextMenuItem.Name = "closeTabContextMenuItem";
            resources.ApplyResources(this.closeTabContextMenuItem, "closeTabContextMenuItem");
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
            resources.ApplyResources(this.controlsToolStrip, "controlsToolStrip");
            this.controlsToolStrip.Name = "controlsToolStrip";
            // 
            // printbtn
            // 
            this.printbtn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.printbtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.printbtn.Image = global::Tabster.Properties.Resources.printer;
            resources.ApplyResources(this.printbtn, "printbtn");
            this.printbtn.Name = "printbtn";
            this.printbtn.Click += new System.EventHandler(this.PrintTab);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            resources.ApplyResources(this.toolStripSeparator8, "toolStripSeparator8");
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.offToolStripMenuItem,
            this.onToolStripMenuItem});
            this.toolStripButton3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toolStripButton3.Image = global::Tabster.Properties.Resources.cursor;
            resources.ApplyResources(this.toolStripButton3, "toolStripButton3");
            this.toolStripButton3.Name = "toolStripButton3";
            // 
            // offToolStripMenuItem
            // 
            this.offToolStripMenuItem.Checked = true;
            this.offToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.offToolStripMenuItem.Name = "offToolStripMenuItem";
            resources.ApplyResources(this.offToolStripMenuItem, "offToolStripMenuItem");
            // 
            // onToolStripMenuItem
            // 
            this.onToolStripMenuItem.Name = "onToolStripMenuItem";
            resources.ApplyResources(this.onToolStripMenuItem, "onToolStripMenuItem");
            // 
            // savebtn
            // 
            resources.ApplyResources(this.savebtn, "savebtn");
            this.savebtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.savebtn.Image = global::Tabster.Properties.Resources.page_save;
            this.savebtn.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
            this.savebtn.Name = "savebtn";
            this.savebtn.Click += new System.EventHandler(this.SaveTab);
            // 
            // fullscreenbtn
            // 
            this.fullscreenbtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.fullscreenbtn.Image = global::Tabster.Properties.Resources.arrow_out;
            resources.ApplyResources(this.fullscreenbtn, "fullscreenbtn");
            this.fullscreenbtn.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
            this.fullscreenbtn.Name = "fullscreenbtn";
            this.fullscreenbtn.Click += new System.EventHandler(this.ToggleFullscreen);
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.ImageList = this.documentStateImageLIst;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
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
            // TablatureViewForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.controlsToolStrip);
            this.KeyPreview = true;
            this.Name = "TablatureViewForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
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