using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows.Forms;
using PushVodIngestion.Properties;
using Microsoft.Office.Interop.Outlook;

/// <summary>
/// Summary description for DAL
/// </summary>
/// 
namespace PushVodIngestion
{

    public class DAL
    {
        private static SqlConnection con = new SqlConnection();
        public DAL()
        {

        }

        static void Connection_Open()
        {
            try
            {
                con.ConnectionString = Global.ConnectionStringProject;
                
                //string value = System.Configuration.ConfigurationSettings.AppSettings["dbPlayoutConnectionString"];

                //string value1 = ConfigurationManager.AppSettings["MySetting"];
                con.Open();
            }
            catch
            {
            }
        }

        static void Connection_Close()
        {
            con.Close();
        }

        public static DataTable data_set(string SQL_Command)
        {
            DataTable functionReturnValue = default(DataTable);


            Connection_Open();

            DataSet DS = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            string SQL = SQL_Command;
            da = new SqlDataAdapter(SQL, con);
            da.Fill(DS, "table");
            functionReturnValue = DS.Tables[0];
            Connection_Close();
            return functionReturnValue;
        }

        public static int data_Count(string SQL_Command)
        {
            DataTable functionReturnValue = default(DataTable);


            Connection_Open();

            DataSet DS = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            string SQL = SQL_Command;
            da = new SqlDataAdapter(SQL, con);
            da.Fill(DS, "table");
            functionReturnValue = DS.Tables[0];
            Connection_Close();

            if (functionReturnValue.Rows.Count > 0)
            {
                return functionReturnValue.Rows.Count;
            }
            else
            {
                return 0;
            }


        }

        public static void data_command(string SQL_Command)
        {
            string SQL = SQL_Command;
            SqlCommand cmd = new SqlCommand();

            Connection_Open();


            cmd = new SqlCommand(SQL, con);
            cmd.ExecuteNonQuery();
            Connection_Close();
        }

       


       


       
       
        public static bool isNumeric(string val, System.Globalization.NumberStyles NumberStyle)
        {
            Double result;
            return Double.TryParse(val, NumberStyle,
                System.Globalization.CultureInfo.CurrentCulture, out result);
        }


        public static void create_logs(string message)
        {
            string strFile = System.Windows.Forms.Application.StartupPath + "\\log.dat";
            bool fileExists = File.Exists(strFile);

            StreamWriter sw = new StreamWriter(strFile, true);
            sw.WriteLine(DateTime.Now.ToString("dd-MMM-yyy HH:mm:ss") + "-" +  message);
            sw.Close();

        }

        public static string DectoFrameDuration(decimal value)
        {
            try
            {
                TimeSpan ts = TimeSpan.FromDays(Decimal.ToDouble(value));
                return ts.Hours.ToString("00") + ":" + ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00") + ":00";
            }
            catch (System.Exception ex)
            {
                create_logs(" DectoFrameDuration Error- " + ex.ToString());
                return "00:00:00:00";
            }

        }

        public static DataTable AddAutoIncrementColumn(DataTable datatable)
        {


    //         DataColumn Col   = datatable.Columns.Add("SNo", System.Type.GetType("System.Boolean"));
    //Col.SetOrdinal(0);// to put the column in position 0;

            datatable.Columns.Add("S.No", typeof(int));


            for (int count = 0; count < datatable.Rows.Count; count++)
            {
                datatable.Rows[count]["S.No"] = count + 1;
            }

             datatable.Columns["S.No"].SetOrdinal(0);
            
            return datatable;

        }

        public static void SentMail_Outlook(string body, string subject, string toMail, string cc, string attachment)
        {

	            try
                {

                    Microsoft.Office.Interop.Outlook.Application olApp = new Microsoft.Office.Interop.Outlook.Application();

                    MailItem olMail = (MailItem)olApp.CreateItem(OlItemType.olMailItem);
		    

		            // Fill out & send message...
		            olMail.To = toMail;
		            olMail.CC = cc;


		            olMail.Subject = subject;

		            olMail.HTMLBody = body;
		            if (File.Exists(attachment)) {
			            olMail.Attachments.Add(attachment);
		            }
		            olMail.Display();
	            } 
                catch (System.Exception ex) 
                {
		            MessageBox.Show(ex.ToString());
	            }

        }

        public static bool TryToDelete(string f)
       {
           try
           {
               // A.
               // Try to delete the file.
               File.Delete(f);
               return true;
           }
           catch (IOException)
           {
               // B.
               // We could not delete the file.
               return false;
           }
       }

     
             public static void  DeleteDir(String GetMessageDownloadFolderPath)
             {
                 System.IO.DirectoryInfo downloadedMessageInfo = new DirectoryInfo(GetMessageDownloadFolderPath);

                 foreach (FileInfo file in downloadedMessageInfo.GetFiles())
                 {
                     try
                     {
                         file.Delete();
                     }
                     catch { }

                 }
                 foreach (DirectoryInfo dir in downloadedMessageInfo.GetDirectories())
                 {
                     try
                     {
                         dir.Delete(true);
                     }
                     catch { }
                 }
             }



             public static void UpdateTableRelation(string desTable, string SourceTable, string desValueFeild, string sourceValueFeild, string desRelationFeild, string sourRelationFeild)
             {
                 string sql = "UPDATE t1 SET  t1." + desValueFeild + " = t2." + sourceValueFeild + " FROM " + desTable + " AS t1 INNER JOIN " + SourceTable + " AS t2 ON t1." + desRelationFeild + " = t2." + sourRelationFeild;

                 data_command(sql);

             }


             public static void UpdateTableRelationStaticValue(string desTable, string SourceTable, string desValueFeild, string staticValue, string desRelationFeild, string sourRelationFeild)
             {
                 string sql = "UPDATE t1 SET  t1." + desValueFeild + " = '" + staticValue + "' FROM " + desTable + " AS t1 INNER JOIN " + SourceTable + " AS t2 ON t1." + desRelationFeild + " = t2." + sourRelationFeild;

                 data_command(sql);

             }
   

    

    }
}