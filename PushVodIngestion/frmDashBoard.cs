using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using PushVodIngestion.Helper;

namespace PushVodIngestion
{
    public partial class frmDashBoard : Form
    {
        public frmDashBoard()
        {
            InitializeComponent();
           
            this.FormClosing += frmDashBoard_FormClosing;
           
        }


      

        void frmDashBoard_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            var f = new Forms.Main();
            f.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var f = new  Forms.Playlist.frmPlaylist(true);
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var f = new Forms.DubList.frmDubList();
            f.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var f = new Forms.Playlist.frmMedia();
            f.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
           
        }
    }
}
