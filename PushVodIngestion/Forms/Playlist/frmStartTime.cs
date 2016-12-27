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
    public partial class frmStartTime : Form
    {
        public frmStartTime(TimeSpan time, DateTime date)
        {
            InitializeComponent();
            dateTimePicker1.Value = new DateTime(2015,1,1,0,0,0);

            if (time != null)
                dateTimePicker1.Value = new DateTime(2015, 1, 1, time.Hours, time.Minutes, time.Seconds);

            if (date != null)
                dtDate.Value = date.Date;

        }

        public TimeSpan StartTime { get; set; }
        public DateTime schDate { get; set; }

        private void radButton2_Click(object sender, EventArgs e)
        {
            StartTime = new TimeSpan(dateTimePicker1.Value.Hour,dateTimePicker1.Value.Minute, dateTimePicker1.Value.Second) ;
            schDate = dtDate.Value.Date;            
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }




    }
}
