using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PushVodIngestion.Helper
{
    class MediaUpdateHelper
    {

        public void MediaUpdatefromVmg()
        {
            try
            {



                var store = LinqBase.GetNewDataContext();
                store.DeferredLoadingEnabled = false;

                DAL.data_command("Update tblIngestion SET ReadyToAir = 1 Where ReadyToAir is NULL");

                var lstVMG = store.tbl_Adi_ingest_item_file_infos.ToList();
                // var medias = 
                foreach (var vmGmedia in lstVMG)
                {
                    var update = true;
                    var media =
                        store.tblIngestions.Where(i => i.itemCode != null)
                            .ToList()
                            .FirstOrDefault(i => i.itemCode.Trim() == vmGmedia.itemCode.Trim());

                    if (media == null)
                    {
                        media = new tblIngestion();
                        update = false;
                    }

                    media.itemCode = vmGmedia.itemCode;
                    media.Duration = TimeSpan.Parse(vmGmedia.duration_Frames.Substring(0, 8));
                    media.ProgrameName = vmGmedia.name;
                    media.ChannelSID = 1;

                    if (media.ReadyToAir == null)
                        media.ReadyToAir = true;

                    if (media.SourceTypeSID == null)
                        media.SourceTypeSID = (int) SourceType.VOD;

                    if (update == false)
                    {
                        media.addon = DateTime.Now;
                        store.tblIngestions.InsertOnSubmit(media);
                    }
                    else
                    {
                        media.modon = DateTime.Now;
                    }

                    store.SubmitChanges();

                    //System.Diagnostics.Debug.Print(VMGmedia.name);
                }

            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.ToString());

            }
        }
    }
}
