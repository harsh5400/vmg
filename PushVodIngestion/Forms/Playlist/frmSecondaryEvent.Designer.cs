namespace PushVodIngestion.Forms.Playlist
{
    partial class frmSecondaryEvent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSecondaryEvent));
            this.cmbPreview = new System.Windows.Forms.GroupBox();
            this.btnCancel = new Telerik.WinControls.UI.RadButton();
            this.btnOK = new Telerik.WinControls.UI.RadButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblImport = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radGridView1 = new Telerik.WinControls.UI.RadGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showSecondaryEventsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOK)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbPreview
            // 
            this.cmbPreview.Controls.Add(this.btnCancel);
            this.cmbPreview.Controls.Add(this.btnOK);
            this.cmbPreview.Controls.Add(this.panel2);
            this.cmbPreview.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbPreview.Location = new System.Drawing.Point(0, 0);
            this.cmbPreview.Margin = new System.Windows.Forms.Padding(4);
            this.cmbPreview.Name = "cmbPreview";
            this.cmbPreview.Padding = new System.Windows.Forms.Padding(4);
            this.cmbPreview.Size = new System.Drawing.Size(547, 133);
            this.cmbPreview.TabIndex = 12;
            this.cmbPreview.TabStop = false;
            this.cmbPreview.Enter += new System.EventHandler(this.cmbPreview_Enter);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Image = global::PushVodIngestion.Properties.Resources.cancel_22;
            this.btnCancel.Location = new System.Drawing.Point(128, 20);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnCancel.Size = new System.Drawing.Size(108, 37);
            this.btnCancel.TabIndex = 35;
            this.btnCancel.Text = " Cancel";
            this.btnCancel.ThemeName = "Office2010";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Image = global::PushVodIngestion.Properties.Resources.LoginOK_22;
            this.btnOK.Location = new System.Drawing.Point(14, 20);
            this.btnOK.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnOK.Size = new System.Drawing.Size(108, 37);
            this.btnOK.TabIndex = 34;
            this.btnOK.Text = "  OK";
            this.btnOK.ThemeName = "Office2010";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblImport);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(232, 19);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(311, 110);
            this.panel2.TabIndex = 13;
            // 
            // lblImport
            // 
            this.lblImport.AutoSize = true;
            this.lblImport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblImport.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblImport.Font = new System.Drawing.Font("Segoe UI", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImport.Location = new System.Drawing.Point(75, 0);
            this.lblImport.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblImport.Name = "lblImport";
            this.lblImport.Size = new System.Drawing.Size(236, 28);
            this.lblImport.TabIndex = 3;
            this.lblImport.Text = "Select Secondary Events";
            this.lblImport.Click += new System.EventHandler(this.lblImport_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 381);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(547, 52);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            // 
            // radGridView1
            // 
            this.radGridView1.AutoScroll = true;
            this.radGridView1.ContextMenuStrip = this.contextMenuStrip1;
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
            this.radGridView1.Size = new System.Drawing.Size(547, 248);
            this.radGridView1.TabIndex = 15;
            this.radGridView1.Text = "RadGridView2";
            this.radGridView1.ThemeName = "Office2010";
            this.radGridView1.Click += new System.EventHandler(this.radGridView1_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showSecondaryEventsToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(240, 30);
            // 
            // showSecondaryEventsToolStripMenuItem
            // 
            this.showSecondaryEventsToolStripMenuItem.Name = "showSecondaryEventsToolStripMenuItem";
            this.showSecondaryEventsToolStripMenuItem.Size = new System.Drawing.Size(239, 26);
            this.showSecondaryEventsToolStripMenuItem.Text = "Show Secondary Events";
            this.showSecondaryEventsToolStripMenuItem.Click += new System.EventHandler(this.showSecondaryEventsToolStripMenuItem_Click);
            // 
            // frmSecondaryEvent
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(547, 433);
            this.Controls.Add(this.radGridView1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmbPreview);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSecondaryEvent";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Secondary Event Group";
            this.cmbPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOK)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox cmbPreview;
        internal Telerik.WinControls.UI.RadButton btnOK;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblImport;
        internal Telerik.WinControls.UI.RadButton btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        internal Telerik.WinControls.UI.RadGridView radGridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showSecondaryEventsToolStripMenuItem;
    }
}