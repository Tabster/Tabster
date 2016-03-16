using Tabster.WinForms;

namespace Tabster.Forms
{
    partial class NewTabDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewTabDialog));
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtArtist = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.okbtn = new System.Windows.Forms.Button();
            this.cancelbtn = new System.Windows.Forms.Button();
            this.typeList = new Tabster.WinForms.TabTypeDropdown();
            this.SuspendLayout();
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Name = "label3";
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
            this.txtArtist.TextChanged += new System.EventHandler(this.ValidateInput);
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
            this.txtTitle.TextChanged += new System.EventHandler(this.ValidateInput);
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
            // typeList
            // 
            this.typeList.DefaultText = "";
            this.typeList.DisplayDefault = false;
            resources.ApplyResources(this.typeList, "typeList");
            this.typeList.Name = "typeList";
            this.typeList.UsePluralizedNames = false;
            this.typeList.TypeChanged += new System.EventHandler(this.ValidateInput);
            // 
            // NewTabDialog
            // 
            this.AcceptButton = this.okbtn;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.cancelbtn;
            this.Controls.Add(this.typeList);
            this.Controls.Add(this.okbtn);
            this.Controls.Add(this.cancelbtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtArtist);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewTabDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtArtist;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Button okbtn;
        private System.Windows.Forms.Button cancelbtn;
        private TabTypeDropdown typeList;
    }
}