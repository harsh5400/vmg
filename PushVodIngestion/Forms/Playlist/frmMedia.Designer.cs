namespace PushVodIngestion.Forms.Playlist
{
    partial class frmMedia
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMedia));
            this.cmbPreview = new System.Windows.Forms.GroupBox();
            this.chkReadyAir = new System.Windows.Forms.CheckBox();
            this.btnEdit = new Telerik.WinControls.UI.RadButton();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtSearchNew = new Telerik.WinControls.UI.RadTextBoxControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblImport = new System.Windows.Forms.Label();
            this.butSave = new Telerik.WinControls.UI.RadButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.lblTotal = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSelectedAssest = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radGridView1 = new Telerik.WinControls.UI.RadGridView();
            this.cmbPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnEdit)).BeginInit();
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
            this.cmbPreview.Controls.Add(this.chkReadyAir);
            this.cmbPreview.Controls.Add(this.btnEdit);
            this.cmbPreview.Controls.Add(this.PictureBox1);
            this.cmbPreview.Controls.Add(this.txtSearchNew);
            this.cmbPreview.Controls.Add(this.panel2);
            this.cmbPreview.Controls.Add(this.butSave);
            this.cmbPreview.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbPreview.Location = new System.Drawing.Point(0, 0);
            this.cmbPreview.Margin = new System.Windows.Forms.Padding(4);
            this.cmbPreview.Name = "cmbPreview";
            this.cmbPreview.Padding = new System.Windows.Forms.Padding(4);
            this.cmbPreview.Size = new System.Drawing.Size(996, 133);
            this.cmbPreview.TabIndex = 11;
            this.cmbPreview.TabStop = false;
            // 
            // chkReadyAir
            // 
            this.chkReadyAir.AutoSize = true;
            this.chkReadyAir.Checked = true;
            this.chkReadyAir.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkReadyAir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkReadyAir.Location = new System.Drawing.Point(8, 68);
            this.chkReadyAir.Margin = new System.Windows.Forms.Padding(4);
            this.chkReadyAir.Name = "chkReadyAir";
            this.chkReadyAir.Size = new System.Drawing.Size(125, 21);
            this.chkReadyAir.TabIndex = 35;
            this.chkReadyAir.Text = "Ready To Air";
            this.chkReadyAir.UseVisualStyleBackColor = true;
            this.chkReadyAir.CheckedChanged += new System.EventHandler(this.chkReadyAir_CheckedChanged);
            // 
            // btnEdit
            // 
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.Location = new System.Drawing.Point(14, 20);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnEdit.Size = new System.Drawing.Size(108, 37);
            this.btnEdit.TabIndex = 34;
            this.btnEdit.Text = "  Edit";
            this.btnEdit.ThemeName = "Office2010";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // PictureBox1
            // 
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("PictureBox1.InitialImage")));
            this.PictureBox1.Location = new System.Drawing.Point(279, 94);
            this.PictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(40, 32);
            this.PictureBox1.TabIndex = 33;
            this.PictureBox1.TabStop = false;
            // 
            // txtSearchNew
            // 
            this.txtSearchNew.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.txtSearchNew.Location = new System.Drawing.Point(8, 94);
            this.txtSearchNew.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearchNew.Name = "txtSearchNew";
            this.txtSearchNew.NullText = "Search";
            this.txtSearchNew.Size = new System.Drawing.Size(264, 32);
            this.txtSearchNew.TabIndex = 32;
            this.txtSearchNew.TextChanged += new System.EventHandler(this.txtSearchNew_TextChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblImport);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(789, 19);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(203, 110);
            this.panel2.TabIndex = 13;
            // 
            // lblImport
            // 
            this.lblImport.AutoSize = true;
            this.lblImport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblImport.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblImport.Font = new System.Drawing.Font("Segoe UI", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImport.Location = new System.Drawing.Point(134, 0);
            this.lblImport.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblImport.Name = "lblImport";
            this.lblImport.Size = new System.Drawing.Size(69, 28);
            this.lblImport.TabIndex = 3;
            this.lblImport.Text = "Media";
            // 
            // butSave
            // 
            this.butSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.butSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butSave.Image = global::PushVodIngestion.Properties.Resources.LoginOK_22;
            this.butSave.Location = new System.Drawing.Point(344, 92);
            this.butSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.butSave.Name = "butSave";
            this.butSave.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.butSave.Size = new System.Drawing.Size(116, 37);
            this.butSave.TabIndex = 11;
            this.butSave.Text = "  Select";
            this.butSave.ThemeName = "Office2010";
            this.butSave.Visible = false;
            this.butSave.Click += new System.EventHandler(this.butSave_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblTotal,
            this.lblSelectedAssest});
            this.toolStrip1.Location = new System.Drawing.Point(0, 574);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip1.Size = new System.Drawing.Size(996, 29);
            this.toolStrip1.TabIndex = 12;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // lblTotal
            // 
            this.lblTotal.Image = global::PushVodIngestion.Properties.Resources.DB_Update_22;
            this.lblTotal.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(89, 24);
            this.lblTotal.Text = "Total  : 0";
            // 
            // lblSelectedAssest
            // 
            this.lblSelectedAssest.Image = global::PushVodIngestion.Properties.Resources.ContractGeneral;
            this.lblSelectedAssest.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
            this.lblSelectedAssest.Name = "lblSelectedAssest";
            this.lblSelectedAssest.Size = new System.Drawing.Size(113, 24);
            this.lblSelectedAssest.Text = "Selected  : 0";
            // 
            // groupBox1
            // 
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 522);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(996, 52);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // radGridView1
            // 
            this.radGridView1.AutoScroll = true;
            this.radGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radGridView1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radGridView1.Location = new System.Drawing.Point(0, 133);
            this.radGridView1.Margin = new System.Windows.Forms.Padding(3, 1230, 3, 2);
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
            this.radGridView1.Size = new System.Drawing.Size(996, 389);
            this.radGridView1.TabIndex = 14;
            this.radGridView1.Text = "RadGridView2";
            this.radGridView1.ThemeName = "Office2010";
            // 
            // frmMedia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(996, 603);
            this.Controls.Add(this.radGridView1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.cmbPreview);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMedia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Media";
            this.cmbPreview.ResumeLayout(false);
            this.cmbPreview.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnEdit)).EndInit();
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
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblImport;
        internal Telerik.WinControls.UI.RadButton butSave;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblTotal;
        private System.Windows.Forms.ToolStripStatusLabel lblSelectedAssest;
        private System.Windows.Forms.GroupBox groupBox1;
        internal Telerik.WinControls.UI.RadGridView radGridView1;
        internal System.Windows.Forms.PictureBox PictureBox1;
        private Telerik.WinControls.UI.RadTextBoxControl txtSearchNew;
        internal Telerik.WinControls.UI.RadButton btnEdit;
        private System.Windows.Forms.CheckBox chkReadyAir;
    }
}