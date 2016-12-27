using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq.Mapping;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using Framework;
using PushVodIngestion.DataProvider;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Configuration;
using EntityState = FrameWork.EntityState;


namespace PushVodIngestion.ServiceProvider
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public List<LINQEntityBase> GetEntities(string entityTypeName)
        {
            var watch = Stopwatch.StartNew();
            var context = new DataModelDataContext();


            try
            {
                List<LINQEntityBase> entities;
                var type = Type.GetType(entityTypeName);
               

                context.DeferredLoadingEnabled = false;


                switch (type.Name.ToUpper())
                {
                    case "TBLPLAYLIST":   
                        var loadOption = new DataLoadOptions();
                        loadOption.LoadWith<tblPlaylist>(p => p.tblIngestion);
                        context.LoadOptions = loadOption;
                        break;
                }


                entities = context.GetTable(type).Cast<LINQEntityBase>().ToList();
                watch.Stop();
                Debug.Print("Time Taken to Load {0} is {1}", type.Name.ToUpper(), watch.ElapsedMilliseconds);
                return entities;
            }
            catch (Exception ex)
            {
            //  //  Logger.Write(LogType.ErrorLog, "GetEntities", "Connection Properties:  Connection string: {0}\n Connection state: {1}", context.Connection.ConnectionString, context.Connection.State);
            //   // Logger.Write(LogType.ErrorLog, "GetEntities", "An unknown error occured {0}\n", ex.ToString());
                throw new Exception("Connection may not established. See inner exception for more details", ex);
            }
        }


        public List<LINQEntityBase> GetEntitiesBasedOnCondition(string entityTypeName, string predicate, params object[] value)
        {
            var watch = Stopwatch.StartNew();
            List<LINQEntityBase> entities;
            try
            {
                var context = new DataModelDataContext();
                context.DeferredLoadingEnabled = false;
                var type = Type.GetType(entityTypeName);


                switch (type.Name.ToUpper())
                {
                    case "TBLPLAYLIST":
                        var loadOption = new DataLoadOptions();
                        loadOption.LoadWith<tblPlaylist>(p => p.tblIngestion);
                        context.LoadOptions = loadOption;
                        break;
                }

                var result = context.GetTable(type);
                entities = result.Where(predicate, value).Cast<LINQEntityBase>().ToList();
                watch.Stop();
                Debug.Print("Time Taken to Load {0} is {1}", type.Name.ToUpper(), watch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {

                throw;
            }
            return entities;

        }


        public  void data_command(string sqlCommand)
        {
             DAL.data_command(sqlCommand);
        }

        public DataTable data_set(String sqlCommand)
        {
            return DAL.data_set(sqlCommand);
        }

        public void Bulk_Copy(DataTable dt, string destinationTable)
        {
            DAL.Bulk_Copy(dt, destinationTable);
        }


        public Boolean SavePlaylist(tblPlaylist playList)
        {
            var result = true;

            //try
            //{
                    
                var context = new DataModelDataContext();
             context.CommandTimeout = 20000;
                context.tblPlaylists.InsertOnSubmit(playList);
                context.SubmitChanges();

            //}
            //catch
            //{
            //    result = false;
            //}



            return result;

        }

        public List<tblIngestion> GetUserIngestion(long userSID)
        {
            var context = new DataModelDataContext();
            return context.GetUserIngestion(userSID).ToList();
        }

        public List<tblPlayoutPort> GetUserChannels(long userSID)
        {
            var context = new DataModelDataContext();
            return context.GetUserChannels(userSID).ToList();
        }

        public LINQEntityBase Save(LINQEntityBase entity, EntityState entityState)
        {
            var context = new DataModelDataContext();
            var type = entity.GetType();     

            var table = context.GetTable(type);
            
            switch (entityState)
            {
                case EntityState.Modified:
                    table.Attach(entity);
                    context.Refresh(RefreshMode.KeepCurrentValues, entity); 
                    break;
                case EntityState.New:
                    table.InsertOnSubmit(entity);
                    break;
            }

            context.SubmitChanges();
            return entity;
        }




    }
}
