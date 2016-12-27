using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PushVodIngestion.Helper;
using System.Collections.ObjectModel;

namespace PushVodIngestion.Forms.Playlist
{
    public partial class frmMultiExportSchedule : Form
    {
        public frmMultiExportSchedule()
        {
            InitializeComponent();
            ComboLoad();
        }

        private void frmMultiExportSchedule_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var result = folderBrowserDialog1.ShowDialog();
            if (result != DialogResult.OK) return;

            txtFolderPath.Text = folderBrowserDialog1.SelectedPath; //


        }

        void ComboLoad()
        {
            var tblPlayoutPorts = DatabaseLookups.Instance.PlayoutPorts.OrderBy(i => i.SID).ToList();
            cmbPlayoutPort.DataSource = tblPlayoutPorts;
            cmbPlayoutPort.DisplayMember = "Descriptions";
            cmbPlayoutPort.ValueMember = "SID";

        }

        private void butSave_Click(object sender, EventArgs e)
        {

            if (!Directory.Exists(txtFolderPath.Text))
            {
                listBox1.Items.Add("Error:- Wrong Path, Please Select valid Path ");
                return;
            }

            lblStatus.Text = "Status: Please Wait Exporting.....";
            Application.DoEvents();
           var Channel = cmbPlayoutPort.SelectedItem as DataProvider.tblPlayoutPort;
          var currentDate = dfFrom.Value.Date;
            listBox1.Items.Clear();

            var dateDiff = dtTo.Value.Date.Subtract(currentDate).Days + 1;
            
            if (dateDiff < 0)
            {
                lblStatus.Text = "Status: Wrong Date Selection ";
                Application.DoEvents();
                listBox1.Items.Add("Error:- Wrong date Selection");
                return;

            }
            for (var i=1 ; i<=dateDiff; i++)
            {
                var displayList = GetDisplayliList(currentDate.Date, Channel.SID);
                if (displayList.Count == 0)
                {   
                    listBox1.Items.Add("Error:- " + currentDate.ToString("dd-MMM-yyyy") + " - " + Channel.Descriptions + " - No Data Found");
                }
                else
                {

                    CreatePlaylist(Channel, txtFolderPath.Text, displayList, currentDate);

                    listBox1.Items.Add("Done:- " + currentDate.ToString("dd-MMM-yyyy") +  " - " + Channel.Descriptions + " Playlist Exported");
                }

            
               currentDate =  currentDate.AddDays(1);
            }

            lblStatus.Text = "Status: Done :)";
            Application.DoEvents();
        
        }
       
       List<Helper.Playlist> GetDisplayliList(DateTime dt, long tblPlayoutPortSID)
        {
            var displayPlaylist = new List<Helper.Playlist>(); 

            var result = true;
            var instance = typeof(DataProvider.tblPlaylist).AssemblyQualifiedName;
            var entities = ServiceHelper.Instance.GetEntitiesBasedOnCondition(instance, "PlayoutPortSID == @0 && Date ==@1", new ObservableCollection<object>() { tblPlayoutPortSID, dt.Date });
            var playList = new List<DataProvider.tblPlaylist>(entities.Cast<DataProvider.tblPlaylist>().ToList().OrderBy(i => i.PlayOrderSID).ToList());

            if (playList.Count == 0)
                return displayPlaylist;

            //create displaylist

           foreach (var tblPlaylist in playList.Where(tblPlaylist => tblPlaylist.tblIngestion == null))
           {
               //    tblPlaylist.tblIngestion =
               //        DatabaseLookups.Instance.FullIngestions.Find(
               //            i => tblPlaylist.MediaSID != null && i.SID == tblPlaylist.MediaSID.Value);
               //

               if (tblPlaylist.MediaSID != null)
                   tblPlaylist.tblIngestion = DatabaseLookups.Instance.GetIngestion(tblPlaylist.MediaSID.Value);
           }

           //recalculate
           playList = PlaylistHelper.Instance.UpdatePlayoutTime(playList);

            displayPlaylist =
                playList.Select(
                    i =>
                        new Helper.Playlist
                        {
                            SID = i.SID,
                            PlayTime = i.PlayoutTime.Value,
                            TransmissionId =
                                i.tblIngestion.itemCode == null
                                    ? i.tblIngestion.TxId.ToString(CultureInfo.InvariantCulture)
                                    : i.tblIngestion.itemCode.ToString(CultureInfo.InvariantCulture),
                            ProgrameName = i.tblIngestion.ProgrameName,
                            Duration = i.tblIngestion.Duration == null ? new TimeSpan(0, 0, 0, 0) : i.tblIngestion.Duration.Value,
                            Status = i.Status != null && (i.Status.Value) ? "Saved" : "Not Saved",
                            Event = i.FixedEvent == true ? "Fixed" : "Follow",
                            PlayOrderSID = i.PlayOrderSID.Value,
                            PlayDate = i.SchDate.Value.Date,
                            Approved = i.Approved != null && (i.Approved.Value) ? "Approved" : "Not Approved",
                            SourceTypeSID = i.tblIngestion.SourceTypeSID.Value,
                            DurationMin = i.tblIngestion.Duration == null ? 0.0 : i.tblIngestion.Duration.Value.TotalMinutes,
                        }).ToList();


            return displayPlaylist;

        }

       void CreatePlaylist(DataProvider.tblPlayoutPort selectedPort, String folderPath, List<Helper.Playlist> displayPlayList, DateTime date)
       {
           var filename = "";
           string innternalName = "";


           if (selectedPort != null && selectedPort.AutomationSID == (int)Automation.EVS)
            {
                filename = folderPath + @"\Evs_Playlist_XML_B5_" + date.ToString("ddMMyyyy") + ".xml";
              
            }
            else if (selectedPort != null && selectedPort.AutomationSID == (int)Automation.Itx)
            {
                filename = folderPath + @"\" + selectedPort.Descriptions + "_ITXML_" +
                           date.ToString("ddMMyyyy") + ".itxml";

            }
            else
            {
                filename = folderPath + @"\" + selectedPort.Descriptions + "_Playlist_B5_" +
                                           date.ToString("ddMMyyyy") + ".xls";
                
            }

            innternalName = Path.GetFileNameWithoutExtension(filename);


            var xml = new XMLCreater();


           //finally export
            switch (selectedPort.AutomationSID)
            {
                case (int)Automation.EVS:
                    listBox1.Items.Add(xml.PlaylistXML(filename, displayPlayList, innternalName));
                    break;
                case (int)Automation.Itx:
                    listBox1.Items.Add(xml.PlaylistItx(filename, displayPlayList, innternalName, false));
                    break;
                default:
                    listBox1.Items.Add(xml.PlaylistSundance(filename, displayPlayList, innternalName, false));
                    break;
            }

        }


        
    }
}
