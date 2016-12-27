using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PushVodIngestion.Forms
{
    public partial class frmSrcEdit : Form
    {
               private static long SID;
        private static int Row_index;


        public frmSrcEdit(long _SID)
        {
            SID = _SID;
            InitializeComponent();
        }


        public void Form_Load(int Row_index_)
        {


            Row_index = Row_index_;
            tblSrc T = new tblSrc();


            if (SID != 0)
            {
                T = LinqBase.GetList<tblSrc>().Where(i => i.SID == SID).FirstOrDefault();
               

            }

            this.tblSrcBindingSource.DataSource = T;



            this.Show();



        }


        void save()
        {
            if (ValidationError() == false)
                return;

            tblSrc T = new tblSrc();
            T = this.tblSrcBindingSource.Current as tblSrc;




            if (SID == 0)
            {



                LinqBase.InsertItemIntoDatabase<tblSrc>(T);
                pdlMasterHelper.Form_loadGrid(0, "frmSrcList");
            }
            else
            {

           


                LinqBase.UpdateDatabaseWithItem<tblSrc>(T);
                pdlMasterHelper.Form_loadGrid(Row_index, "frmSrcList");

            }

            this.Close();
        }

        Boolean ValidationError()
        {
            Boolean result = true;
            String Message = "";
            if (this.nameTextBox.Text.Length == 0)
            {
                result = false;
                Message = Message + " Channal Name, ";
            }

        




            if (result == false)
                MessageBox.Show("Invalid - " + Message);

            return result;
        }

        private void butSave_Click(object sender, EventArgs e)
        {
            save();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
