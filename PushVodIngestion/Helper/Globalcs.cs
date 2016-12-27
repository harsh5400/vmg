using PushVodIngestion.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PushVodIngestion
{

    public struct TSTVID
    {
        public long id;
        public Boolean resetBelowID;        
    };



    public enum AlarmPattern
    {
        Daily,
        Monthly,
        Weekly,
        OneTime,
      
    };



    public enum MusicChannels
    {
        Music1 = 66,
        Music2 = 68,
        Music3 = 69,
        Music4 = 70,

    };


    public enum Commands
    {
        Append,
        Insert   

    };


    public enum Redirection
    {
        Recording,
        Playlist,
        Dublist,
        Media

    };

  

    public enum SourceType
    {
        Catchup =1,
        VOD =2,
        Filler=3

    };

    public enum PlaylistStatus
    {
        Approved = 1,
        NotApproved =0 

    };

    public class PortAvaible
    {
        public long PortSID { get; set; }
        public bool isAvaible { get; set; }
    }
	class Global
	{


        private static string connectionStringProject;

        public static string ConnectionStringProject
        {
            get {
                return connectionStringProject ?? (connectionStringProject = Settings.Default.dbPushVODConnectionString);
            }
            set {
                connectionStringProject = value
                    
                    ; }
        }

        private static string _liveFeedTXid;

        public static string LiveFeedTXid
        {
            get { return _liveFeedTXid ?? (_liveFeedTXid = Settings.Default.LiveFeedTXID); }
            set { _liveFeedTXid = value; }
        }


        private static string _liveFeedTxTitle;

        public static string LiveFeedTxTitle
        {
            get { return _liveFeedTxTitle ?? (_liveFeedTxTitle = Settings.Default.LiveFeedTitle); }
            set {  _liveFeedTxTitle = value; }
        }
        


        private Boolean _thirdParty;

        public Boolean ThirdParty
        {
            get
            {
                return Settings.Default.ThirdParty.ToLower() == "true";
            }
            set { _thirdParty = value; }
        }
        

        private static TimeSpan deFaultStartTime  = new TimeSpan(5, 30, 0);

        public static TimeSpan DefaultStartTime
        {
            get {

                if (deFaultStartTime == null)
                    deFaultStartTime = new TimeSpan(5, 30, 0);

                return deFaultStartTime; 
            }
            set { deFaultStartTime = value; }
        }
        

        //public static tblUser CurrentUSer { get; set; }
        private static int _SchduleTstvChangeHour=8;

        public static int SchduleTstvChangeHour
        {
            get { return _SchduleTstvChangeHour; }
            set { _SchduleTstvChangeHour = value; }
        }

        private static string sundancePath;

        public static string SundancePath
        {
            get { 
                
                if(sundancePath == null)
                    sundancePath = @"\\192.168.97.10\Playout\harsh\Ch 100 Application\Data\DLDatabase.mdb";

                return sundancePath; 
            }
            set { sundancePath = value; }
        }


	    public static DataProvider.User CurreUser { get; set; }

        private static String  tempFolder;

        public static String TempFolder
        {
            get {

                if (tempFolder == null)
                    tempFolder = Application.StartupPath.ToString() + @"\data\temp\";

                return tempFolder; 
            }
            set { tempFolder = value; }
        }

        private static String reportFolder;

        public static String ReportFolder
        {
            get {

                if (reportFolder == null)
                    reportFolder = Application.StartupPath.ToString() + @"\data\report\";
                return reportFolder; 
            }
            set { reportFolder = value; }
        }
        
        

        

       

        //Shifts
        private static   Shift currentShift;

        public static Shift CurrentShift
        {
            get {

                currentShift = GetCurrentShift();
                return currentShift; 
            
            }
            set { currentShift = value; }
        }




        public static string getEndTimeString(String ShiftString)
        {
            String result = "1500";

            if (ShiftString == Shift.Evening.ToString())
                result = "2300";
            else if(ShiftString == Shift.Night.ToString())
                result = "0700";

            return result; 

        }
        public static string getStartTimeString(String ShiftString)
        {
            String result = "0700";

            if (ShiftString == Shift.Evening.ToString())
                result = "1500";
            else if (ShiftString == Shift.Night.ToString())
                result = "2300";

            return result;

        }


        private static Shift GetCurrentShift()
        {         

            Shift sh = Shift.Morining;


            if (DateTime.Now.Hour >= 15 && DateTime.Now.Hour < 23)
                sh = Shift.Evening;
            else if ((DateTime.Now.Hour >= 23) || (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 7))
                sh = Shift.Night;

            return sh;

        }

        

        public static Shift GetShift(DateTime dt)
        {

            Shift sh = Shift.Morining;


            if (dt.Hour >= 15 && dt.Hour < 23)
                sh = Shift.Evening;
            else if ((dt.Hour >= 23) || (dt.Hour >= 0 && dt.Hour < 7))
                sh = Shift.Night;

            return sh;

        }

        private int getStartHour()
        {
            int result = 7;

            if (DateTime.Now.Hour >= 15 && DateTime.Now.Hour < 23)
                result = 15;
            else if ((DateTime.Now.Hour >= 23) || (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 7))
                result = 23;

            return result;

        }

        public static String getYesNo(bool? obj)
        {
            if (obj == null)
                obj = false;

            String result = "YES";

                if(obj == false)
                    result = "NO";

                return result; 
        }



        
        
	}
}
