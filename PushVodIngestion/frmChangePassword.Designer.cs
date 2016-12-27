namespace PushVodIngestion
{
    partial class frmChangePassword
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
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label channelLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChangePassword));
            this.cmbPreview = new System.Windows.Forms.GroupBox();
            this.butSave = new Telerik.WinControls.UI.RadButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblImport = new System.Windows.Forms.Label();
            this.btnCancel = new Telerik.WinControls.UI.RadButton();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.txtOldPassword = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            channelLabel = new System.Windows.Forms.Label();
            this.cmbPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.butSave)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.ForeColor = System.Drawing.Color.Black;
            label1.Location = new System.Drawing.Point(62, 148);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(134, 23);
            label1.TabIndex = 27;
            label1.Text = "New Password :";
            // 
            // channelLabel
            // 
            channelLabel.AutoSize = true;
            channelLabel.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            channelLabel.ForeColor = System.Drawing.Color.Black;
            channelLabel.Location = new System.Drawing.Point(61, 105);
            channelLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            channelLabel.Name = "channelLabel";
            channelLabel.Size = new System.Drawing.Size(126, 23);
            channelLabel.TabIndex = 25;
            channelLabel.Text = "Old Password :";
            // 
            // cmbPreview
            // 
            this.cmbPreview.Controls.Add(this.butSave);
            this.cmbPreview.Controls.Add(this.panel2);
            this.cmbPreview.Controls.Add(this.btnCancel);
            this.cmbPreview.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbPreview.Location = new System.Drawing.Point(0, 0);
            this.cmbPreview.Margin = new System.Windows.Forms.Padding(4);
            this.cmbPreview.Name = "cmbPreview";
            this.cmbPreview.Padding = new System.Windows.Forms.Padding(4);
            this.cmbPreview.Size = new System.Drawing.Size(560, 84);
            this.cmbPreview.TabIndex = 17;
            this.cmbPreview.TabStop = false;
            // 
            // butSave
            // 
            this.butSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butSave.ForeColor = System.Drawing.Color.Black;
            this.butSave.Image = ((System.Drawing.Image)(resources.GetObject("butSave.Image")));
            this.butSave.Location = new System.Drawing.Point(15, 22);
            this.butSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.butSave.Name = "butSave";
            this.butSave.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.butSave.Size = new System.Drawing.Size(108, 37);
            this.butSave.TabIndex = 25;
            this.butSave.Text = "  Save";
            this.butSave.ThemeName = "Office2010";
            this.butSave.Click += new System.EventHandler(this.butSave_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblImport);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(353, 19);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(203, 61);
            this.panel2.TabIndex = 13;
            // 
            // lblImport
            // 
            this.lblImport.AutoSize = true;
            this.lblImport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblImport.Font = new System.Drawing.Font("Segoe UI", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImport.ForeColor = System.Drawing.Color.Black;
            this.lblImport.Location = new System.Drawing.Point(20, 2);
            this.lblImport.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblImport.Name = "lblImport";
            this.lblImport.Size = new System.Drawing.Size(180, 28);
            this.lblImport.TabIndex = 3;
            this.lblImport.Text = "Change Password";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(135, 22);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnCancel.Size = new System.Drawing.Size(108, 37);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "  Cancel";
            this.btnCancel.ThemeName = "Office2010";
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Font = new System.Drawing.Font("Calibri", 11.25F);
            this.txtNewPassword.Location = new System.Drawing.Point(203, 138);
            this.txtNewPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.PasswordChar = '*';
            this.txtNewPassword.Size = new System.Drawing.Size(285, 30);
            this.txtNewPassword.TabIndex = 28;
            // 
            // txtOldPassword
            // 
            this.txtOldPassword.Font = new System.Drawing.Font("Calibri", 11.25F);
            this.txtOldPassword.Location = new System.Drawing.Point(202, 95);
            this.txtOldPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtOldPassword.Name = "txtOldPassword";
            this.txtOldPassword.PasswordChar = '*';
            this.txtOldPassword.Size = new System.Drawing.Size(285, 30);
            this.txtOldPassword.TabIndex = 26;
            // 
            // frmChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 226);
            this.Controls.Add(this.txtNewPassword);
            this.Controls.Add(label1);
            this.Controls.Add(this.txtOldPassword);
            this.Controls.Add(channelLabel);
            this.Controls.Add(this.cmbPreview);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmChangePassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Change Password";
            this.cmbPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.butSave)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox cmbPreview;
        internal Telerik.WinControls.UI.RadButton butSave;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblImport;
        internal Telerik.WinControls.UI.RadButton btnCancel;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.TextBox txtOldPassword;
    }
}