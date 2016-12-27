using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PushVodIngestion.Helper
{
    class Mail
    {
        public static void SentMail_Outlook(string body, string subject, string toMail, string cc, string attachment, string attachment2 = "")
        {

            try
            {

                Microsoft.Office.Interop.Outlook.Application olApp = new Microsoft.Office.Interop.Outlook.Application();

                MailItem olMail = (MailItem)olApp.CreateItem(OlItemType.olMailItem);


                // Fill out & send message...
                olMail.To = toMail;
                olMail.CC = cc;


                olMail.Subject = subject;

                olMail.HTMLBody = body;
                if (File.Exists(attachment))
                {
                    olMail.Attachments.Add(attachment);
                }


                if (File.Exists(attachment2))
                {
                    olMail.Attachments.Add(attachment2);
                }

                olMail.Display();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
}



