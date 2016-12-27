using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic.FileIO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
namespace PushVodIngestion
{
	class Excel_Function
	{
       

        public static  Excel.Workbook Open_excel(Excel.Application _excelApp, string FileName, Boolean visible = false)
        {

           
            Excel.Workbook workBook = _excelApp.Workbooks.Open(FileName,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing);

            _excelApp.Visible = visible;

            return workBook;

        }

        public static void Close_Excel(Excel.Application _excelApp, Excel.Workbook workBook,  Boolean SaveChange= false)
        {
            workBook.Close(SaveChange);
            _excelApp.Quit();
            Marshal.ReleaseComObject(workBook);
            Marshal.ReleaseComObject(_excelApp);
        }
        public static DataTable ImportFromExcel(String path, String SheetName)
        {
            string strConn = null;
            strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 8.0;";

            ////You must use the $ after the object you reference in the spreadsheet

            DataTable dt = default(DataTable);
            OleDbDataAdapter myCommand = default(OleDbDataAdapter);

            myCommand = new OleDbDataAdapter("SELECT * FROM [" + SheetName + "$]", strConn);





            DataSet myDataSet = new DataSet();

            try
            {
                myCommand.Fill(myDataSet, "ExcelInfo");
            }
            catch
            {
                myCommand = new OleDbDataAdapter("SELECT * FROM [sheet1$]", strConn);
                myCommand.Fill(myDataSet, "ExcelInfo");
            }

            dt = myDataSet.Tables[0];



            return dt;

        }


        public static void ExportToExcel(DataTable dt, String ExcelOpenPath, String ExcelSavePath, int sheetIndex, int startRow, int startCol = 1, Boolean Visible = true, Boolean Header = false, String HeaderRange = "A1", String HeaderValue = "")
        {
            // Create the Excel Application object            
            Excel.Application excelApp = new Excel.Application();

            excelApp.DisplayAlerts = false;
            // Create a new Excel Workbook
            //  Dim excelWorkbook As Excel.Workbook = excelApp.Workbooks.Add(Type.Missing)

            Excel.Workbook excelWorkbook;
            if (ExcelOpenPath.Length == 0)
            {
                 excelWorkbook = excelApp.Workbooks.Add();
            }
            else
            {
                excelWorkbook = excelApp.Workbooks.Open(ExcelOpenPath);

                if(ExcelSavePath.Length != 0)
                    excelWorkbook.SaveAs(ExcelSavePath);

            }

            
           
            
            //Dim sheetIndex As Integer = 1
            int col = 0;
            int row = 0;
            Excel.Worksheet excelSheet = default(Excel.Worksheet);
            excelApp.Application.DisplayAlerts = false;
            

           
          



            // Copy the DataTable to an object array
            object[,] rawData = new object[dt.Rows.Count + 1, dt.Columns.Count];

            // Copy the column names to the first row of the object array
            for (col = 0; col <= dt.Columns.Count - 1; col++)
            {
                rawData[0, col] = dt.Columns[col].ColumnName;
            }

            // Copy the values to the object array
            for (col = 0; col <= dt.Columns.Count - 1; col++)
            {
                for (row = 0; row <= dt.Rows.Count - 1; row++)
                {
                    if (dt.Columns[col].ColumnName.ToLower().Contains("date"))
                    {
                        rawData[row + 1, col] = "'"+ DateTime.Parse(dt.Rows[row].ItemArray[col].ToString()).ToString("dd-MMM-yyyy");
                    }
                    else
                    {
                        rawData[row + 1, col] = dt.Rows[row].ItemArray[col].ToString();
                    }
                }
            }

            // Calculate the final column letter


            
            string finalColLetter = col_char(dt.Columns.Count + (startCol -1)) ;
           

            // set sheet range
            excelSheet = excelWorkbook.Sheets[sheetIndex];


            //Header
            if (Header)
                excelSheet.Range[HeaderRange].Value = HeaderValue;

            // Fast data export to Excel
            string excelRange = string.Format("{3}{0}:{1}{2}", startRow, finalColLetter, dt.Rows.Count + startRow, col_char(startCol));
            //MessageBox.Show(excelRange);
            excelSheet.Range[excelRange, Type.Missing].Value2 = rawData;

            // Mark the first row as BOLD
            excelSheet.Rows[startRow, Type.Missing].Font.Bold = true;

            excelSheet.Columns.AutoFit();
           // excelSheet.Rows.RowHeight = 12;



            excel_border_Internal_Outer(excelSheet.Range[excelRange]);

          
            excelSheet = null;

           
          
            excelApp.DisplayAlerts = true;
            //excelWorkbook.Close(True, Type.Missing, Type.Missing)
            excelApp.Visible = Visible;

            excelWorkbook.Save();

            if (Visible == false)
            {
                Close_Excel(excelApp, excelWorkbook, true);   
            }

            
        }

        public static void ExportToExcel2(Excel.Application excelApp, Excel.Workbook excelWorkbook, DataTable dt,  int sheetIndex, int startRow, int startCol = 1)
        {            
            int col = 0;
            int row = 0;
            Excel.Worksheet excelSheet = default(Excel.Worksheet);
            excelApp.Application.DisplayAlerts = false;
            // Copy each DataTable as a new Sheet


            // Copy the DataTable to an object array
            object[,] rawData = new object[dt.Rows.Count + 1, dt.Columns.Count];

            // Copy the column names to the first row of the object array
            for (col = 0; col <= dt.Columns.Count - 1; col++)
            {
                rawData[0, col] = dt.Columns[col].ColumnName;
            }

            // Copy the values to the object array
            for (col = 0; col <= dt.Columns.Count - 1; col++)
            {
                for (row = 0; row <= dt.Rows.Count - 1; row++)
                {

                    rawData[row + 1, col] = dt.Rows[row].ItemArray[col].ToString();

                }
            }

            // Calculate the final column letter

            string finalColLetter = col_char(dt.Columns.Count + (startCol - 1));


            // set sheet range
            excelSheet = excelWorkbook.Sheets[sheetIndex];


            // Fast data export to Excel
            string excelRange = string.Format("{3}{0}:{1}{2}", startRow, finalColLetter, dt.Rows.Count + startRow, col_char(startCol));
            //MessageBox.Show(excelRange);
            excelSheet.Range[excelRange, Type.Missing].Value2 = rawData;

            // Mark the first row as BOLD
            excelSheet.Rows[startRow, Type.Missing].Font.Bold = true;

            excelSheet.Columns.AutoFit();
           // excelSheet.Rows.RowHeight = 12;



            excel_border_Internal_Outer(excelSheet.Range[excelRange]);

                        excelSheet = null;

            excelApp.DisplayAlerts = false;
            // Save and Close the Workbook
            
            excelApp.DisplayAlerts = true;
            //excelWorkbook.Close(True, Type.Missing, Type.Missing)
           
               

        }


        public static string col_char(int col)
        {
            string finalColLetter = string.Empty;
            string colCharset = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int colCharsetLen = colCharset.Length;

            if (col > colCharsetLen)
            {
                finalColLetter = colCharset.Substring((col - 1) / colCharsetLen - 1, 1);
            }

            finalColLetter += colCharset.Substring((col - 1) % colCharsetLen, 1);

            return finalColLetter;
        }

        public static void excel_border_Internal_Outer(Excel.Range rng, Boolean OuterBorder = true)
        {
            rng.BorderAround(ColorIndex: Excel.XlColorIndex.xlColorIndexAutomatic, Weight: Excel.XlBorderWeight.xlThick);


            var _with1 = rng.Borders;
            _with1.LineStyle = Excel.XlLineStyle.xlContinuous;

            if(OuterBorder == true)
                _with1.Weight = Excel.XlBorderWeight.xlThick;
            else
                _with1.Weight = Excel.XlBorderWeight.xlThin;

            var _with2 = rng.Borders[Excel.XlBordersIndex.xlInsideHorizontal];
            _with2.LineStyle = Excel.XlLineStyle.xlContinuous;
            _with2.Weight = Excel.XlBorderWeight.xlThin;

            var _with3 = rng.Borders[Excel.XlBordersIndex.xlInsideVertical];
            _with3.LineStyle = Excel.XlLineStyle.xlContinuous;
            _with3.Weight = Excel.XlBorderWeight.xlThin;
        }

        public static string ColumnIndexToColumnLetter(int colIndex)
        {
            int div = colIndex;
            string colLetter = String.Empty;
            int mod = 0;

            while (div > 0)
            {
                mod = (div - 1) % 26;
                colLetter = (char)(65 + mod) + colLetter;
                div = (int)((div - mod) / 26);
            }
            return colLetter;
        }

        public static void RangeBackRoundColor(Excel.Range rng)
        {
   
            rng.Interior.Pattern = Excel.Constants.xlSolid;
            rng.Interior.PatternColorIndex = Excel.Constants.xlAutomatic;
            rng.Interior.Color  =  255;
         

        }

        public static string RangetoHTML(Excel.Range r)
        {
            r.Copy();

            IDataObject data = Clipboard.GetDataObject();

            string table = data.GetData(DataFormats.Html, true).ToString();

            char[] delimiter = "<".ToCharArray();


            table = "<" + (table.Split(delimiter, 3))[2];


            return table;
        }

        public static string getSheetName(string filename, int sheetNumber)
        {
            string sheetName = "";

            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook wb = Excel_Function.Open_excel(excelApp, filename, false);

            Excel.Worksheet ws = wb.Sheets[sheetNumber] as Excel.Worksheet;
            sheetName = ws.Name;

            Close_Excel(excelApp, wb);


            return sheetName;

        }

        public static DataTable ListToDataTable<T>(List<T> list)
        {
            DataTable dt = new DataTable();

            foreach (PropertyInfo info in typeof(T).GetProperties())
            {
                dt.Columns.Add(new DataColumn(info.Name, GetNullableType(info.PropertyType)));
            }
            foreach (T t in list)
            {
                DataRow row = dt.NewRow();
                foreach (PropertyInfo info in typeof(T).GetProperties())
                {
                    if (!IsNullableType(info.PropertyType))
                        row[info.Name] = info.GetValue(t, null);
                    else
                        row[info.Name] = (info.GetValue(t, null) ?? DBNull.Value);
                }
                dt.Rows.Add(row);
            }
            return dt;
        }

        private static bool IsNullableType(Type type)
        {
            return (type == typeof(string) ||
                    type.IsArray ||
                    (type.IsGenericType &&
                     type.GetGenericTypeDefinition().Equals(typeof(Nullable<>))));
        }

        private static Type GetNullableType(Type t)
        {
            Type returnType = t;
            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                returnType = Nullable.GetUnderlyingType(t);
            }
            return returnType;
        }

        public static DataTable CsvFileToDatatable(string path)
        {
          
            var csvData = new DataTable();;
            try
            {

                using (var csvReader = new TextFieldParser(path))
                {
                    csvReader.SetDelimiters(new[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    //read column names
                    var colFields = csvReader.ReadFields();
                    foreach (var column in colFields)
                    {
                        var datecolumn = new DataColumn(column) {AllowDBNull = true};
                        csvData.Columns.Add(datecolumn);
                    }
                    while (!csvReader.EndOfData)
                    {
                        object[] fieldData = csvReader.ReadFields();
                        //Making empty value as null
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            if ((string) fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }
                        csvData.Rows.Add(fieldData);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return csvData;
        }


	}



}
