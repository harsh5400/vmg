using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Framework;
using PushVodIngestion.DataProvider;
using EntityState = FrameWork.EntityState;

namespace PushVodIngestion.ServiceProvider
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        List<LINQEntityBase> GetEntities(string entityTypeName);

        [OperationContract]
        List<LINQEntityBase> GetEntitiesBasedOnCondition(string entityTypeName, string predicate, params object[] value);

        [OperationContract]
        Boolean SavePlaylist(tblPlaylist playList);

        [OperationContract]
        void data_command(string sqlCommand);

        [OperationContract]
        List<tblIngestion> GetUserIngestion(long userSID);

        [OperationContract]
        List<tblPlayoutPort> GetUserChannels(long userSID);

        [OperationContract]
        LINQEntityBase Save(LINQEntityBase entity, EntityState entityState);

        [OperationContract]
        DataTable data_set(String sqlCommand);


        [OperationContract]
        void Bulk_Copy(DataTable dt, string destinationTable);

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
