namespace PushVodIngestion.Forms
{
    partial class frmChannelEdit
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
            System.Windows.Forms.Label nameLabel;
            System.Windows.Forms.Label srcSIDLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChannelEdit));
            this.cmbPreview = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblImport = new System.Windows.Forms.Label();
            this.btnCancel = new Telerik.WinControls.UI.RadButton();
            this.butSave = new Telerik.WinControls.UI.RadButton();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.tblChannelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.srcSIDComboBox = new System.Windows.Forms.ComboBox();
            nameLabel = new System.Windows.Forms.Label();
            srcSIDLabel = new System.Windows.Forms.Label();
            this.cmbPreview.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.butSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblChannelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            nameLabel.Location = new System.Drawing.Point(18, 83);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new System.Drawing.Size(44, 15);
            nameLabel.TabIndex = 11;
            nameLabel.Text = "Name:";
            // 
            // srcSIDLabel
            // 
            srcSIDLabel.AutoSize = true;
            srcSIDLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            srcSIDLabel.Location = new System.Drawing.Point(18, 115);
            srcSIDLabel.Name = "srcSIDLabel";
            srcSIDLabel.Size = new System.Drawing.Size(49, 15);
            srcSIDLabel.TabIndex = 13;
            srcSIDLabel.Text = "Source:";
            // 
            // cmbPreview
            // 
            this.cmbPreview.Controls.Add(this.panel2);
            this.cmbPreview.Controls.Add(this.btnCancel);
            this.cmbPreview.Controls.Add(this.butSave);
            this.cmbPreview.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbPreview.Location = new System.Drawing.Point(0, 0);
            this.cmbPreview.Name = "cmbPreview";
            this.cmbPreview.Size = new System.Drawing.Size(394, 58);
            this.cmbPreview.TabIndex = 10;
            this.cmbPreview.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblImport);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(239, 16);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(152, 39);
            this.panel2.TabIndex = 13;
            // 
            // lblImport
            // 
            this.lblImport.AutoSize = true;
            this.lblImport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblImport.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblImport.Font = new System.Drawing.Font("Segoe UI", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImport.Location = new System.Drawing.Point(73, 0);
            this.lblImport.Name = "lblImport";
            this.lblImport.Size = new System.Drawing.Size(79, 21);
            this.lblImport.TabIndex = 3;
            this.lblImport.Text = "Channels";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(101, 18);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.btnCancel.Size = new System.Drawing.Size(81, 30);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "  Cancel";
            this.btnCancel.ThemeName = "Office2010";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // butSave
            // 
            this.butSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butSave.Image = ((System.Drawing.Image)(resources.GetObject("butSave.Image")));
            this.butSave.Location = new System.Drawing.Point(12, 18);
            this.butSave.Margin = new System.Windows.Forms.Padding(2);
            this.butSave.Name = "butSave";
            this.butSave.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.butSave.Size = new System.Drawing.Size(81, 30);
            this.butSave.TabIndex = 11;
            this.butSave.Text = "  Save";
            this.butSave.ThemeName = "Office2010";
            this.butSave.Click += new System.EventHandler(this.butSave_Click);
            // 
            // nameTextBox
            // 
            this.nameTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblChannelBindingSource, "Name", true));
            this.nameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameTextBox.Location = new System.Drawing.Point(72, 80);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(288, 21);
            this.nameTextBox.TabIndex = 12;
            // 
            // tblChannelBindingSource
            // 
            this.tblChannelBindingSource.DataSource = typeof(PushVodIngestion.tblChannel);
            // 
            // srcSIDComboBox
            // 
            this.srcSIDComboBox.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.tblChannelBindingSource, "SrcSID", true));
            this.srcSIDComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.srcSIDComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.srcSIDComboBox.FormattingEnabled = true;
            this.srcSIDComboBox.Location = new System.Drawing.Point(71, 112);
            this.srcSIDComboBox.Name = "srcSIDComboBox";
            this.srcSIDComboBox.Size = new System.Drawing.Size(121, 23);
            this.srcSIDComboBox.TabIndex = 14;
            this.srcSIDComboBox.SelectedIndexChanged += new System.EventHandler(this.srcSIDComboBox_SelectedIndexChanged);
            // 
            // frmChannelEdit
            // 
            this.AcceptButton = this.butSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(394, 170);
            this.Controls.Add(srcSIDLabel);
            this.Controls.Add(this.srcSIDComboBox);
            this.Controls.Add(nameLabel);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.cmbPreview);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmChannelEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Channels";
            this.cmbPreview.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.butSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblChannelBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox cmbPreview;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblImport;
        internal Telerik.WinControls.UI.RadButton btnCancel;
        internal Telerik.WinControls.UI.RadButton butSave;
        private System.Windows.Forms.BindingSource tblChannelBindingSource;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.ComboBox srcSIDComboBox;
    }
}