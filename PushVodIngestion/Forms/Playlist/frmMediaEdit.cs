using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PushVodIngestion.Forms.Playlist
{
    public partial class frmMediaEdit : Form
    {
        private static long SID;
        private static int Row_index;

        public frmMediaEdit(long _SID)
        {
            SID = _SID;
            InitializeComponent();
            ComboLoad();
        }
        private void frmMediaEdit_Load(object sender, EventArgs e)
        {

        }

        void ComboLoad()
        {
            sourceTypeSIDComboBox.DataSource = LinqBase.GetList<tblSourceType>();
            sourceTypeSIDComboBox.DisplayMember = "Description";
            sourceTypeSIDComboBox.ValueMember = "SID";
        }




        public void Form_Load(int Row_index_)
        {
            Row_index = Row_index_;
            tblIngestion T = new tblIngestion();


            if (SID != 0)
            {
                T = LinqBase.GetList<tblIngestion>().Where(i => i.SID == SID).FirstOrDefault();

                dtDuration.Value = new DateTime(1900, 1, 1, T.Duration.Value.Hours, T.Duration.Value.Minutes, T.Duration.Value.Seconds);
            }
            this.tblIngestionBindingSource.DataSource = T;
            this.Show();
        }

        void save()
        {
            if (ValidationError() == false)
                return;

            tblIngestion T = new tblIngestion();
            T = this.tblIngestionBindingSource.Current as tblIngestion;

            T.Duration = new TimeSpan(this.dtDuration.Value.Hour, dtDuration.Value.Minute, dtDuration.Value.Second);


            if (SID == 0)
            {
                LinqBase.InsertItemIntoDatabase<tblIngestion>(T);
                pdlMasterHelper.Form_loadGrid(0, "frmDes");
            }
            else
            {
                LinqBase.UpdateDatabaseWithItem<tblIngestion>(T);
                pdlMasterHelper.Form_loadGrid(Row_index, "frmMedia");
            }

            this.Close();
        }

        Boolean ValidationError()
        {
            Boolean result = true;
            String Message = "";
            if (this.programeNameTextBox.Text.Length == 0)
            {
                result = false;
                Message = Message + " Programe Name, ";
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
