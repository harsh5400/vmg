using System.Data.Linq;
using PushVodIngestion.DataProvider;
using PushVodIngestion.Helper;
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
    public partial class frmSecondaryEventDetails : Form
    {
        private long _sid;
        public frmSecondaryEventDetails(long sid)
        {
            _sid = sid;
            InitializeComponent();
            load_grid();
        }

        public void load_grid(int RowIndex = 0, bool byRow = false, String SerachText = "")
        {
            var lst = DatabaseLookups.Instance.TblPlaylistSecondryEventDetail.Where(i=> i.tblPlaylistSecondryEventSID == _sid).ToList();

            var secondaryEventRules = lst.Select(detail => DatabaseLookups.Instance.SecondaryRule.FirstOrDefault(i => i.SID == detail.tblSecondryEventRuleSID)).Where(secondaryEventRule => secondaryEventRule != null).ToList();

            radGridView1.DataSource = secondaryEventRules.Select(i=> new {i.TxIdPrefix, i.OffsetTime, i.Duration, i.TimeMode , i.Mode}).ToList();

            radGridProperty.change_Property(this.radGridView1, false, false, true);



            Application.DoEvents();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }


    }
}
