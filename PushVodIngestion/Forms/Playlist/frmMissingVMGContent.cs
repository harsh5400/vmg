using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PushVodIngestion.Forms.Playlist
{
    public partial class frmMissingVMGContent : Form
    {
        public List<Helper.MediaLibrary> MissingMedia;
        public frmMissingVMGContent(List<Helper.MediaLibrary> missingMedia)
        {
            MissingMedia = missingMedia;

            InitializeComponent();

            radGridView1.DataSource = MissingMedia;

            radGridProperty.change_Property(radGridView1, true, true, true);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
