using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PushVodIngestion.Helper;

namespace PushVodIngestion.Forms.Playlist
{
    public partial class frmKeepList : Form
    {
        private DataTable keepListDataTable;
        public frmKeepList()
        {
            InitializeComponent();
            LoadAutomation();
            keepListDataTable = new DataTable();

        }

        void LoadAutomation()
        {
            cmbAutomation.DataSource = DatabaseLookups.Instance.AutoMationList;
            cmbAutomation.DisplayMember = "AutomationPlateform";
            cmbAutomation.ValueMember = "SID";
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            if (ValidateShow() == false)
                return;

            lblTotal.Text = "Loading";

            Application.DoEvents();
            keepListDataTable =
                ServiceHelper.Instance.data_set(
                    "SELECT tblIngestion.itemCode AS TransmissionId,  tblIngestion.ProgrameName, tblIngestion.Duration FROM [VMG].[dbo].[tblPlaylist] as pl Inner join tblIngestion  on tblIngestion.SID =  pl.MediaSID  Inner join tblPlayoutPort  on tblPlayoutPort.SID =  pl.PlayoutPortSID  Where pl.Date BETWEEN '"+ dtStartDate.Value.ToString("dd-MMM-yyyy") +"' AND '"+ this.dtEndDate.Value.ToString("dd-MMM-yyyy") + "' AND tblPlayoutPort.AutomationSID = "+ (cmbAutomation.SelectedItem as DataProvider.tblAutomation ).SID +" GROUP BY   tblIngestion.ProgrameName, tblIngestion.Duration, tblIngestion.itemCode");



            radGridView1.DataSource = keepListDataTable;

            radGridProperty.change_Property(this.radGridView1);
            lblTotal.Text = "Total :" + keepListDataTable.Rows.Count;
                
        }

        bool ValidateShow()
        {
            var result = true;

            if (cmbAutomation.SelectedItem  == null)
            {
                MessageBox.Show("Please Select Automation");
                return false;

            }




            return result;

        }

        private void butSave_Click(object sender, EventArgs e)
        {

            if (ValidateShow() == false || keepListDataTable.Rows.Count == 0)
            {
                MessageBox.Show("No Items Found");
                return;
            }

            saveFileDialog1.FileName = "KeepList_" + this.dtStartDate.Value.ToString("ddMMyyyy") + ".xml";
            



            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                string filename = saveFileDialog1.FileName;

                XMLCreater xml = new XMLCreater();
                xml.DaletNewDubLIst(filename, GetVwDubllistFromDataTable(keepListDataTable));
            }


            
        }

        List<vwDubList> GetVwDubllistFromDataTable(DataTable dt) {

            var vwDubList = new List<vwDubList>();

            foreach (DataRow row in dt.Rows) {

                var vwDub  = new vwDubList();
                vwDub.itemCode = row["TransmissionId"].ToString();
                vwDub.ProgrameName = row["ProgrameName"].ToString();
                vwDub.Duration = TimeSpan.Parse(row["Duration"].ToString());
                vwDub.Date = DateTime.Now.Date;


                vwDubList.Add(vwDub);
                    
            
            }


            return vwDubList;
        }

    }
}
