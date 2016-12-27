using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PushVodIngestion.Forms.Playlist
{
    public partial class frmLiveEventDuration : Form
    {
        public frmLiveEventDuration()
        {
            InitializeComponent();
        }

        public TimeSpan Duration { get; set; }

        private void radButton2_Click(object sender, EventArgs e)
        {
            Duration = new TimeSpan(dateTimePicker1.Value.Hour, dateTimePicker1.Value.Minute, dateTimePicker1.Value.Second);
     
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
           Close();
        }
    }
}
