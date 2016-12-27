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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            this.radGridView1.SelectionChanged += radGridView1_SelectionChanged;
            load_grid();
            this.Activated += Main_Activated;
            comboLoad();

            dtDate.Value = DateTime.Now.Date;
        }


        void comboLoad()
        {

            
            //this.desSIDComboBox.DataSource = LinqBase.GetList<tblDe>().OrderBy(i => i.Name).ToList();
            //desSIDComboBox.DisplayMember = "Name";
            //desSIDComboBox.ValueMember = "SID";


        }

        void Main_Activated(object sender, EventArgs e)
        {
            //load_grid();
        }


        public void load_grid(int RowIndex = 0, bool byRow = false, String SerachText = "")
        {


            if(chkFilter.Checked)
            {
                loadIngegtions();
                return;
            }


            string Sql = "Select  * from vwIngestion WHERE SourceTypeSID = " + ((int)SourceType.Catchup).ToString() +" Order by SID DESC";

            if (SerachText.Length > 0)
                Sql = "Select  * from vwIngestion Where (ProgrameName like '%" + SerachText + "%' OR ChannelName like '%" + SerachText + "%' OR TXID like '%" + SerachText + "%') AND ( SourceTypeSID = " + ((int)SourceType.Catchup).ToString() + ")  Order by SID DESC";

            
            DataTable dt = DAL.data_set(Sql);
            this.radGridView1.DataSource = dt;



            if (byRow == true)
                radGridProperty.SelectRowRadGridView(RowIndex, this.radGridView1);

            radGridProperty.change_Property(this.radGridView1, false, false, true);

            lblTotal.Text = "Total :" + dt.Rows.Count;

            Application.DoEvents();
        }

        void radGridView1_SelectionChanged(object sender, EventArgs e)
        {
            lblSelectedAssest.Text = "Selected  :" + radGridView1.SelectedRows.Count.ToString(); ;

        }

        private void cmbPreview_Enter(object sender, EventArgs e)
        {

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            add();
        }

        void add()
        {

            Forms.frmIngestion f = new frmIngestion(0);
            f.Form_Load(0);
        }

        private void channelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.frmChannelsList f = new frmChannelsList();
            f.Show();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Edit_Load();
        }

        private void Edit_Load()
        {
            if (radGridView1.SelectedRows == null)
                return;

            long sid = int.Parse(radGridView1.SelectedRows[0].Cells["SID"].Value.ToString());
            Forms.frmIngestion f = new frmIngestion(sid);
            f.Form_Load(radGridView1.CurrentRow.Index);
        }

        private void txtSearchNew_TextChanged(object sender, EventArgs e)
        {
            load_grid(0, false, txtSearchNew.Text);
        }

        private void addRecordingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            add();
        }

        private void editRecordingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Edit_Load();
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            frmExport f = new frmExport();
            f.ShowDialog();
        }

        private void sourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSrcList f = new frmSrcList();
            f.Show();
        }

        private void destinationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.frmDes f = new frmDes();
            f.Show();
        }

        private void chkFilter_CheckedChanged(object sender, EventArgs e)
        {
            grpFilter.Visible = chkFilter.Checked;
            load_grid();
        }


        void loadIngegtions()
        {

            long SourceTypeSID = (long)SourceType.Catchup;
                   List<vwIngestion> lstIngestion = LinqBase.GetList<vwIngestion>().Where(i=>i.SourceTypeSID == SourceTypeSID).Where(i =>  i.StartDate == dtDate.Value.Date).ToList();

            radGridView1.DataSource = lstIngestion;
            radGridProperty.change_Property(radGridView1, false, false, true);
        }

        private void dtDate_ValueChanged(object sender, EventArgs e)
        {
            load_grid();
        }

        private void desSIDComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_grid();
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete", "Alert", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            if (radGridView1.SelectedRows.Count > 0)
            {
                var SIDs = radGridView1.SelectedRows.Select(i => int.Parse(radGridView1.Rows[i.Index].Cells["SID"].Value.ToString())).ToList();
                foreach (var siD in SIDs)
                {
                    var playlist = LinqBase.GetList<tblPlaylist>(true).Where(i => i.MediaSID != null && i.MediaSID.Value == siD).ToList();

                    if (playlist.Count == 0)  
                        DAL.data_command("Delete from tblIngestion Where SID=" + siD);
                    else
                    {
                        MessageBox.Show(playlist[0].tblIngestion.TxId + ", Title: " + playlist[0].tblIngestion.TxId + "  cannot be Deleted becoause It is in use on " + playlist[0].Date.Value.ToString("dd-MMM-yyyy") + " playlist");
                    }
                    
                }

                load_grid();
            }
        }
    }
}
