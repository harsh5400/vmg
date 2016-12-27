using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PushVodIngestion.Helper;

namespace PushVodIngestion.Forms.Playlist
{
    public partial class frmFindandReplace : Form
    {
        private frmPlaylist _playlistForm = new frmPlaylist();
        private List<int> _foundIndex;
        private int CurrentIndex;
        private string PreviousSerach;
        public frmFindandReplace(string findMedia)
        {
            InitializeComponent();

            _playlistForm = Application.OpenForms["frmPlaylist"] as frmPlaylist;

            FindMedia = findMedia;
            txtFind.Text = findMedia;
            CurrentIndex = -1;

        }

        public String FindMedia { get; set; }

        private void butSave_Click(object sender, EventArgs e)
        {

            Find();

        }

        void Find()
        {
            _foundIndex =
               _playlistForm.DisplayPlayList.Where(i => i.TransmissionId.ToLower().Contains(txtFind.Text.ToLower())).Select(x => _playlistForm.DisplayPlayList.IndexOf(x)).ToList();


            if (!_foundIndex.Any())
            {
                MessageBox.Show("Not Found");
                return;
            }


            if (CurrentIndex == _foundIndex.LastOrDefault())
            {
                MessageBox.Show("Search Cpmpleted");
                CurrentIndex = -1;
            }
            else
            {
                CurrentIndex = _foundIndex.FirstOrDefault(x => x > CurrentIndex);
                _playlistForm.SelectPlaylistRow(CurrentIndex);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            ReplaceDelete();

        }

        void ReplaceDelete(Boolean onlyDelete = false)
        {

            if (txtReplace.Text.Trim().Length == 0 && onlyDelete == false)
            {
                MessageBox.Show("Please Enter TX ID in Replace");
                return;
            }

            if (CurrentIndex < 0)
            {
                MessageBox.Show("Please Find TX ID for Replace");
                return;
            }



            var ingestionItem =
                DatabaseLookups.Instance.Ingestions.FirstOrDefault(
                    i => i.itemCode == txtReplace.Text || i.TxId == txtReplace.Text);

            if (ingestionItem == null && onlyDelete == false)
            {
                MessageBox.Show("TX ID in is Invalid, Cannot replace TX ID");
                return;
            }

            _playlistForm.ReplaceMediaID(CurrentIndex, ingestionItem, onlyDelete);


            CurrentIndex = -1;

            Find(); 
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ReplaceDelete(true);
        }

        
        void ReplaceAll(Boolean onlyDelete = false)
        {
            lblStatus.Text = "Status: Please Wait...";
            Application.DoEvents();

            if (ValidateReplace(onlyDelete) == false)
                return;

            //found index list
            var foundIndex =
               _playlistForm.DisplayPlayList.Where(i => i.TransmissionId.ToLower().Contains(txtFind.Text.ToLower())).Select(x => _playlistForm.DisplayPlayList.IndexOf(x)).ToList();

            if (!foundIndex.Any())
            {
                MessageBox.Show("TX ID Not Found");
                return;
            }

           //replaceable item
            var ingestionItem =
               DatabaseLookups.Instance.Ingestions.FirstOrDefault(
                   i => i.itemCode == txtReplace.Text || i.TxId == txtReplace.Text);

            for (var index = foundIndex.Count - 1; index >= 0; index--)
            {
                var i = foundIndex[index];
                _playlistForm.ReplaceMediaID(i, ingestionItem, onlyDelete, false);
            }

            _playlistForm.RefreshPlaylist();

            lblStatus.Text = "Status: Done";
            Application.DoEvents();

            if (onlyDelete)
            {

                MessageBox.Show(foundIndex.Count + " TXIDS has been Deleted");
                return;
            }
            MessageBox.Show(foundIndex.Count + " TXIDS has been Replaced");


        }




        bool ValidateReplace(Boolean onlyDelete = false)
        {


            if (txtFind.Text.ToLower().Trim().Length == 0)
            {
                MessageBox.Show("Please Enter TX ID in Find");
                return false;
            }

            if (onlyDelete) return true;

            var ingestionItem =
                DatabaseLookups.Instance.Ingestions.FirstOrDefault(
                    i => i.itemCode == txtReplace.Text || i.TxId == txtReplace.Text);

            if (ingestionItem != null) return true;

            MessageBox.Show("TX ID in is Invalid, Cannot replace TX ID");
            return false;
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            ReplaceAll();
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            ReplaceAll(true);
        }



       
    }
}
