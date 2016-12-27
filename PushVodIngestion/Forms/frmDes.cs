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
    public partial class frmDes : Form
    {
        public frmDes()
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
            Forms.frmDesEdit f = new frmDesEdit(sid);
            f.Form_Load(radGridView1.CurrentRow.Index);
        }

        void radGridView1_SelectionChanged(object sender, EventArgs e)
        {
            lblSelectedAssest.Text = "Selected  :" + radGridView1.SelectedRows.Count.ToString(); ;

        }



        void add()
        {
            Forms.frmDesEdit f = new frmDesEdit(0);
            f.Form_Load(0);
        }

        public void load_grid(int RowIndex = 0, bool byRow = false, String SerachText = "")
        {



            string Sql = "Select * from tblDes";

            if (SerachText.Length > 0)
                Sql = "Select * from tblDes WHERE Name Like '%" + SerachText + "%'";


            DataTable dt = DAL.data_set(Sql);
            this.radGridView1.DataSource = dt;



            if (byRow == true)
                radGridProperty.SelectRowRadGridView(RowIndex, this.radGridView1);

            radGridProperty.change_Property(this.radGridView1, true, false, true);

            lblTotal.Text = "Total :" + dt.Rows.Count;


        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            add();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Edit_Load();
        }
    }
}
