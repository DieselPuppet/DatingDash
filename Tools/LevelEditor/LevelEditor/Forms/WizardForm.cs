using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using LevelData;
using LevelData.SpawnItems;
using LevelData.Utility;
using LevelData.Utility.Extensions;
using LevelEditor.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace LevelEditor.Forms
{
	public partial class WizardForm : Form
	{
        public class Item : SpawnItem, IDXDataErrorInfo
        {            
            public int Probability
            {
                get;
                set;
            }

            public Item()
                : base(Pack.SpawnItemConfig.Types.Keys.First(), 0)
            {
            }

            public void GetError(ErrorInfo info)
            {
            }

            public void GetPropertyError(string propertyName, ErrorInfo info)
            {
                switch (propertyName)
                {
                    case "Type":
                        if (Type.IsNullOrWhiteSpace())
                        {
                            info.ErrorType = ErrorType.Warning;
                            info.ErrorText = "Type is required";
                        }
                        break;

                    case "Probability":
                        if (Probability <= 0)
                        {
                            info.ErrorType = ErrorType.Warning;
                            info.ErrorText = "Probability must be greater or equal than 0.";
                        }
                        break;
                }
            }
        }

        private BindingList<Item> _items = new BindingList<Item>();
        private Dictionary<GridColumn, string> _attachTypeColMap = new Dictionary<GridColumn, string>();
        private PackForm _parent;

        public IReadOnlyList<Item> Items
        {
            get { return _items; }
        }

        public int Count
        {
            get { return (int)txtCount.Value; }
        }

        public float Interval
        {
            get { return (float)txtInterval.Value; }
        }

		public WizardForm(PackForm parent)
		{
			InitializeComponent();

            _parent = parent;
            gdItems.DataSource = _items;
            PopulateCombos();

            foreach (string type in Pack.SpawnItemConfig.AttachmentTypes.Keys)
            {
                for (int i = 0; i < PackForm.MaxAttachmentsForType; i++)
                {
                    var gc = gvItems.Columns.Add();
                    gc.Name = String.Format("{0}{1}{2}", PackForm.AttachmentColNamePrefix, type, i);
                    gc.FieldName = gc.Name;
                    gc.Caption = type;
                    gc.UnboundType = UnboundColumnType.String;
                    gc.Width = 30;
                    gc.Visible = true;

                    _attachTypeColMap.Add(gc, type);
                }
            }

            // instant commit (will be detached automatically when form closes)
            foreach (var cmb in _parent.AttachmentEditorsByType.Values)
            {
                GridUtility.ConfigureInstantCommit(gvItems, cmb);
            }
		}

        private void PopulateCombos()
        {
            _parent.PopulateSpawnItemTypes(repCmbItemType, true, true);
            GridUtility.ConfigureInstantCommit(gvItems, repCmbItemType);
        }

        private bool ValidateData()
        {
            gvItems.CloseEditor();
            gvItems.UpdateCurrentRow();

            if (txtCount.Value <= 1)
            {
                DialogUtility.ShowWarning("{0} must be greater than 1.", lblCount.Text);
                return false;
            }

            if (txtInterval.Value <= 0)
            {
                DialogUtility.ShowWarning("{0} must be greater than 0.", lblInterval.Text);
                return false;
            }

            if (_items.Count == 0)
            {
                DialogUtility.ShowWarning("No items added.");
                return false;
            }
            
            int prevFocused = gvItems.FocusedRowHandle;
            bool hasItemErrors = false;
            for (int i = 0; i < gvItems.DataRowCount; i++)
            {
                gvItems.FocusedRowHandle = i;
                
                // NOTE: HasColumnErrors doesn't work
                foreach (GridColumn gc in gvItems.Columns)
                {
                    if (!gvItems.GetColumnError(gc).IsNullOrWhiteSpace())
                    {
                        hasItemErrors = true;
                        break;
                    }
                }

                if (hasItemErrors)
                    break;
            }

            gvItems.FocusedRowHandle = prevFocused;

            if (hasItemErrors)
            {
                DialogUtility.ShowWarning("One ore more items in the grid has errors. Correct or remove invalid item(s).");
                return false;
            }

            return true;
        }

        private void gvItems_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (!_attachTypeColMap.ContainsKey(e.Column))
                return;

            string attachmentType = _attachTypeColMap[e.Column];
            var item = (Item)e.Row;
            int index = int.Parse(e.Column.Name.Replace(PackForm.AttachmentColNamePrefix + attachmentType, String.Empty));
            SpawnItemAttachment[] attachments = item.Attachments.Where(att => att.Type == attachmentType).ToArray();
            SpawnItemAttachment attach = index < attachments.Length ? attachments[index] : null;

            if (e.IsGetData)
            {
                if (attach != null)
                    e.Value = attachments[index].Name;
                else
                    e.Value = null;
            }
            else if (e.IsSetData)
            {
                string attachmentName = (string)e.Value;
                if (attachmentName == null)
                {
                    if (attach != null)
                        item.Attachments.Remove(attach);
                }
                else
                {
                    if (attach != null)
                    {
                        attach.Name = attachmentName;
                    }
                    else
                    {
                        attach = new SpawnItemAttachment(attachmentType, attachmentName);
                        item.Attachments.Add(attach);
                    }
                }
            }
        }

        private void gvItems_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (!_attachTypeColMap.ContainsKey(e.Column))
                return;

            string type = _attachTypeColMap[e.Column];
            e.RepositoryItem = _parent.AttachmentEditorsByType[type];
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!ValidateData())
            {
                DialogResult = DialogResult.None;
                return;
            }
        }
    }
}
