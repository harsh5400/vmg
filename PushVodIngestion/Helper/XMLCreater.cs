using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using PushVodIngestion.DataProvider;
using Excel = Microsoft.Office.Interop.Excel;

namespace PushVodIngestion.Helper
{

    enum Automation { EVS = 1, Itx = 2, Sundance = 3, ItxWithSecondaryEvent = 4 };

    enum SecondaryEventType {Now =1, Next =2 , General =3, Ignore = 4, IgnoreProgrammingBug = 5};

    class XMLCreater
    {



        public void IngestionXml(string filename, List<vwIngestion> lst)
        {

            XmlTextWriter xml = new XmlTextWriter(filename, Encoding.UTF8);

            xml.WriteStartDocument();
            xml.Formatting = Formatting.Indented;
            xml.Indentation = 2;

            //Startingest
            xml.WriteStartElement("Ingest"); // This is the start element of the xml, is should be closed after inserting child nodes
            xml.WriteStartAttribute("Version");
            xml.WriteValue("1.0.2");



            ////loopstart
            foreach (vwIngestion ing in lst)
            {
                //start ingestblock
                xml.WriteStartElement("IngestBlock");
                xml.WriteStartAttribute("ThirdParty_ID");
                xml.WriteValue(ing.TxId);


                //    //strat recorders
                xml.WriteStartElement("Recorders");

                //start recorder

                xml.WriteStartElement("Recorder");
                xml.WriteStartAttribute("UmID");
                xml.WriteValue("");

                xml.WriteStartAttribute("Name");
                xml.WriteValue(ing.DestinationPort);

                xml.WriteStartAttribute("SerialNumber");
                xml.WriteValue("146590");

                xml.WriteStartAttribute("Camera");
                xml.WriteValue(ing.Camera);

                xml.WriteStartAttribute("LSMID");
                xml.WriteValue(0);
                xml.WriteEndElement(); //end recorder                             


                xml.WriteEndElement(); //end recorders


                xml.WriteElementString("TCIN", ing.StartTime.Value.ToString() + ":00");

                xml.WriteElementString("TCOUT", ing.EndTime.Value.ToString() + ":00");

                xml.WriteElementString("DATEIN", ing.StartDate.Value.ToString("dd-MMM-yyyy"));

                xml.WriteElementString("DATEOUT", ing.EndDate.Value.ToString("dd-MMM-yyyy"));

                xml.WriteElementString("Name", ing.ProgrameName);

                xml.WriteElementString("PurgeDate", ing.DeleteDate.Value.ToString("dd-MMM-yyyy"));


                xml.WriteStartElement("EVS_Metadatas");

                xml.WriteStartAttribute("Revision");
                xml.WriteValue("0");



                //Clips_Infos
                xml.WriteStartElement("Clips_Infos");
                //Clip
                xml.WriteStartElement("Clip");

                ////XFile_Clip_Infos
                xml.WriteStartElement("XFile_Clip_Infos");
                xml.WriteElementString("XT_UmID", "");
                xml.WriteElementString("XT_VarID", ing.TxId);
                //    xml.WriteElementString("XT_Name", "EVS-HD");
                //    xml.WriteElementString("XT_Camera", ing.Camera);
                //    xml.WriteElementString("XT_Short_IN_TC_str", ing.StartTime.Value.ToString() + ":00");
                //    xml.WriteElementString("XT_Short_OUT_TC_str", ing.EndTime.Value.ToString() + ":00");
                //    xml.WriteElementString("XT_Clip_Duration_str", ing.Duration.Value.ToString() + ":00");
                //    xml.WriteElementString("XT_TC_System", "3");
                //    xml.WriteElementString("XT_TC_System_str", "sys_25 (PAL)");
                //    xml.WriteElementString("XT_TC_System", "3");
                //    xml.WriteElementString("XT_Offset_TC", "0");
                //    xml.WriteElementString("XT_NumClip", "625");
                //    xml.WriteElementString("XT_FillAndKey_Type", "0");
                //    //xml.WriteElementString("XT_Video_Aspect_Ratio", "2");
                //    //xml.WriteElementString("XT_Video_Aspect_Ratio_str", "4/3");
                //    //xml.WriteElementString("XT_Video_Bitrate", "0");
                //    //xml.WriteElementString("XT_Video_Bitrate", "0");
                //    xml.WriteElementString("XT_ClipName", ing.ProgrameName);

                xml.WriteEndElement(); //close XFile_Clip_Infos

                ////XFile_Clip_Infos


                xml.WriteStartElement("Other_Clip_Infos");
                xml.WriteElementString("LongClipName", ing.ProgrameName);
                xml.WriteElementString("FeedName", ing.SorcePort);
                xml.WriteElementString("Owner", "administrator");
                xml.WriteElementString("TCInDate", ing.StartDate.Value.ToString("dd-MMM-yyyy"));
                xml.WriteElementString("TCOutDate", ing.EndDate.Value.ToString("dd-MMM-yyyy"));

                xml.WriteEndElement(); //close Other_Clip_Infos
                xml.WriteEndElement(); //close Clip
                xml.WriteEndElement(); //close Clips_Infos

                xml.WriteEndElement(); //close EVS_Metadatas






                xml.WriteEndElement(); // 'closing ingestblock

            }


            ////loopend



            //xml.WriteEndAttribute();// 'Closing Ingest
            xml.WriteEndElement();
            xml.WriteEndDocument();
            xml.Close();




        }

        public void dubListXml(string filename, List<vwDubList> lst)
        {

            XmlTextWriter xml = new XmlTextWriter(filename, Encoding.UTF8);

            xml.WriteStartDocument();
            xml.Formatting = Formatting.Indented;
            xml.Indentation = 2;

            //Titles
            xml.WriteStartElement("Titles"); // This is the start element of the xml, is should be closed after inserting child nodes




            ////loopstart
            foreach (vwDubList ing in lst)
            {
                //start ingestblock
                xml.WriteStartElement("Title");

                xml.WriteElementString("Key1", ing.itemCode.ToString());
                xml.WriteElementString("itemCode", ing.itemCode.ToString());
                xml.WriteElementString("Title", ing.ProgrameName.ToString());

                xml.WriteEndElement(); // 'closing ingestblock

            }


            ////loopend



            //xml.WriteEndAttribute();// 'Closing Ingest
            xml.WriteEndElement();
            xml.WriteEndDocument();
            xml.Close();




        }

        public void DaletNewDubLIst(String filename, List<vwDubList> lst)
        {
            XmlElement ClockPack = default(XmlElement);
            XmlElement clock = default(XmlElement);
            XmlElement Content = default(XmlElement);
            XmlElement item = default(XmlElement);

            XmlElement ditemcode = default(XmlElement);
            XmlElement dtitle = default(XmlElement);

            XmlElement dkey1 = default(XmlElement);
            XmlElement ddatetimefield = default(XmlElement);

            XmlDocument doc = new XmlDocument();
            string Sname = "";
            string SeOriginalStartTime = "";
            string SuSimpleid = "";


            XmlDeclaration xmldecl;
            xmldecl = doc.CreateXmlDeclaration("1.0", "UTF-8", null);

            //Add the new node to the document. 
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmldecl, root);

            //ClockPck
            ClockPack = doc.CreateElement("ClockPack");
            doc.AppendChild(ClockPack);



            clock = doc.CreateElement("Clock");
            ClockPack.AppendChild(clock);

            clock.SetAttribute("scheduledTime", "00:00:00.000");

            clock.SetAttribute("name", "");



           
            clock.SetAttribute("blockStartMode", "");

            Content = doc.CreateElement("content");
            clock.AppendChild(Content);




            TimeSpan Playtime = new TimeSpan(0, 0, 0);
            foreach (vwDubList ing in lst)
            {
                XmlElement dTime = default(XmlElement);
                XmlElement dduration = default(XmlElement);
                //item

                item = doc.CreateElement("item");
                Content.AppendChild(item);


                dTime = doc.CreateElement("time");
                item.AppendChild(dTime);


                dTime.InnerText = Playtime.ToString() + ".000";
                Playtime = Playtime.Add(ing.Duration.Value);

                ditemcode = doc.CreateElement("itemcode");
                item.AppendChild(ditemcode);

                ditemcode.InnerText = ing.itemCode;

                dtitle = doc.CreateElement("title");
                item.AppendChild(dtitle);

                dtitle.InnerText = ing.ProgrameName;

                dduration = doc.CreateElement("duration");
                item.AppendChild(dduration);
                dduration.InnerText = ing.Duration.Value.ToString() + ".000";

                dkey1 = doc.CreateElement("key1");
                item.AppendChild(dkey1);

                dkey1.InnerText = ing.itemCode;

                ddatetimefield = doc.CreateElement("datetimefield");
                item.AppendChild(ddatetimefield);
                ddatetimefield.InnerText = ing.Date.Value.ToString("yyyy-MM-dd") + " 00:00:00.000";





            }

            clock.SetAttribute("duration", Playtime.ToString() + ".000");



            doc.Save(filename);


            System.Windows.Forms.MessageBox.Show("Dalet XML is Converted");



        }

        public string PlaylistXML(string filename, List<Playlist> displayPlayList, string filenameOnly)
        {
            XmlTextWriter xml = new XmlTextWriter(filename, Encoding.UTF8);

            xml.WriteStartDocument();
            xml.Formatting = Formatting.Indented;
            xml.Indentation = 2;

            //edl
            xml.WriteStartElement("edl"); // This is the start element of the xml, is should be closed after inserting child nodes
            xml.WriteStartAttribute("xmlns");
            xml.WriteValue("http://www.evs.tv/EVS_EDL");
            xml.WriteStartAttribute("xmlns:xs");
            xml.WriteValue("http://www.w3.org/2001/XMLSchema-instance");
            xml.WriteStartAttribute("xs:schemaLocation");
            xml.WriteValue("http://www.evs.tv/EVS_EDL http://schemas.evs.tv/XMLSchema/v2/EVS_EDL.xsd");
            xml.WriteStartAttribute("xmlns:evsmd");
            xml.WriteValue("http://www.evs.tv/XMLSchema/Metadatas/");
            xml.WriteStartAttribute("version");
            xml.WriteValue("0");

            //edit
            xml.WriteStartElement("edit");
            xml.WriteStartAttribute("type");
            xml.WriteValue("playlist");
            xml.WriteStartAttribute("readonly");
            xml.WriteValue("no");
            xml.WriteStartAttribute("iteration");
            xml.WriteValue("1");

            //editinfo
            xml.WriteStartElement("editinfo");

            //application
            xml.WriteStartElement("application");

            xml.WriteStartAttribute("name");
            xml.WriteValue("IpDirector");

            xml.WriteStartAttribute("version");
            xml.WriteValue("6.57.4.122");

            //application
            xml.WriteEndElement();

            xml.WriteElementString("name", filenameOnly);
            xml.WriteElementString("varid", DateTime.Now.ToString("yyyyMMddHHmmss"));
            xml.WriteElementString("umid", "");
            xml.WriteElementString("videotype", "highres");
            xml.WriteElementString("videoratio", "16/9");
            xml.WriteElementString("videosystem", "pal");
            xml.WriteElementString("creationdate", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"));

            //editinfo 
            xml.WriteEndElement();

            //track
            xml.WriteStartElement("track");
            ////loopstart

            long id = 0;
            foreach (Playlist ply in displayPlayList)
            {
                id++;

                //xml.WriteString("<transitioneffect type=" + '\u0022' + "audio_fade" + '\u0022' + "><parameters> <key>part_in</key> <integer>1</integer> <key>part_out</key> <integer>1</integer> <key>position</key> <string>center</string> </parameters> </transitioneffect> <transitioneffect type=" + '\u0022' + "video_cut" + '\u0022' + "> <parameters> <key>duration</key> <integer>0</integer> <key>position</key> <string>none</string> </parameters> </transitioneffect>");
                //transitioneffect audio
                xml.WriteStartElement("transitioneffect");
                xml.WriteStartAttribute("type");
                xml.WriteValue("audio_fade");

                //parameters   
                xml.WriteStartElement("parameters");
                xml.WriteElementString("key", "part_in");
                xml.WriteElementString("integer", "1");
                xml.WriteElementString("key", "part_out");
                xml.WriteElementString("integer", "1");
                xml.WriteElementString("key", "position");
                xml.WriteElementString("integer", "center");
                //parameters
                xml.WriteEndElement();

                //transitioneffect audio 
                xml.WriteEndElement();



                //transitioneffect video
                xml.WriteStartElement("transitioneffect");
                xml.WriteStartAttribute("type");
                xml.WriteValue("video_cut");

                //parameters   
                xml.WriteStartElement("parameters");
                xml.WriteElementString("key", "duration");
                xml.WriteElementString("integer", "0");
                xml.WriteElementString("key", "position");
                xml.WriteElementString("integer", "none");
                //parameters
                xml.WriteEndElement();

                //transitioneffect video 
                xml.WriteEndElement();




                //element
                xml.WriteStartElement("element");

                //elementinfo
                xml.WriteStartElement("elementinfo");


                xml.WriteElementString("name", ply.ProgrameName);
                xml.WriteElementString("id", id.ToString());
                //transmittedtc
                xml.WriteStartElement("transmittedtc");
                xml.WriteStartAttribute("type");
                xml.WriteValue("tod");
                //transmittedtc
                xml.WriteEndElement();

                //elementinfo
                xml.WriteEndElement();



                //media
                xml.WriteStartElement("media");

                //mediaitem
                xml.WriteStartElement("mediaitem");
                xml.WriteStartAttribute("version");
                xml.WriteValue("main");
                xml.WriteStartAttribute("proxy");
                xml.WriteValue("high");

                xml.WriteElementString("id", id.ToString());
                xml.WriteElementString("name", ply.ProgrameName);



                //evsmd:EVS_Metadatas
                xml.WriteStartElement("evsmd:EVS_Metadatas");
                xml.WriteStartAttribute("xmlns");
                xml.WriteValue("http://www.evs.tv/XMLSchema/Metadatas/");
                xml.WriteStartAttribute("Revision");
                xml.WriteValue("0");

                //Clips_Infos
                xml.WriteStartElement("Clips_Infos");
                //Clip
                xml.WriteStartElement("Clip");
                //XFile_Clip_Infos
                xml.WriteStartElement("XFile_Clip_Infos");

                int len = ply.TransmissionId.Length;
                string umid = ply.TransmissionId;

                if (len > 8)
                    umid = ply.TransmissionId.ToString().Substring(len - 8);



                // xml.WriteElementString("XT_UmID", umid);
                xml.WriteElementString("XT_VarID", ply.TransmissionId);
                xml.WriteElementString("XT_Name", "EVS-HD");
                xml.WriteElementString("XT_ClipName", ply.ProgrameName);

                //XFile_Clip_Infos
                xml.WriteEndElement();

                //Clip
                xml.WriteEndElement();



                //Clips_Infos
                xml.WriteEndElement();

                //evsmd:EVS_Metadatas
                xml.WriteEndElement();

                //mediaitem
                xml.WriteEndElement();



                //media 
                xml.WriteEndElement();




                xml.WriteElementString("in", "0");
                string durationInPal = (ply.Duration.TotalSeconds * 50).ToString();
                xml.WriteElementString("duration", durationInPal);


                if (id == 1 || ply.Event.ToLower() == "fixed")
                {
                    //startmode
                    xml.WriteStartElement("startmode");
                    xml.WriteStartAttribute("type");
                    xml.WriteValue("jumpontime");
                    xml.WriteElementString("startmodetime", ply.PlayDate.ToString("dd-MMM-yyyy") + " " + ply.PlayTime.ToString() + ":00");

                    //startmode
                    xml.WriteEndElement();

                    ////stopmode
                    //xml.WriteStartElement("stopmode");
                    //xml.WriteStartAttribute("type");
                    //xml.WriteValue("lastframe");
                    ////stopmode
                    /// 
                    // xml.WriteEndElement();


                }


                //element
                xml.WriteEndElement();




               
            }

            //track
            xml.WriteEndElement();

            //edit
            xml.WriteEndElement();


            // 'Closing edl
            xml.WriteEndElement();
            xml.WriteEndDocument();
            xml.Close();

            return "Done";

        }

        public string PlaylistItx(string filename, List<Playlist> displayPlayList, string filenameOnly, bool showMessage = true)
        {

            var doc = new XmlDocument();
            var xmldecl = doc.CreateXmlDeclaration("1.0", "UTF-8", null);



            //Add the new node to the document. 
            var root = doc.DocumentElement;
            doc.InsertBefore(xmldecl, root);

            var schedule = (XmlElement)doc.AppendChild(doc.CreateElement("Schedule"));
            schedule.SetAttribute("name", filenameOnly);

            var firstTime = "";
            var i = 1;
            foreach (var playlist in displayPlayList)
            {
                if (i == 1)
                {

                    firstTime = string.Format("{0:00}{1:00}{2:00}",
                             playlist.PlayTime.Hours,
                                  playlist.PlayTime.Minutes,
                                  playlist.PlayTime.Seconds);
                }


                var date = string.Format("{0:00}-{1:00}-{2:00}",
                             playlist.PlayDate.Year,
                                  playlist.PlayDate.Month,
                                  playlist.PlayDate.Day);

                var playtime = string.Format("{0:00}:{1:00}:{2:00}",
                             playlist.PlayTime.Hours,
                                  playlist.PlayTime.Minutes,
                                  playlist.PlayTime.Seconds);

                var durationF = string.Format("{0:00}:{1:00}:{2:00}",
                             playlist.Duration.Hours,
                                  playlist.Duration.Minutes,
                                  playlist.Duration.Seconds);


                var scheduledEvent = (XmlElement)schedule.AppendChild(doc.CreateElement("ScheduledEvent"));
                scheduledEvent.SetAttribute("originalStartTime", date + "T" + playtime);

                var summary = (XmlElement)scheduledEvent.AppendChild(doc.CreateElement("Summary"));
                summary.SetAttribute("simpleId", (i - 1).ToString(CultureInfo.InvariantCulture));

                var uName = (XmlElement)summary.AppendChild(doc.CreateElement("Name"));
                uName.InnerText = playlist.TransmissionId;

                var title = (XmlElement)summary.AppendChild(doc.CreateElement("Title"));
                title.InnerText = playlist.ProgrameName;


                var startDAte = (XmlElement)summary.AppendChild(doc.CreateElement("StartDate"));
                startDAte.InnerText = date;

                var startTimecode = (XmlElement)summary.AppendChild(doc.CreateElement("StartTimecode"));
                startTimecode.SetAttribute("timecodeFormat", "FPS25");

                var startimecodeTime = (XmlElement)startTimecode.AppendChild(doc.CreateElement("Time"));
                startimecodeTime.InnerText = playtime + ":00";


                var duration = (XmlElement)summary.AppendChild(doc.CreateElement("Duration"));
                var durationTime = (XmlElement)duration.AppendChild(doc.CreateElement("Time"));

                durationTime.InnerText = durationF;


                var eventType = (XmlElement)summary.AppendChild(doc.CreateElement("EventType"));

                eventType.InnerText = playlist.TransmissionId == Global.LiveFeedTXid ? "TXLiveEvent" : "TXMediaEvent";


                var timeMode = (XmlElement)summary.AppendChild(doc.CreateElement("TimeMode"));
                timeMode.InnerText = i == 1 || playlist.Event.ToLower() == "fixed" ? "Fixed" : "Follow";


                if (playlist.TransmissionId == Global.LiveFeedTXid)
                {
                    var contentType = (XmlElement) summary.AppendChild(doc.CreateElement("ContentType"));
                    contentType.InnerText = "Live";
                }


                i++;
            }



            // ReSharper disable once AssignNullToNotNullAttribute
            var ff = Path.Combine(Path.GetDirectoryName(filename), filenameOnly + "-" + firstTime + ".itxml");
            doc.Save(ff);

            if (showMessage)
                MessageBox.Show("ITX XML is Converted");


            return "ITX XML is Converted";
        }

        public string PlaylistSundance(string filename, List<Playlist> displayPlayList, string filenameOnly, bool showMessage = true)
        {
            if (displayPlayList.Count == 0)
            {
                if (showMessage)
                    MessageBox.Show("No Data to Export");


                return "No Data to Export";
            }

            var excelApp = new Excel.Application { DisplayAlerts = false };
            var excelWorkbook = excelApp.Workbooks.Open(Application.StartupPath + "\\data\\sundanceTemplate.xls");
            excelWorkbook.SaveAs(Path.GetDirectoryName(filename) + @"\" + filenameOnly + ".xls");

            Excel.Worksheet excelSheet = excelWorkbook.Sheets[1];



            excelSheet.Range["D2"].Value = displayPlayList[0].PlayDate.ToString("dd-MMM-yyyy");

            var scRow = 5;

            foreach (var playlist in displayPlayList)
            {

                var durationF = string.Format("{0:00}:{1:00}:{2:00}",
                             playlist.Duration.Hours,
                                  playlist.Duration.Minutes,
                                  playlist.Duration.Seconds);


                excelSheet.Range["B" + scRow].Value = playlist.TransmissionId;

                excelSheet.Range["C" + scRow].Value = durationF;
                excelSheet.Range["F" + scRow].Value = playlist.ProgrameName;


                if (scRow == 5 || playlist.Event.ToLower() == "fixed")
                {
                    excelSheet.Range["A" + scRow].Value = playlist.PlayTime.Hours.ToString("00") + ":" +
                                                  playlist.PlayTime.Minutes.ToString("00") + ":" +
                                                  playlist.PlayTime.Seconds.ToString("00");

                    excelSheet.Range["E" + scRow].Value = "C";
                }

                scRow++;
            }




            ((Excel.Range)excelSheet.Rows["45000:" + (scRow)]).Delete();


            //   excelApp.Visible = true;

            excelApp.Application.DisplayAlerts = false;

            excelWorkbook.Save();

            excelWorkbook.SaveAs(Path.GetDirectoryName(filename) + @"\" + filenameOnly + ".txt",
                  Excel.XlFileFormat.xlUnicodeText, null, null, null, false);


            excelWorkbook.Close(false);
            excelApp.Quit();
            Marshal.ReleaseComObject(excelWorkbook);
            Marshal.ReleaseComObject(excelApp);

            if (showMessage)
                MessageBox.Show("Sundace PlayList Created in two formats (xls, txt)");

            return "Sundace PlayList Created in two formats (xls, txt)";

        }

        public string PlaylistItxWithSecondryEvent(string filename, List<Playlist> displayPlayList, string filenameOnly, bool showMessage = true)
        {

            var doc = new XmlDocument();
            var xmldecl = doc.CreateXmlDeclaration("1.0", "UTF-8", null);



            //Add the new node to the document. 
            var root = doc.DocumentElement;
            doc.InsertBefore(xmldecl, root);

            var schedule = (XmlElement)doc.AppendChild(doc.CreateElement("Schedule"));
            schedule.SetAttribute("name", filenameOnly);

            var firstTime = "";
            var i = 1;
            var index = 0;
            foreach (var playlist in displayPlayList)
            {
                if (i == 1)
                {

                    firstTime = string.Format("{0:00}{1:00}{2:00}",
                             playlist.PlayTime.Hours,
                                  playlist.PlayTime.Minutes,
                                  playlist.PlayTime.Seconds);
                }


                var date = string.Format("{0:00}-{1:00}-{2:00}",
                             playlist.PlayDate.Year,
                                  playlist.PlayDate.Month,
                                  playlist.PlayDate.Day);

                var playtime = string.Format("{0:00}:{1:00}:{2:00}",
                             playlist.PlayTime.Hours,
                                  playlist.PlayTime.Minutes,
                                  playlist.PlayTime.Seconds);

                var durationF = string.Format("{0:00}:{1:00}:{2:00}",
                             playlist.Duration.Hours,
                                  playlist.Duration.Minutes,
                                  playlist.Duration.Seconds);


                var scheduledEvent = (XmlElement)schedule.AppendChild(doc.CreateElement("ScheduledEvent"));
                scheduledEvent.SetAttribute("originalStartTime", date + "T" + playtime);

                var summary = (XmlElement)scheduledEvent.AppendChild(doc.CreateElement("Summary"));
                summary.SetAttribute("simpleId", (i - 1).ToString(CultureInfo.InvariantCulture));

                var uName = (XmlElement)summary.AppendChild(doc.CreateElement("Name"));
                uName.InnerText = playlist.TransmissionId;

                var title = (XmlElement)summary.AppendChild(doc.CreateElement("Title"));
                title.InnerText = playlist.ProgrameName;


                var startDAte = (XmlElement)summary.AppendChild(doc.CreateElement("StartDate"));
                startDAte.InnerText = date;

                var startTimecode = (XmlElement)summary.AppendChild(doc.CreateElement("StartTimecode"));
                startTimecode.SetAttribute("timecodeFormat", "FPS25");

                var startimecodeTime = (XmlElement)startTimecode.AppendChild(doc.CreateElement("Time"));
                startimecodeTime.InnerText = playtime + ":00";


                var duration = (XmlElement)summary.AppendChild(doc.CreateElement("Duration"));
                var durationTime = (XmlElement)duration.AppendChild(doc.CreateElement("Time"));

                durationTime.InnerText = durationF;

                var eventType = (XmlElement)summary.AppendChild(doc.CreateElement("EventType"));
                //eventType.InnerText = "TXMediaEvent";
                eventType.InnerText = playlist.TransmissionId == Global.LiveFeedTXid ? "TXLiveEvent" : "TXMediaEvent";

                var timeMode = (XmlElement)summary.AppendChild(doc.CreateElement("TimeMode"));
                //timeMode.InnerText = i == 1 ? "Fixed" : "Follow";
                timeMode.InnerText = i == 1 || playlist.Event.ToLower() == "fixed" ? "Fixed" : "Follow";

                if (playlist.TransmissionId == Global.LiveFeedTXid)
                {
                    var contentType = (XmlElement)summary.AppendChild(doc.CreateElement("ContentType"));
                    contentType.InnerText = "Live";
                }


                //adding secondary event
                //now rules

                var secondaryEvnentRules =
                 DatabaseLookups.Instance.SecondaryRule.Where(
                     x => x.TxIdPrefix.ToLower().Trim() == playlist.TransmissionId.Substring(0, 5).ToLower().Trim() && x.Enable == true);

                //auto  now next
                foreach (var secondaryEvnentRule in secondaryEvnentRules)
                {
                    i++;


                    switch (secondaryEvnentRule.tblSecondaryEventTypeSID)
                    {
                        case (int)SecondaryEventType.Now:
                            AddSecondaryEvent(secondaryEvnentRule, SecondaryEventType.Now, doc, schedule, i, playlist);
                            break;
                        case (int) SecondaryEventType.Next:
                            if (index != displayPlayList.Count - 1) //ignore last content for next
                            {
                                //check Ignore list 

                                var validIndex = index + 1;
                                var ignoreList =
                                    DatabaseLookups.Instance.SecondaryRule.Where(
                                        x =>
                                            x.tblSecondaryEventTypeSID == (int) SecondaryEventType.Ignore &&
                                            x.Enable == true && displayPlayList[validIndex].TransmissionId.Contains(x.TxIdPrefix)).ToList();

                                //move forward for next because current event is on ignore list
                                if (ignoreList.Count > 0 && validIndex + 1 <= displayPlayList.Count - 1) //check next index should not greater then last index
                                    validIndex = validIndex + 1;




                                AddSecondaryEvent(secondaryEvnentRule, SecondaryEventType.Next, doc, schedule, i,
                                        displayPlayList[validIndex]);

                            }
                            break;
                    }
                }

                //general logo

                if (playlist.SecondaryEventSID != null && (playlist.SecondaryEventSID > 0))
                {
                    //get SecondaryEvents
                    var playlist1 = playlist;
                    var genralSecondaryEvents =
                        DatabaseLookups.Instance.TblPlaylistSecondryEventDetail.Where(
                            x => x.tblPlaylistSecondryEventSID == playlist1.SecondaryEventSID);
                    foreach (var genralSecondaryEvent in genralSecondaryEvents)
                    {
                        i++;

                        //get secondryEvent Rules
                        var secondaryEvnentRule =
                            DatabaseLookups.Instance.SecondaryRule.FirstOrDefault(
                                x => x.SID == genralSecondaryEvent.tblSecondryEventRuleSID);

                        AddSecondaryEvent(secondaryEvnentRule, SecondaryEventType.General, doc, schedule, i, playlist);
                        
                    }


                }


                i++;
                index++;
            }



            // ReSharper disable once AssignNullToNotNullAttribute
            //var ff = Path.Combine(Path.GetDirectoryName(filename), filenameOnly + "-" + firstTime + ".itxml");
            var ff = Path.Combine(Path.GetDirectoryName(filename), filenameOnly + ".itxml");
            doc.Save(ff);

            if (showMessage)
                MessageBox.Show("ITX XML is Converted");


            return "ITX XML is Converted";
        }


        XmlElement AddSecondaryEvent(tblSecondryEventRule secondaryEvnentRule, SecondaryEventType secondaryEventType, XmlDocument doc, XmlElement schedule, int simpleid, Playlist playlist)
        {
           
         
            var secondaryEventTitle = secondaryEventType + " Logo - " + playlist.ProgrameName;
            var secondaryOffsetTimeString = secondaryEvnentRule.OffsetTime.ToString();
            var duration = secondaryEvnentRule.Duration.ToString();
            var secondaryEventID = secondaryEvnentRule.Prefix + playlist.TransmissionId.Substring(2, playlist.TransmissionId.Length - 2);
            var eventTypeString = secondaryEvnentRule.EventType;
            var timeModeString = secondaryEvnentRule.TimeMode;
            var mode = secondaryEvnentRule.Mode;
            
            
            
            if (secondaryEventType == SecondaryEventType.General)
                secondaryEventID = secondaryEvnentRule.TxIdPrefix;


            if (mode.ToLower() == "logo")
            {
                if (secondaryEvnentRule.OffsetTime != null)
                    duration = playlist.Duration.Subtract(secondaryEvnentRule.OffsetTime.Value).ToString();
               
            }

            return GetSecondaryNode(doc, schedule, simpleid, secondaryEventID, secondaryEventTitle, secondaryOffsetTimeString, duration, eventTypeString, timeModeString, mode);
        }

        XmlElement GetSecondaryNode(XmlDocument doc, XmlElement schedule, int simpleid, String secondaryEventID, string secondaryEventTitle, string secondaryOffsetTimeString, string durationF, string eventTypeString, string timeModeString, string mode)
        {

            var scheduledEvent = (XmlElement)schedule.AppendChild(doc.CreateElement("ScheduledEvent"));
            
            var summary = (XmlElement)scheduledEvent.AppendChild(doc.CreateElement("Summary"));
            summary.SetAttribute("simpleId", (simpleid - 1).ToString(CultureInfo.InvariantCulture));

            var uName = (XmlElement)summary.AppendChild(doc.CreateElement("Name"));
            uName.InnerText = secondaryEventID;

            var title = (XmlElement)summary.AppendChild(doc.CreateElement("Title"));
            title.InnerText = secondaryEventTitle;


            // secondaryOffset
            var secondaryOffset = (XmlElement)summary.AppendChild(doc.CreateElement("SecondaryOffset"));

                    var secondaryOffsetTimecode = (XmlElement)secondaryOffset.AppendChild(doc.CreateElement("Timecode"));
                    secondaryOffsetTimecode.SetAttribute("timecodeFormat", "FPS25");

                    var secondaryOffsetTimecodeTime = (XmlElement)secondaryOffsetTimecode.AppendChild(doc.CreateElement("Time"));
                    secondaryOffsetTimecodeTime.InnerText = secondaryOffsetTimeString + ":00";


            if (mode.ToLower() != "on" && mode.ToLower() != "off")
            {
                var duration = (XmlElement) summary.AppendChild(doc.CreateElement("Duration"));

                    var durationTimecode = (XmlElement) duration.AppendChild(doc.CreateElement("Timecode"));
                    durationTimecode.SetAttribute("timecodeFormat", "FPS25");

                    var durationTime = (XmlElement) durationTimecode.AppendChild(doc.CreateElement("Time"));
                    durationTime.InnerText = durationF + ":00";
            }


            var eventType = (XmlElement)summary.AppendChild(doc.CreateElement("EventType"));
            eventType.InnerText = eventTypeString;


            var timeMode = (XmlElement)summary.AppendChild(doc.CreateElement("TimeMode"));
            timeMode.InnerText = timeModeString;

            
            // details
            if (mode.ToLower() == "on" || mode.ToLower() == "off")
            {
                var details = (XmlElement)scheduledEvent.AppendChild(doc.CreateElement("Details"));
                details.SetAttribute("type", "http://www.w3.org/2001/XMLSchema-instance", "XmlLogoEvent");

                var modeNode = (XmlElement)details.AppendChild(doc.CreateElement("Mode"));
                modeNode.InnerText = mode;
            }

            return scheduledEvent;

        }


        public string PlaylistCSV(string filename, List<Playlist> displayPlayList, string filenameOnly, bool showMessage = true)
        {
            if (displayPlayList.Count == 0)
            {
                if (showMessage)
                    MessageBox.Show("No Data to Export");


                return "No Data to Export";
            }



            //before your loop
            var csv = new StringBuilder();

            //in your loop
            var dates = "Date";
            var starts = "Start Time";
          
            var title = "Title";
            var duration = "Duration";
            var fileID = "File ID";
           

            //Suggestion made by KyleMit
            var newLine = string.Format("{0},{1},{2},{3},{4}", dates, starts, title, duration, fileID);
            csv.AppendLine(newLine);

            foreach (var playlist in displayPlayList)
            {

                 dates = playlist.PlayDate.ToString("dd-MM-yyyy");
                 starts = playlist.PlayTime.Hours.ToString("00") + ":" +
                                                   playlist.PlayTime.Minutes.ToString("00") + ":" +
                                                   playlist.PlayTime.Seconds.ToString("00");
                title = playlist.ProgrameName.Replace(",", "");
                duration = string.Format("{0:00}:{1:00}:{2:00}",
                                playlist.Duration.Hours,
                                  playlist.Duration.Minutes,
                                  playlist.Duration.Seconds);
                fileID = playlist.TransmissionId;
                  newLine = string.Format("{0},{1},{2},{3},{4}", dates, starts, title, duration, fileID);
                csv.AppendLine(newLine);
            }

            //after your loop
            File.WriteAllText(Path.GetDirectoryName(filename) + @"\" + filenameOnly + ".csv", csv.ToString());



            if (showMessage)
                MessageBox.Show("CSV PlayList has been Created ");

            return "CSV PlayList has been Created";

        }

    }
}
