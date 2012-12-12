using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LevelEditor.Forms
{
    public partial class PackForm
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PackForm));
            this.pnlParam = new System.Windows.Forms.Panel();
            this.gdLevelItems = new DevExpress.XtraGrid.GridControl();
            this.gvLevelItems = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcItemCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcItemType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcItemValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.splLevelItems = new DevExpress.XtraEditors.SplitterControl();
            this.grpSpawnItemContainer = new DevExpress.XtraEditors.GroupControl();
            this.pnlSpawnItemBinding = new System.Windows.Forms.Panel();
            this.txtSpawnItemAbsoluteTime = new DevExpress.XtraEditors.SpinEdit();
            this.txtSpawnItemDelay = new DevExpress.XtraEditors.SpinEdit();
            this.gdAttachmentTypes = new DevExpress.XtraGrid.GridControl();
            this.gvAttachmentTypes = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcAttachmentType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbSpawnItemType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.ilSpawnItemTypesLarge = new System.Windows.Forms.ImageList();
            this.lblSpawnItemDelay = new System.Windows.Forms.Label();
            this.lblSpawnItemAbsoluteTime = new System.Windows.Forms.Label();
            this.lblSpawnItemType = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtTime = new DevExpress.XtraEditors.SpinEdit();
            this.lblTime = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMinScore = new System.Windows.Forms.TextBox();
            this.txtMinScore10 = new System.Windows.Forms.TextBox();
            this.tbcEditors = new System.Windows.Forms.TabControl();
            this.tbpPeoplesEditor = new System.Windows.Forms.TabPage();
            this.pnlLines = new System.Windows.Forms.Panel();
            this.tabBottom = new DevExpress.XtraTab.XtraTabControl();
            this.tbpTasks = new DevExpress.XtraTab.XtraTabPage();
            this.gdTasks = new DevExpress.XtraGrid.GridControl();
            this.gvTasks = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcTaskCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repCmbTaskCategory = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gcTaskType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repCmbTaskType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gcTaskScore = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcTaskParam1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcTaskParam2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcTaskParam3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tbpLog = new DevExpress.XtraTab.XtraTabPage();
            this.txtLog = new DevExpress.XtraEditors.MemoEdit();
            this.pnlLine5 = new System.Windows.Forms.Panel();
            this.pnlLine4 = new System.Windows.Forms.Panel();
            this.pnlLine3 = new System.Windows.Forms.Panel();
            this.pnlLine2 = new System.Windows.Forms.Panel();
            this.pnlLine1 = new System.Windows.Forms.Panel();
            this.pnlTimer = new System.Windows.Forms.Panel();
            this.pnlLineNumbers = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbTimer = new System.Windows.Forms.Label();
            this.hScrollBar = new System.Windows.Forms.HScrollBar();
            this.cmLinePanel = new System.Windows.Forms.ContextMenu();
            this.cmButton = new System.Windows.Forms.ContextMenu();
            this.splMain = new DevExpress.XtraEditors.SplitterControl();
            this.ilAttachmentNames = new System.Windows.Forms.ImageList();
            this.ilSpawnItemTypesSmall = new System.Windows.Forms.ImageList();
            this.pnlParam.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gdLevelItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLevelItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpSpawnItemContainer)).BeginInit();
            this.grpSpawnItemContainer.SuspendLayout();
            this.pnlSpawnItemBinding.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSpawnItemAbsoluteTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSpawnItemDelay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gdAttachmentTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAttachmentTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSpawnItemType.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTime.Properties)).BeginInit();
            this.tbcEditors.SuspendLayout();
            this.tbpPeoplesEditor.SuspendLayout();
            this.pnlLines.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabBottom)).BeginInit();
            this.tabBottom.SuspendLayout();
            this.tbpTasks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gdTasks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTasks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repCmbTaskCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repCmbTaskType)).BeginInit();
            this.tbpLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLog.Properties)).BeginInit();
            this.pnlLineNumbers.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlParam
            // 
            this.pnlParam.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlParam.Controls.Add(this.gdLevelItems);
            this.pnlParam.Controls.Add(this.splLevelItems);
            this.pnlParam.Controls.Add(this.grpSpawnItemContainer);
            this.pnlParam.Controls.Add(this.panel1);
            this.pnlParam.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlParam.Location = new System.Drawing.Point(0, 0);
            this.pnlParam.MinimumSize = new System.Drawing.Size(232, 0);
            this.pnlParam.Name = "pnlParam";
            this.pnlParam.Size = new System.Drawing.Size(232, 670);
            this.pnlParam.TabIndex = 0;
            // 
            // gdLevelItems
            // 
            this.gdLevelItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gdLevelItems.Location = new System.Drawing.Point(0, 233);
            this.gdLevelItems.MainView = this.gvLevelItems;
            this.gdLevelItems.Name = "gdLevelItems";
            this.gdLevelItems.Size = new System.Drawing.Size(228, 433);
            this.gdLevelItems.TabIndex = 0;
            this.gdLevelItems.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLevelItems});
            // 
            // gvLevelItems
            // 
            this.gvLevelItems.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black;
            this.gvLevelItems.Appearance.GroupRow.Options.UseForeColor = true;
            this.gvLevelItems.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcItemCategory,
            this.gcItemType,
            this.gcItemValue});
            this.gvLevelItems.GridControl = this.gdLevelItems;
            this.gvLevelItems.GroupCount = 1;
            this.gvLevelItems.GroupFormat = "[#image]{1} {2}";
            this.gvLevelItems.Name = "gvLevelItems";
            this.gvLevelItems.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvLevelItems.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvLevelItems.OptionsBehavior.AutoExpandAllGroups = true;
            this.gvLevelItems.OptionsBehavior.AutoPopulateColumns = false;
            this.gvLevelItems.OptionsCustomization.AllowColumnMoving = false;
            this.gvLevelItems.OptionsCustomization.AllowGroup = false;
            this.gvLevelItems.OptionsCustomization.AllowQuickHideColumns = false;
            this.gvLevelItems.OptionsView.ShowGroupPanel = false;
            this.gvLevelItems.OptionsView.ShowIndicator = false;
            this.gvLevelItems.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gcItemCategory, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gcItemCategory
            // 
            this.gcItemCategory.Caption = "Category";
            this.gcItemCategory.FieldName = "Category";
            this.gcItemCategory.Name = "gcItemCategory";
            this.gcItemCategory.OptionsColumn.AllowEdit = false;
            this.gcItemCategory.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gcItemCategory.OptionsColumn.AllowMove = false;
            this.gcItemCategory.OptionsColumn.AllowShowHide = false;
            this.gcItemCategory.OptionsColumn.AllowSize = false;
            this.gcItemCategory.OptionsColumn.ReadOnly = true;
            this.gcItemCategory.OptionsColumn.ShowInCustomizationForm = false;
            this.gcItemCategory.Visible = true;
            this.gcItemCategory.VisibleIndex = 0;
            // 
            // gcItemType
            // 
            this.gcItemType.AppearanceCell.Options.UseTextOptions = true;
            this.gcItemType.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gcItemType.Caption = "Type";
            this.gcItemType.FieldName = "Type";
            this.gcItemType.Name = "gcItemType";
            this.gcItemType.OptionsColumn.AllowEdit = false;
            this.gcItemType.OptionsColumn.ReadOnly = true;
            this.gcItemType.Visible = true;
            this.gcItemType.VisibleIndex = 0;
            this.gcItemType.Width = 100;
            // 
            // gcItemValue
            // 
            this.gcItemValue.Caption = "Value";
            this.gcItemValue.FieldName = "Value";
            this.gcItemValue.Name = "gcItemValue";
            this.gcItemValue.Visible = true;
            this.gcItemValue.VisibleIndex = 1;
            this.gcItemValue.Width = 126;
            // 
            // splLevelItems
            // 
            this.splLevelItems.Dock = System.Windows.Forms.DockStyle.Top;
            this.splLevelItems.Location = new System.Drawing.Point(0, 228);
            this.splLevelItems.Name = "splLevelItems";
            this.splLevelItems.Size = new System.Drawing.Size(228, 5);
            this.splLevelItems.TabIndex = 6;
            this.splLevelItems.TabStop = false;
            // 
            // grpSpawnItemContainer
            // 
            this.grpSpawnItemContainer.Controls.Add(this.pnlSpawnItemBinding);
            this.grpSpawnItemContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpSpawnItemContainer.Location = new System.Drawing.Point(0, 79);
            this.grpSpawnItemContainer.MinimumSize = new System.Drawing.Size(0, 149);
            this.grpSpawnItemContainer.Name = "grpSpawnItemContainer";
            this.grpSpawnItemContainer.Size = new System.Drawing.Size(228, 149);
            this.grpSpawnItemContainer.TabIndex = 0;
            this.grpSpawnItemContainer.Text = "Spawn item";
            // 
            // pnlSpawnItemBinding
            // 
            this.pnlSpawnItemBinding.Controls.Add(this.txtSpawnItemAbsoluteTime);
            this.pnlSpawnItemBinding.Controls.Add(this.txtSpawnItemDelay);
            this.pnlSpawnItemBinding.Controls.Add(this.gdAttachmentTypes);
            this.pnlSpawnItemBinding.Controls.Add(this.cmbSpawnItemType);
            this.pnlSpawnItemBinding.Controls.Add(this.lblSpawnItemDelay);
            this.pnlSpawnItemBinding.Controls.Add(this.lblSpawnItemAbsoluteTime);
            this.pnlSpawnItemBinding.Controls.Add(this.lblSpawnItemType);
            this.pnlSpawnItemBinding.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSpawnItemBinding.Location = new System.Drawing.Point(2, 21);
            this.pnlSpawnItemBinding.Margin = new System.Windows.Forms.Padding(0);
            this.pnlSpawnItemBinding.Name = "pnlSpawnItemBinding";
            this.pnlSpawnItemBinding.Size = new System.Drawing.Size(224, 126);
            this.pnlSpawnItemBinding.TabIndex = 0;
            // 
            // txtSpawnItemAbsoluteTime
            // 
            this.txtSpawnItemAbsoluteTime.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSpawnItemAbsoluteTime.Location = new System.Drawing.Point(44, 24);
            this.txtSpawnItemAbsoluteTime.Name = "txtSpawnItemAbsoluteTime";
            this.txtSpawnItemAbsoluteTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.txtSpawnItemAbsoluteTime.Properties.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.txtSpawnItemAbsoluteTime.Properties.ReadOnly = true;
            this.txtSpawnItemAbsoluteTime.Size = new System.Drawing.Size(75, 20);
            this.txtSpawnItemAbsoluteTime.TabIndex = 29;
            // 
            // txtSpawnItemDelay
            // 
            this.txtSpawnItemDelay.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSpawnItemDelay.Location = new System.Drawing.Point(149, 24);
            this.txtSpawnItemDelay.Name = "txtSpawnItemDelay";
            this.txtSpawnItemDelay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtSpawnItemDelay.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.txtSpawnItemDelay.Properties.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.txtSpawnItemDelay.Properties.MaxValue = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.txtSpawnItemDelay.Size = new System.Drawing.Size(68, 20);
            this.txtSpawnItemDelay.TabIndex = 1;
            this.txtSpawnItemDelay.EditValueChanged += new System.EventHandler(this.txtSpawnItemDelay_EditValueChanged);
            // 
            // gdAttachmentTypes
            // 
            this.gdAttachmentTypes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gdAttachmentTypes.Location = new System.Drawing.Point(44, 48);
            this.gdAttachmentTypes.MainView = this.gvAttachmentTypes;
            this.gdAttachmentTypes.Name = "gdAttachmentTypes";
            this.gdAttachmentTypes.Size = new System.Drawing.Size(173, 74);
            this.gdAttachmentTypes.TabIndex = 1;
            this.gdAttachmentTypes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAttachmentTypes});
            // 
            // gvAttachmentTypes
            // 
            this.gvAttachmentTypes.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black;
            this.gvAttachmentTypes.Appearance.GroupRow.Options.UseForeColor = true;
            this.gvAttachmentTypes.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcAttachmentType});
            this.gvAttachmentTypes.GridControl = this.gdAttachmentTypes;
            this.gvAttachmentTypes.GroupFormat = "[#image]{1} {2}";
            this.gvAttachmentTypes.Name = "gvAttachmentTypes";
            this.gvAttachmentTypes.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvAttachmentTypes.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvAttachmentTypes.OptionsBehavior.AutoExpandAllGroups = true;
            this.gvAttachmentTypes.OptionsBehavior.AutoPopulateColumns = false;
            this.gvAttachmentTypes.OptionsCustomization.AllowColumnMoving = false;
            this.gvAttachmentTypes.OptionsCustomization.AllowGroup = false;
            this.gvAttachmentTypes.OptionsCustomization.AllowQuickHideColumns = false;
            this.gvAttachmentTypes.OptionsView.ShowColumnHeaders = false;
            this.gvAttachmentTypes.OptionsView.ShowGroupPanel = false;
            this.gvAttachmentTypes.OptionsView.ShowIndicator = false;
            this.gvAttachmentTypes.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gvAttachmentTypes_CustomRowCellEdit);
            this.gvAttachmentTypes.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(this.gvAttachmentTypes_CustomUnboundColumnData);
            // 
            // gcAttachmentType
            // 
            this.gcAttachmentType.AppearanceCell.Options.UseTextOptions = true;
            this.gcAttachmentType.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gcAttachmentType.Caption = "Type";
            this.gcAttachmentType.FieldName = "Column";
            this.gcAttachmentType.Name = "gcAttachmentType";
            this.gcAttachmentType.OptionsColumn.AllowEdit = false;
            this.gcAttachmentType.OptionsColumn.ReadOnly = true;
            this.gcAttachmentType.Visible = true;
            this.gcAttachmentType.VisibleIndex = 0;
            this.gcAttachmentType.Width = 50;
            // 
            // cmbSpawnItemType
            // 
            this.cmbSpawnItemType.Location = new System.Drawing.Point(6, 24);
            this.cmbSpawnItemType.Name = "cmbSpawnItemType";
            this.cmbSpawnItemType.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.cmbSpawnItemType.Properties.AutoHeight = false;
            this.cmbSpawnItemType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
            this.cmbSpawnItemType.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.cmbSpawnItemType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("test", null, 0)});
            this.cmbSpawnItemType.Properties.SmallImages = this.ilSpawnItemTypesLarge;
            this.cmbSpawnItemType.Size = new System.Drawing.Size(32, 56);
            this.cmbSpawnItemType.TabIndex = 0;
            this.cmbSpawnItemType.EditValueChanged += new System.EventHandler(this.cmbSpawnItemType_EditValueChanged);
            // 
            // ilSpawnItemTypesLarge
            // 
            this.ilSpawnItemTypesLarge.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilSpawnItemTypesLarge.ImageStream")));
            this.ilSpawnItemTypesLarge.TransparentColor = System.Drawing.Color.Transparent;
            this.ilSpawnItemTypesLarge.Images.SetKeyName(0, "Question.png");
            // 
            // lblSpawnItemDelay
            // 
            this.lblSpawnItemDelay.AutoSize = true;
            this.lblSpawnItemDelay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSpawnItemDelay.Location = new System.Drawing.Point(146, 6);
            this.lblSpawnItemDelay.Name = "lblSpawnItemDelay";
            this.lblSpawnItemDelay.Size = new System.Drawing.Size(39, 13);
            this.lblSpawnItemDelay.TabIndex = 27;
            this.lblSpawnItemDelay.Text = "Delay";
            this.lblSpawnItemDelay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSpawnItemAbsoluteTime
            // 
            this.lblSpawnItemAbsoluteTime.AutoSize = true;
            this.lblSpawnItemAbsoluteTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSpawnItemAbsoluteTime.Location = new System.Drawing.Point(41, 6);
            this.lblSpawnItemAbsoluteTime.Name = "lblSpawnItemAbsoluteTime";
            this.lblSpawnItemAbsoluteTime.Size = new System.Drawing.Size(34, 13);
            this.lblSpawnItemAbsoluteTime.TabIndex = 27;
            this.lblSpawnItemAbsoluteTime.Text = "Time";
            this.lblSpawnItemAbsoluteTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSpawnItemType
            // 
            this.lblSpawnItemType.AutoSize = true;
            this.lblSpawnItemType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSpawnItemType.Location = new System.Drawing.Point(3, 6);
            this.lblSpawnItemType.Name = "lblSpawnItemType";
            this.lblSpawnItemType.Size = new System.Drawing.Size(35, 13);
            this.lblSpawnItemType.TabIndex = 23;
            this.lblSpawnItemType.Text = "Type";
            this.lblSpawnItemType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtTime);
            this.panel1.Controls.Add(this.lblTime);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtMinScore);
            this.panel1.Controls.Add(this.txtMinScore10);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(228, 79);
            this.panel1.TabIndex = 5;
            // 
            // txtTime
            // 
            this.txtTime.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTime.Location = new System.Drawing.Point(10, 23);
            this.txtTime.Name = "txtTime";
            this.txtTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtTime.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.txtTime.Properties.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.txtTime.Properties.MaxValue = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.txtTime.Size = new System.Drawing.Size(68, 20);
            this.txtTime.TabIndex = 0;
            this.txtTime.EditValueChanged += new System.EventHandler(this.txtTime_EditValueChanged);
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTime.Location = new System.Drawing.Point(7, 3);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(34, 13);
            this.lblTime.TabIndex = 2;
            this.lblTime.Text = "Time";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(166, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 75;
            this.label1.Text = "Scores";
            // 
            // txtMinScore
            // 
            this.txtMinScore.BackColor = System.Drawing.Color.LightGray;
            this.txtMinScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtMinScore.ForeColor = System.Drawing.Color.MediumBlue;
            this.txtMinScore.Location = new System.Drawing.Point(160, 23);
            this.txtMinScore.Name = "txtMinScore";
            this.txtMinScore.ReadOnly = true;
            this.txtMinScore.Size = new System.Drawing.Size(59, 20);
            this.txtMinScore.TabIndex = 1;
            this.txtMinScore.Text = "0";
            this.txtMinScore.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtMinScore10
            // 
            this.txtMinScore10.BackColor = System.Drawing.Color.LightGray;
            this.txtMinScore10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtMinScore10.ForeColor = System.Drawing.Color.MediumBlue;
            this.txtMinScore10.Location = new System.Drawing.Point(160, 49);
            this.txtMinScore10.Name = "txtMinScore10";
            this.txtMinScore10.ReadOnly = true;
            this.txtMinScore10.Size = new System.Drawing.Size(59, 20);
            this.txtMinScore10.TabIndex = 1;
            this.txtMinScore10.Text = "0";
            this.txtMinScore10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbcEditors
            // 
            this.tbcEditors.Controls.Add(this.tbpPeoplesEditor);
            this.tbcEditors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcEditors.Location = new System.Drawing.Point(232, 0);
            this.tbcEditors.Name = "tbcEditors";
            this.tbcEditors.SelectedIndex = 0;
            this.tbcEditors.Size = new System.Drawing.Size(1046, 670);
            this.tbcEditors.TabIndex = 1;
            // 
            // tbpPeoplesEditor
            // 
            this.tbpPeoplesEditor.Controls.Add(this.tabBottom);
            this.tbpPeoplesEditor.Controls.Add(this.pnlLines);
            this.tbpPeoplesEditor.Controls.Add(this.pnlTimer);
            this.tbpPeoplesEditor.Controls.Add(this.pnlLineNumbers);
            this.tbpPeoplesEditor.Controls.Add(this.hScrollBar);
            this.tbpPeoplesEditor.Location = new System.Drawing.Point(4, 22);
            this.tbpPeoplesEditor.Name = "tbpPeoplesEditor";
            this.tbpPeoplesEditor.Size = new System.Drawing.Size(1038, 644);
            this.tbpPeoplesEditor.TabIndex = 1;
            this.tbpPeoplesEditor.Text = "Edit Peoples Line";
            // 
            // pnlLines
            // 
            this.pnlLines.AutoSize = true;
            this.pnlLines.Controls.Add(this.pnlLine5);
            this.pnlLines.Controls.Add(this.pnlLine4);
            this.pnlLines.Controls.Add(this.pnlLine3);
            this.pnlLines.Controls.Add(this.pnlLine2);
            this.pnlLines.Controls.Add(this.pnlLine1);
            this.pnlLines.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLines.Location = new System.Drawing.Point(64, 48);
            this.pnlLines.Name = "pnlLines";
            this.pnlLines.Size = new System.Drawing.Size(974, 400);
            this.pnlLines.TabIndex = 5;
            // 
            // tabBottom
            // 
            this.tabBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabBottom.Location = new System.Drawing.Point(64, 448);
            this.tabBottom.Name = "tabBottom";
            this.tabBottom.SelectedTabPage = this.tbpTasks;
            this.tabBottom.Size = new System.Drawing.Size(974, 196);
            this.tabBottom.TabIndex = 5;
            this.tabBottom.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tbpTasks,
            this.tbpLog});
            // 
            // tbpTasks
            // 
            this.tbpTasks.Controls.Add(this.gdTasks);
            this.tbpTasks.Name = "tbpTasks";
            this.tbpTasks.Size = new System.Drawing.Size(968, 168);
            this.tbpTasks.Text = "Tasks";
            // 
            // gdTasks
            // 
            this.gdTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gdTasks.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gdTasks.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gdTasks.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gdTasks.EmbeddedNavigator.Buttons.First.Visible = false;
            this.gdTasks.EmbeddedNavigator.Buttons.Last.Visible = false;
            this.gdTasks.EmbeddedNavigator.Buttons.Next.Visible = false;
            this.gdTasks.EmbeddedNavigator.Buttons.NextPage.Visible = false;
            this.gdTasks.EmbeddedNavigator.Buttons.Prev.Visible = false;
            this.gdTasks.EmbeddedNavigator.Buttons.PrevPage.Visible = false;
            this.gdTasks.EmbeddedNavigator.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.gdTasks.EmbeddedNavigator.TextLocation = DevExpress.XtraEditors.NavigatorButtonsTextLocation.None;
            this.gdTasks.Location = new System.Drawing.Point(0, 0);
            this.gdTasks.MainView = this.gvTasks;
            this.gdTasks.Name = "gdTasks";
            this.gdTasks.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repCmbTaskCategory,
            this.repCmbTaskType});
            this.gdTasks.Size = new System.Drawing.Size(968, 168);
            this.gdTasks.TabIndex = 5;
            this.gdTasks.UseEmbeddedNavigator = true;
            this.gdTasks.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTasks});
            // 
            // gvTasks
            // 
            this.gvTasks.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black;
            this.gvTasks.Appearance.GroupRow.Options.UseForeColor = true;
            this.gvTasks.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcTaskCategory,
            this.gcTaskType,
            this.gcTaskScore,
            this.gcTaskParam1,
            this.gcTaskParam2,
            this.gcTaskParam3});
            this.gvTasks.GridControl = this.gdTasks;
            this.gvTasks.GroupCount = 1;
            this.gvTasks.GroupFormat = "[#image]{1} {2}";
            this.gvTasks.Name = "gvTasks";
            this.gvTasks.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gvTasks.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.gvTasks.OptionsBehavior.AutoExpandAllGroups = true;
            this.gvTasks.OptionsBehavior.AutoPopulateColumns = false;
            this.gvTasks.OptionsCustomization.AllowColumnMoving = false;
            this.gvTasks.OptionsCustomization.AllowGroup = false;
            this.gvTasks.OptionsCustomization.AllowQuickHideColumns = false;
            this.gvTasks.OptionsNavigation.AutoFocusNewRow = true;
            this.gvTasks.OptionsView.ColumnAutoWidth = false;
            this.gvTasks.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gvTasks.OptionsView.ShowGroupedColumns = true;
            this.gvTasks.OptionsView.ShowGroupPanel = false;
            this.gvTasks.OptionsView.ShowIndicator = false;
            this.gvTasks.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gcTaskCategory, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvTasks.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(this.gvTasks_CustomUnboundColumnData);
            // 
            // gcTaskCategory
            // 
            this.gcTaskCategory.Caption = "Category";
            this.gcTaskCategory.ColumnEdit = this.repCmbTaskCategory;
            this.gcTaskCategory.FieldName = "Category";
            this.gcTaskCategory.Name = "gcTaskCategory";
            this.gcTaskCategory.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gcTaskCategory.OptionsColumn.AllowMove = false;
            this.gcTaskCategory.OptionsColumn.AllowShowHide = false;
            this.gcTaskCategory.OptionsColumn.ShowInCustomizationForm = false;
            this.gcTaskCategory.Visible = true;
            this.gcTaskCategory.VisibleIndex = 0;
            this.gcTaskCategory.Width = 138;
            // 
            // repCmbTaskCategory
            // 
            this.repCmbTaskCategory.AutoHeight = false;
            this.repCmbTaskCategory.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repCmbTaskCategory.Name = "repCmbTaskCategory";
            // 
            // gcTaskType
            // 
            this.gcTaskType.AppearanceCell.Options.UseTextOptions = true;
            this.gcTaskType.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gcTaskType.Caption = "Type";
            this.gcTaskType.ColumnEdit = this.repCmbTaskType;
            this.gcTaskType.FieldName = "Type";
            this.gcTaskType.Name = "gcTaskType";
            this.gcTaskType.Visible = true;
            this.gcTaskType.VisibleIndex = 1;
            this.gcTaskType.Width = 162;
            // 
            // repCmbTaskType
            // 
            this.repCmbTaskType.AutoHeight = false;
            this.repCmbTaskType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repCmbTaskType.Name = "repCmbTaskType";
            // 
            // gcTaskScore
            // 
            this.gcTaskScore.Caption = "Score";
            this.gcTaskScore.FieldName = "Score";
            this.gcTaskScore.Name = "gcTaskScore";
            this.gcTaskScore.Visible = true;
            this.gcTaskScore.VisibleIndex = 2;
            this.gcTaskScore.Width = 151;
            // 
            // gcTaskParam1
            // 
            this.gcTaskParam1.Caption = "Param 1";
            this.gcTaskParam1.FieldName = "gcTaskParam1";
            this.gcTaskParam1.Name = "gcTaskParam1";
            this.gcTaskParam1.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.gcTaskParam1.Visible = true;
            this.gcTaskParam1.VisibleIndex = 3;
            this.gcTaskParam1.Width = 94;
            // 
            // gcTaskParam2
            // 
            this.gcTaskParam2.Caption = "Param 2";
            this.gcTaskParam2.FieldName = "gcTaskParam2";
            this.gcTaskParam2.Name = "gcTaskParam2";
            this.gcTaskParam2.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.gcTaskParam2.Visible = true;
            this.gcTaskParam2.VisibleIndex = 4;
            this.gcTaskParam2.Width = 100;
            // 
            // gcTaskParam3
            // 
            this.gcTaskParam3.Caption = "Param 3";
            this.gcTaskParam3.FieldName = "gcTaskParam3";
            this.gcTaskParam3.Name = "gcTaskParam3";
            this.gcTaskParam3.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.gcTaskParam3.Visible = true;
            this.gcTaskParam3.VisibleIndex = 5;
            this.gcTaskParam3.Width = 83;
            // 
            // tbpLog
            // 
            this.tbpLog.Controls.Add(this.txtLog);
            this.tbpLog.Name = "tbpLog";
            this.tbpLog.Size = new System.Drawing.Size(968, 168);
            this.tbpLog.Text = "Log";
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Location = new System.Drawing.Point(0, 0);
            this.txtLog.Name = "txtLog";
            this.txtLog.Properties.HideSelection = false;
            this.txtLog.Properties.ReadOnly = true;
            this.txtLog.Size = new System.Drawing.Size(968, 168);
            this.txtLog.TabIndex = 0;
            // 
            // pnlLine5
            // 
            this.pnlLine5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlLine5.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLine5.Location = new System.Drawing.Point(0, 320);
            this.pnlLine5.Name = "pnlLine5";
            this.pnlLine5.Size = new System.Drawing.Size(974, 80);
            this.pnlLine5.TabIndex = 4;
            // 
            // pnlLine4
            // 
            this.pnlLine4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlLine4.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLine4.Location = new System.Drawing.Point(0, 240);
            this.pnlLine4.Name = "pnlLine4";
            this.pnlLine4.Size = new System.Drawing.Size(974, 80);
            this.pnlLine4.TabIndex = 3;
            // 
            // pnlLine3
            // 
            this.pnlLine3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlLine3.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLine3.Location = new System.Drawing.Point(0, 160);
            this.pnlLine3.Name = "pnlLine3";
            this.pnlLine3.Size = new System.Drawing.Size(974, 80);
            this.pnlLine3.TabIndex = 2;
            // 
            // pnlLine2
            // 
            this.pnlLine2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlLine2.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLine2.Location = new System.Drawing.Point(0, 80);
            this.pnlLine2.Name = "pnlLine2";
            this.pnlLine2.Size = new System.Drawing.Size(974, 80);
            this.pnlLine2.TabIndex = 1;
            // 
            // pnlLine1
            // 
            this.pnlLine1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlLine1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLine1.Location = new System.Drawing.Point(0, 0);
            this.pnlLine1.Name = "pnlLine1";
            this.pnlLine1.Size = new System.Drawing.Size(974, 80);
            this.pnlLine1.TabIndex = 0;
            // 
            // pnlTimer
            // 
            this.pnlTimer.BackColor = System.Drawing.Color.LightGray;
            this.pnlTimer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlTimer.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTimer.Location = new System.Drawing.Point(64, 16);
            this.pnlTimer.Name = "pnlTimer";
            this.pnlTimer.Size = new System.Drawing.Size(974, 32);
            this.pnlTimer.TabIndex = 4;
            this.pnlTimer.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlTimer_Paint);
            // 
            // pnlLineNumbers
            // 
            this.pnlLineNumbers.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlLineNumbers.Controls.Add(this.label6);
            this.pnlLineNumbers.Controls.Add(this.label5);
            this.pnlLineNumbers.Controls.Add(this.label4);
            this.pnlLineNumbers.Controls.Add(this.label3);
            this.pnlLineNumbers.Controls.Add(this.label2);
            this.pnlLineNumbers.Controls.Add(this.lbTimer);
            this.pnlLineNumbers.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLineNumbers.Location = new System.Drawing.Point(0, 16);
            this.pnlLineNumbers.Name = "pnlLineNumbers";
            this.pnlLineNumbers.Size = new System.Drawing.Size(64, 628);
            this.pnlLineNumbers.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(-2, 379);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 16);
            this.label6.TabIndex = 3;
            this.label6.Text = "5";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(-2, 299);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 16);
            this.label5.TabIndex = 3;
            this.label5.Text = "4";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(-2, 219);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "3";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(-2, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "2";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(-2, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "1";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbTimer
            // 
            this.lbTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbTimer.Location = new System.Drawing.Point(-2, 8);
            this.lbTimer.Name = "lbTimer";
            this.lbTimer.Size = new System.Drawing.Size(50, 16);
            this.lbTimer.TabIndex = 3;
            this.lbTimer.Text = "Time";
            this.lbTimer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // hScrollBar
            // 
            this.hScrollBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.hScrollBar.LargeChange = 1;
            this.hScrollBar.Location = new System.Drawing.Point(0, 0);
            this.hScrollBar.Maximum = 0;
            this.hScrollBar.Name = "hScrollBar";
            this.hScrollBar.Size = new System.Drawing.Size(1038, 16);
            this.hScrollBar.TabIndex = 1;
            this.hScrollBar.ValueChanged += new System.EventHandler(this.hScrollBar_ValueChanged);
            // 
            // splMain
            // 
            this.splMain.Location = new System.Drawing.Point(232, 0);
            this.splMain.Name = "splMain";
            this.splMain.Size = new System.Drawing.Size(5, 670);
            this.splMain.TabIndex = 2;
            this.splMain.TabStop = false;
            // 
            // ilAttachmentNames
            // 
            this.ilAttachmentNames.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilAttachmentNames.ImageStream")));
            this.ilAttachmentNames.TransparentColor = System.Drawing.Color.Transparent;
            this.ilAttachmentNames.Images.SetKeyName(0, "Question.png");
            // 
            // ilSpawnItemTypesSmall
            // 
            this.ilSpawnItemTypesSmall.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.ilSpawnItemTypesSmall.ImageSize = new System.Drawing.Size(16, 16);
            this.ilSpawnItemTypesSmall.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // PackForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(1000, 600);
            this.ClientSize = new System.Drawing.Size(1278, 670);
            this.Controls.Add(this.splMain);
            this.Controls.Add(this.tbcEditors);
            this.Controls.Add(this.pnlParam);
            this.Name = "PackForm";
            this.Text = "S";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.pnlParam.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gdLevelItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLevelItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpSpawnItemContainer)).EndInit();
            this.grpSpawnItemContainer.ResumeLayout(false);
            this.pnlSpawnItemBinding.ResumeLayout(false);
            this.pnlSpawnItemBinding.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSpawnItemAbsoluteTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSpawnItemDelay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gdAttachmentTypes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAttachmentTypes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSpawnItemType.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTime.Properties)).EndInit();
            this.tbcEditors.ResumeLayout(false);
            this.tbpPeoplesEditor.ResumeLayout(false);
            this.tbpPeoplesEditor.PerformLayout();
            this.pnlLines.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabBottom)).EndInit();
            this.tabBottom.ResumeLayout(false);
            this.tbpTasks.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gdTasks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTasks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repCmbTaskCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repCmbTaskType)).EndInit();
            this.tbpLog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtLog.Properties)).EndInit();
            this.pnlLineNumbers.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repCmbTaskCategory;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repCmbTaskType;
        private DevExpress.XtraTab.XtraTabControl tabBottom;
        private DevExpress.XtraTab.XtraTabPage tbpTasks;
        private DevExpress.XtraTab.XtraTabPage tbpLog;
        private DevExpress.XtraEditors.MemoEdit txtLog;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.Panel pnlParam;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.TabControl tbcEditors;
        private System.Windows.Forms.TabPage tbpPeoplesEditor;
        private System.Windows.Forms.TextBox txtMinScore;
        private System.Windows.Forms.HScrollBar hScrollBar;
        private System.Windows.Forms.Panel pnlTimer;
        private System.Windows.Forms.Panel pnlLines;
        private System.Windows.Forms.Panel pnlLine1;
        private System.Windows.Forms.Panel pnlLine2;
        private System.Windows.Forms.Panel pnlLine3;
        private System.Windows.Forms.Panel pnlLine4;
        private System.Windows.Forms.Panel pnlLine5;
        private System.Windows.Forms.ImageList ilSpawnItemTypesLarge;
        private System.Windows.Forms.ContextMenu cmLinePanel;
        private System.Windows.Forms.ContextMenu cmButton;
        private System.Windows.Forms.Label lblSpawnItemType;
        private System.Windows.Forms.Label lbTimer;
        private System.Windows.Forms.Panel pnlLineNumbers;
        private Label lblSpawnItemDelay;
        private Label label1;
        private TextBox txtMinScore10;
        private Label lblSpawnItemAbsoluteTime;
        private DevExpress.XtraGrid.GridControl gdLevelItems;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLevelItems;
        private DevExpress.XtraGrid.Columns.GridColumn gcItemCategory;
        private DevExpress.XtraGrid.Columns.GridColumn gcItemType;
        private DevExpress.XtraGrid.Columns.GridColumn gcItemValue;
        private Panel pnlSpawnItemBinding;
        private Panel panel1;
        private DevExpress.XtraGrid.GridControl gdTasks;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTasks;
        private DevExpress.XtraGrid.Columns.GridColumn gcTaskCategory;
        private DevExpress.XtraGrid.Columns.GridColumn gcTaskType;
        private DevExpress.XtraGrid.Columns.GridColumn gcTaskScore;
        private DevExpress.XtraGrid.Columns.GridColumn gcTaskParam1;
        private DevExpress.XtraGrid.Columns.GridColumn gcTaskParam2;
        private DevExpress.XtraGrid.Columns.GridColumn gcTaskParam3;
        private DevExpress.XtraEditors.SplitterControl splMain;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbSpawnItemType;
        private DevExpress.XtraGrid.GridControl gdAttachmentTypes;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAttachmentTypes;
        private DevExpress.XtraGrid.Columns.GridColumn gcAttachmentType;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private DevExpress.XtraEditors.GroupControl grpSpawnItemContainer;
        private DevExpress.XtraEditors.SpinEdit txtTime;
        private DevExpress.XtraEditors.SpinEdit txtSpawnItemDelay;
        private DevExpress.XtraEditors.SpinEdit txtSpawnItemAbsoluteTime;
        private DevExpress.XtraEditors.SplitterControl splLevelItems;
        private ImageList ilAttachmentNames;
        private ImageList ilSpawnItemTypesSmall;
    }
}
