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
    public partial class frmSelectChannel : Form
    {
        public frmSelectChannel(List<DataProvider.tblPlayoutPort> lstChannel)
        {
            InitializeComponent(); 
            cmbPlayoutPort.DataSource = lstChannel;
            cmbPlayoutPort.DisplayMember = "Descriptions";
            cmbPlayoutPort.ValueMember = "SID";
        }

        public DateTime Date { get; set; }
        public long ChannelSID { get; set; }

        private void radButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            Date = dateTimePicker1.Value.Date;
            ChannelSID = (long) cmbPlayoutPort.SelectedValue;
        }

    }
}
