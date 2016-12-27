namespace PushVodIngestion.Forms
{
    partial class frmSrcList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSrcList));
            this.cmbPreview = new System.Windows.Forms.GroupBox();
            this.txtSearchNew = new Telerik.WinControls.UI.RadTextBoxControl();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnEdit = new Telerik.WinControls.UI.RadButton();
            this.btnInsert = new Telerik.WinControls.UI.RadButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblImport = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblTotal = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSelectedAssest = new System.Windows.Forms.ToolStripStatusLabel();
            this.radGridView1 = new Telerik.WinControls.UI.RadGridView();
            this.cmbPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnInsert)).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbPreview
            // 
            this.cmbPreview.Controls.Add(this.txtSearchNew);
            this.cmbPreview.Controls.Add(this.PictureBox1);
            this.cmbPreview.Controls.Add(this.btnEdit);
            this.cmbPreview.Controls.Add(this.btnInsert);
            this.cmbPreview.Controls.Add(this.panel2);
            this.cmbPreview.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbPreview.Location = new System.Drawing.Point(0, 0);
            this.cmbPreview.Name = "cmbPreview";
            this.cmbPreview.Size = new System.Drawing.Size(527, 107);
            this.cmbPreview.TabIndex = 15;
            this.cmbPreview.TabStop = false;
            // 
            // txtSearchNew
            // 
            this.txtSearchNew.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.txtSearchNew.Location = new System.Drawing.Point(7, 66);
            this.txtSearchNew.Name = "txtSearchNew";
            this.txtSearchNew.NullText = "Search";
            this.txtSearchNew.Size = new System.Drawing.Size(198, 26);
            this.txtSearchNew.TabIndex = 26;
            this.txtSearchNew.TextChanged += new System.EventHandler(this.txtSearchNew_TextChanged_1);
            // 
            // PictureBox1
            // 
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("PictureBox1.InitialImage")));
            this.PictureBox1.Location = new System.Drawing.Point(210, 66);
            this.PictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(30, 26);
            this.PictureBox1.TabIndex = 25;
            this.PictureBox1.TabStop = false;
            // 
            // btnEdit
            // 
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.Location = new System.Drawing.Point(93, 19);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(2);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.btnEdit.Size = new System.Drawing.Size(81, 30);
            this.btnEdit.TabIndex = 23;
            this.btnEdit.Text = "  Edit";
            this.btnEdit.ThemeName = "Office2010";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnInsert
            // 
            this.btnInsert.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnInsert.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInsert.Image = ((System.Drawing.Image)(resources.GetObject("btnInsert.Image")));
            this.btnInsert.Location = new System.Drawing.Point(8, 19);
            this.btnInsert.Margin = new System.Windows.Forms.Padding(2);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.btnInsert.Size = new System.Drawing.Size(81, 30);
            this.btnInsert.TabIndex = 22;
            this.btnInsert.Text = "  Insert";
            this.btnInsert.ThemeName = "Office2010";
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblImport);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(372, 16);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(152, 88);
            this.panel2.TabIndex = 13;
            // 
            // lblImport
            // 
            this.lblImport.AutoSize = true;
            this.lblImport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblImport.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblImport.Font = new System.Drawing.Font("Segoe UI", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImport.Location = new System.Drawing.Point(93, 0);
            this.lblImport.Name = "lblImport";
            this.lblImport.Size = new System.Drawing.Size(59, 21);
            this.lblImport.TabIndex = 3;
            this.lblImport.Text = "Source";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.statusStrip1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 250);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(527, 42);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblTotal,
            this.lblSelectedAssest});
            this.statusStrip1.Location = new System.Drawing.Point(3, 17);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(521, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblTotal
            // 
            this.lblTotal.Image = global::PushVodIngestion.Properties.Resources.DB_Update_22;
            this.lblTotal.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(65, 17);
            this.lblTotal.Text = "Total : 0";
            // 
            // lblSelectedAssest
            // 
            this.lblSelectedAssest.Image = global::PushVodIngestion.Properties.Resources.ContractGeneral;
            this.lblSelectedAssest.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
            this.lblSelectedAssest.Name = "lblSelectedAssest";
            this.lblSelectedAssest.Size = new System.Drawing.Size(82, 17);
            this.lblSelectedAssest.Text = "Selected : 0";
            // 
            // radGridView1
            // 
            this.radGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radGridView1.Location = new System.Drawing.Point(0, 107);
            // 
            // radGridView1
            // 
            this.radGridView1.MasterTemplate.AllowAddNewRow = false;
            this.radGridView1.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.radGridView1.MasterTemplate.MultiSelect = true;
            this.radGridView1.Name = "radGridView1";
            this.radGridView1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.radGridView1.ReadOnly = true;
            this.radGridView1.Size = new System.Drawing.Size(527, 143);
            this.radGridView1.TabIndex = 17;
            this.radGridView1.Text = "radGridView1";
            // 
            // frmSrcList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 292);
            this.Controls.Add(this.radGridView1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmbPreview);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSrcList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Source";
            this.cmbPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnInsert)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox cmbPreview;
        private Telerik.WinControls.UI.RadTextBoxControl txtSearchNew;
        internal System.Windows.Forms.PictureBox PictureBox1;
        internal Telerik.WinControls.UI.RadButton btnEdit;
        internal Telerik.WinControls.UI.RadButton btnInsert;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblImport;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblTotal;
        private System.Windows.Forms.ToolStripStatusLabel lblSelectedAssest;
        private Telerik.WinControls.UI.RadGridView radGridView1;
    }
}