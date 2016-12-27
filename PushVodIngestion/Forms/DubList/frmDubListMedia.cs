using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PushVodIngestion.Forms.DubList
{
    public partial class frmDubListMedia : Form
    {

        public List<long> MediaSid { get; set; }

        public frmDubListMedia()
        {
            InitializeComponent();
            this.radGridView1.SelectionChanged += radGridView1_SelectionChanged;
            load_grid();
        }

        void radGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (lblSelectedAssest != null) lblSelectedAssest.Text = @"Selected  :" + radGridView1.SelectedRows.Count;
        }

        public void load_grid(int rowIndex = 0, bool byRow = false, String serachText = "")
        {

            List<tblIngestion> lst = LinqBase.GetList<tblIngestion>().Where(i => i.SourceTypeSID != (int)SourceType.Catchup ).ToList();

            var dataGrid = lst.Select(i => new { i.SID, TransmissionId = i.itemCode == null ? i.TxId.ToString() : i.itemCode.ToString(), i.ProgrameName, i.Duration });



            if (serachText.Length > 0)
                dataGrid = dataGrid.Where(i => i.ProgrameName.ToLower().Contains(txtSearchNew.Text.ToLower()) || i.TransmissionId.ToLower().Contains(txtSearchNew.Text.ToLower()));



            this.radGridView1.DataSource = dataGrid;



            if (byRow == true)
                radGridProperty.SelectRowRadGridView(rowIndex, this.radGridView1);

            radGridProperty.change_Property(this.radGridView1, false, false, true);

            lblTotal.Text = @"Total :" + dataGrid.Count();

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
            MediaSid = new List<long>();

            foreach (var row in radGridView1.SelectedRows)
            {
                MediaSid.Add(long.Parse(row.Cells["SID"].Value.ToString()));
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
