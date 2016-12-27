using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrameWork;
using PushVodIngestion.Helper;

namespace PushVodIngestion
{
    public partial class frmChangePassword : Form
    {
        public frmChangePassword()
        {
            InitializeComponent();
        }

        private void butSave_Click(object sender, EventArgs e)
        {
            if (txtNewPassword.Text.Trim().Length == 0 || this.txtOldPassword.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please Enter Password");

                return;

            }

            if (txtOldPassword.Text != Global.CurreUser.LoginPassword)
            {
                MessageBox.Show("Old password is not Correct");

                return;
            }


            var usr = Global.CurreUser;

          
            usr.LoginPassword = txtNewPassword.Text.Trim();

            usr.BusinessValidationResult = null;

          //  LinqBase.UpdateDatabaseWithItem<Databa>(usr);


            

            ServiceHelper.Instance.SaveEntity(usr, EntityState.Modified);


            MessageBox.Show("Password Changed");
            this.Close();

        }
    }
}
