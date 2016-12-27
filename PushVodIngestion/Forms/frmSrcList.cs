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
    public partial class frmSrcList : Form
    {
        public frmSrcList()
        {
            InitializeComponent();
            this.Activated += frmChannelsList_Activated;
            this.radGridView1.SelectionChanged += radGridView1_SelectionChanged;

            load_grid();
        }

        void frmChannelsList_Activated(object sender, EventArgs e)
        {


        }

        private void Edit_Load()
        {
            if (radGridView1.SelectedRows == null)
                return;

            long sid = int.Parse(radGridView1.SelectedRows[0].Cells["SID"].Value.ToString());
            Forms.frmSrcEdit f = new frmSrcEdit(sid);
            f.Form_Load(radGridView1.CurrentRow.Index);
        }

        void radGridView1_SelectionChanged(object sender, EventArgs e)
        {
            lblSelectedAssest.Text = "Selected  :" + radGridView1.SelectedRows.Count.ToString(); ;

        }





        public void load_grid(int RowIndex = 0, bool byRow = false, String SerachText = "")
        {


            string Sql = "Select * from tblSrc";

            if (SerachText.Length > 0)
                Sql = "Select * from tblSrc WHERE Name Like '%" + SerachText + "%'";


            DataTable dt = DAL.data_set(Sql);
            this.radGridView1.DataSource = dt;



            if (byRow == true)
                radGridProperty.SelectRowRadGridView(RowIndex, this.radGridView1);

            radGridProperty.change_Property(this.radGridView1, true, false, true);

            lblTotal.Text = "Total :" + dt.Rows.Count;


        }

        private void txtSearchNew_TextChanged(object sender, EventArgs e)
        {
            load_grid(0, false, txtSearchNew.Text);
        }

        void add()
        {
            Forms.frmSrcEdit f = new frmSrcEdit(0);
            f.Form_Load(0);
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            add();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Edit_Load();
        }

        private void txtSearchNew_TextChanged_1(object sender, EventArgs e)
        {
            load_grid(0, false, txtSearchNew.Text);
        }
    }
}
