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
    public partial class frmExport : Form
    {
        public frmExport()
        {
            InitializeComponent();

            saveFileDialog1.FileOk += saveFileDialog1_FileOk;
        }

        void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            ////string name = saveFileDialog1.FileName;
            //textBox1.Text = name;
            
        }

        private void frmExport_Load(object sender, EventArgs e)
        {

        }

        private void radButton2_Click(object sender, EventArgs e)
        {

            string filename = "";
            saveFileDialog1.FileName = "Evs_Recording_XML_" + dateTimePicker1.Value.ToString("ddMMyyyy");
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = saveFileDialog1.FileName;
            }
            else
            {
              MessageBox.Show("Please Select File");

                                               return; 
            }

            

            List<vwIngestion> lst = LinqBase.GetList<vwIngestion>().Where(i=> i.SourceTypeSID == (int)SourceType.Catchup).Where(i=> i.StartDate == dateTimePicker1.Value.Date).ToList();

            Helper.XMLCreater xml = new Helper.XMLCreater();
            xml.IngestionXml(filename, lst);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void radButton4_Click(object sender, EventArgs e)
        {

            //get last version

            int version = 1;

            tblReportVersion tblRevison = LinqBase.GetList<tblReportVersion>().Where(i => i.lastDate.Value.Date == dateTimePicker1.Value.Date && i.ReportName == Redirection.Recording.ToString()).FirstOrDefault();

            try
            {
                if (tblRevison != null)
                {
                    version = tblRevison.lastVersion.Value + 1;


                    tblRevison.lastVersion = version;
                    LinqBase.UpdateDatabaseWithItem<tblReportVersion>(tblRevison);
                }
                else
                {
                    tblRevison = new tblReportVersion();
                    tblRevison.ReportName = Redirection.Recording.ToString();
                    tblRevison.lastVersion = version;
                    tblRevison.lastDate = dateTimePicker1.Value.Date;
                    LinqBase.InsertItemIntoDatabase<tblReportVersion>(tblRevison);
                }
            }
            catch { }


            saveFileDialog2.FileName = "Evs_Recording_Excel_" + dateTimePicker1.Value.ToString("ddMMyyyy") + "_V" + version.ToString();

            string filename = "";
            if (saveFileDialog2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = saveFileDialog2.FileName;
            }
            else
            {
                MessageBox.Show("Please Select File");

                return;
            }



            List<vwIngestion> lst = LinqBase.GetList<vwIngestion>().Where(i => i.SourceTypeSID == (int)SourceType.Catchup).Where(i => i.StartDate == dateTimePicker1.Value.Date).ToList();

            DataTable dt = Excel_Function.ListToDataTable<vwIngestion>(lst);



            try
            {
                dt.Columns.RemoveAt(12);
                dt.Columns.RemoveAt(11);
                dt.Columns.RemoveAt(0);
            }
            catch { }

            Excel_Function.ExportToExcel(dt, Application.StartupPath + "\\data\\RecordingTemplate.xlsx", filename, 1, 3, 1, true, true, "A1", "Evs Recording Schedule" + dateTimePicker1.Value.ToString("dd-MMM-yyyy"));
        }

        private void radButton1_Click(object sender, EventArgs e)
        {

            //get last version

            int version =1;

            tblReportVersion tblRevison = LinqBase.GetList<tblReportVersion>().Where(i => i.lastDate.Value.Date == dateTimePicker1.Value.Date && i.ReportName == Redirection.Recording.ToString()).FirstOrDefault();

            try
            {
                if (tblRevison != null)
                {
                    version = tblRevison.lastVersion.Value + 1;


                    tblRevison.lastVersion = version;
                    LinqBase.UpdateDatabaseWithItem<tblReportVersion>(tblRevison);
                }
                else
                {
                    tblRevison = new tblReportVersion();
                    tblRevison.ReportName = Redirection.Recording.ToString();
                    tblRevison.lastVersion = version;
                    tblRevison.lastDate = dateTimePicker1.Value.Date;
                    LinqBase.InsertItemIntoDatabase<tblReportVersion>(tblRevison);
                }
            }
            catch { }


            saveFileDialog2.FileName = "Evs_Recording_Excel_"  + dateTimePicker1.Value.ToString("ddMMyyyy") + "_V" + version.ToString();
            string filename = "";
            if (saveFileDialog2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = saveFileDialog2.FileName;

            }
            else
            {
                MessageBox.Show("Please Select File");

                return;
            }



            List<vwIngestion> lst = LinqBase.GetList<vwIngestion>().Where(i => i.SourceTypeSID == (int)SourceType.Catchup).Where(i => i.StartDate == dateTimePicker1.Value.Date).ToList();

            DataTable dt = Excel_Function.ListToDataTable<vwIngestion>(lst);



            try
            {
                dt.Columns.RemoveAt(12);
                dt.Columns.RemoveAt(11);
                dt.Columns.RemoveAt(0);
            }
            catch { }

            Excel_Function.ExportToExcel(dt, Application.StartupPath + "\\data\\RecordingTemplate.xlsx", filename, 1, 3, 1, false, true, "A1", "Evs Recording Schedule" + dateTimePicker1.Value.ToString("dd-MMM-yyyy"));


            string body = "Hi Team<br/><br/>PFA excel exported from the VMG for shows set for recording  for " + dateTimePicker1.Value.ToString("dd-MMM-yyyy") + " in XL and XML format.<br/><br/>Regards,<br/>";
            string subject = "Shows set for Recording  - " + dateTimePicker1.Value.ToString("dd MMM yyyy") + " Version " + version.ToString();
            string to = "Playoutops";
            string cc = "DL-NDS Operations; Vinitha Kompella/MKTG/BLR/KAR; Anitha Joy/MKTG/BLR/KAR; Joby Sebastian/TECH/DEL/DL; Asha Malini/MKTG/BLR/KAR; rabiyas@tatasky.com";
            Helper.Mail.SentMail_Outlook(body, subject, to, cc, filename);


        }
    }
}
