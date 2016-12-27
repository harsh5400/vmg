using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PushVodIngestion.Forms.DubList
{
    public partial class frmDubList : Form
    {
        ObservableCollection<vwDubList> dubList = new ObservableCollection<vwDubList>();
        public frmDubList()
        {
            InitializeComponent();
            dateTimePicker1.Value = DateTime.Now.Date;

            load_grid();

            DataChange();
        }

        public void DataChange()
        {
                          dubList.Clear();
            var lstDublist =
                LinqBase.GetList<vwDubList>().Where(i => i.Date.Value.Date == dateTimePicker1.Value.Date).ToList();

            foreach (var item in lstDublist)
            {
              dubList.Add(item);  
            }
           

        }
        public void load_grid(int RowIndex = 0, bool byRow = false, String SerachText = "")
        {
            
            this.radGridView1.DataSource = dubList;



            if (byRow == true)
                radGridProperty.SelectRowRadGridView(RowIndex, this.radGridView1);

            radGridProperty.change_Property(this.radGridView1, false, false, true);

            lblTotal.Text = "Total :" + dubList.Count.ToString();


        }

        private void cmbPreview_Enter(object sender, EventArgs e)
        {
           
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DataChange();
        }

        private void butSave_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "DubList_" + dateTimePicker1.Value.ToString("MMddyy");
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                Helper.XMLCreater f = new Helper.XMLCreater();
                f.dubListXml(saveFileDialog1.FileName, dubList.ToList());
                  
                
            }
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            var f = new frmDubListMedia();
            if (f.ShowDialog() == DialogResult.OK)
            {
                foreach (var sid in f.MediaSid)
                {
                  
                    var media = LinqBase.GetList<tblIngestion>().FirstOrDefault(i => i.SID == sid);
                    if (media != null)
                    {
                        var dubItem = new vwDubList();
                        dubItem.Duration = media.Duration;
                        dubItem.ProgrameName = media.ProgrameName;
                        dubItem.itemCode = media.itemCode;
                        dubItem.Date = dateTimePicker1.Value.Date;
                        dubList.Add(dubItem);

                    }
                }
            }
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            if (radGridView1.SelectedRows.Count > 0)
            {
               var indexes = radGridView1.SelectedRows.Select(rows => rows.Index).ToList();

                indexes = indexes.OrderByDescending(i=> i).ToList();

                foreach (var index in indexes)
                    dubList.RemoveAt(index);
            }
        }
    }
}
