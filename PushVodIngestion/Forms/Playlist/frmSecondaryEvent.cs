using Microsoft.Office.Interop.Outlook;
using PushVodIngestion.DataProvider;
using PushVodIngestion.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;

namespace PushVodIngestion.Forms.Playlist
{
    public partial class frmSecondaryEvent : Form
    {
        public frmSecondaryEvent()
        {
            InitializeComponent();
            load_grid();
        }

        public long SID { get; set; }

        private void lblImport_Click(object sender, EventArgs e)
        {


        }

        private void cmbPreview_Enter(object sender, EventArgs e)
        {

        }


        public void load_grid(int RowIndex = 0, bool byRow = false, String SerachText = "")
        {

            var lst = DatabaseLookups.Instance.PlaylistSecondaryEvents.Select(i=> new {i.Description, i.SID});   
            radGridView1.DataSource = lst;
            
            radGridProperty.change_Property(this.radGridView1, false, false, true);
               
          

            Application.DoEvents();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (radGridView1.SelectedRows == null) return;
            if (radGridView1.SelectedRows.Count <= 0) return;
            
            var tblPlaylistSecondaryEvent = radGridView1.SelectedRows[0];

            if (tblPlaylistSecondaryEvent != null) SID = (long)tblPlaylistSecondaryEvent.Cells["SID"].Value;
        }

        private void radGridView1_Click(object sender, EventArgs e)
        {

        }

        private void showSecondaryEventsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (radGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please Select PlayEvent");
                return;
            }
            var secondaryEventID = 0L;

            var row = radGridView1.SelectedRows[0];
            if (row != null)
            {
                secondaryEventID = (long)(row.Cells["SID"].Value);
            }

            if (secondaryEventID > 0)
            {
                var frm = new frmSecondaryEventDetails(secondaryEventID);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("No Secondary Event in this event");
            }
        }
    }
}
