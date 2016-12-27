using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Telerik.WinControls.UI;

namespace PushVodIngestion.Helper
{
    public class Playlist
    {
        public long SID { get; set; }
        public TimeSpan PlayTime { get; set; }
        public string TransmissionId { get; set; }
        public String ProgrameName { get; set; }
        public TimeSpan Duration { get; set; }
        public string Status { get; set; }
        public string Event { get; set; }
        public double PlayOrderSID { get; set; }
        public DateTime PlayDate  { get; set; }
        public string Approved { get; set; }
        public long SourceTypeSID { get; set; }
        public double DurationMin { get; set; }
        public long? SecondaryEventSID { get; set; }
        public string ReadyToAir { get; set; }
        private string _musicPlusCheck;

        public string MusicPlusCheck
        {
            get
            {
                if (TransmissionId != null && TransmissionId.Length > 5)
                {
                    _musicPlusCheck = TransmissionId.Substring(4, 1).ToUpper();
                }
                
                return _musicPlusCheck;
            }
            set { _musicPlusCheck = value; }
        }
        

    }


    public class CsvData
    {
      

        private List<DataProvider.tblPlaylist> _tblPlaylists; 
        public List<DataProvider.tblPlaylist> TblPlaylists
        {
            get
            {
                return _tblPlaylists ?? new List<DataProvider.tblPlaylist>();
            }
            set { _tblPlaylists = value; }
        }


        private List<MediaLibrary> _displayPlaylist;
        public List<MediaLibrary> DisplayPlaylist
        {
            get
            {
                return _displayPlaylist ?? new List<MediaLibrary>();
            }
            set { _displayPlaylist = value; }
        }
        

    
        public bool Error { get; set; }
        
    }

    public class MediaLibrary
    {
       
           public string TxId { get; set; }
         public string ProgrameName { get; set; }
         public TimeSpan? Duration { get; set; }
          public long SID { get; set; }

    }

    class PlaylistHelper
    {
        private static PlaylistHelper _instance;
        public static PlaylistHelper Instance
        {
            get { return _instance ?? (_instance = new PlaylistHelper()); }
        }



        public  List<DataProvider.tblPlaylist> GetPlaylist(List<DataProvider.tblPlaylist> lst, List<long> MediaSids, long PlayoutPortSID, DateTime date, Commands command, int SelectedIndex = 0, bool liveEvent = false,TimeSpan? liveEventDuration = default(TimeSpan?))
        {
            if (lst == null)
            {
                lst = new List<DataProvider.tblPlaylist>();
                  command = Commands.Append;
            }


            lst = lst.OrderBy(i => i.PlayOrderSID).ToList();
            double subClips = 0;
            
            if(lst.Count == 0)
                command  = Commands.Append;
            
            if(command == Commands.Insert)
                subClips =  lst[SelectedIndex].PlayOrderSID.Value + 0.1;



            foreach (var mediaSID in MediaSids)
            {
                var playList = new DataProvider.tblPlaylist
                {
                    MediaSID = mediaSID,
                    Date = date,
                    PlayoutPortSID = PlayoutPortSID,
                    Status = false
                };

                if ((playList.LiveEvent != null && playList.LiveEvent.Value) || liveEvent)
                {
                    playList.LiveEventDuration = liveEventDuration;
                    playList.LiveEvent = true;
                    playList.LiveTXID = Global.LiveFeedTXid;
                    playList.LiveTitle = Global.LiveFeedTxTitle;

                }
                else
                    playList.tblIngestion = DatabaseLookups.Instance.GetIngestion(mediaSID);
                        // DatabaseLookups.Instance.FullIngestions.FirstOrDefault(i => i.SID == mediaSID);




                if (command == Commands.Append)
                {
                    lst.Add(playList);
                }
                else
                {
                    playList.PlayOrderSID = subClips;
                    lst.Insert(SelectedIndex + 1, playList);
                    subClips = subClips + 0.1;
                    SelectedIndex = SelectedIndex + 1;
                }
            }


        
            return UpdatePlayoutTime(lst);
        }


        public  List<DataProvider.tblPlaylist> UpdatePlayoutTime(List<DataProvider.tblPlaylist> lst)
        {
            var index = 0;
            var NextDate = false;
            foreach (var playlist in lst)
            {
                if (playlist.FixedEvent != true)
                {

                    if (index == 0 && playlist.PlayoutTime == null)
                    {
                        var selectedChannel =
                            DatabaseLookups.Instance.PlayoutPorts.FirstOrDefault(i => i.SID == playlist.PlayoutPortSID);
                        //Set Start time ZERO
                        playlist.PlayoutTime =  new TimeSpan(0,0,0);

                        //Get Channel Default Start Time
                        if (selectedChannel != null)
                            if (selectedChannel.StartTime != null)
                                playlist.PlayoutTime = selectedChannel.StartTime.Value;

                        playlist.SchDate = playlist.Date.Value;
                    }

                    if (index != 0)
                    {
                        TimeSpan? previousDuration = new TimeSpan();

                        //if live duration then it will take duration from live not need to go ingestgion
                        var liveEvent = lst[index - 1].LiveEvent;
                        if (liveEvent != null && liveEvent.Value)
                        {
                            previousDuration = lst[index - 1].LiveEventDuration;
                        }
                        else
                        {
                            if (lst[index - 1].tblIngestion == null)
                            {
                                var firstOrDefault = DatabaseLookups.Instance.GetIngestion(lst[index - 1].MediaSID.Value);
                                    // DatabaseLookups.Instance.FullIngestions.FirstOrDefault(i => i.SID == lst[index - 1].MediaSID);
                                if (firstOrDefault != null)
                                    previousDuration =
                                        firstOrDefault.Duration;
                            }
                            else
                            {
                                previousDuration = lst[index - 1].tblIngestion.Duration;
                            }
                        }

                        var PlayOutTime = previousDuration.Value + lst[index - 1].PlayoutTime.Value;

                        var playDateTime = lst[index - 1].SchDate.Value.Add(PlayOutTime);



                        if (PlayOutTime < new TimeSpan(24, 0, 0))
                        {
                            playlist.PlayoutTime = PlayOutTime;

                        }
                        else
                        {
                            playlist.PlayoutTime = PlayOutTime.Subtract(new TimeSpan(24, 0, 0));
                            NextDate = true;

                        }

                        playlist.SchDate = playDateTime.Date;

                        //if (NextDate == true)
                        //    playlist.SchDate = playlist.Date.Value.AddDays(1);
                        //else
                        //{
                        //    if (playlist.Date != null)
                        //        playlist.SchDate = playlist.SchDate == null ? playlist.Date.Value : playlist.SchDate.Value;
                        //}

                    }
                }
                playlist.PlayOrderSID = index + 1;

                index++;
            }

            return lst;
        }


        public static CsvData ConvertMusicCSVtoPlaylist(DataTable dt, DateTime date, long playoutPort)
        {
            var result = new CsvData();
            var disPlayList = new List<MediaLibrary>();
            var playlists = new List<DataProvider.tblPlaylist>();
            foreach (DataRow row in dt.Rows)
            {
              
                var transmissionID = row["File ID"].ToString().Trim();
                var title = row["Title"].ToString().Trim();
                var duration  =     row["Duration"].StringToTimeSpan();

                if (disPlayList.Count(i => i.TxId == transmissionID) > 0)
                    continue;



                var playlist = new DataProvider.tblPlaylist();
                playlist.Date = date;
                playlist.PlayoutTime = row[1].ToString().StringToTimeSpan();

                var media =
                    DatabaseLookups.Instance.GetIngestionbyItemCode(row["File ID"].ToString());// DatabaseLookups.Instance.FullIngestions.FirstOrDefault(i => i.itemCode == row["File ID"].ToString());

                if (media != null && (media.ReadyToAir != null && media.ReadyToAir.Value))
                    playlist.MediaSID = media.SID;
                else
                {
                    result.Error = true;
                    disPlayList.Add(new MediaLibrary() { TxId = transmissionID, ProgrameName = title, Duration = duration });
                }

                playlist.SchDate = date;
                playlist.PlayoutPortSID = playoutPort;


                playlists.Add(playlist);

            }


            result.DisplayPlaylist = disPlayList;
            result.TblPlaylists = playlists;

            return result;
        }

    }

    class MusicFilter
    {
        public string DisplayValue { get; set; }
        public string Value { get; set; }
    }

    

}
