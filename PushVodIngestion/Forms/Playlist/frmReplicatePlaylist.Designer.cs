namespace PushVodIngestion.Forms.Playlist
{
    partial class frmReplicatePlaylist
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
            System.Windows.Forms.Label programeNameLabel;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReplicatePlaylist));
            this.dfFrom = new System.Windows.Forms.DateTimePicker();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.butSave = new Telerik.WinControls.UI.RadButton();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            programeNameLabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.butSave)).BeginInit();
            this.SuspendLayout();
            // 
            // dfFrom
            // 
            this.dfFrom.CustomFormat = "dd-MMM-yyyy";
            this.dfFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dfFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dfFrom.Location = new System.Drawing.Point(23, 86);
            this.dfFrom.Margin = new System.Windows.Forms.Padding(4);
            this.dfFrom.Name = "dfFrom";
            this.dfFrom.Size = new System.Drawing.Size(171, 27);
            this.dfFrom.TabIndex = 32;
            // 
            // programeNameLabel
            // 
            programeNameLabel.AutoSize = true;
            programeNameLabel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            programeNameLabel.Location = new System.Drawing.Point(19, 61);
            programeNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            programeNameLabel.Name = "programeNameLabel";
            programeNameLabel.Size = new System.Drawing.Size(52, 21);
            programeNameLabel.TabIndex = 33;
            programeNameLabel.Text = "From:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.Location = new System.Drawing.Point(231, 61);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(30, 21);
            label1.TabIndex = 35;
            label1.Text = "To:";
            // 
            // dtTo
            // 
            this.dtTo.CustomFormat = "dd-MMM-yyyy";
            this.dtTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTo.Location = new System.Drawing.Point(235, 86);
            this.dtTo.Margin = new System.Windows.Forms.Padding(4);
            this.dtTo.Name = "dtTo";
            this.dtTo.Size = new System.Drawing.Size(171, 27);
            this.dtTo.TabIndex = 34;
            // 
            // butSave
            // 
            this.butSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butSave.Image = global::PushVodIngestion.Properties.Resources.LoginOK_22;
            this.butSave.Location = new System.Drawing.Point(124, 154);
            this.butSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.butSave.Name = "butSave";
            this.butSave.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.butSave.Size = new System.Drawing.Size(158, 37);
            this.butSave.TabIndex = 36;
            this.butSave.Text = "  Copy Playlist";
            this.butSave.ThemeName = "Office2010";
            this.butSave.Click += new System.EventHandler(this.butSave_Click);
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(0, 250);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(432, 212);
            this.listBox1.TabIndex = 37;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label2.Location = new System.Drawing.Point(3, 226);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(47, 17);
            label2.TabIndex = 38;
            label2.Text = "Status:";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(432, 35);
            this.label3.TabIndex = 39;
            this.label3.Text = "No Channel";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmReplicatePlaylist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 462);
            this.Controls.Add(this.label3);
            this.Controls.Add(label2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.butSave);
            this.Controls.Add(label1);
            this.Controls.Add(this.dtTo);
            this.Controls.Add(programeNameLabel);
            this.Controls.Add(this.dfFrom);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReplicatePlaylist";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Replicate Playlist";
            ((System.ComponentModel.ISupportInitialize)(this.butSave)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dfFrom;
        private System.Windows.Forms.DateTimePicker dtTo;
        internal Telerik.WinControls.UI.RadButton butSave;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label3;
    }
}