using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace PushVodIngestion.ServiceProvider
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
                con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PushVodIngestion.DataProvider.Properties.Settings.dbPushVODConnectionString"].ConnectionString;
               
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




        public  static void Bulk_Copy(DataTable dt,string destinationTable)
        {
     
            using (var sqlBulkCopy = new SqlBulkCopy(con))
            {
                //Set the database table name
                sqlBulkCopy.DestinationTableName = destinationTable;//"dbo.Customers";
 
                ////[OPTIONAL]: Map the DataTable columns with that of the database table
                //sqlBulkCopy.ColumnMappings.Add("Id", "CustomerId");
                //sqlBulkCopy.ColumnMappings.Add("Name", "Name");
                //sqlBulkCopy.ColumnMappings.Add("Country", "Country");
                con.Open();
                sqlBulkCopy.WriteToServer(dt);
                Storeprocedure("sp_addtabletocontents", destinationTable, con);
                con.Close();
            }

           

        }  


        public static void Storeprocedure(string spName, string parameter, SqlConnection conNew)
        {
            try
            {

                var command = new SqlCommand(spName, conNew) { CommandType = CommandType.StoredProcedure };
                command.Parameters.AddWithValue("@table_name", parameter);
                //command.Parameters.Add("@Name", SqlDbType.DateTime).Value = txtName.Text;

                var result  = command.ExecuteNonQuery();

                Debug.Print(result.ToString(CultureInfo.InvariantCulture));

            }
            catch (SqlException ex)
            {

            }

        }






        public static bool isNumeric(string val, System.Globalization.NumberStyles NumberStyle)
        {
            Double result;
            return Double.TryParse(val, NumberStyle,
                System.Globalization.CultureInfo.CurrentCulture, out result);
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


        public static void DeleteDir(String GetMessageDownloadFolderPath)
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