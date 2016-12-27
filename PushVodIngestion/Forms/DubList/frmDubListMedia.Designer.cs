namespace PushVodIngestion.Forms.DubList
{
    partial class frmDubListMedia
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDubListMedia));
            this.cmbPreview = new System.Windows.Forms.GroupBox();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtSearchNew = new Telerik.WinControls.UI.RadTextBoxControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblImport = new System.Windows.Forms.Label();
            this.butSave = new Telerik.WinControls.UI.RadButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.lblTotal = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSelectedAssest = new System.Windows.Forms.ToolStripStatusLabel();
            this.radGridView1 = new Telerik.WinControls.UI.RadGridView();
            this.cmbPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchNew)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.butSave)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbPreview
            // 
            this.cmbPreview.Controls.Add(this.PictureBox1);
            this.cmbPreview.Controls.Add(this.txtSearchNew);
            this.cmbPreview.Controls.Add(this.panel2);
            this.cmbPreview.Controls.Add(this.butSave);
            this.cmbPreview.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbPreview.Location = new System.Drawing.Point(0, 0);
            this.cmbPreview.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbPreview.Name = "cmbPreview";
            this.cmbPreview.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbPreview.Size = new System.Drawing.Size(653, 127);
            this.cmbPreview.TabIndex = 12;
            this.cmbPreview.TabStop = false;
            // 
            // PictureBox1
            // 
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("PictureBox1.InitialImage")));
            this.PictureBox1.Location = new System.Drawing.Point(314, 76);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(42, 40);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox1.TabIndex = 33;
            this.PictureBox1.TabStop = false;
            // 
            // txtSearchNew
            // 
            this.txtSearchNew.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.txtSearchNew.Location = new System.Drawing.Point(9, 77);
            this.txtSearchNew.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSearchNew.Name = "txtSearchNew";
            this.txtSearchNew.NullText = "Search";
            this.txtSearchNew.Size = new System.Drawing.Size(297, 40);
            this.txtSearchNew.TabIndex = 32;
            this.txtSearchNew.TextChanged += new System.EventHandler(this.txtSearchNew_TextChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblImport);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(421, 24);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(228, 98);
            this.panel2.TabIndex = 13;
            // 
            // lblImport
            // 
            this.lblImport.AutoSize = true;
            this.lblImport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblImport.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblImport.Font = new System.Drawing.Font("Segoe UI", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImport.Location = new System.Drawing.Point(144, 0);
            this.lblImport.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblImport.Name = "lblImport";
            this.lblImport.Size = new System.Drawing.Size(84, 32);
            this.lblImport.TabIndex = 3;
            this.lblImport.Text = "Media";
            // 
            // butSave
            // 
            this.butSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.butSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butSave.Image = global::PushVodIngestion.Properties.Resources.LoginOK_22;
            this.butSave.Location = new System.Drawing.Point(7, 19);
            this.butSave.Name = "butSave";
            this.butSave.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.butSave.Size = new System.Drawing.Size(130, 46);
            this.butSave.TabIndex = 11;
            this.butSave.Text = "  Select";
            this.butSave.ThemeName = "Office2010";
            this.butSave.Click += new System.EventHandler(this.butSave_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblTotal,
            this.lblSelectedAssest});
            this.toolStrip1.Location = new System.Drawing.Point(0, 614);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip1.Size = new System.Drawing.Size(653, 30);
            this.toolStrip1.TabIndex = 13;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // lblTotal
            // 
            this.lblTotal.Image = global::PushVodIngestion.Properties.Resources.DB_Update_22;
            this.lblTotal.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(104, 25);
            this.lblTotal.Text = "Total  : 0";
            // 
            // lblSelectedAssest
            // 
            this.lblSelectedAssest.Image = global::PushVodIngestion.Properties.Resources.ContractGeneral;
            this.lblSelectedAssest.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
            this.lblSelectedAssest.Name = "lblSelectedAssest";
            this.lblSelectedAssest.Size = new System.Drawing.Size(131, 25);
            this.lblSelectedAssest.Text = "Selected  : 0";
            // 
            // radGridView1
            // 
            this.radGridView1.AutoScroll = true;
            this.radGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radGridView1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radGridView1.Location = new System.Drawing.Point(0, 127);
            this.radGridView1.Margin = new System.Windows.Forms.Padding(3, 1538, 3, 3);
            // 
            // radGridView1
            // 
            this.radGridView1.MasterTemplate.AllowAddNewRow = false;
            this.radGridView1.MasterTemplate.AllowDeleteRow = false;
            this.radGridView1.MasterTemplate.AllowDragToGroup = false;
            this.radGridView1.MasterTemplate.AllowEditRow = false;
            this.radGridView1.MasterTemplate.EnableGrouping = false;
            this.radGridView1.MasterTemplate.MultiSelect = true;
            this.radGridView1.Name = "radGridView1";
            this.radGridView1.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.radGridView1.ReadOnly = true;
            this.radGridView1.Size = new System.Drawing.Size(653, 487);
            this.radGridView1.TabIndex = 15;
            this.radGridView1.Text = "RadGridView2";
            this.radGridView1.ThemeName = "Office2010";
            // 
            // frmDubListMedia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 644);
            this.Controls.Add(this.radGridView1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.cmbPreview);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDubListMedia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmDubListMedia";
            this.cmbPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchNew)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.butSave)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox cmbPreview;
        internal System.Windows.Forms.PictureBox PictureBox1;
        private Telerik.WinControls.UI.RadTextBoxControl txtSearchNew;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblImport;
        internal Telerik.WinControls.UI.RadButton butSave;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblTotal;
        private System.Windows.Forms.ToolStripStatusLabel lblSelectedAssest;
        internal Telerik.WinControls.UI.RadGridView radGridView1;
    }
}