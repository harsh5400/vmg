using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PushVodIngestion.Forms.Playlist;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace PushVodIngestion.Helper
{
    public class DragAndDropRadGrid : RadGridView
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DraggableGridView"/> class.
        /// </summary>
        public DragAndDropRadGrid()
        {
            // comment this line to disable multi selection
            this.MultiSelect = true;

            var dragDropService = this.GridViewElement.GetService<RadGridViewDragDropService>();
            dragDropService.PreviewDragStart += this.dragDropService_PreviewDragStart;
            dragDropService.PreviewDragDrop += this.dragDropService_PreviewDragDrop;
            dragDropService.PreviewDragOver += this.dragDropService_PreviewDragOver;
            dragDropService.PreviewDragHint += this.dragDropService_PreviewDragHint;

            this.RowFormatting += this.DraggableGridView_RowFormatting;

            var gridBehavior = this.GridBehavior as BaseGridBehavior;
            gridBehavior.UnregisterBehavior(typeof(GridViewDataRowInfo));
            gridBehavior.RegisterBehavior(typeof(GridViewDataRowInfo), new MyGridDataRowBehavior());
        }

        #endregion Constructors and Destructors

        #region Properties

        /// <summary>
        /// Gets ThemeClassName.
        /// </summary>
        public override string ThemeClassName
        {
            get
            {
                return typeof(RadGridView).FullName;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// The draggable grid view_ row formatting.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void DraggableGridView_RowFormatting(object sender, RowFormattingEventArgs e)
        {
            e.RowElement.AllowDrag = true;
            e.RowElement.AllowDrop = true;
        }

        /// <summary>
        /// The get target row index.
        /// </summary>
        /// <param name="row">
        /// The row.
        /// </param>
        /// <param name="dropLocation">
        /// The drop location.
        /// </param>
        /// <returns>
        /// The get target row index.
        /// </returns>
        private int GetTargetRowIndex(GridDataRowElement row, Point dropLocation)
        {
            int halfHeight = row.Size.Height / 2;
            int index = row.RowInfo.Index;
            if (dropLocation.Y > halfHeight)
            {
                index++;
            }

            return index;
        }

        /// <summary>
        /// The move rows.
        /// </summary>
        /// <param name="targetGrid">
        /// The target grid.
        /// </param>
        /// <param name="dragGrid">
        /// The drag grid.
        /// </param>
        /// <param name="dragRows">
        /// The drag rows.
        /// </param>
        /// <param name="index">
        /// The index.
        /// </param>
        private void MoveRows(RadGridView targetGrid, RadGridView dragGrid, IList<GridViewRowInfo> dragRows, int index)
        {
            try
            {



                var startIndex = index;
                dragGrid.BeginUpdate();
                targetGrid.BeginUpdate();

                var mediaSID = new List<long>();
                for (int i = dragRows.Count - 1; i >= 0; i--)
                {
                    GridViewRowInfo row = dragRows[i];
                    if (row is GridViewSummaryRowInfo)
                    {
                        continue;
                    }

                    if (typeof (DataSet).IsAssignableFrom(targetGrid.DataSource.GetType()))
                    {
                        // customize data set tables here if required
                        var sourceTable = ((DataSet) dragGrid.DataSource).Tables[0];
                        var targetTable = ((DataSet) targetGrid.DataSource).Tables[0];

                        var newRow = targetTable.NewRow();
                        foreach (GridViewCellInfo cell in row.Cells)
                        {
                            newRow[cell.ColumnInfo.Name] = cell.Value;
                        }

                        sourceTable.Rows.Remove(((DataRowView) row.DataBoundItem).Row);
                        targetTable.Rows.InsertAt(newRow, index);
                    }
                    else if (typeof (IList).IsAssignableFrom(targetGrid.DataSource.GetType()))
                    {
                        var targetCollection = (IList) targetGrid.DataSource;
                        //var sourceCollection = (IList)dragGrid.DataSource;

                        // handle custom removing of data bound item here if necessary
                        //sourceCollection.Remove(row.DataBoundItem);

                        // format insert data here
                        // targetCollection.Add(row.DataBoundItem);


                        var insertableItem = row.DataBoundItem as MediaLibrary;
                        if (insertableItem != null) mediaSID.Add(insertableItem.SID);





                        //targetCollection.Insert(index, row.DataBoundItem);
                    }
                    else
                    {
                        // handle custom data operations here
                    }

                    index++;
                }


                var frmPlaylistInstance = Application.OpenForms["frmPlaylist"] as frmPlaylist;
                if (frmPlaylistInstance != null)
                {
                    var command = Commands.Insert;

                    if (startIndex == targetGrid.Rows.Count)
                        command = Commands.Append;

                    frmPlaylistInstance.AddMediabyMediaID(command, mediaSID, startIndex);
                }


                dragGrid.EndUpdate(true);
                targetGrid.EndUpdate(true);


                radGridProperty.SelectRowRadGridView(startIndex, targetGrid);
            }
            catch (Exception ex)
            {
              
            }
        }

        /// <summary>
        /// The drag drop service_ preview drag drop.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void dragDropService_PreviewDragDrop(object sender, RadDropEventArgs e)
        {
            var rowElement = e.DragInstance as GridDataRowElement;
            if (rowElement == null)
            {
                return;
            }

            e.Handled = true;

            var dropTarget = e.HitTarget as RadItem;
            var targetGrid = dropTarget.ElementTree.Control as RadGridView;
            if (targetGrid == null)
            {
                return;
            }

            var dragGrid = rowElement.ElementTree.Control as RadGridView;

            if (targetGrid != dragGrid)
            {
                e.Handled = true;
                var dropTargetRow = dropTarget as GridDataRowElement;
                int index = dropTargetRow != null
                                ? this.GetTargetRowIndex(dropTargetRow, e.DropLocation)
                                : targetGrid.RowCount;

                this.MoveRows(targetGrid, dragGrid, dragGrid.SelectedRows, index);
            }
        }

        /// <summary>
        /// The drag drop service_ preview drag hint.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void dragDropService_PreviewDragHint(object sender, PreviewDragHintEventArgs e)
        {
            var dataRowElement = e.DragInstance as GridDataRowElement;
            if (dataRowElement != null && dataRowElement.ViewTemplate.MasterTemplate.SelectedRows.Count > 1)
            {
                // set custom drag hint for multiple rows here
                //e.DragHint = new Bitmap(this.imageList1.Images[6]);
                //e.UseDefaultHint = false;
            }
        }

        /// <summary>
        /// The drag drop service_ preview drag over.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void dragDropService_PreviewDragOver(object sender, RadDragOverEventArgs e)
        {
            if (e.DragInstance is GridDataRowElement)
            {
                e.CanDrop = e.HitTarget is GridDataRowElement || e.HitTarget is GridTableElement ||
                            e.HitTarget is GridSummaryRowElement;
            }
        }

        /// <summary>
        /// The drag drop service_ preview drag start.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void dragDropService_PreviewDragStart(object sender, PreviewDragStartEventArgs e)
        {
            e.CanStart = true;
        }

        #endregion Methods

        /// <summary>
        /// The my grid data row behavior.
        /// </summary>
        private class MyGridDataRowBehavior : GridDataRowBehavior
        {
            #region Methods

            /// <summary>
            /// The on mouse down left.
            /// </summary>
            /// <param name="e">
            /// The e.
            /// </param>
            /// <returns>
            /// The on mouse down left.
            /// </returns>
            protected override bool OnMouseDownLeft(MouseEventArgs e)
            {
                var dataRowElement = this.GetRowAtPoint(e.Location) as GridDataRowElement;

                if (dataRowElement != null)
                {
                    var dragDropService = this.GridViewElement.GetService<RadGridViewDragDropService>();
                    dragDropService.Start(dataRowElement);
                }

                return base.OnMouseDownLeft(e);
            }

            #endregion Methods
        }
    }

    public class RowSelectionGridBehavior : GridDataRowBehavior
    {
        protected override bool OnMouseDownLeft(MouseEventArgs e)
        {
            GridDataRowElement row = this.GetRowAtPoint(e.Location) as GridDataRowElement;
            if (row != null)
            {
                RadGridViewDragDropService svc = this.GridViewElement.GetService<RadGridViewDragDropService>();
                svc.AllowAutoScrollColumnsWhileDragging = false;
                svc.AllowAutoScrollRowsWhileDragging = false;
                svc.Start(row);
            }
            return base.OnMouseDownLeft(e);
        }
    }
}
