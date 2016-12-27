using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Windows.Forms;
using Framework;
using FrameWork;
using Microsoft.Office.Interop.Excel;
using PushVodIngestion.DataProvider;
using PushVodIngestion.ServiceReference1;
using System.Collections.ObjectModel;


namespace PushVodIngestion.Helper
{
    class ServiceHelper
    {
        private Service1Client client;

        private static ServiceHelper _instance;
        public static ServiceHelper Instance
        {
            get { return _instance ?? (_instance = new ServiceHelper()); }
        }


        private ServiceHelper()
        {
            client = new Service1Client();
        }



        public ObservableCollection<LINQEntityBase> GetEntities(string instanceTypeName)
        {
           return client.GetEntities(instanceTypeName);
        }

        public ObservableCollection<LINQEntityBase> GetEntitiesBasedOnCondition(string entityTypeName, string predicate, ObservableCollection<object> value)
        {
            return client.GetEntitiesBasedOnCondition(entityTypeName, predicate, value);
        }


       public  Boolean SavePlaylist(DataProvider.tblPlaylist playList)
        {
            return client.SavePlaylist(playList);
        }

        public void DataCommand(string sql)
        {
            client.data_command(sql);
        }


        public System.Data.DataTable data_set(string sql)
        {
           return  client.data_set(sql);
        }

        public ObservableCollection<DataProvider.tblIngestion> GetUserIngestion(long userSID)
        {
            return client.GetUserIngestion(userSID);
        }

        public ObservableCollection<DataProvider.tblPlayoutPort> GetUserChannels(long userSID)
        {
            return client.GetUserChannels(userSID);
        }

        public LINQEntityBase Save(LINQEntityBase entity, EntityState entityState)
        {
            return client.Save(entity, entityState);
        }


        public LINQEntityBase SaveEntity(LINQEntityBase entity, EntityState entitystate)
        {
            //try
            //{
                using (var client = new Service1Client())
                {
                    //var client = new ServiceClient.ServiceClient();
                    var returnEntity = client.Save(entity, entitystate);
                    if (returnEntity != null && returnEntity.BusinessValidationResult != null && !returnEntity.BusinessValidationResult.Status)
                    {
                        MessageBox.Show(returnEntity.BusinessValidationResult.Result);
                    }

                    return returnEntity;
                }
           // }
            //catch (Exception ex)
            //{
            //    //Logger.Write(LogType.ErrorLog, "SaveEntity", ex.Message);
            //    return null;
            //}
        }

        public  void Bulk_Copy(System.Data.DataTable dt, string destinationTable)
        {
            client.Bulk_Copy(dt, destinationTable);
        }


    }
}
