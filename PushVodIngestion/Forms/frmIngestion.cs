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
    public partial class frmIngestion : Form
    {

        private static long SID;
        private static int Row_index;
        List<vwIngestion> lstIngestion;

        public frmIngestion(long _SID)
        {
            SID = _SID;
            InitializeComponent();
            comboLoad();

            List<vwIngestion> lstIngestion = new List<vwIngestion>();
            dtDate.Value = DateTime.Now.Date;
            this.dtEndDate.Value = DateTime.Now.Date;

            this.dtStartTime.Value = new DateTime(1900, 1, 1, 0, 0, 0);
            this.dtEndTime.Value = new DateTime(1900, 1, 1, 0, 30, 0);


        }

        void comboLoad() {

            channelSIDComboBox.DataSource = LinqBase.GetList<tblChannel>().OrderBy(i => i.Name).ToList();
            channelSIDComboBox.DisplayMember = "Name";
            channelSIDComboBox.ValueMember   = "SID";



           

        
        }

        private void dateDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            loadIngegtions();
        }

        public void Form_Load(int Row_index_)
        {
            

             Row_index = Row_index_;
            tblIngestion T = new tblIngestion();
           

            if (SID != 0)
            {
                T = LinqBase.GetList<tblIngestion>().Where(i => i.SID == SID).FirstOrDefault();
                dtDate.Value = T.Date.Value;
                this.dtEndDate.Value = T.EndDate.Value;
 
                dtStartTime.Value = new DateTime(1900,1,1, T.StartTime.Value.Hours,T.StartTime.Value.Minutes, T.StartTime.Value.Seconds );
                this.dtEndTime.Value = new DateTime(1990, 1, 1, T.EndTime.Value.Hours, T.EndTime.Value.Minutes, T.EndTime.Value.Seconds);

                dtDate.Enabled = false;
                dtEndDate.Enabled = false;
                dtStartTime.Enabled = false;
                dtEndTime.Enabled = false;

            }

             this.tblIngestionBindingSource.DataSource = T;
           
            

            this.Show();



        }

        private void butSave_Click(object sender, EventArgs e)
        {
            if (ValidationError() == false)
                return;


            var p = new PortAvaible { isAvaible = false };

            if (SID == 0)
            {
             
                p = CheckPort();

                if (p.isAvaible == false)
                {
                    MessageBox.Show("Recording Port is now available at specified time");
                    return;
                }
            }

            var T = this.tblIngestionBindingSource.Current as tblIngestion;


            if (T != null)
            {
                T.Date = dtDate.Value.Date;
                T.EndDate    = this.dtEndDate.Value.Date;
                T.StartTime =    new TimeSpan(dtStartTime.Value.Hour, dtStartTime.Value.Minute, dtStartTime.Value.Second);
                T.EndTime = new TimeSpan(dtEndTime.Value.Hour, dtEndTime.Value.Minute, dtEndTime.Value.Second);
               
                if(SID==0)
                    T.DesSID = p.PortSID;


                TimeSpan dur = ( (T.EndDate.Value.Add(T.EndTime.Value)) - (T.Date.Value.Add(T.StartTime.Value)));
                T.Duration = dur;
                T.DeleteDate = T.Date.Value.AddDays(10);
                T.SourceTypeSID = (long)SourceType.Catchup;
                
                

                if (SID == 0)
                {
                    T.ProgrameName = T.ProgrameName + T.Date.Value.ToString("_yyyyMMdd");
                    
                    T.addon = DateTime.Now;
               
                    LinqBase.InsertItemIntoDatabase<tblIngestion>(T);
                    pdlMasterHelper.Form_loadGrid(0, "Main");
                }
                else
                {
                
                    T.modon = DateTime.Now;


                    LinqBase.UpdateDatabaseWithItem<tblIngestion>(T);
                    pdlMasterHelper.Form_loadGrid(Row_index, "Main");

                }
            }

            this.Close();
        }


        PortAvaible CheckPort()
        {
            var p = new PortAvaible {isAvaible = false};

            //TimeSpan startTime = new TimeSpan(dtStartTime.Value.Hour, dtStartTime.Value.Minute, dtStartTime.Value.Second);
            //TimeSpan endime = new TimeSpan(this.dtEndTime.Value.Hour, dtEndTime.Value.Minute, dtEndTime.Value.Second);

            DateTime startTime = dtDate.Value.Date.Add(new TimeSpan(dtStartTime.Value.Hour, dtStartTime.Value.Minute, dtStartTime.Value.Second));
            DateTime endime = dtEndDate.Value.Date.Add(new TimeSpan(this.dtEndTime.Value.Hour, dtEndTime.Value.Minute, dtEndTime.Value.Second));

            List<tblDe> destinations    = LinqBase.GetList<tblDe>();
            
           vwIngestion ingest = new vwIngestion();
           vwIngestion ingest2 = new vwIngestion();
            foreach(tblDe  des in destinations)
            {
                ingest = lstIngestion.Where(i => i.DestinationPortSID == des.SID).ToList().Where(i => (startTime >= i.StartDate.Value.Add(i.StartTime.Value) && startTime <= i.EndDate.Value.Add(i.EndTime.Value)) || (endime >= i.StartDate.Value.Add(i.StartTime.Value) && endime <= i.EndDate.Value.Add(i.EndTime.Value))).FirstOrDefault();
                ingest2 = lstIngestion.Where(i => i.DestinationPortSID == des.SID).ToList().Where(i => (i.StartDate.Value.Add(i.StartTime.Value) > startTime  &&  i.EndDate.Value.Add(i.EndTime.Value) < endime)).FirstOrDefault();
                   if (ingest == null && ingest2 == null)
                    {
                        p.isAvaible = true;
                        p.PortSID = des.SID;
                        break;
                    }


            }
           

            return p;

        }


        Boolean ValidationError()
        {
            Boolean result = true;
            String Message = "";
            if (this.channelSIDComboBox.Text.Length == 0)
            {
                result = false;
                Message = Message + " Channal Name, ";
            }


            DateTime startTime = dtDate.Value.Date.Add( new TimeSpan(dtStartTime.Value.Hour, dtStartTime.Value.Minute, dtStartTime.Value.Second));
            DateTime endime = dtEndDate.Value.Date.Add( new TimeSpan(this.dtEndTime.Value.Hour, dtEndTime.Value.Minute, dtEndTime.Value.Second));

            

            if (startTime >= endime)
            {
                result = false;
                Message = Message + " Time, ";
            }

            if (result == false)
                MessageBox.Show("Invalid - " + Message);

            return result;
        }

        private void channelSIDComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void loadIngegtions()
        {         



           lstIngestion = LinqBase.GetList<vwIngestion>().Where(i=> i.StartDate == dtDate.Value.Date).ToList();

            radGridView1.DataSource = lstIngestion;
            radGridProperty.change_Property(radGridView1, false, false, false);
        }

        private void desSIDComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadIngegtions();

        }

    }
}
