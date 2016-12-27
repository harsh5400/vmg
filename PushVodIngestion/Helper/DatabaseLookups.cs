using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;

namespace PushVodIngestion.Helper
{
     public sealed class DatabaseLookups
    {
        private DatabaseLookups()
        {

        }
        private static DatabaseLookups instance;
        public static DatabaseLookups Instance
        {
            get { return instance ?? (instance = new DatabaseLookups()); }
        }

        private List<DataProvider.tblChannel> _channels;
        public List<DataProvider.tblChannel> Channels
        {
            get
            {
                if (_channels != null) return _channels;
                var instance = typeof(DataProvider.tblChannel).AssemblyQualifiedName;
                var entities = ServiceHelper.Instance.GetEntities(instance);  
                _channels = entities.Cast<DataProvider.tblChannel>().ToList(); 

                return _channels;
            }
            set
            {
                _channels = value;
            }
        }


        private List<DataProvider.tblPlayoutPort> _playoutPorts;
        public List<DataProvider.tblPlayoutPort> PlayoutPorts
        {
            get
            {
                if (_playoutPorts != null) return _playoutPorts;
               // var instance = typeof(DataProvider.tblPlayoutPort).AssemblyQualifiedName;
                //var entities = ServiceHelper.Instance.GetEntities(instance);

                var playoutPortList  = ServiceHelper.Instance.GetUserChannels(Global.CurreUser.SID).ToList(); //entities.Cast<DataProvider.tblPlayoutPort>().ToList();


                _playoutPorts = playoutPortList;

                return _playoutPorts;
            }
            set
            {
                _playoutPorts = value;
            }
        }

        private List<DataProvider.tblAutomation> _autoMationList;
        public List<DataProvider.tblAutomation> AutoMationList
        {
            get
            {
                if (_autoMationList != null) return _autoMationList;
                 var instance = typeof(DataProvider.tblAutomation).AssemblyQualifiedName;
                    var entities = ServiceHelper.Instance.GetEntities(instance);


                _autoMationList = entities.Cast<DataProvider.tblAutomation>().ToList(); ;

                return _autoMationList;
            }
            set
            {
                _autoMationList = value;
            }
        }

        private List<DataProvider.tblIngestion> _ingestions;
        public List<DataProvider.tblIngestion> Ingestions
        {
            get
            {
                if (_ingestions != null) return _ingestions;

                Debug.Print("Ingestions started at: {0}",
                       DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.ms"));
                var stopWatch = new Stopwatch();
                stopWatch.Start();

                
                //var instance = typeof(DataProvider.tblIngestion).AssemblyQualifiedName;
                //var entities = ServiceHelper.Instance.GetEntities(instance);
                _ingestions = ServiceHelper.Instance.GetUserIngestion(Global.CurreUser.SID).ToList();// entities.Cast<DataProvider.tblIngestion>().ToList(); ;

                stopWatch.Stop();
                Debug.Print("Ingestions Time ended at: {0}, and time taken: {1}ms",
                    DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.ms"),
                    stopWatch.ElapsedMilliseconds);
                return _ingestions;
            }
            set
            {
                _ingestions = value;
            }
        }


        private List<DataProvider.tblIngestion> _fullIngestions;
        public List<DataProvider.tblIngestion> FullIngestions1
        {
            get
            {
               

                if (_fullIngestions != null) return _fullIngestions;

                var instance = typeof(DataProvider.tblIngestion).AssemblyQualifiedName;
                var entities = ServiceHelper.Instance.GetEntities(instance);
                _fullIngestions = entities.Cast<DataProvider.tblIngestion>().ToList();



                return _fullIngestions;
            }
            set
            {
                _fullIngestions = value;
            }
        }

         public DataProvider.tblIngestion GetIngestion(long mediaSID)
         {
             var tblIngestion = Ingestions.FirstOrDefault(i => i.SID == mediaSID);


             if (tblIngestion != null)
                 return tblIngestion;

             Debug.Print(mediaSID.ToString(CultureInfo.InvariantCulture));

             var assemblyQualifiedName = typeof(DataProvider.tblIngestion).AssemblyQualifiedName;
             var entities = ServiceHelper.Instance.GetEntitiesBasedOnCondition(assemblyQualifiedName, "SID == @0", new ObservableCollection<object>() { mediaSID});
             
             tblIngestion    = entities.Cast<DataProvider.tblIngestion>().ToList().FirstOrDefault();
             
             if (tblIngestion != null)   
                 _ingestions.Add(tblIngestion);

             return tblIngestion;

         }

         public DataProvider.tblIngestion GetIngestionbyItemCode(string itemCode)
         {
             var tblIngestion = Ingestions.FirstOrDefault(i => i.itemCode == itemCode);


             if (tblIngestion != null)
                 return tblIngestion;

             var assemblyQualifiedName = typeof(DataProvider.tblIngestion).AssemblyQualifiedName;
             var entities = ServiceHelper.Instance.GetEntitiesBasedOnCondition(assemblyQualifiedName, "itemCode == @0", new ObservableCollection<object>() { itemCode });

             tblIngestion = entities.Cast<DataProvider.tblIngestion>().ToList().FirstOrDefault();

             if (tblIngestion != null)
                 _ingestions.Add(tblIngestion);

             return tblIngestion;

         }



        private List<DataProvider.tblSecondryEventRule> _secondaryRule;
        public List<DataProvider.tblSecondryEventRule> SecondaryRule
        {
            get
            {


                if (_secondaryRule != null) return _secondaryRule;

                var instanceTypeName = typeof(DataProvider.tblSecondryEventRule).AssemblyQualifiedName;
                var entities = ServiceHelper.Instance.GetEntities(instanceTypeName);
                _secondaryRule = entities.Cast<DataProvider.tblSecondryEventRule>().ToList();



                return _secondaryRule;
            }
            set
            {
                _secondaryRule = value;
            }
        }

        private List<DataProvider.tblPlaylistSecondryEvent> _playlistSecondaryEvents;

        public List<DataProvider.tblPlaylistSecondryEvent> PlaylistSecondaryEvents
        {
            get
            {
                if (_playlistSecondaryEvents != null) return _playlistSecondaryEvents;

                var instanceTypeName = typeof(DataProvider.tblPlaylistSecondryEvent).AssemblyQualifiedName;
                var entities = ServiceHelper.Instance.GetEntities(instanceTypeName);
                _playlistSecondaryEvents = entities.Cast<DataProvider.tblPlaylistSecondryEvent>().ToList();
                
                return _playlistSecondaryEvents;
            }
            set { _playlistSecondaryEvents = value; }
        }



        private List<DataProvider.tblPlaylistSecondryEventDetail> _tblPlaylistSecondryEventDetail;

        public List<DataProvider.tblPlaylistSecondryEventDetail> TblPlaylistSecondryEventDetail
        {
            get
            {
                if (_tblPlaylistSecondryEventDetail != null) return _tblPlaylistSecondryEventDetail;

                var instanceTypeName = typeof(DataProvider.tblPlaylistSecondryEventDetail).AssemblyQualifiedName;
                var entities = ServiceHelper.Instance.GetEntities(instanceTypeName);
                _tblPlaylistSecondryEventDetail = entities.Cast<DataProvider.tblPlaylistSecondryEventDetail>().ToList(); 
                
                return _tblPlaylistSecondryEventDetail;
            }
            set { _tblPlaylistSecondryEventDetail = value; }
        }



        


    }
}
