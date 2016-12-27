using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace PushVodIngestion
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            //transfer media from vmg to media
          //  var f = new Helper.MediaUpdateHelper();
           // var thread1 = new Thread(f.MediaUpdatefromVmg);
            //thread1.Start();

            var arg = Environment.GetCommandLineArgs();

            
            if (arg.Count() > 1)
            {
                var value  = Redirection.Recording;;

                try
                {
                    value = (Redirection)System.Enum.Parse(typeof(Redirection), arg[1]);
                }
                catch { }

                try {
                  
                    Global.ConnectionStringProject = arg[2];

                
                }
                catch { }

                switch (value)
                {
                    case Redirection.Recording:
                        Application.Run(new Forms.Main());
                        break;
                    case Redirection.Playlist:
                        Application.Run(new Forms.Playlist.frmPlaylist(true));
                        break;
                         case Redirection.Media:
                       Application.Run(new Forms.Playlist.frmMedia());
                        break;
                      case Redirection.Dublist:
                       Application.Run(new Forms.DubList.frmDubList());
                        break;
                    default:
                        Application.Run(new Forms.Main());
                        break;

                }
               

              
            }
            else {

                //Application.Run(new frmDashBoard());
                Application.Run(new frmLogin());
            }


        
        }

        
    }
}
