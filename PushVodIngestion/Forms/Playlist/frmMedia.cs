using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PushVodIngestion.Helper;

namespace PushVodIngestion.Forms.Playlist
{
    public partial class frmMedia : Form
    {

        public List<long> MediaSID { get; set; }

        public frmMedia()
        {
            InitializeComponent();
            this.radGridView1.SelectionChanged += radGridView1_SelectionChanged;
            this.radGridView1.DoubleClick += radGridView1_DoubleClick;
            load_grid();
         
        }

        private void Edit_Load()
        {
            if (radGridView1.SelectedRows == null)
                return;

            long sid = int.Parse(radGridView1.SelectedRows[0].Cells["SID"].Value.ToString());
            var f = new frmMediaEdit(sid);
            f.Form_Load(radGridView1.CurrentRow.Index);
        }

        void radGridView1_DoubleClick(object sender, EventArgs e)
        {
            Save();
        }

        void radGridView1_SelectionChanged(object sender, EventArgs e)
        {
            lblSelectedAssest.Text = "Selected  :" + radGridView1.SelectedRows.Count;

        }


        public void load_grid(int RowIndex = 0, bool byRow = false, String SerachText = "")
        {

            var lst = DatabaseLookups.Instance.Ingestions.Where(i=> i.ReadyToAir == chkReadyAir.Checked).ToList();

            var dataGrid = lst.Select(i => new { i.SID, TransmissionId = i.itemCode == null ? i.TxId.ToString(CultureInfo.InvariantCulture) : i.itemCode.ToString(CultureInfo.InvariantCulture), i.ProgrameName, i.Duration});

           

            if (SerachText.Length > 0)
                dataGrid  = dataGrid.Where(i=> i.ProgrameName.ToLower().Contains(txtSearchNew.Text.ToLower()) || i.TransmissionId.ToLower().Contains(txtSearchNew.Text.ToLower()));


            
            radGridView1.DataSource = dataGrid;



            if (byRow == true)
                radGridProperty.SelectRowRadGridView(RowIndex, this.radGridView1);

            radGridProperty.change_Property(this.radGridView1, false, false, true);

            lblTotal.Text = "Total :" + dataGrid.Count();

            Application.DoEvents();
        }

        private void txtSearchNew_TextChanged(object sender, EventArgs e)
        {
            load_grid(0, false, txtSearchNew.Text);
        }

        private void butSave_Click(object sender, EventArgs e)
        {

            Save();
          
        }

        void Save()
        {
            MediaSID = new List<long>();

            foreach (var row in radGridView1.SelectedRows)
            {   
                MediaSID.Add(long.Parse(row.Cells["SID"].Value.ToString()));
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Edit_Load();
        }

        private void chkReadyAir_CheckedChanged(object sender, EventArgs e)
        {
            load_grid();
        }
    }
}
