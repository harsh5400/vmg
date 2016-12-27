using PushVodIngestion.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PushVodIngestion.Forms.Playlist
{
    public partial class frmReplicatePlaylist : Form
    {
        public frmReplicatePlaylist(DataProvider.tblPlayoutPort channel, DateTime selectedDate)
        {
            Channel = channel;
            SelectedDate = selectedDate;
            InitializeComponent();

            label3.Text = Channel.Descriptions;

            //adding dates
            dfFrom.Value = selectedDate.Date.AddDays(1);
            dtTo.Value = selectedDate.Date.AddDays(6); 

        }

        public DataProvider.tblPlayoutPort Channel { get; set; }
        public DateTime SelectedDate { get; set; }

        private void butSave_Click(object sender, EventArgs e)
        {
            var currentDate = dfFrom.Value.Date;
            listBox1.Items.Clear();

            var dateDiff = dtTo.Value.Date.Subtract(currentDate).Days + 1;
            
            if (dateDiff < 0)
            {
                listBox1.Items.Add("Error:- Wrong date Selection");
                return;

            }
            for (var i=1 ; i<=dateDiff; i++)
            {
                if (CheckData(currentDate.Date, Channel.SID))
                {
                    //Copy Playlist to another dates;
                    CopyPlaylist(currentDate.Date, Channel.SID, SelectedDate.Date);

                    listBox1.Items.Add("Done:- " + currentDate.ToString("dd-MMM-yyyy") + " - " + Channel.Descriptions + " - Copied");
                }
                else
                {
                    listBox1.Items.Add("Error:- " + currentDate.ToString("dd-MMM-yyyy") +  " - " + Channel.Descriptions + " has already a playlist. So it cannot Copy. Please delete Playlist to copy other playlist, manually");
                }

            
               currentDate =  currentDate.AddDays(1);
            }
        }

        Boolean CheckData(DateTime dt, long tblPlayoutPortSID)
        {
            var result = true;
            var instance = typeof(DataProvider.tblPlaylist).AssemblyQualifiedName;
            var entities = ServiceHelper.Instance.GetEntitiesBasedOnCondition(instance, "PlayoutPortSID == @0 && Date ==@1", new ObservableCollection<object>() { tblPlayoutPortSID, dt.Date });
            var playList = new List<DataProvider.tblPlaylist>(entities.Cast<DataProvider.tblPlaylist>().ToList().OrderBy(i => i.PlayOrderSID).ToList());

            if (playList.Count > 0)
                result = false;

            return result;

        }

        void CopyPlaylist(DateTime playListDate, long playoutPortSID, DateTime SelectedDate)
        {
            //saving existing playlist in to Udo table
           
            
            ServiceHelper.Instance.DataCommand(
                "Insert into  tblPlaylist ([PlayoutTime], [Date], [PlayoutPortSID], [MediaSID], [addon], [modon], [Status], [PlayOrderSID], [FixedEvent], [SchDate], [Approved], [Exported]) select [PlayoutTime], '" + playListDate.Date.ToString("dd-MMM-yyyy") + "', [PlayoutPortSID], [MediaSID], [addon], [modon], [Status], [PlayOrderSID], [FixedEvent], '" + playListDate.Date.ToString("dd-MMM-yyyy") + "', [Approved], [Exported] from [tblPlaylist] WHERE Date = '" +
                SelectedDate.Date.ToString("dd-MMM-yyyy") + "' AND PlayoutPortSID=" + playoutPortSID);
           
        }
    }
}
