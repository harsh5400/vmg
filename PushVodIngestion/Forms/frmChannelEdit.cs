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
    public partial class frmChannelEdit : Form
    {

        private static long SID;
        private static int Row_index;


        public frmChannelEdit(long _SID)
        {
            SID = _SID;
            InitializeComponent();
            COMBOlOAD();
        }

       


        public void Form_Load(int Row_index_)
        {


            Row_index = Row_index_;
            tblChannel T = new tblChannel();


            if (SID != 0)
            {
                T = LinqBase.GetList<tblChannel>().Where(i => i.SID == SID).FirstOrDefault();
               

            }

            this.tblChannelBindingSource.DataSource = T;



            this.Show();



        }


        void save()
        {
            if (ValidationError() == false)
                return;

            tblChannel T = new tblChannel();
            T = this.tblChannelBindingSource.Current as tblChannel;




            if (SID == 0)
            {

                T.addon = DateTime.Now;

                LinqBase.InsertItemIntoDatabase<tblChannel>(T);
                pdlMasterHelper.Form_loadGrid(0, "frmChannelsList");
            }
            else
            {

                T.modon = DateTime.Now;


                LinqBase.UpdateDatabaseWithItem<tblChannel>(T);
                pdlMasterHelper.Form_loadGrid(Row_index, "frmChannelsList");

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

            if (this.srcSIDComboBox.Text.Length == 0)
            {
                result = false;
                Message = Message + "Source, ";
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

        private void srcSIDComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        void COMBOlOAD()
        {
            srcSIDComboBox.DataSource = LinqBase.GetList<tblSrc>().OrderBy(i=> i.Name).ToList();
            srcSIDComboBox.DisplayMember = "Name";
            srcSIDComboBox.ValueMember = "SID";
        }
    }
}
