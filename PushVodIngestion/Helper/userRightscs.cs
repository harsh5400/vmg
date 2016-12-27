using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PushVodIngestion
{
    public enum UserRigts
    {
        Add_New_User = 1,
        Add_New_Service = 2,
        Add_New_Content_Provider = 3,
        Library_ID = 4,
        Genrate_TxId = 5,
        Ingest = 6,
        Preview = 7,
        PreviewOK = 8,
        QC_Pass_Fail = 9,
        Update_Libraty_Id = 10,
        Delete_TXID = 11,
        Change_Password = 12,
        Update_Profile = 13,
        Sent_Mail = 14,

    }
    
    public enum Shift
    {
        Morining = 1,
        Evening = 2,
        Night = 3

    }
}
