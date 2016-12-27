namespace PushVodIngestion.Forms.Playlist
{
    partial class frmMediaEdit
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
            System.Windows.Forms.Label programeNameLabel;
            System.Windows.Forms.Label remarkLabel;
            System.Windows.Forms.Label sourceTypeSIDLabel;
            System.Windows.Forms.Label durationLabel;
            System.Windows.Forms.Label readyToAirLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMediaEdit));
            this.cmbPreview = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblImport = new System.Windows.Forms.Label();
            this.btnCancel = new Telerik.WinControls.UI.RadButton();
            this.butSave = new Telerik.WinControls.UI.RadButton();
            this.programeNameTextBox = new System.Windows.Forms.TextBox();
            this.tblIngestionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.remarkTextBox = new System.Windows.Forms.TextBox();
            this.sourceTypeSIDComboBox = new System.Windows.Forms.ComboBox();
            this.readyToAirCheckBox = new System.Windows.Forms.CheckBox();
            this.dtDuration = new System.Windows.Forms.DateTimePicker();
            programeNameLabel = new System.Windows.Forms.Label();
            remarkLabel = new System.Windows.Forms.Label();
            sourceTypeSIDLabel = new System.Windows.Forms.Label();
            durationLabel = new System.Windows.Forms.Label();
            readyToAirLabel = new System.Windows.Forms.Label();
            this.cmbPreview.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.butSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblIngestionBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // programeNameLabel
            // 
            programeNameLabel.AutoSize = true;
            programeNameLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            programeNameLabel.Location = new System.Drawing.Point(47, 98);
            programeNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            programeNameLabel.Name = "programeNameLabel";
            programeNameLabel.Size = new System.Drawing.Size(128, 21);
            programeNameLabel.TabIndex = 14;
            programeNameLabel.Text = "Programe Name:";
            // 
            // remarkLabel
            // 
            remarkLabel.AutoSize = true;
            remarkLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            remarkLabel.Location = new System.Drawing.Point(47, 228);
            remarkLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            remarkLabel.Name = "remarkLabel";
            remarkLabel.Size = new System.Drawing.Size(68, 21);
            remarkLabel.TabIndex = 15;
            remarkLabel.Text = "Remark:";
            // 
            // sourceTypeSIDLabel
            // 
            sourceTypeSIDLabel.AutoSize = true;
            sourceTypeSIDLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            sourceTypeSIDLabel.Location = new System.Drawing.Point(47, 183);
            sourceTypeSIDLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            sourceTypeSIDLabel.Name = "sourceTypeSIDLabel";
            sourceTypeSIDLabel.Size = new System.Drawing.Size(98, 21);
            sourceTypeSIDLabel.TabIndex = 16;
            sourceTypeSIDLabel.Text = "Source Type:";
            // 
            // durationLabel
            // 
            durationLabel.AutoSize = true;
            durationLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            durationLabel.Location = new System.Drawing.Point(47, 143);
            durationLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            durationLabel.Name = "durationLabel";
            durationLabel.Size = new System.Drawing.Size(75, 21);
            durationLabel.TabIndex = 17;
            durationLabel.Text = "Duration:";
            // 
            // readyToAirLabel
            // 
            readyToAirLabel.AutoSize = true;
            readyToAirLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            readyToAirLabel.Location = new System.Drawing.Point(47, 337);
            readyToAirLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            readyToAirLabel.Name = "readyToAirLabel";
            readyToAirLabel.Size = new System.Drawing.Size(100, 21);
            readyToAirLabel.TabIndex = 18;
            readyToAirLabel.Text = "Ready To Air:";
            // 
            // cmbPreview
            // 
            this.cmbPreview.Controls.Add(this.panel2);
            this.cmbPreview.Controls.Add(this.btnCancel);
            this.cmbPreview.Controls.Add(this.butSave);
            this.cmbPreview.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbPreview.Location = new System.Drawing.Point(0, 0);
            this.cmbPreview.Margin = new System.Windows.Forms.Padding(4);
            this.cmbPreview.Name = "cmbPreview";
            this.cmbPreview.Padding = new System.Windows.Forms.Padding(4);
            this.cmbPreview.Size = new System.Drawing.Size(781, 71);
            this.cmbPreview.TabIndex = 13;
            this.cmbPreview.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblImport);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(574, 19);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(203, 48);
            this.panel2.TabIndex = 13;
            // 
            // lblImport
            // 
            this.lblImport.AutoSize = true;
            this.lblImport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblImport.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblImport.Font = new System.Drawing.Font("Segoe UI", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImport.Location = new System.Drawing.Point(91, 0);
            this.lblImport.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblImport.Name = "lblImport";
            this.lblImport.Size = new System.Drawing.Size(112, 28);
            this.lblImport.TabIndex = 3;
            this.lblImport.Text = "Media Edit";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(135, 22);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnCancel.Size = new System.Drawing.Size(108, 37);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "  Cancel";
            this.btnCancel.ThemeName = "Office2010";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // butSave
            // 
            this.butSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butSave.Image = ((System.Drawing.Image)(resources.GetObject("butSave.Image")));
            this.butSave.Location = new System.Drawing.Point(16, 22);
            this.butSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.butSave.Name = "butSave";
            this.butSave.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.butSave.Size = new System.Drawing.Size(108, 37);
            this.butSave.TabIndex = 11;
            this.butSave.Text = "  Save";
            this.butSave.ThemeName = "Office2010";
            this.butSave.Click += new System.EventHandler(this.butSave_Click);
            // 
            // programeNameTextBox
            // 
            this.programeNameTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblIngestionBindingSource, "ProgrameName", true));
            this.programeNameTextBox.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.programeNameTextBox.Location = new System.Drawing.Point(201, 95);
            this.programeNameTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.programeNameTextBox.Name = "programeNameTextBox";
            this.programeNameTextBox.Size = new System.Drawing.Size(435, 27);
            this.programeNameTextBox.TabIndex = 15;
            // 
            // tblIngestionBindingSource
            // 
            this.tblIngestionBindingSource.DataSource = typeof(PushVodIngestion.tblIngestion);
            // 
            // remarkTextBox
            // 
            this.remarkTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblIngestionBindingSource, "Remark", true));
            this.remarkTextBox.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.remarkTextBox.Location = new System.Drawing.Point(201, 224);
            this.remarkTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.remarkTextBox.Multiline = true;
            this.remarkTextBox.Name = "remarkTextBox";
            this.remarkTextBox.Size = new System.Drawing.Size(479, 95);
            this.remarkTextBox.TabIndex = 16;
            // 
            // sourceTypeSIDComboBox
            // 
            this.sourceTypeSIDComboBox.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.tblIngestionBindingSource, "SourceTypeSID", true));
            this.sourceTypeSIDComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sourceTypeSIDComboBox.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sourceTypeSIDComboBox.FormattingEnabled = true;
            this.sourceTypeSIDComboBox.Location = new System.Drawing.Point(201, 180);
            this.sourceTypeSIDComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.sourceTypeSIDComboBox.Name = "sourceTypeSIDComboBox";
            this.sourceTypeSIDComboBox.Size = new System.Drawing.Size(160, 27);
            this.sourceTypeSIDComboBox.TabIndex = 17;
            // 
            // readyToAirCheckBox
            // 
            this.readyToAirCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.tblIngestionBindingSource, "ReadyToAir", true));
            this.readyToAirCheckBox.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.readyToAirCheckBox.Location = new System.Drawing.Point(201, 332);
            this.readyToAirCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.readyToAirCheckBox.Name = "readyToAirCheckBox";
            this.readyToAirCheckBox.Size = new System.Drawing.Size(139, 30);
            this.readyToAirCheckBox.TabIndex = 19;
            this.readyToAirCheckBox.UseVisualStyleBackColor = true;
            // 
            // dtDuration
            // 
            this.dtDuration.CustomFormat = "HH:mm:ss";
            this.dtDuration.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtDuration.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDuration.Location = new System.Drawing.Point(201, 144);
            this.dtDuration.Margin = new System.Windows.Forms.Padding(4);
            this.dtDuration.Name = "dtDuration";
            this.dtDuration.ShowUpDown = true;
            this.dtDuration.Size = new System.Drawing.Size(99, 24);
            this.dtDuration.TabIndex = 49;
            // 
            // frmMediaEdit
            // 
            this.AcceptButton = this.butSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(781, 390);
            this.Controls.Add(this.dtDuration);
            this.Controls.Add(readyToAirLabel);
            this.Controls.Add(this.readyToAirCheckBox);
            this.Controls.Add(durationLabel);
            this.Controls.Add(sourceTypeSIDLabel);
            this.Controls.Add(this.sourceTypeSIDComboBox);
            this.Controls.Add(remarkLabel);
            this.Controls.Add(this.remarkTextBox);
            this.Controls.Add(programeNameLabel);
            this.Controls.Add(this.programeNameTextBox);
            this.Controls.Add(this.cmbPreview);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMediaEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Media Edit";
            this.Load += new System.EventHandler(this.frmMediaEdit_Load);
            this.cmbPreview.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.butSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblIngestionBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox cmbPreview;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblImport;
        internal Telerik.WinControls.UI.RadButton btnCancel;
        internal Telerik.WinControls.UI.RadButton butSave;
        private System.Windows.Forms.BindingSource tblIngestionBindingSource;
        private System.Windows.Forms.TextBox programeNameTextBox;
        private System.Windows.Forms.TextBox remarkTextBox;
        private System.Windows.Forms.ComboBox sourceTypeSIDComboBox;
        private System.Windows.Forms.CheckBox readyToAirCheckBox;
        private System.Windows.Forms.DateTimePicker dtDuration;
    }
}