using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelEditor.Forms
{
    public partial class WizardForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblCount = new System.Windows.Forms.Label();
            this.lblInterval = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.gdItems = new DevExpress.XtraGrid.GridControl();
            this.gvItems = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcProbability = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repTxtProbability = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gcItemType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repCmbItemType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.txtCount = new DevExpress.XtraEditors.SpinEdit();
            this.txtInterval = new DevExpress.XtraEditors.SpinEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gdItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtProbability)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repCmbItemType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInterval.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(669, 398);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 43;
            this.btnCancel.Text = "Cancel";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(589, 398);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 42;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblCount
            // 
            this.lblCount.Location = new System.Drawing.Point(16, 8);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(43, 20);
            this.lblCount.TabIndex = 65;
            this.lblCount.Text = "Count";
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblInterval
            // 
            this.lblInterval.Location = new System.Drawing.Point(171, 8);
            this.lblInterval.Name = "lblInterval";
            this.lblInterval.Size = new System.Drawing.Size(44, 20);
            this.lblInterval.TabIndex = 67;
            this.lblInterval.Text = "Interval";
            this.lblInterval.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label23
            // 
            this.label23.Location = new System.Drawing.Point(284, 8);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(56, 20);
            this.label23.TabIndex = 69;
            this.label23.Text = "seconds";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(8, 382);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(733, 2);
            this.label2.TabIndex = 71;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(9, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(733, 2);
            this.label3.TabIndex = 72;
            // 
            // gdItems
            // 
            this.gdItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gdItems.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gdItems.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gdItems.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gdItems.EmbeddedNavigator.Buttons.First.Visible = false;
            this.gdItems.EmbeddedNavigator.Buttons.Last.Visible = false;
            this.gdItems.EmbeddedNavigator.Buttons.Next.Visible = false;
            this.gdItems.EmbeddedNavigator.Buttons.NextPage.Visible = false;
            this.gdItems.EmbeddedNavigator.Buttons.Prev.Visible = false;
            this.gdItems.EmbeddedNavigator.Buttons.PrevPage.Visible = false;
            this.gdItems.EmbeddedNavigator.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.gdItems.EmbeddedNavigator.TextLocation = DevExpress.XtraEditors.NavigatorButtonsTextLocation.None;
            this.gdItems.Location = new System.Drawing.Point(9, 45);
            this.gdItems.MainView = this.gvItems;
            this.gdItems.Name = "gdItems";
            this.gdItems.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repTxtProbability,
            this.repCmbItemType});
            this.gdItems.Size = new System.Drawing.Size(730, 334);
            this.gdItems.TabIndex = 73;
            this.gdItems.UseEmbeddedNavigator = true;
            this.gdItems.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvItems});
            // 
            // gvItems
            // 
            this.gvItems.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black;
            this.gvItems.Appearance.GroupRow.Options.UseForeColor = true;
            this.gvItems.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcProbability,
            this.gcItemType});
            this.gvItems.GridControl = this.gdItems;
            this.gvItems.GroupFormat = "[#image]{1} {2}";
            this.gvItems.Name = "gvItems";
            this.gvItems.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gvItems.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.gvItems.OptionsBehavior.AutoExpandAllGroups = true;
            this.gvItems.OptionsBehavior.AutoPopulateColumns = false;
            this.gvItems.OptionsCustomization.AllowColumnMoving = false;
            this.gvItems.OptionsCustomization.AllowGroup = false;
            this.gvItems.OptionsCustomization.AllowQuickHideColumns = false;
            this.gvItems.OptionsDetail.EnableMasterViewMode = false;
            this.gvItems.OptionsNavigation.AutoFocusNewRow = true;
            this.gvItems.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gvItems.OptionsView.ShowGroupedColumns = true;
            this.gvItems.OptionsView.ShowGroupPanel = false;
            this.gvItems.OptionsView.ShowIndicator = false;
            this.gvItems.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gvItems_CustomRowCellEdit);
            this.gvItems.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(this.gvItems_CustomUnboundColumnData);
            // 
            // gcProbability
            // 
            this.gcProbability.Caption = "Probability";
            this.gcProbability.ColumnEdit = this.repTxtProbability;
            this.gcProbability.FieldName = "Probability";
            this.gcProbability.Name = "gcProbability";
            this.gcProbability.OptionsColumn.FixedWidth = true;
            this.gcProbability.Visible = true;
            this.gcProbability.VisibleIndex = 0;
            this.gcProbability.Width = 65;
            // 
            // repTxtProbability
            // 
            this.repTxtProbability.AutoHeight = false;
            this.repTxtProbability.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.repTxtProbability.IsFloatValue = false;
            this.repTxtProbability.Mask.EditMask = "N00";
            this.repTxtProbability.MaxValue = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.repTxtProbability.Name = "repTxtProbability";
            // 
            // gcItemType
            // 
            this.gcItemType.AppearanceCell.Options.UseTextOptions = true;
            this.gcItemType.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gcItemType.Caption = "Type";
            this.gcItemType.ColumnEdit = this.repCmbItemType;
            this.gcItemType.FieldName = "Type";
            this.gcItemType.Name = "gcItemType";
            this.gcItemType.OptionsColumn.FixedWidth = true;
            this.gcItemType.Visible = true;
            this.gcItemType.VisibleIndex = 1;
            this.gcItemType.Width = 50;
            // 
            // repCmbItemType
            // 
            this.repCmbItemType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repCmbItemType.Name = "repCmbItemType";
            // 
            // txtCount
            // 
            this.txtCount.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtCount.Location = new System.Drawing.Point(65, 8);
            this.txtCount.Name = "txtCount";
            this.txtCount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtCount.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.txtCount.Properties.IsFloatValue = false;
            this.txtCount.Properties.Mask.EditMask = "N00";
            this.txtCount.Properties.MaxValue = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.txtCount.Size = new System.Drawing.Size(68, 20);
            this.txtCount.TabIndex = 74;
            // 
            // txtInterval
            // 
            this.txtInterval.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtInterval.Location = new System.Drawing.Point(218, 8);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtInterval.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.txtInterval.Properties.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.txtInterval.Properties.MaxValue = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.txtInterval.Size = new System.Drawing.Size(60, 20);
            this.txtInterval.TabIndex = 75;
            // 
            // WizardForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(751, 437);
            this.Controls.Add(this.txtInterval);
            this.Controls.Add(this.txtCount);
            this.Controls.Add(this.gdItems);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.lblInterval);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WizardForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Insert spawn items";
            ((System.ComponentModel.ISupportInitialize)(this.gdItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repTxtProbability)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repCmbItemType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInterval.Properties)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblInterval;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraGrid.GridControl gdItems;
        private DevExpress.XtraGrid.Views.Grid.GridView gvItems;
        private DevExpress.XtraGrid.Columns.GridColumn gcProbability;
        private DevExpress.XtraGrid.Columns.GridColumn gcItemType;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repTxtProbability;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repCmbItemType;
        private DevExpress.XtraEditors.SpinEdit txtCount;
        private DevExpress.XtraEditors.SpinEdit txtInterval;
    }
}
