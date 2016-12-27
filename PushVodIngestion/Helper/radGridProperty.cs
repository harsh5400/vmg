using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Telerik.WinControls.UI;
using System.Drawing;
using System.Windows.Forms;

namespace PushVodIngestion
{
    class radGridProperty
    {

        public static void change_Property(RadGridView radGridView1, bool filter = false, bool alterNativeColor = true, bool colFill = false)
        {

            try
            {



                radGridView1.EnableGrouping = false;
                radGridView1.AllowAddNewRow = false;
                radGridView1.AllowDeleteRow = false;

                if(colFill)
                radGridView1.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;


                radGridView1.AllowEditRow = false;
                radGridView1.EnableFiltering = filter;

                radGridView1.EnableAlternatingRowColor = alterNativeColor;
                (radGridView1.TableElement as GridTableElement).AlternatingRowColor = Color.LightGreen;


                foreach (var col in radGridView1.Columns)
                {

                    if (col.Name.ToUpper().Contains("SID"))
                    {
                        col.IsVisible = false;
                    }

                    if (col.Name.ToUpper().Contains("DATE"))
                    {
                        col.FormatString = "{0:dd-MMM-yyyy}";
                    }


                    radGridView1.BestFitColumns();
                }



            }
            catch { }

        

        }


        public static void SelectRowRadGridView(int rowIndex, RadGridView radGridView1)
        {
            try
            {
                radGridView1.Rows[rowIndex].IsSelected = true;
                radGridView1.Rows[rowIndex].IsCurrent = true;
                radGridView1.TableElement.ScrollToRow(radGridView1.Rows[rowIndex]);
            }
            catch { }
        }

        public static void RowSelected(RadGridView radGridView1, int rowIndex)
        {
            radGridView1.Rows[rowIndex].IsSelected = true;
                radGridView1.Rows[rowIndex].IsCurrent = true;
                radGridView1.TableElement.ScrollToRow(radGridView1.Rows[rowIndex]);
        }

      public static  void Load_Grid(string sql, Telerik.WinControls.UI.RadGridView radGridView1)
      {
             DataTable dt = DAL.data_set(sql);
          
              radGridView1.DataSource = dt;
              change_Property(radGridView1);
          
      }

      public static void addRow(int Row, Telerik.WinControls.UI.RadGridView radGridView1)
      {
          for (int i = 1; i <= Row; i++)
          {
              var rowInfo = new GridViewDataRowInfo(radGridView1.MasterView);
              radGridView1.Rows.Add(rowInfo);
          }


          radGridView1.Rows[0].IsSelected = true;
          radGridView1.Rows[0].IsCurrent = true;

          var tableElement = radGridView1.CurrentView as GridTableElement;
          var row = radGridView1.CurrentRow;

          if (tableElement != null && row != null)
          {
              tableElement.ScrollToRow(row);
          }
      }

         public static void RadcolorRow( RadGridView radGridView1 , int ROWiNDEX, System.Drawing.Color color)
         {
                    for(int rrr  = 0; rrr< radGridView1.Columns.Count;rrr++)
                    {
                        radGridView1.Rows[ROWiNDEX].Cells[rrr].Style.BackColor = color;
                        radGridView1.Rows[ROWiNDEX].Cells[rrr].Style.DrawFill = true;
                        radGridView1.Rows[ROWiNDEX].Cells[rrr].Style.CustomizeFill = true;
                    }
         }


         
         public static void SelectRowDataGridView(int rowIndex, DataGridView dgvSearchResults)
         {
             dgvSearchResults.ClearSelection();
             dgvSearchResults.Rows[rowIndex].Selected = true;
             dgvSearchResults.FirstDisplayedScrollingRowIndex = rowIndex;
             dgvSearchResults.Focus();
         }

         public static void AllRowColorDataGrid(DataGridView datagridAccess, System.Drawing.Color color)
         {
             foreach (DataGridViewRow row in datagridAccess.Rows)
             {
                 if (row.Cells["Select"].Value != null)
                 {
                     if (row.Cells["Select"].Value.ToString() == "1" || row.Cells["Select"].Value.ToString().ToLower() == "true")
                     {
                         row.DefaultCellStyle.BackColor = color;
                         
                     }
                 }

             }
         }



    }
}
