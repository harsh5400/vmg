using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using Telerik.WinControls.UI;
namespace PushVodIngestion
{
    class pdlMasterHelper
    {
        

        public static void Form_loadGrid(int rowno =0, string formNAme="frmPdlMaster")
        {

            switch (formNAme)
            {
                case "Main":
                    Forms.Main obj = (Forms.Main)Application.OpenForms[formNAme];
                    if (obj != null)
                    {
                        if (rowno == 0)
                            obj.load_grid();
                        else
                            obj.load_grid(rowno, true);

                    }
                    break;

                case "frmChannelsList":
                    Forms.frmChannelsList obj2 = (Forms.frmChannelsList)Application.OpenForms[formNAme];
                    if (obj2 != null)
                    {
                        if (rowno == 0)
                            obj2.load_grid();
                        else
                            obj2.load_grid(rowno, true);

                    }
                    break;
                case "frmSrcList":
                    Forms.frmSrcList obj3 = (Forms.frmSrcList)Application.OpenForms[formNAme];
                    if (obj3 != null)
                    {
                        if (rowno == 0)
                            obj3.load_grid();
                        else
                            obj3.load_grid(rowno, true);

                    }
                    break;


                case "frmDes":
                    Forms.frmDes obj4 = (Forms.frmDes)Application.OpenForms[formNAme];
                    if (obj4 != null)
                    {
                        if (rowno == 0)
                            obj4.load_grid();
                        else
                            obj4.load_grid(rowno, true);

                    }

                    break;

                case "frmMedia":
                    Forms.Playlist.frmMedia obj5 = (Forms.Playlist.frmMedia)Application.OpenForms[formNAme];
                    if (obj5 != null)
                    {
                        if (rowno == 0)
                            obj5.load_grid();
                        else
                            obj5.load_grid(rowno, true);

                    }

                    break;
                    
                    
                //    default:
                //     frmPdlMaster obj1 = (frmPdlMaster)Application.OpenForms[formNAme];
                //     if (obj1 != null)
                //     {
                //         if (rowno == 0)
                //             obj1.load_grid();
                //         else
                //             obj1.load_grid(rowno, true);

                //     }
                //         break;



                //}

            }


           

            
            }

        public static DataTable ListToDataTable(List<String> List, String table,  String Key)
        {
            DataTable dt = new DataTable();

            dt = DAL.data_set("Select * from  " + table + "  Where " + Key + "='0'");

            foreach (String id in List)
            {
                DataTable tempDt = DAL.data_set("Select * from " + table +"  Where " + Key + "='" + id + "'");

                if (tempDt.Rows.Count > 0)
                {
                    DataRow row = dt.NewRow();

                    foreach (DataColumn col in tempDt.Columns)
                    {
                        try
                        {
                            if (tempDt.Rows[0][col.ColumnName].ToString() != null || tempDt.Rows[0][col.ColumnName].ToString().Length != 0)
                            row[col.ColumnName] = tempDt.Rows[0][col.ColumnName].ToString();
                        }
                        catch (Exception ex)
                        {
                         DAL.create_logs (tempDt.Rows[0][col.ColumnName].ToString() + "  " + ex.ToString());
                        }





                    }

                    dt.Rows.Add(row);
                }
            }

            return dt;
        }


        

        public static List<String> Get_Selected_Row(String RowName, RadGridView  radGridView1)
        {
            List<String> ExportList = new List<String>();

            if (radGridView1.CurrentRow != null)
            {
                GridViewSelectedRowsCollection RowCollection = radGridView1.SelectedRows;

                foreach (GridViewRowInfo GridDataRow in RowCollection)
                {
                    ExportList.Add(GridDataRow.Cells[RowName].Value.ToString());
                }


            }

            return ExportList;

        }

        

    }
}
