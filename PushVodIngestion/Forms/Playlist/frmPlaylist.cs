using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FrameWork;
using Microsoft.Office.Interop.Outlook;
using PushVodIngestion.Helper;
using PushVodIngestion.ServiceReference1;
using Telerik.Collections.Generic;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Application = System.Windows.Forms.Application;
using Exception = System.Exception;

namespace PushVodIngestion.Forms.Playlist
{
    public partial class frmPlaylist : Form
    {
        private  bool _showSchedule;
        private readonly SynchronizationContext synchronizationContext;
        private Service1Client client = new Service1Client();
        List<long> _mediaSiDs = new List<long>();
        public List<DataProvider.tblPlaylist> PlayList = new List<DataProvider.tblPlaylist>();
        public List<Helper.Playlist> DisplayPlayList = new List<Helper.Playlist>();
        List<long> _copyMediaId = new List<long>();
        List<DataProvider.tblPlayoutPort> _tblPlayoutPorts = new List<DataProvider.tblPlayoutPort>(); 
        private Boolean loading;
        private Boolean loadingwithDatabase;
        private Boolean isSaving;
        private Boolean isMusicChannel;
        public frmPlaylist(Boolean showSchedule = false)
        {
            isMusicChannel = false;
            Debug.Print("Form Start started at: {0}",
                   DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.ms"));
            
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            _showSchedule = showSchedule;
            loading = true;
            InitializeComponent();

            synchronizationContext = SynchronizationContext.Current;

            Application.DoEvents();
            ConditionsinRadgrid();


           
            this.radGridView1.SelectionChanged += radGridView1_SelectionChanged;
            dateTimePicker1.Value = DateTime.Now.Date;

            //catchup
            //this.radGridCatchMedia.SelectionChanged += radGridView2_SelectionChanged;
            //this.radGridCatchMedia.DoubleClick += radGridView2_DoubleClick;
            //load_grid_CatchMedia();

            //load music filter
            load_music_Filter();

            //Vod
            this.radGridVODMedia.SelectionChanged += radGridVODMedia_SelectionChanged;
            this.radGridVODMedia.DoubleClick += radGridView3_DoubleClick;
            load_grid_VodMedia(0,false,"", false, true);



            //Filler
            this.radGridFiller.SelectionChanged += radGridFiller_SelectionChanged;
            this.radGridFiller.DoubleClick += radGridFiller_DoubleClick;


            _tblPlayoutPorts = DatabaseLookups.Instance.PlayoutPorts.OrderBy(i => i.SID).ToList();
            ComboLoad();

            dateTimePicker1.Value = DateTime.Now.Date;

            if (_showSchedule)
                ShowSchedule();

           
            //MainGrid
   
           

            radGridView1.RowFormatting += radGridView1_RowFormatting;

         

             Debug.Print("Form END Time ended at: {0}, and time taken: {1}ms",
             DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.ms"),
             stopWatch.ElapsedMilliseconds);


            //checking is music channel or not bu selected port
            CheckMusicChannel();
            loading = false;
        }

        void CheckMusicChannel()
        {
            var tblPlayoutPort = cmbPlayoutPort.SelectedItem as DataProvider.tblPlayoutPort;
            if (tblPlayoutPort != null)
            {

                var playoutPortSID = tblPlayoutPort.SID;
                if (playoutPortSID == (long) MusicChannels.Music1 || playoutPortSID == (long) MusicChannels.Music2 ||
                    playoutPortSID == (long) MusicChannels.Music3 || playoutPortSID == (long) MusicChannels.Music4)
                    isMusicChannel = true;
                else
                    isMusicChannel = false;

            }


            if (isMusicChannel)
            {
                cmbFilter.Visible = true;
                label3.Visible = true; 
            }
            else
            {
                cmbFilter.Visible = false;
                label3.Visible = false;
              
            }



            Application.DoEvents();


        }

        void radGridView1_RowFormatting(object sender, RowFormattingEventArgs e)
        {
            if (e.RowElement.IsSelected)
            {
                e.RowElement.GradientStyle = GradientStyles.Solid;
                e.RowElement.BackColor = Color.Chocolate;
            }
        }

        void ShowSchedule()
        {
            var f = new frmSelectChannel(_tblPlayoutPorts);

            if (f.ShowDialog() != DialogResult.OK) return;

            

            dateTimePicker1.Value = f.Date;
            cmbPlayoutPort.SelectedValue = f.ChannelSID;
              Application.DoEvents();
            _showSchedule = false;
            LoadGridWithDatabase();
       
        }

        void radGridFiller_SelectionChanged(object sender, EventArgs e)
        {
            this.lblSelectedFiller.Text = "Selected  :" + this.radGridCatchMedia.SelectedRows.Count; ;
        }

        void radGridFiller_DoubleClick(object sender, EventArgs e)
        {
          
            AddMedia(Commands.Append, radGridFiller.SelectedRows);
        }

        public async void load_music_Filter()
        {
           

          
            await Task.Run(() =>
            {

                var listMusic = new List<MusicFilter>
                {
                    new MusicFilter() {DisplayValue = "All", Value = ""},
                    new MusicFilter() {DisplayValue = "International", Value = "MPIN"},
                    new MusicFilter() {DisplayValue = "Indie Rootz", Value = "MPIR"},
                    new MusicFilter() {DisplayValue = "Jazz", Value = "MPJM"},
                    new MusicFilter() {DisplayValue = "Mehfil ", Value = "MPMH"}
                };


                cmbFilter.DataSource = listMusic;
                cmbFilter.DisplayMember = "DisplayValue";
                cmbFilter.ValueMember = "Value";

                cmbFilter.SelectedItem = new MusicFilter() { DisplayValue = "All", Value = "" };

            });


            //load_grid_VodMedia();
        
            Application.DoEvents();


        }




        public async void load_grid_VodMedia(int rowIndex = 0, bool byRow = false, String serachText = "", bool refresh = false, bool firstLoading = false)
        {
            

            //if (refresh)
            //    DatabaseLookups.Instance.Ingestions = null;



          //  serachText = txtVodSearch.Text;

            List<MediaLibrary> mediaLibrary;
            mediaLibrary = new List<MediaLibrary>();
            var selectedFilterValue = "";

            try
            {
                 selectedFilterValue = cmbFilter.SelectedValue.ToString();
            }
            catch { }

            lblMediaStatus.Text = "Loading";

            if (refresh || firstLoading)
            {
                txtVodSearch.Enabled = false;
                Application.DoEvents();
            }

            await Task.Run(() =>
                {

                    if (refresh || firstLoading)
                    {
                        Debug.Print("VOD DATA LOADING started at: {0}",
                     DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.ms"));
                        var stopWatch = new Stopwatch();
                        stopWatch.Start();
                         
                       
                        var ingestions = ServiceHelper.Instance.GetUserIngestion(Global.CurreUser.SID).ToList();
                            // entities.Cast<DataProvider.tblIngestion>().ToList(); ;
                        DatabaseLookups.Instance.Ingestions = ingestions;

                        stopWatch.Stop();
                        Debug.Print("VOD DATA LOADING Time ended at: {0}, and time taken: {1}ms",
                   DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.ms"),
                   stopWatch.ElapsedMilliseconds);
                    }

                    if (isMusicChannel)
                    {

                         //show music channel ids only
                        mediaLibrary = serachText.Length > 0
                            ? DatabaseLookups.Instance.Ingestions.Where(
                                i =>
                                    i.ReadyToAir != null &&
                                    (i.SourceTypeSID == ((int) SourceType.VOD) && i.ReadyToAir.Value &&
                                     i.itemCode.Contains(selectedFilterValue) &&
                                     (i.ProgrameName.ToLower().Contains(serachText.ToLower()) ||
                                      i.itemCode.ToLower().Contains(serachText.ToLower()) ||
                                      i.TxId.ToLower().Contains(serachText.ToLower()))))
                                .Select(
                                    i =>
                                        new MediaLibrary
                                        {
                                            TxId = i.itemCode,
                                            Duration = i.Duration,
                                            ProgrameName = i.ProgrameName,
                                            SID = i.SID
                                        })
                                .ToList()
                            : DatabaseLookups.Instance.Ingestions.Where(
                                i =>
                                    i.ReadyToAir != null &&
                                    (i.SourceTypeSID == ((int) SourceType.VOD) && i.ReadyToAir.Value &&
                                     i.itemCode.Contains(selectedFilterValue)))
                                .Select(
                                    i =>
                                        new MediaLibrary
                                        {
                                            TxId = i.itemCode,
                                            Duration = i.Duration,
                                            ProgrameName = i.ProgrameName,
                                            SID = i.SID
                                        })
                                .ToList();
                    }
                    else
                    {
                        mediaLibrary = serachText.Length > 0
                         ? DatabaseLookups.Instance.Ingestions.Where(
                             i =>
                                 i.ReadyToAir != null &&
                                 (i.SourceTypeSID == ((int)SourceType.VOD) && i.ReadyToAir.Value &&
                                  (i.ProgrameName.ToLower().Contains(serachText.ToLower()) ||
                                   i.itemCode.ToLower().Contains(serachText.ToLower()) ||
                                   i.TxId.ToLower().Contains(serachText.ToLower()))))
                             .Select(
                                 i =>
                                     new MediaLibrary
                                     {
                                         TxId = i.itemCode,
                                         Duration = i.Duration,
                                         ProgrameName = i.ProgrameName,
                                         SID = i.SID
                                     })
                             .ToList()
                         : DatabaseLookups.Instance.Ingestions.Where(
                             i =>
                                 i.ReadyToAir != null &&
                                 (i.SourceTypeSID == ((int)SourceType.VOD) && i.ReadyToAir.Value))
                             .Select(
                                 i =>
                                     new MediaLibrary
                                     {
                                         TxId = i.itemCode,
                                         Duration = i.Duration,
                                         ProgrameName = i.ProgrameName,
                                         SID = i.SID
                                     })
                             .ToList(); 
                    }
                });
       


            radGridVODMedia.DataSource = mediaLibrary;
            
            if (byRow)
                radGridProperty.SelectRowRadGridView(rowIndex, this.radGridVODMedia);

            radGridProperty.change_Property(this.radGridVODMedia, true, false, false);

            lblMediaStatus.Text = "Done";
            lblTotalVOD.Text = "Total :" + mediaLibrary.Count;

            if (refresh || firstLoading)
            {
                txtVodSearch.Enabled = true;
              
            }

            load_grid_Filler_Media();
            Application.DoEvents();


        }

        public void load_grid_Filler_Media(int RowIndex = 0, bool byRow = false, String SerachText = "")
        {

            var mediaLibrary = SerachText.Length > 0 ? DatabaseLookups.Instance.Ingestions.Where(i => i.ReadyToAir != null && (i.SourceTypeSID == ((int)SourceType.Filler) && i.ReadyToAir.Value && (i.ProgrameName.ToLower().Contains(SerachText.ToLower()) || i.itemCode.ToLower().Contains(SerachText.ToLower()) || i.TxId.ToLower().Contains(SerachText.ToLower())))).Select(i => new MediaLibrary { TxId = i.itemCode, Duration = i.Duration, ProgrameName = i.ProgrameName, SID = i.SID }).ToList() : DatabaseLookups.Instance.Ingestions.Where(i => i.ReadyToAir != null && (i.SourceTypeSID == ((int)SourceType.Filler) && i.ReadyToAir.Value)).Select(i => new MediaLibrary { TxId = i.itemCode, Duration = i.Duration, ProgrameName = i.ProgrameName, SID = i.SID }).ToList();

            radGridFiller.DataSource = mediaLibrary;

            if (byRow)
                radGridProperty.SelectRowRadGridView(RowIndex, this.radGridFiller);

            radGridProperty.change_Property(this.radGridFiller, false, false, true);

            this.lblTotalFiller.Text = "Total :" + mediaLibrary.Count;

            Application.DoEvents();
        }



        void radGridVODMedia_SelectionChanged(object sender, EventArgs e)
        {
            this.lblSelectedVOD.Text = "Selected  :" + radGridVODMedia.SelectedRows.Count.ToString(); ;

        }

        void radGridView2_DoubleClick(object sender, EventArgs e)
        {
            AddMedia(Commands.Append, radGridCatchMedia.SelectedRows);
        }


        void radGridView3_DoubleClick(object sender, EventArgs e)
        {
          

            AddMedia(Commands.Append, radGridVODMedia.SelectedRows);
        }

        public void load_grid_CatchMedia(int RowIndex = 0, bool byRow = false, String SerachText = "")
        {
            

             

            //string Sql = "Select  TxId, ProgrameName, Duration, SID   from tblIngestion WHERE (ReadyToAir=1 AND Date='" + dtDateCatchup.Value.ToString("dd-MMM-yyyy") + "') AND (SourceTypeSID = " + ((int)SourceType.Catchup).ToString() + ") Order by SID DESC";

            //if (SerachText.Length > 0)
            //    Sql = "Select  TxId, ProgrameName, Duration, SID from tblIngestion Where (ReadyToAir=1 AND Date='" + dtDateCatchup.Value.ToString("dd-MMM-yyyy") + "') AND (ProgrameName like '%" + SerachText + "%'  OR TXID like '%" + SerachText + "%') AND ( SourceTypeSID = " + ((int)SourceType.Catchup).ToString() + ")  Order by SID DESC";


            //if (chkShowAll.Checked)
            //{

            //     Sql = "Select  TxId, ProgrameName, Duration, SID   from tblIngestion WHERE (ReadyToAir=1) AND (SourceTypeSID = " + ((int)SourceType.Catchup).ToString() + ") Order by SID DESC";

            //    if (SerachText.Length > 0)
            //        Sql = "Select  TxId, ProgrameName, Duration, SID from tblIngestion Where (ReadyToAir=1) AND (ProgrameName like '%" + SerachText + "%' OR TXID like '%" + SerachText + "%') AND ( SourceTypeSID = " + ((int)SourceType.Catchup).ToString() + ")  Order by SID DESC";
            
            //}


            //DataTable dt = DAL.data_set(Sql);
            //this.radGridCatchMedia.DataSource = dt;



            //if (byRow == true)
            //    radGridProperty.SelectRowRadGridView(RowIndex, this.radGridView1);

            //radGridProperty.change_Property(this.radGridCatchMedia, false, false, true);

            //lblCatchupTotal.Text = "Total :" + dt.Rows.Count;

            //Application.DoEvents();
        }


       

        void radGridView1_SelectionChanged(object sender, EventArgs e)
        {
            lblSelectedAssest.Text = "Selected  :" + radGridView1.SelectedRows.Count; 

        }

        void radGridView2_SelectionChanged(object sender, EventArgs e)
        {
            lblCatchpLabe.Text = "Selected  :" + radGridCatchMedia.SelectedRows.Count ;

        }

        void DeleteMedia()
        {
            if (isSaving)
            {
                MessageBox.Show("Please Wait.. Playlist is Saving");
                return;
            }

            foreach (var row in radGridView1.SelectedRows)
            {
                var itemToRemove = PlayList.FirstOrDefault(r => r.PlayOrderSID == long.Parse(row.Cells["PlayOrderSID"].Value.ToString()));

                if(itemToRemove != null)
                    PlayList.Remove(itemToRemove);          
            }


            PlayList = Helper.PlaylistHelper.Instance.UpdatePlayoutTime(PlayList);
            LoadGrid();
          
            lblStatus.Text =  "Please Save for Final Changes"; 
        }

        void SetStartTime() 
        {
            if (isSaving)
            {
                MessageBox.Show("Please Wait.. Playlist is Saving");
                return;
            }

          var f = new frmStartTime(new TimeSpan(0), dateTimePicker1.Value);

            if (f.ShowDialog() != DialogResult.OK) return;

            if (PlayList.Count <= 0) return;

            PlayList[0].PlayoutTime = f.StartTime;
            PlayList[0].SchDate = f .schDate;
            PlayList = PlaylistHelper.Instance.UpdatePlayoutTime(PlayList);
            LoadGrid();
        }


        void SetFixedTime()
        {
            if (isSaving)
            {
                MessageBox.Show("Please Wait.. Playlist is Saving");
                return;
            }

            if (radGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please Select PlayEvent");
                return;

            }


           

            int index = radGridView1.SelectedRows[0].Index;

            var playoutTime = PlayList[index].PlayoutTime;
            if (playoutTime != null)
            {
                var f = new frmStartTime(playoutTime.Value, PlayList[index].SchDate.Value.Date);

                if (f.ShowDialog() == DialogResult.OK)
                {
                    if (PlayList.Count > 0)
                    {
                    
                        PlayList[index].PlayoutTime = f.StartTime;
                        PlayList[index].FixedEvent = true;
                        PlayList[index].SchDate = f.schDate;
                        PlayList = Helper.PlaylistHelper.Instance.UpdatePlayoutTime(PlayList);
                        LoadGrid();
                    }
                }
            }

            radGridProperty.RowSelected(radGridView1, index);

        }

        void LoadGridWithDatabase()
        {
            if (_showSchedule)
                return;

            
        

            var selectedPOrt = cmbPlayoutPort.SelectedItem as DataProvider.tblPlayoutPort;

            if (selectedPOrt == null)
                return;

            //if (loading)
            //    return;

            loading = true;   
            grpChannelSelection.Enabled = false;
            panel2.Enabled = false;
            lblStatus.Text = "Please Wait Playlist Loading..";
            Application.DoEvents();

            Application.DoEvents();

           //await Task.Run(() =>
           // {
            var instance = typeof(DataProvider.tblPlaylist).AssemblyQualifiedName;   
            var entities = ServiceHelper.Instance.GetEntitiesBasedOnCondition(instance, "PlayoutPortSID == @0 && Date ==@1", new ObservableCollection<object>(){ selectedPOrt.SID, dateTimePicker1.Value.Date });
            PlayList = entities.Cast<DataProvider.tblPlaylist>().OrderBy(i => i.PlayOrderSID).ToList();

            try
            {
                var playlistLocal = PlayList;
                //Update Ingestion
                foreach (var playList in playlistLocal.Where(playList => playList.tblIngestion == null))
                {
                    playList.tblIngestion = DatabaseLookups.Instance.GetIngestion(playList.MediaSID.Value);
                        // DatabaseLookups.Instance.FullIngestions.FirstOrDefault(i => i.SID == playList.MediaSID);
                }

                //reCalculate Schedule
                PlayList = PlaylistHelper.Instance.UpdatePlayoutTime(playlistLocal);
            }
            catch (Exception ex)
            {
                Debug.Print("Exception during for loop of entities, {0}", ex);
            }

            //});




           loading = false;
            LoadGrid();
            isSaving = false;

            grpChannelSelection.Enabled = true;
            panel2.Enabled = true;
            Application.DoEvents();
        }
        void ComboLoad()
        {


            cmbPlayoutPort.DataSource = _tblPlayoutPorts;
            cmbPlayoutPort.DisplayMember = "Descriptions";
            cmbPlayoutPort.ValueMember = "SID";


        }

        void AddMedia()
        {
            if (isSaving)
            {
                MessageBox.Show("Please Wait.. Playlist is Saving");
                return;
            }

            var f = new frmMedia();
            if (f.ShowDialog() != DialogResult.OK) return;
            _mediaSiDs = f.MediaSID;

            var tblPlayoutPort = cmbPlayoutPort.SelectedItem as DataProvider.tblPlayoutPort;
            if (tblPlayoutPort != null)
                PlayList = PlaylistHelper.Instance.GetPlaylist(PlayList, _mediaSiDs, tblPlayoutPort.SID, dateTimePicker1.Value.Date, Commands.Append, 0);
            LoadGrid();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            AddMedia();

        }


        void LoadGrid(Boolean byRow = false, int rowIndex= 0)
        {
            if (loading)
                return;


            if (radGridView1.SelectedRows.Count > 0)
            {
                byRow = true;
                rowIndex = radGridView1.SelectedRows[0].Index;

            }

            lblStatus.Text = "Please Wait Playlist Loading..";
            Application.DoEvents();

            radGridView1.DataSource = null;
            
            

            if (PlayList.Count > 0)
            {
                 //Update Playlist
                foreach (var tblPlaylist in PlayList.Where(i=> i.tblIngestion == null && (i.LiveEvent == null || i.LiveEvent == false )))
                {
                    tblPlaylist.tblIngestion =
                        DatabaseLookups.Instance.GetIngestion(tblPlaylist.MediaSID.Value);
                        // DatabaseLookups.Instance.FullIngestions.Find(i => tblPlaylist.MediaSID != null && i.SID == tblPlaylist.MediaSID.Value);
                }

                DisplayPlayList =
                    PlayList.Select(
                        i =>
                            new Helper.Playlist
                            {
                                SID = i.SID,
                                PlayTime = i.PlayoutTime.Value,
                                TransmissionId =
                                    i.LiveEvent!=null && i.LiveEvent.Value? i.LiveTXID :(i.tblIngestion.itemCode == null
                                        ? i.tblIngestion.TxId.ToString(CultureInfo.InvariantCulture)
                                        : i.tblIngestion.itemCode.ToString(CultureInfo.InvariantCulture)),
                                ProgrameName = i.LiveEvent != null && i.LiveEvent.Value ? i.LiveTitle : i.tblIngestion.ProgrameName,
                                Duration = i.LiveEvent != null && i.LiveEvent.Value ? i.LiveEventDuration.Value :( i.tblIngestion.Duration == null ? new TimeSpan(0, 0, 0, 0) : i.tblIngestion.Duration.Value),
                                Status = i.Status != null && (i.Status.Value)? "Saved" : "Not Saved",
                                Event = i.FixedEvent == true ? "Fixed" : "Follow",
                                PlayOrderSID = i.PlayOrderSID.Value,
                                PlayDate = i.SchDate.Value.Date,
                                Approved = i.Approved != null && (i.Approved.Value) ? "Approved" : "Not Approved",
                                SourceTypeSID = i.LiveEvent != null && i.LiveEvent.Value ? (int)SourceType.Filler : i.tblIngestion.SourceTypeSID.Value,
                                DurationMin = i.LiveEvent != null && i.LiveEvent.Value && i.LiveEventDuration != null ? i.LiveEventDuration.Value.TotalMinutes : (i.tblIngestion.Duration == null ? 0.0 : i.tblIngestion.Duration.Value.TotalMinutes),
                                SecondaryEventSID = i.tblPlaylistSecondryEventSID,
                                ReadyToAir = i.LiveEvent != null && i.LiveEvent.Value ? "YES" : (i.tblIngestion.ReadyToAir == false ? "Not Ready" : "YES") 
                                

                            }).ToList();

                radGridView1.DataSource = DisplayPlayList;


            }

          
           

         
            if (radGridView1.Rows.Count > 0)
            {
                var totalDuration =
                DisplayPlayList[DisplayPlayList.Count - 1].PlayTime.Add(
                    DisplayPlayList[DisplayPlayList.Count - 1].Duration);

                var totalDurationString = ((int) totalDuration.TotalHours).ToString("00") + ":" +
                                          totalDuration.Minutes.ToString("00") + ":" +
                                          totalDuration.Seconds.ToString("00");

                lblLastDuration.Text = "Start Time:-" + DisplayPlayList[0].PlayTime + " End time:-" +
                                       DisplayPlayList[DisplayPlayList.Count - 1].PlayTime + " Total Duration:-" + totalDurationString
                                       ;
            }
            else
                lblLastDuration.Text = "";


            lblStatus.Text = "Playlist Loaded... Enjoy  ";

            lblTotal.Text = "Total :" + PlayList.Count();
            Application.DoEvents();

            if (byRow)
                radGridProperty.SelectRowRadGridView(rowIndex, radGridView1);

            //MessageBox.Show(rowIndex.ToString());
        }

        void ConditionsinRadgrid()
        {
            var obj1 = new ConditionalFormattingObject("MyCondition", ConditionTypes.Equal, "1", "", true);
            obj1.RowBackColor = Color.WhiteSmoke;
            obj1.RowForeColor = Color.Black;
            obj1.TextAlignment = ContentAlignment.MiddleRight;
            this.radGridView1.Columns["SourceTypeSID"].ConditionalFormattingObjectList.Add(obj1);

            var obj2 = new ConditionalFormattingObject("MyCondition", ConditionTypes.Equal, "2", "", true);
            obj2.RowBackColor = Color.WhiteSmoke;
            obj2.RowForeColor = Color.Black;
            obj2.TextAlignment = ContentAlignment.MiddleRight;
            radGridView1.Columns["SourceTypeSID"].ConditionalFormattingObjectList.Add(obj2);


            var obj = new ConditionalFormattingObject("MyCondition1", ConditionTypes.Equal, "Fixed", "", false)
            {
                CellBackColor = Color.Red,
                CellForeColor = Color.Black,
                TextAlignment = ContentAlignment.MiddleRight
            };
            radGridView1.Columns["Event"].ConditionalFormattingObjectList.Add(obj);


            var obj4 = new ExpressionFormattingObject("MyCondition", "DurationMin > 30", true)
            {
                RowBackColor = Color.DarkSeaGreen,
                RowForeColor = Color.Black,
                TextAlignment = ContentAlignment.MiddleRight
            };
            radGridView1.Columns["DurationMin"].ConditionalFormattingObjectList.Add(obj4);


            var obj5 = new ExpressionFormattingObject("MyCondition4", "SecondaryEventSID > 0", false)
            {
                CellBackColor = Color.LightSteelBlue,
                CellForeColor = Color.Black,
                TextAlignment = ContentAlignment.MiddleRight
            };
            radGridView1.Columns["PlayTime"].ConditionalFormattingObjectList.Add(obj5);



            var obj6 = new ConditionalFormattingObject("MyCondition5", ConditionTypes.Equal, "C", "", true)
            {
                RowBackColor = Color.BlueViolet,
                RowForeColor = Color.Black
            };
            radGridView1.Columns["MusicPlusCheck"].ConditionalFormattingObjectList.Add(obj6);

            var obj7 = new ConditionalFormattingObject("MyCondition5", ConditionTypes.Equal, "I", "", true)
            {
                RowBackColor = Color.OrangeRed,
                RowForeColor = Color.Black
            };
            radGridView1.Columns["MusicPlusCheck"].ConditionalFormattingObjectList.Add(obj7);


            var obj8 = new ConditionalFormattingObject("MyCondition5", ConditionTypes.Equal, "Not Ready", "", false)
            {
                CellBackColor = Color.Crimson,
                CellForeColor =   Color.Black, 
                

            };
            radGridView1.Columns["ReadyToAir"].ConditionalFormattingObjectList.Add(obj8);  




            //properties
            radGridProperty.change_Property(radGridView1, false, false, true);
            try
            {
                radGridView1.Columns["PlayTime"].Width = 400;
                radGridView1.Columns["TransmissionId"].Width = 300;
                radGridView1.Columns["ProgrameName"].Width = 800;
                radGridView1.Columns["Duration"].Width = 200;
                radGridView1.Columns["Status"].Width = 150;
                radGridView1.Columns["Event"].Width = 150;
                radGridView1.Columns["PlayDate"].Width = 150;
                radGridView1.Columns["ReadyToAir"].Width = 150;
                radGridView1.Columns["Approved"].IsVisible = false;
                radGridView1.Columns["DurationMin"].IsVisible = false;
                radGridView1.Columns["SecondaryEventSID"].IsVisible = false;
                radGridView1.Columns["MusicPlusCheck"].IsVisible = false;

            }

            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }


        }

        async void SaveMedia()
        {
            if (isSaving)
            {
                MessageBox.Show("Please Wait Already Saving");
                return;
            }

            if (CheckPlaylist_UnderRun())
            {
                MessageBox.Show("Please check, Red Items are Under Run");
                return;
            }
            try
            {


                isSaving = true;
                lblStatus.Text = "Playlist Saving Please Wait..";
                Application.DoEvents();

                //saving existing playlist in to Udo table
                var playoutPortSID = (cmbPlayoutPort.SelectedItem as DataProvider.tblPlayoutPort).SID;
                var playListDate = dateTimePicker1.Value.Date.ToString("dd-MMM-yyyy");

                ServiceHelper.Instance.DataCommand("Delete from tblPlaylistUndo WHERE Date = '" + playListDate +
                                                   "' AND PlayoutPortSID=" + playoutPortSID);
                ServiceHelper.Instance.DataCommand(
                    "Insert into  tblPlaylistUndo ([PlayoutTime], [Date], [PlayoutPortSID], [MediaSID], [addon], [modon], [Status], [PlayOrderSID], [FixedEvent], [SchDate], [Approved], [Exported], [Version]) select [PlayoutTime], [Date], [PlayoutPortSID], [MediaSID], [addon], [modon], [Status], [PlayOrderSID], [FixedEvent], [SchDate], [Approved], [Exported], 1 from [tblPlaylist] WHERE Date = '" +
                    playListDate + "' AND PlayoutPortSID=" + playoutPortSID);
                ServiceHelper.Instance.DataCommand("Delete from tblPlaylist WHERE Date = '" +
                                                   dateTimePicker1.Value.Date.ToString("dd-MMM-yyyy") +
                                                   "' AND PlayoutPortSID=" +
                                                   (cmbPlayoutPort.SelectedItem as DataProvider.tblPlayoutPort).SID);

                int index = 0;
                bool error = false;

                await Task.Run(() =>
                {
                    Debug.Print("Saving started at: {0}",
                        DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.ms"));
                    var stopWatch = new Stopwatch();
                    stopWatch.Start();

                    //Get blank datatable of tblPlaylist
                    var dt = ServiceHelper.Instance.data_set("Select SID, PlayoutTime, Date , PlayoutPortSID , MediaSID, addon, modon, Status, PlayOrderSID, FixedEvent, SchDate, Approved, Exported, tblPlaylistSecondryEventSID, LiveEvent, LiveEventDuration, LiveTitle, LiveTXID  from tblPlaylist WHERE SID = -1");

                    foreach (var playItems in PlayList)
                    {
                        //  if (playItems.PlayoutTime.Value < new TimeSpan(24, 0, 0))
                        //{

                        var dr = dt.NewRow();
                        dr["addon"] = DateTime.Now;
                        dr["Status"] = 1;
                        dr["Date"] = playItems.Date;
                        dr["MediaSID"] = playItems.MediaSID ?? -1;
                        dr["PlayOrderSID"] = playItems.PlayOrderSID;
                        dr["PlayoutPortSID"] = playItems.PlayoutPortSID;
                        dr["PlayoutTime"] = playItems.PlayoutTime;
                        dr["FixedEvent"] = playItems.FixedEvent != null && playItems.FixedEvent == true ? 1 : 0;
                        dr["SchDate"] = playItems.SchDate;
                        dr["Approved"] = playItems.Approved != null && playItems.Approved == true ? 1 : 0;
                        dr["tblPlaylistSecondryEventSID"] = playItems.tblPlaylistSecondryEventSID ?? 0;
                        dr["LiveEvent"] = playItems.LiveEvent != null && playItems.LiveEvent == true;
                        dr["LiveEventDuration"] = playItems.LiveEventDuration ?? new TimeSpan(0,0,0);
                        dr["LiveTitle"] = playItems.LiveTitle;
                        dr["LiveTXID"] = playItems.LiveTXID;



     
                        //var pItems = new DataProvider.tblPlaylist
                        //{
                        //    addon = DateTime.Now,
                        //    Status = true,
                        //    Date = playItems.Date,
                        //    MediaSID = playItems.MediaSID,
                        //    PlayOrderSID = playItems.PlayOrderSID,
                        //    PlayoutPortSID = playItems.PlayoutPortSID,
                        //    PlayoutTime = playItems.PlayoutTime,
                        //    FixedEvent = playItems.FixedEvent,
                        //    SchDate = playItems.SchDate,
                        //    Approved = playItems.Approved
                        //};
                        //  ServiceHelper.Instance.SavePlaylist(pItems);
                        // UpdateStatus(index + "/" + PlayList.Count + " - " + playItems.tblIngestion.ProgrameName);
                        //client.SavePlaylist(pItems);
                        //}
                        // else
                        //{
                        //   radGridProperty.RadcolorRow(radGridView1, index, Color.Red);
                        //  error = true;
                        //  }

                        dt.Rows.Add(dr);
                        index++;
                    }


                    ServiceHelper.Instance.Bulk_Copy(dt, "tblPlaylist");
                    stopWatch.Stop();
                    Debug.Print("Saving Time ended at: {0}, and time taken: {1}ms",
                        DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.ms"),
                        stopWatch.ElapsedMilliseconds);

                });

                //if (error)
                //    MessageBox.Show("Playlist Item Saved before 24:00:00, Red items are not saved, After 24:00:00 we need to save on next date ");
                //else
                MessageBox.Show("Saved");

                lblStatus.Text = "Playlist Saved Successfully...";
                Application.DoEvents();


                LoadGridWithDatabase();

            }
            catch (Exception e)
            {
                isSaving = false;
                lblStatus.Text = "Please Re Save Playlist";
                MessageBox.Show("There Are Some Error Please Save Again..\r\n\r\n" + e);
            }


        }

        //update status
        public void UpdateStatus(String value)
        {
            
            synchronizationContext.Post(o =>
            {
                lblStatus.Text = @"Updating :- " + o.ToString();
            }, value);

            
        }

        private void butSave_Click(object sender, EventArgs e)
        {
            SaveMedia();

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            LoadGridWithDatabase();
            dtDateCatchup.Value = dateTimePicker1.Value.Date;
        }

        private void cmbPlayoutPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGridWithDatabase();
            CheckMusicChannel();
        }

        private void addMediaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddMedia();
        }

        private void radGridView1_Click(object sender, EventArgs e)
        {

        }

        private void deleteMediaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteMedia();
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            DeleteMedia();
        }

        private void saveApproveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveMedia();
        }

        private void txtSearchNew_TextChanged(object sender, EventArgs e)
        {
            load_grid_CatchMedia(0, false, txtSearchNew.Text);
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            AddMedia(Commands.Append, radGridCatchMedia.SelectedRows);
        }

      

      

        void AddCopyMedia()
        {
            if (isSaving)
            {
                MessageBox.Show("Please Wait.. Playlist is Saving");
                return;
            }

            var rowIndex = 0;
            if (radGridView1.SelectedRows.Count > 0)
                rowIndex = radGridView1.SelectedRows[0].Index;

            if (_copyMediaId.Count <= 0) return;
            var tblPlayoutPort = cmbPlayoutPort.SelectedItem as DataProvider.tblPlayoutPort;
            if (tblPlayoutPort != null)
                PlayList = PlaylistHelper.Instance.GetPlaylist(PlayList, _copyMediaId, tblPlayoutPort.SID, dateTimePicker1.Value.Date, Commands.Insert, rowIndex);
            LoadGrid(true, rowIndex);
        }

        void CopyMedia()
        {
            _copyMediaId = new List<long>();

            foreach (var row in radGridView1.SelectedRows)
            {
                var mediaSID = PlayList[row.Index].MediaSID;
                if (mediaSID != null) _copyMediaId.Add((mediaSID.Value));
            }

            lblCopyStatus.Text = "Copied Item(s) : " + _copyMediaId.Count;
            Application.DoEvents();

        }


        void CutMedia()
        {
            CopyMedia();
            DeleteMedia();


        }




      


        private void txtVodSearch_TextChanged(object sender, EventArgs e)
        {
            load_grid_VodMedia(0, false, txtVodSearch.Text);
        }

        private void radButton3_Click(object sender, EventArgs e)
        {
            
        }

        private void radButton4_Click(object sender, EventArgs e)
        {
            if (CheckPlaylist_UnderRun())
            {
                MessageBox.Show("Please check, Red Items are Under Run");
                return;
            }

            var selectedPort = cmbPlayoutPort.SelectedItem as DataProvider.tblPlayoutPort;

            saveFileDialog1.FileName = selectedPort.Descriptions + "_Playlist_" + dateTimePicker1.Value.ToString("ddMMyyyy");



            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                string filename = saveFileDialog1.FileName;

                DataTable dt = Excel_Function.ListToDataTable<Helper.Playlist>(DisplayPlayList);


                dt.Columns.RemoveAt(13);
                dt.Columns.RemoveAt(12);
                dt.Columns.RemoveAt(11);
                dt.Columns.RemoveAt(10);
                dt.Columns.RemoveAt(9);
                dt.Columns.RemoveAt(8);
                dt.Columns.RemoveAt(7);
                dt.Columns.RemoveAt(0);

                // Excel_Function.ExportToExcel(dt, "", saveFileDialog1.FileName, 1, 1);

                Excel_Function.ExportToExcel(dt, Application.StartupPath + "\\data\\PlaylistTemplate.xlsx", filename, 1, 3, 1, true, true, "A1", "Playlist Schedule" + dateTimePicker1.Value.ToString("dd-MMM-yyyy"));

            }
        }

        Boolean CheckPlaylist_UnderRun()
        {
            Boolean result = false;

            var Index = 0;
            foreach (var item in DisplayPlayList)
            {
                if (Index != 0)
                {
                    if (item.Event.ToLower() == "fixed" &&
                        (DisplayPlayList[Index - 1].PlayTime + DisplayPlayList[Index - 1].Duration) < item.PlayTime)
                    {
                        radGridProperty.RadcolorRow(this.radGridView1, Index, Color.Crimson);
                        result = true;
                    }
                }


                Index++;
            }


            return result;

        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyMedia();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddCopyMedia();
        }

        private void setStartTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetStartTime();
        }

        private void radButton5_Click(object sender, EventArgs e)
        {
            AddMedia();
        }

        private void radButton6_Click(object sender, EventArgs e)
        {
            DeleteMedia();
        }

        private void radButton7_Click(object sender, EventArgs e)
        {
            SaveMedia();
        }

        private void radButton8_Click(object sender, EventArgs e)
        {
            CopyMedia();
        }

        private void radButton9_Click(object sender, EventArgs e)
        {
            AddCopyMedia();
        }

        private void radButton10_Click(object sender, EventArgs e)
        {
            SetStartTime();
        }

        private void radButton1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void fixedEventToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetFixedTime();
        }

        private void followEventToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetFollowEvent();
               
        }

        void SetFollowEvent()
        {
            if (isSaving)
            {
                MessageBox.Show("Please Wait.. Playlist is Saving");
                return;
            }

            if (radGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please Select PlayEvent");
                return;

            }



            if (PlayList.Count > 0)
            {
                int Index = radGridView1.SelectedRows[0].Index;


                PlayList[Index].FixedEvent = false;
                PlayList = Helper.PlaylistHelper.Instance.UpdatePlayoutTime(PlayList);
                LoadGrid();

                radGridProperty.RowSelected(radGridView1, Index);
            }

          
        }

        private void radButton11_Click(object sender, EventArgs e)
        {
            load_grid_CatchMedia();
        }

        private void radButton12_Click(object sender, EventArgs e)
        {
            
        }

        private void radButton13_Click(object sender, EventArgs e)
        {
            
        }

        private void radButton14_Click(object sender, EventArgs e)
        {
            

            if (DisplayPlayList.Count == 0)
            {
                MessageBox.Show("No Data to export");
                return;

            }

            if (CheckReadyToAir() == false)
            {
                MessageBox.Show("Some Clip(s) are not Ready to Air, You Cannot Export,  Please contact Playout");
                return;
            }
            
            if (CheckPlaylist_UnderRun())
            {
                MessageBox.Show("Please check, Red Items are Under Run");
                return;
            }

            lblStatus.Text = "Status: Please wait ....";
            Application.DoEvents();

            var selectedPort = cmbPlayoutPort.SelectedItem as DataProvider.tblPlayoutPort;

            if (selectedPort != null && selectedPort.AutomationSID == (int) Automation.EVS)
            {
                saveFileDialog2.FileName = "Evs_Playlist_XML_B5_" + dateTimePicker1.Value.ToString("ddMMyyyy");
                saveFileDialog2.DefaultExt = "xml";
            }
            else if (selectedPort != null && (selectedPort.AutomationSID == (int) Automation.Itx || selectedPort.AutomationSID == (int) Automation.ItxWithSecondaryEvent))
            {
                saveFileDialog2.FileName = selectedPort.Descriptions + "_ITXML_" +
                                           dateTimePicker1.Value.ToString("ddMMyyyy");
                saveFileDialog2.DefaultExt = "itxml";
            }
            else
            {
                saveFileDialog2.FileName = selectedPort.Descriptions + "_Playlist_B5_" +
                                           dateTimePicker1.Value.ToString("ddMMyyyy");
                saveFileDialog2.DefaultExt = "xls";
            }

            if (saveFileDialog2.ShowDialog() != DialogResult.OK) return;

            string filename = saveFileDialog2.FileName;
            string innternalName = Path.GetFileNameWithoutExtension(filename);


          

            var xml = new XMLCreater();

            switch (selectedPort.AutomationSID)
            {
                case (int)Automation.EVS:
                    xml.PlaylistXML(filename, DisplayPlayList, innternalName);
                    break;
                case (int)Automation.Itx:
                    xml.PlaylistItx(filename, DisplayPlayList, innternalName);
                    break;
                case (int)Automation.ItxWithSecondaryEvent:
                    xml.PlaylistItxWithSecondryEvent(filename, DisplayPlayList, innternalName);
                    break;
                default:
                    xml.PlaylistSundance(filename, DisplayPlayList, innternalName);
                    break;
            }


            lblStatus.Text = "Done";
            Application.DoEvents();
        }


        private bool CheckReadyToAir()
        {
            

            return DisplayPlayList.Count(i => i.ReadyToAir == "Not Ready") == 0 ;


        }

        private void dtDateCatchup_ValueChanged(object sender, EventArgs e)
        {
            load_grid_CatchMedia();
        }

        private void radButton15_Click(object sender, EventArgs e)
        {
            SetFixedTime();
        }

        private void radButton16_Click(object sender, EventArgs e)
        {
            SetFollowEvent();
        }

        private void chkShowAll_CheckedChanged(object sender, EventArgs e)
        {
            load_grid_CatchMedia();
            dtDateCatchup.Visible = chkShowAll.Checked ? false : true;
            Application.DoEvents();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            AddMedia(Commands.Append, radGridCatchMedia.SelectedRows);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            load_grid_CatchMedia();
        }



        void AddMedia(Commands command, GridViewSelectedRowsCollection rows)
        {
            if (isSaving)
            {
                MessageBox.Show("Please Wait.. Playlist is Saving");
                return;
            }

            int SelectedIndex = 0;

            if (radGridView1.SelectedRows.Count == 0)
                command = Commands.Append;
            else
                SelectedIndex = radGridView1.SelectedRows[0].Index;
        
            

            List<long> MediaSID = rows.Select(row => long.Parse(row.Cells["SID"].Value.ToString())).ToList();


            if (MediaSID.Count > 0)
            {
                PlayList = Helper.PlaylistHelper.Instance.GetPlaylist(PlayList, MediaSID, (cmbPlayoutPort.SelectedItem as DataProvider.tblPlayoutPort).SID, dateTimePicker1.Value.Date, command, SelectedIndex);
             
                    LoadGrid(true, SelectedIndex);
            }
        }


        public void AddMediabyMediaID(Commands command, List<long> mediaSID, int selectedIndex)
        {
            if (mediaSID.Count <= 0) return;
           
            
            PlayList = PlaylistHelper.Instance.GetPlaylist(PlayList, mediaSID, (cmbPlayoutPort.SelectedItem as DataProvider.tblPlayoutPort).SID, dateTimePicker1.Value.Date, command, selectedIndex);

            LoadGrid(true, selectedIndex);
        }

        private void radButton17_Click(object sender, EventArgs e)
        {
           
            AddMedia(Commands.Insert, radGridCatchMedia.SelectedRows);
        }

        private void radButton19_Click(object sender, EventArgs e)
        {
            load_grid_VodMedia(0,false,"", true);
        }

        private void radButton20_Click(object sender, EventArgs e)
        {
            AddMedia(Commands.Append, radGridVODMedia.SelectedRows);
        }

        private void radButton18_Click(object sender, EventArgs e)
        {
            
             AddMedia(Commands.Insert, radGridVODMedia.SelectedRows);
        }

        private void radButton12_Click_1(object sender, EventArgs e)
        {
            load_grid_Filler_Media();
        }

        private void radButton21_Click(object sender, EventArgs e)
        {
            AddMedia(Commands.Append, this.radGridFiller.SelectedRows);
        }

        private void radButton3_Click_1(object sender, EventArgs e)
        {
           
            AddMedia(Commands.Insert, this.radGridFiller.SelectedRows);
        }

        private void insertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
                AddMedia(Commands.Insert, radGridCatchMedia.SelectedRows);
            
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            AddMedia(Commands.Append, radGridVODMedia.SelectedRows);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            
                AddMedia(Commands.Insert, radGridVODMedia.SelectedRows);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            load_grid_VodMedia();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            AddMedia(Commands.Append, this.radGridFiller.SelectedRows);
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
          
                AddMedia(Commands.Insert, this.radGridFiller.SelectedRows);
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            load_grid_VodMedia(0, false, "", true);
        }

        private void radButton1_Click_2(object sender, EventArgs e)
        {
            export();

        }


        void export(bool withMail = true)
        {
            if (CheckReadyToAir() == false)
            {
                MessageBox.Show("Some Clip(s) are not Ready to Air, You Cannot Export,  Please contact Playout");
                return;
            }

            var selectedPort = cmbPlayoutPort.SelectedItem as DataProvider.tblPlayoutPort;
            var subject = "";
            var body = "";
            var playlListName = "";
            if (DisplayPlayList.Count == 0)
            {
                MessageBox.Show("No Data to export");
                return;

            }

            if (CheckPlaylist_UnderRun())
            {
                MessageBox.Show("Please check, Red Items are Under Run");
                return;
            }

            //get last version
            int version = 1;

            var instance = typeof(DataProvider.tblReportVersion).AssemblyQualifiedName;
            var entities = ServiceHelper.Instance.GetEntities(instance);
            var tblRevision = entities.Cast<DataProvider.tblReportVersion>().ToList().FirstOrDefault(i => i.lastDate != null && (selectedPort != null && (i.lastDate.Value.Date == dateTimePicker1.Value.Date && i.ReportName == Redirection.Playlist.ToString() && i.ChannelSID == selectedPort.SID))); ; ;



            try
            {
                if (tblRevision != null)
                {
                    if (tblRevision.lastVersion != null) version = tblRevision.lastVersion.Value + 1;


                    tblRevision.lastVersion = version;

                    ServiceHelper.Instance.Save(tblRevision, EntityState.Modified);
                    //LinqBase.UpdateDatabaseWithItem<DataProvider.tblReportVersion>(tblRevision);
                }
                else
                {
                    tblRevision = new DataProvider.tblReportVersion();
                    tblRevision.ReportName = Redirection.Playlist.ToString();
                    tblRevision.lastVersion = version;
                    tblRevision.lastDate = dateTimePicker1.Value.Date;
                    if (selectedPort != null) tblRevision.ChannelSID = selectedPort.SID;

                    ServiceHelper.Instance.Save(tblRevision, EntityState.New);

                    //LinqBase.InsertItemIntoDatabase(tblRevision);
                }
            }
            catch { }


            lblStatus.Text = "Status: Please wait...";
            Application.DoEvents();
            //XML
            string filenameXML = "";
            saveFileDialog2.FileName = "Evs_Playlist_XML_B5_" + dateTimePicker1.Value.ToString("ddMMyyyy") + "_Ver " + version;



            if (selectedPort != null && selectedPort.AutomationSID == (int)Automation.EVS)
            {
                saveFileDialog2.FileName = "Evs_Playlist_XML_B5_" + dateTimePicker1.Value.ToString("ddMMyyyy") + "_Ver " +
                                           version;

                body = "Hi Team<br/><br/>Please find attached the Push VoD - HD Playout  schedule for " + dateTimePicker1.Value.ToString("dd-MMM-yyyy") + " in XL.<br/><br/>Regards,<br/>";
                subject = "Push VoD - HD Playout schedule - " + dateTimePicker1.Value.ToString("dd MMM yyyy") + " Version " + version;
                playlListName = "Evs_";
                saveFileDialog2.DefaultExt = "xml";

            }
            else if (selectedPort != null && (selectedPort.AutomationSID == (int)Automation.Itx || selectedPort.AutomationSID == (int)Automation.ItxWithSecondaryEvent))
            {
                saveFileDialog2.FileName = selectedPort.Descriptions + "_ITXML_" + dateTimePicker1.Value.ToString("ddMMyyyy") + "_Ver " +
                                         version;



                saveFileDialog2.DefaultExt = "itxml";

                body = "Hi Team<br/><br/>Please find attached the " + selectedPort.Descriptions + "  schedule for " + dateTimePicker1.Value.ToString("dd-MMM-yyyy") + ".<br/><br/>Regards,<br/>";
                subject = selectedPort.Descriptions + "  schedule- " + dateTimePicker1.Value.ToString("dd MMM yyyy") + " Version " + version;
                playlListName = "Itx_";

            }
            else
            {
                saveFileDialog2.FileName = selectedPort.Descriptions + "_Playlist_B5_" + dateTimePicker1.Value.ToString("ddMMyyyy") +
                                           "_Ver " + version;
                saveFileDialog2.DefaultExt = "xls";

                body = "Hi Team<br/><br/>Please find attached the " + selectedPort.Descriptions + "  schedule for " + dateTimePicker1.Value.ToString("dd-MMM-yyyy") + ".<br/><br/>Regards,<br/>";
                subject = selectedPort.Descriptions + "  schedule- " + dateTimePicker1.Value.ToString("dd MMM yyyy") + " Version " + version;
                playlListName = "Sundance_";
            }


            if (saveFileDialog2.ShowDialog() != DialogResult.OK) return;


            filenameXML = saveFileDialog2.FileName;
            string innternalName = Path.GetFileNameWithoutExtension(filenameXML);


            var xml = new XMLCreater();

            switch (selectedPort.AutomationSID)
            {
                case (int)Automation.EVS:
                    xml.PlaylistXML(filenameXML, DisplayPlayList, innternalName);
                    break;
                case (int)Automation.Itx:
                    xml.PlaylistItx(filenameXML, DisplayPlayList, innternalName);
                    break;
                case (int)Automation.ItxWithSecondaryEvent:
                    xml.PlaylistItxWithSecondryEvent(filenameXML, DisplayPlayList, innternalName);
                    break;
                default:
                    xml.PlaylistSundance(filenameXML, DisplayPlayList, innternalName);
                    break;
            }


            //playlist
            var filenamePlaylist = "";

            if (selectedPort.AutomationSID != (int)Automation.Sundance)
            {
                saveFileDialog1.FileName = selectedPort.Descriptions + "_" + playlListName + "_" + dateTimePicker1.Value.ToString("ddMMyyyy") + "_Ver " + version;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    filenamePlaylist = saveFileDialog1.FileName;

                    var dt = Excel_Function.ListToDataTable<Helper.Playlist>(DisplayPlayList);

                    dt.Columns.RemoveAt(8);
                    dt.Columns.RemoveAt(7);
                    dt.Columns.RemoveAt(0);

                    // Excel_Function.ExportToExcel(dt, "", saveFileDialog1.FileName, 1, 1);

                    Excel_Function.ExportToExcel(dt, Application.StartupPath + "\\data\\PlaylistTemplate.xlsx",
                        filenamePlaylist, 1, 3, 1, false, true, "A1",
                        "Playlist Schedule" + dateTimePicker1.Value.ToString("dd-MMM-yyyy"));

                }
            }
            else
            {
                filenamePlaylist = (Path.GetDirectoryName(filenameXML) + @"\" + innternalName + ".txt");
            }

            if (withMail)
            {
                const string to = "Playoutops";
                const string cc = "";
                Mail.SentMail_Outlook(body, subject, to, cc, filenamePlaylist, filenameXML);
            }

            lblStatus.Text = "Status: Done";
            Application.DoEvents();
        }

        private void radGridView1_Click_1(object sender, EventArgs e)
        {

        }

        private void cmbPreview_Enter(object sender, EventArgs e)
        {

        }

        private void frmPlaylist_Load(object sender, EventArgs e)
        {
            lblStatus.Text = "Loading...";
           
            LoadGrid();
          
        }

        private void radButton13_Click_1(object sender, EventArgs e)
        {
            CheckPlaylist_UnderRun();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void approvedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            approved();
        }

        private void approved()
        {
            if (isSaving)
            {
                MessageBox.Show("Please Wait.. Playlist is Saving");
                return;
            }

            foreach (var playItems in PlayList)
            {
                playItems.Approved = true;
            }

            SaveMedia();
        }

        private void radButton22_Click(object sender, EventArgs e)
        {
            approved();
        }


        void SetLoop()
        {
            if (isSaving)
            {
                MessageBox.Show("Please Wait.. Playlist is Saving");
                return;
            }
            if (_copyMediaId.Count == 0)
            {
                MessageBox.Show("Please Copy Item(s) from Playlist to Built Loop");
                return;
            }

            var tblPlayoutPort = cmbPlayoutPort.SelectedItem as DataProvider.tblPlayoutPort;

            if (tblPlayoutPort == null)
                return;

            


            var f = new frmStartTime(new TimeSpan(0), dateTimePicker1.Value);

            if (f.ShowDialog() != DialogResult.OK) return;

            if (PlayList.Count <= 0) return;

            var time = new TimeSpan(0, 0, 0, 0);

            var lastOrDefault = PlayList.LastOrDefault();
            if (lastOrDefault != null)
            {
                if (lastOrDefault.PlayoutTime != null)
                {
                     time = lastOrDefault.PlayoutTime.Value;
                }
            }


            if (time > f.StartTime)
            {
                MessageBox.Show("Please enter Time Greater than Last Playlist Time");
                return;
            }

            var mediaList =  DatabaseLookups.Instance.Ingestions;

            while (time <= f.StartTime)
            {
                foreach (var mediaSID in _copyMediaId)
                {
                    var playList = new DataProvider.tblPlaylist
                    {
                        MediaSID = mediaSID,
                        Date = dateTimePicker1.Value.Date,
                        PlayoutPortSID = tblPlayoutPort.SID,
                        Status = false
                    };   
                    var media = mediaList.FirstOrDefault(i => i.SID == mediaSID);
                    playList.tblIngestion = media;


                    if (media != null) if (media.Duration != null) time = time.Add(media.Duration.Value);

                    if (time > f.StartTime)
                        break;

                    lblStatus.Text = "Status: Creating Loop......Timings: " + time ;
                    Application.DoEvents();

                    PlayList.Add(playList);
                }

            }

            PlayList = PlaylistHelper.Instance.UpdatePlayoutTime(PlayList);
          
            LoadGrid();
        }

        private void radButton23_Click(object sender, EventArgs e)
        {
            SetLoop();
        }

        private void createLoopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLoop();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new frmChangePassword();
            f.ShowDialog();
        }

        private void getLastSavedPlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isSaving)
            {
                MessageBox.Show("Please Wait.. Playlist is Saving");
                return;
            }

            var result =
            MessageBox.Show("Do want to retrieve last Saved Playlist, It will delete Present Playlist",
                "Confirmation", MessageBoxButtons.YesNo);

            if (result != DialogResult.Yes) return;



            var playoutPortSID = (cmbPlayoutPort.SelectedItem as DataProvider.tblPlayoutPort).SID;
            var playListDate = dateTimePicker1.Value.Date.ToString("dd-MMM-yyyy");
            var Dt = ServiceHelper.Instance.data_set("Select SID from tblPlaylistUndo  WHERE Date = '" + playListDate + "' AND PlayoutPortSID=" + playoutPortSID);

            if (Dt.Rows.Count == 0)
            {
                MessageBox.Show("No Item in last Save Playlist");
                return;
            }


            ServiceHelper.Instance.DataCommand("Delete from tblPlaylist WHERE Date = '" + playListDate + "' AND PlayoutPortSID=" + playoutPortSID);

            ServiceHelper.Instance.DataCommand(
                "Insert into  tblPlaylist ([PlayoutTime], [Date], [PlayoutPortSID], [MediaSID], [addon], [modon], [Status], [PlayOrderSID], [FixedEvent], [SchDate], [Approved], [Exported]) select [PlayoutTime], [Date], [PlayoutPortSID], [MediaSID], [addon], [modon], [Status], [PlayOrderSID], [FixedEvent], [SchDate], [Approved], [Exported] from [tblPlaylistUndo] WHERE Date = '" +
                playListDate + "' AND PlayoutPortSID=" + playoutPortSID);

            LoadGridWithDatabase();
        }

        private void findAndReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {

            FindAndReplace();
        }

        private void copyCurrentPlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var playoutPort = (cmbPlayoutPort.SelectedItem as DataProvider.tblPlayoutPort);
            var frm = new frmReplicatePlaylist(playoutPort, dateTimePicker1.Value.Date);
            frm.ShowDialog();

        }

        private void multiPlaylistExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new frmMultiExportSchedule();
            frm.ShowDialog();
        }

        public void SelectPlaylistRow(int rowIndex)
        {
           radGridProperty.RowSelected(radGridView1, rowIndex);
        }

        private void findAndReplaceToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FindAndReplace();
        }

        void FindAndReplace()
        {
            if (isSaving)
            {
                MessageBox.Show("Please Wait.. Playlist is Saving");
                return;
            }

            var selectedTXID = "";

            if (radGridView1.SelectedRows.Count > 0)
                selectedTXID = radGridView1.SelectedRows[0].Cells["TransmissionId"].Value.ToString();

            var frm = new frmFindandReplace(selectedTXID);
            frm.ShowDialog();
        }

        public void ReplaceMediaID(int index, DataProvider.tblIngestion ingestionItem, Boolean onlyDelete = false, Boolean resfreshPlaylist = true)
        {
            if (onlyDelete == false)
            {
                var mediaids = new List<long> {ingestionItem.SID};
                var tblPlayoutPort = cmbPlayoutPort.SelectedItem as DataProvider.tblPlayoutPort;
                if (tblPlayoutPort != null)
                    PlayList = PlaylistHelper.Instance.GetPlaylist(PlayList, mediaids, tblPlayoutPort.SID,
                        dateTimePicker1.Value.Date, Commands.Insert, index);
            }

            PlayList.Remove(PlayList[index]);

            if (resfreshPlaylist)
            {
                PlayList = PlaylistHelper.Instance.UpdatePlayoutTime(PlayList);
                LoadGrid(true, index);
            }

        }


        public void RefreshPlaylist()
        {

            PlayList = PlaylistHelper.Instance.UpdatePlayoutTime(PlayList);
            LoadGrid(true);
        }

        private async void importPlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportCSV();
        }

        async void ImportCSV()
        {
            var result = openFileDialog1.ShowDialog();
            if (result != DialogResult.OK) return;

            var selectedPOrt = cmbPlayoutPort.SelectedItem as DataProvider.tblPlayoutPort;
            var date = dateTimePicker1.Value.Date;
            var file = openFileDialog1.FileName;

            //status
            lblStatus.Text = "Please Wait ..... Data Importing...";
            Application.DoEvents();
            var csvData = new CsvData();

            await Task.Run(() =>
            {

                try
                {
                    var dt = Excel_Function.CsvFileToDatatable(file);

                    csvData = PlaylistHelper.ConvertMusicCSVtoPlaylist(dt, date, selectedPOrt.SID);


                }
                catch (Exception ex)
                {
                    //status
                    lblStatus.Text = "Error In Importing...";
                    Application.DoEvents();
                    MessageBox.Show(ex.ToString());
                }
            });

            if (csvData.Error == false)
            {
                PlayList = PlaylistHelper.Instance.UpdatePlayoutTime(csvData.TblPlaylists);
                LoadGrid();
            }
            else
            {
                //status
                lblStatus.Text = "Missing Media in VMG";
                Application.DoEvents();

                var frmMissing = new frmMissingVMGContent(csvData.DisplayPlaylist);
                frmMissing.ShowDialog();


            }



            //status
            lblStatus.Text = "Importing Done..";
            Application.DoEvents();
        }

        private void secondryEventToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSecondaryEvent();
        }

        void AddSecondaryEvent()
        {
            if (isSaving)
            {
                MessageBox.Show("Please Wait.. Playlist is Saving");
                return;
            }

            if (radGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please Select PlayEvent");
                return;
            }   


            var rows = radGridView1.SelectedRows;
            
            var f = new frmSecondaryEvent();

            if (f.ShowDialog() != DialogResult.OK) return;


            
            foreach (var index in rows.Select(row => row.Index).Where(index => PlayList.Count > 0))
            {


                var txID = DisplayPlayList[index].TransmissionId;

                var ignoreList =
                                   DatabaseLookups.Instance.SecondaryRule.Where(
                                       x =>
                                           x.tblSecondaryEventTypeSID == (int)SecondaryEventType.IgnoreProgrammingBug &&
                                           x.Enable == true && txID.Contains(x.TxIdPrefix)).ToList();

                if (ignoreList.Count == 0)       
                     PlayList[index].tblPlaylistSecondryEventSID = f.SID;    
                
            }

            LoadGrid();
        }

        void RemoveSecondaryEvent()
        {
            if (isSaving)
            {
                MessageBox.Show("Please Wait.. Playlist is Saving");
                return;
            }

            if (radGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please Select PlayEvent");
                return;
            }


            var rows = radGridView1.SelectedRows;

           

            foreach (var index in rows.Select(row => row.Index).Where(index => PlayList.Count > 0))
            {

                PlayList[index].tblPlaylistSecondryEventSID = 0;

            }

            LoadGrid();
        }

        private void radDock1_Click(object sender, EventArgs e)
        {

        }

        private void removeSecondaryEventToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveSecondaryEvent();
        }

        private void showSecondaryEventsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSecondaryEvent();
        }

        void ShowSecondaryEvent()
        {

        
            if (radGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please Select PlayEvent");
                return;
            }
            var secondaryEventID = 0L;

            var playlist = radGridView1.SelectedRows[0].DataBoundItem as Helper.Playlist;
            if (playlist != null)
            {
                if (playlist.SecondaryEventSID != null) secondaryEventID = playlist.SecondaryEventSID.Value;
            }

            if (secondaryEventID > 0)
            {
                var frm = new frmSecondaryEventDetails(secondaryEventID);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("No Secondary Event in this event");
            }

        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportCSV();
        }

        private void ShowMediaHistory()
        {
             
            if (isSaving)
            {
                MessageBox.Show("Please Wait.. Playlist is Saving");
                return;
            }

            var rows = radGridView1.SelectedRows;

            if (rows.Count == 0)
                return;


            var index = rows[0].Index;

            var mediaSID = PlayList[index].MediaSID;

            if (mediaSID == null) return;
            
            var frm = new MediaHistory(mediaSID.Value);
            frm.Show();
        }

        private void showMediaHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowMediaHistory();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CutMedia();
        }

        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loading)
                return;


            load_grid_VodMedia();
        }

        private void exportCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DisplayPlayList.Count == 0)
            {
                MessageBox.Show("No Data to export");
                return;

            }

            if (CheckPlaylist_UnderRun())
            {
                MessageBox.Show("Please check, Red Items are Under Run");
                return;
            }

            lblStatus.Text = "Status: Please wait ....";
            Application.DoEvents();

            var selectedPort = cmbPlayoutPort.SelectedItem as DataProvider.tblPlayoutPort;
            
                saveFileDialog2.FileName = selectedPort.Descriptions + "_CSV_" + dateTimePicker1.Value.ToString("ddMMyyyy");
                saveFileDialog2.DefaultExt = "csv";

            if (saveFileDialog2.ShowDialog() != DialogResult.OK) return;

            var filename = saveFileDialog2.FileName;
            var innternalName = Path.GetFileNameWithoutExtension(filename);




            var xml = new XMLCreater();

                    xml.PlaylistCSV(filename, DisplayPlayList, innternalName);
          
            lblStatus.Text = "Done";
            Application.DoEvents();
        }

        private void exportExcelAndXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            export(false);
        }

        private void insertLiveEventToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isSaving)
            {
                MessageBox.Show("Please Wait.. Playlist is Saving");
                return;
            }

            if (radGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please Select PlayEvent");
                return;

            }




            var index = radGridView1.SelectedRows[0].Index;

            var playoutTime = PlayList[index].PlayoutTime;
            if (playoutTime != null)
            {
                var f = new frmLiveEventDuration();

                if (f.ShowDialog() == DialogResult.OK)
                {
                    if (PlayList.Count > 0)
                    {

                        var rowIndex = 0;
                        if (radGridView1.SelectedRows.Count > 0)
                            rowIndex = radGridView1.SelectedRows[0].Index;


                     var tblPlayoutPort = cmbPlayoutPort.SelectedItem as DataProvider.tblPlayoutPort;
                     PlayList = PlaylistHelper.Instance.GetPlaylist(PlayList, new List<long>{-1}, tblPlayoutPort.SID, dateTimePicker1.Value.Date, Commands.Insert, rowIndex, true, f.Duration);
                        
                      
                        LoadGrid();
                    }
                }
            }

            radGridProperty.RowSelected(radGridView1, index);
        }

        private void keepListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmKeepList frm = new frmKeepList();
            frm.ShowDialog();
        }
    }


}
