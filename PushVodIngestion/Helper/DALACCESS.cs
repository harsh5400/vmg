using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using PushVodIngestion.Properties;
namespace PushVodIngestion.Hepler
{
    class DALACCESS
    {
        public static DataTable data_set(String SQL_Command)
        {

            DataTable functionReturnValue = new DataTable();

            OleDbConnection con = new OleDbConnection();
            con.ConnectionString = Settings.Default.dbPushVODConnectionString;
                con.Open();

            DataSet DS = new DataSet();
            

            string SQL = SQL_Command;

           OleDbDataAdapter da = new OleDbDataAdapter(SQL, con);

            da.Fill(DS, "table");

            functionReturnValue = DS.Tables[0];


            con.Close();

            return functionReturnValue;
        }
    }
}
