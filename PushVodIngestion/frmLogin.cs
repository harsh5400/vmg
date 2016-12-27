using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using PushVodIngestion.Forms.Playlist;
using PushVodIngestion.Helper;
using System.Threading.Tasks;
using Application = System.Windows.Forms.Application;


namespace PushVodIngestion
{
    public partial class frmLogin : Form
    {
        Boolean loading = false;
        public frmLogin()
        {
            InitializeComponent();
           // load_Ingestions();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (loading)
                return;

            loading = true;

            
            lblStatus.Text = "Status : Please wait ..";
            Application.DoEvents();
            
           

            var instance = typeof(DataProvider.User).AssemblyQualifiedName;
            var entities = ServiceHelper.Instance.GetEntitiesBasedOnCondition(instance, "LoginName == @0 && LoginPassword ==@1", new ObservableCollection<object>() { txtUser.Text.Trim(), txtPass.Text.Trim() });
            var user = entities.Cast<DataProvider.User>().ToList().FirstOrDefault();


            if (user != null)
            {
                Global.CurreUser = user;

                lblStatus.Text = "Status :Logged in .. Data Loading Please Wait..";
                Application.DoEvents();
                

                load_IngestionsandLogin();

               
             
                
            }
            else
            {

                lblStatus.Text = "Status : Wrong Information";
                Application.DoEvents();
                MessageBox.Show("Wrong Username And Password");
            }

            loading = false;
        }

        void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        async void load_IngestionsandLogin()
        {
            await Task.Run(() =>
            {
                try
                {

                    //first get ingestsions 
                    //var count = DatabaseLookups.Instance.Ingestions;
                    //count = null;

                    DatabaseLookups.Instance.Ingestions = new List<DataProvider.tblIngestion>();


                   


                }
                catch (Exception e)
                {
                    MessageBox.Show("Please Check Internet Connection " + e.ToString());
                }
            });

            var f = new frmPlaylist(true);
            f.FormClosed += frmMain_FormClosed;
            f.Show();
            f.BringToFront();

            lblStatus.Text = "Status : Done";
            Application.DoEvents();
            Hide();
        }
    }
}
