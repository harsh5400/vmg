using System.Windows.Forms;
using PushVodIngestion.Helper;

namespace PushVodIngestion.Forms.Playlist
{
    public partial class MediaHistory : Form
    {

        private long _mediaSID;
        public MediaHistory(long mediaSID)
        {
            _mediaSID = mediaSID;
            InitializeComponent();

            load_grid();
        }

        void load_grid()
        {

            var sql = "SELECT TOP 100 tblPlaylist.[SID] , tblPlayoutPort.ChannelName, tblIngestion.itemCode AS TXID, tblIngestion.ProgrameName, tblPlaylist.[PlayoutTime], tblPlaylist.[Date] FROM [VMG].[dbo].[tblPlaylist] INNER JOIN tblIngestion on tblIngestion.SID = tblPlaylist.MediaSID  INNER JOIN tblPlayoutPort on tblPlayoutPort.SID = tblPlaylist.PlayoutPortSID Where tblPlaylist.MediaSID = '" + _mediaSID + "' Order by tblPlaylist.Date Desc";

            var dt = ServiceHelper.Instance.data_set(sql);

            if (dt.Rows.Count > 0)
            {
                radGridView1.DataSource = dt;
                radGridProperty.change_Property(radGridView1);

                lblMediainfo.Text = dt.Rows[0]["TXID"] + @"  -   " +
                                    dt.Rows[0]["ProgrameName"];
                Application.DoEvents();

            }
            else
            {
                MessageBox.Show("No History Found");
                lblMediainfo.Text = "No History Found";
                Application.DoEvents();
            }

        }

    }
}
