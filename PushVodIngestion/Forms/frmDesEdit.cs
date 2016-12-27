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
    public partial class frmDesEdit : Form
    {
               private static long SID;
        private static int Row_index;


        public frmDesEdit(long _SID)
        {
            SID = _SID;
            InitializeComponent();
        }


        public void Form_Load(int Row_index_)
        {


            Row_index = Row_index_;
            tblDe T = new tblDe();


            if (SID != 0)
            {
                T = LinqBase.GetList<tblDe>().Where(i => i.SID == SID).FirstOrDefault();
               

            }

            this.tblDeBindingSource.DataSource = T;



            this.Show();



        }


        void save()
        {
            if (ValidationError() == false)
                return;

            tblDe T = new tblDe();
            T = this.tblDeBindingSource.Current as tblDe;




            if (SID == 0)
            {



                LinqBase.InsertItemIntoDatabase<tblDe>(T);
                pdlMasterHelper.Form_loadGrid(0, "frmDes");
            }
            else
            {

           


                LinqBase.UpdateDatabaseWithItem<tblDe>(T);
                pdlMasterHelper.Form_loadGrid(Row_index, "frmDes");

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


            if (this.cameraTextBox.Text.Length == 0)
            {
                result = false;
                Message = Message + " Camera, ";
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
