using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using LevelData;
using LevelData.CategorizedItems;
using LevelData.Exceptions;
using LevelData.Log;
using LevelData.SpawnItems;
using LevelData.Utility.Extensions;
using LevelEditor.Controls;
using LevelEditor.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace LevelEditor.Forms
{
	public partial class PackForm : Form, ILog
	{
        #region Consts


        /// <summary>
        /// Максимальное количество <see cref="SpawnItemAttachment"/> одного типа, которое можно добавить одному <see cref="SpawnItem"/>.
        /// Влияет на количество колонок каждого типа в гриде <see cref="gdAttachmentTypes"/>.
        /// </summary>
        public const int MaxAttachmentsForType = 3;

        public const string AttachmentColNamePrefix = "gcAttachmentName";
        private const int WM_SETREDRAW = 11;

        private const int MissingImageIndex = 0;
        private const int NoneImageIndex = -1;
        private const int PixelInSec = 10;
        private const string TaskParamColNameFormat = "gcTaskParam{0}";        

        #endregion Consts
    
        #region Fields

        public readonly Dictionary<string, RepositoryItemImageComboBox> AttachmentEditorsByType = new Dictionary<string, RepositoryItemImageComboBox>();

        private readonly Dictionary<GridColumn, int> _taskParamMap = new Dictionary<GridColumn, int>();        

        private Pack _pack;
        private SpawnItemButton _curItemButton;
        private SpawnItemButton _insertAfterBtnItem;
        private int _levelIndex = 0;
        private Level _copiedLevel = null;
        private Level _loadedLevel = null;
        private List<Panel> _linePanels = new List<Panel>();
        private int _unstableCounter = 0;

        #endregion Fields

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);

        public int LevelIndex
        {
            get
            {
                return _levelIndex;
            }

            set
            {
                if (_levelIndex == value)
                    return;
                
                if (value >= LevelCount)
                    throw new CommonException("Level index {0} is greater than level count {1}", value, LevelCount);

                _levelIndex = value;
                LoadLevel();
            }
        }

        public bool IsNeedSave
        {
            get;
            private set;
        }

        public bool IsNew
        {
            get;
            private set;
        }

        public int LevelCount
        {
            get
            {
                return _pack.Levels.Count;
            }
        }

        public bool IsUnstableState
        {
            get { return _unstableCounter > 0; }
        }

        private Level CurrentLevel
        {
            get { return _pack.Levels[LevelIndex]; }
        }

		public PackForm(string levelTemplatePath, string packPath = null)
		{
			InitializeComponent();

            cmLinePanel.MenuItems.Add(new MenuItem("Add", cmLinePanel_ClickAdd));
            cmLinePanel.MenuItems.Add(new MenuItem("Add wizard...", cmLinePanel_ClickAddWizard));
            cmLinePanel.MenuItems.Add(new MenuItem("Delete all", cmLinePanel_ClickDeleteAll));

            cmButton.MenuItems.Add(new MenuItem("Insert", cmButton_ClickInsert));
            cmButton.MenuItems.Add(new MenuItem("Insert wizard...", cmButton_ClickInsertWizard));
            cmButton.MenuItems.Add(new MenuItem("Delete", cmButton_ClickDelete));
            cmButton.MenuItems.Add(new MenuItem("Delete all", cmButton_ClickDeleteAll));
            

            decimal zoomOut = (decimal)23 / Math.Max(ilSpawnItemTypesLarge.ImageSize.Width, ilSpawnItemTypesLarge.ImageSize.Height);
            ilSpawnItemTypesSmall.ImageSize = new Size((int)(ilSpawnItemTypesLarge.ImageSize.Width * zoomOut), (int)(ilSpawnItemTypesLarge.ImageSize.Height * zoomOut));
            ilSpawnItemTypesSmall.Images.Clear();
            foreach (string key in ilSpawnItemTypesLarge.Images.Keys)
            {
                ilSpawnItemTypesSmall.Images.Add(key, ilSpawnItemTypesLarge.Images[key]);
            }
            
            BeginBinding();
            try
            {
                for (int i = 0; i < MaxAttachmentsForType; i++)
                {
                    var gc = gvAttachmentTypes.Columns.Add();

                    gc.Name = AttachmentColNamePrefix + i.ToString();
                    gc.FieldName = gc.Name;
                    gc.UnboundType = UnboundColumnType.String;
                    gc.Visible = true;
                    gc.Width = 30;
                }

                txtTime.Properties.Increment = (decimal)SpawnLine.DefaultDelay;
                txtSpawnItemDelay.Properties.Increment = (decimal)SpawnLine.DefaultDelay;

                for (int i = 0; ; i++)
                {
                    string panelName = String.Format("pnlLine{0}", i + 1);
                    if (!pnlLines.Controls.ContainsKey(panelName))
                        break;

                    var pnlLine = (Panel)pnlLines.Controls[panelName];
                    _linePanels.Add(pnlLine);

                    pnlLine.AllowDrop = true;

                    pnlLine.ContextMenu = cmLinePanel;
                    pnlLine.MouseDown += pnlLine_MouseDown;
                    pnlLine.Paint += pnlLine_Paint;
                    pnlLine.DragEnter += pnlLine_DragEnter;
                    pnlLine.DragDrop += pnlLine_DragDrop;
                }

                InitTaskParamMap();

                BindSpawnItem(null);
                Text = packPath;

                string packXml = packPath.IsNullOrWhiteSpace() ? null : File.ReadAllText(packPath);
                _pack = new Pack(File.ReadAllText(levelTemplatePath), packXml, this);

                LookupUtility.Configure(repCmbTaskCategory, Pack.TaskConfig.Categories);
                LookupUtility.Configure(repCmbTaskType, Pack.TaskConfig.Types);

                GridUtility.ConfigureInstantCommit(gvTasks, repCmbTaskCategory);
                GridUtility.ConfigureInstantCommit(gvTasks, repCmbTaskType);

                ConfigureSpawnItems();

                gvAttachmentTypes.CellValueChanged += view_CellValueChanged;
                gvTasks.CellValueChanged += view_CellValueChanged;
                gvLevelItems.CellValueChanged += view_CellValueChanged;

                LoadLevel(true);

                // сбрасываем флаги в последнюю очередь, т.к. биндинг может привести к изменению значений
                bool isNew = packPath == null;

                IsNeedSave = isNew;
                IsNew = isNew;
            }
            finally
            {
                EndBinding();
            }
		}

		public bool Save(string path)
		{
            gvTasks.CloseEditor();
            gvLevelItems.CloseEditor();
            gvAttachmentTypes.CloseEditor();

            int selectionStart = txtLog.Text.Length - 1;
            if (!_pack.Save(path))
            {                
                tabBottom.SelectedTabPage = tbpLog;
                txtLog.Select(selectionStart, txtLog.Text.Length - selectionStart);
                txtLog.ScrollToCaret();

                DialogUtility.ShowWarning("Pack was not saved, see errors on the \"{0}\" tab.", tbpLog.Text);
   
                return false;
            }

			Text = path;			
			IsNeedSave = false;
			IsNew = false;

            return true;
		}

        #region Level Edit

        public void AddLevel()
		{
			var level = _pack.AddLevel();
            LevelIndex = _pack.Levels.IndexOf(level);

			IsNeedSave = true;
		}

		public void InsertLevel()
		{
			_pack.InsertLevel(LevelIndex);
            LoadLevel();
			IsNeedSave = true;
		}

		public void CopyAddLevel()
		{
            CopyLevel();
            AddLevel();
            PasteLevel();
		}

		public void DeleteLevel()
		{
            if (LevelCount == 1)
            {
                ClearLevel();
            }
            else
            {
                _pack.Levels.RemoveAt(LevelIndex);

                if (LevelIndex >= LevelCount)
                    LevelIndex = LevelCount - 1;

                LoadLevel();
                IsNeedSave = true;
            }
		}

		public void CopyLevel()
		{
            _copiedLevel = CurrentLevel.Clone();
		}

		public void PasteLevel()
		{
            if (_copiedLevel == null)
                return;

            CurrentLevel.CopyFrom(_copiedLevel);
            LoadLevel(true);
			IsNeedSave = true;
		}

		public void CutLevel()
		{
            CopyLevel();
            DeleteLevel();
		}

		public void MoveLevelFirst()
		{
            for (int i = LevelCount - 1; i > 0 && InterchangeLevels(i, i - 1); i--) ;

            LevelIndex = 0;
		}

		public void MoveLevelBack()
		{
            if (InterchangeLevels(LevelIndex - 1, LevelIndex))
                LevelIndex--;
		}

		public void MoveLevelForward()
		{
            if (InterchangeLevels(LevelIndex + 1, LevelIndex))
                LevelIndex++;
		}

		public void MoveLevelLast()
		{
            for (int i = 0; i < (LevelCount - 1) && InterchangeLevels(i, i + 1); i++) ;

            LevelIndex = LevelCount - 1;
		}

		public void ClearLevel()
		{
            CurrentLevel.Clear();
            LoadLevel(true);
			IsNeedSave = true;
		}

        public bool InterchangeLevels(int index1,  int index2)
        {
            if (index1 == index2)
                return false;

            if (index1 < 0 || index2 < 0 || index1 >= LevelCount || index2 >= LevelCount)
                return false;

            Level temp = _pack.Levels[index1];
            _pack.Levels[index1] = _pack.Levels[index2];
            _pack.Levels[index2] = temp;

            IsNeedSave = true;

            return true;
        }

        #endregion Level Edit

        #region Level Navigation

        public void FirstLevel()
		{
            LevelIndex = 0;
		}

		public bool PreviousLevel()
		{
            if (LevelIndex > 0)
            {
                LevelIndex--;
                return true;
            }

            return false;
		}

		public bool NextLevel()
		{
            if (LevelIndex < (LevelCount - 1))
            {
                LevelIndex++;
                return true;
            }

            return false;
		}

		public void LastLevel()
		{
            LevelIndex = LevelCount - 1;
		}

        #endregion Level Navigation

        #region ILog

        void ILog.Write(string message)
        {
            txtLog.Text += message + Environment.NewLine;

            if (tabBottom.SelectedTabPage != tbpLog)
                tabBottom.SelectedTabPage = tbpLog;
        }

        void ILog.Write(string format, params object[] args)
        {
            ((ILog)this).Write(String.Format(format, args));
        }

        #endregion ILog

        #region Initialization and binding

        private void UpdatePrognosis()
        {
            int sum_orders = 0;
            int sum10 = 0;
            int sum25 = 0;
            int sum50 = 0;
            /*
            for (int i = 0; i < Lines.Length; ++i)
            {
                if (Lines[i].Index > 0)
                {
                    for (int j = 0; j < Lines[i].Items.Count; ++j)
                    {
                        if (((Customer)((Lines[i]).Items[j])).absTime <= time)
                        {
                            sum_orders += getSumOrder(((Customer)((Lines[i]).Items[j])).order1);
                            sum_orders += getSumOrder(((Customer)((Lines[i]).Items[j])).order2);
                            sum_orders += getSumOrder(((Customer)((Lines[i]).Items[j])).order3);
                            sum_orders += getSumOrder(((Customer)((Lines[i]).Items[j])).order4);
                            sum_orders += getSumOrder(((Customer)((Lines[i]).Items[j])).order5);
                            sum_orders += getSumOrder(((Customer)((Lines[i]).Items[j])).order6);

                            sum10 += 10;
                            sum25 += 25;
                            sum50 += 50;
                        }
                    }
                }
            }
            */
            
            int minScore = sum_orders;
            int minScore10 = sum_orders + sum10;
            int minScore25 = sum_orders + sum25;
            int minScore50 = sum_orders + sum50;

            txtMinScore.Text = minScore.ToString();
            txtMinScore10.Text = minScore10.ToString();
        }

        private void LoadLevel(bool isForceReload = false)
        {
            Level level = CurrentLevel;
            if (!isForceReload && CurrentLevel == _loadedLevel)
                return;

            BeginBinding();
            try
            {
                _loadedLevel = CurrentLevel;

                if (_linePanels.Count != level.Lines.Length)
                    throw new CommonException("Level line count {0} doesn't match editor line count {1}", level.Lines.Length, _linePanels.Count);

                for (int i = 0; i < level.Lines.Length; i++)
                {
                    LoadLine(_linePanels[i], level.Lines[i]);
                }

                hScrollBar.Minimum = 0;
                hScrollBar.Value = 0;
                txtTime.Value = (decimal)CurrentLevel.Time;
                UpdateTime();

                BindSpawnItem(null);

                ConfigureListGrid(gvLevelItems, level.Items);
                ConfigureListGrid(gvTasks, level.Tasks);

                UpdatePrognosis();
            }
            finally
            {
                EndBinding();
            }
        }

        private void InitTaskParamMap()
        {
            for (int i = 0; i < Task.ParamsCount; i++)
            {
                string colName = String.Format(TaskParamColNameFormat, i + 1);

                var gc = gvTasks.Columns.ColumnByName(colName);
                if (gc == null)
                    throw new CommonException("{0} missing column with name {1}", gvTasks.Name, colName);

                _taskParamMap.Add(gc, i);
            }
        }

        private void ConfigureSpawnItems()
        {
            PopulateSpawnItemCombos();
            gdAttachmentTypes.DataSource = Pack.SpawnItemConfig.AttachmentTypes.Keys;
        }

        public void PopulateSpawnItemTypes(RepositoryItemImageComboBox cmb, bool isAddEmpty, bool isSmallImages)
        {
            var spawnItemTypes = cmb.Items;
            spawnItemTypes.Clear();

            ImageList ilImages = isSmallImages ? ilSpawnItemTypesSmall : ilSpawnItemTypesLarge;
            cmb.SmallImages = ilImages;
            cmb.Buttons[0].Visible = false;
            cmb.GlyphAlignment = HorzAlignment.Center;
            try
            {
                if (isAddEmpty)
                    spawnItemTypes.Add(null);

                foreach (KeyValuePair<string, string> itemType in Pack.SpawnItemConfig.Types)
                {
                    string imgFileName = itemType.Value;
                    string type = itemType.Key;
                    int imgIndex = GetImageIndex(ilImages, imgFileName, true);

                    spawnItemTypes.Add(new ImageComboBoxItem(type, type, imgIndex));
                }
            }
            catch (Exception ex)
            {
                ((ILog)this).Write("Error while populating spawn item types combo: {0}", ex.Message);
            }
        }

        private void PopulateAttachmentNames(RepositoryItemImageComboBox cmb, string singleType = null)
        {
            cmb.SmallImages = ilAttachmentNames;
            cmb.GlyphAlignment = HorzAlignment.Center;
            cmb.Buttons[0].Visible = false;
            GridUtility.ConfigureInstantCommit(gvAttachmentTypes, cmb);

            var attachmentTypes = cmb.Items;

            // добавляем пустой элемент, чтобы можно было сбросить значение
            attachmentTypes.Add(new ImageComboBoxItem(null));
            IEnumerable<string> types;
            if (singleType == null)
                types = Pack.SpawnItemConfig.AttachmentTypes.Keys;
            else
                types = new string[] { singleType };

            foreach (string type in types)
            {
                // добавляем пустой элемент типа, если это комбик со множеством типов
                if (singleType == null)
                    attachmentTypes.Add(new ImageComboBoxItem(type, null));

                foreach (KeyValuePair<string, string> attachName in Pack.SpawnItemConfig.AttachmentTypes[type])
                {
                    string imgFileName = attachName.Value;
                    string name = attachName.Key;
                    int imgIndex = GetImageIndex(ilAttachmentNames, imgFileName, true);

                    attachmentTypes.Add(new ImageComboBoxItem(name, name, imgIndex));
                }
            }
        }

        private void PopulateSpawnItemCombos()
        {
            PopulateSpawnItemTypes(cmbSpawnItemType.Properties, false, false);

            try
            {
                foreach (KeyValuePair<string, Dictionary<string, string>> attachType in Pack.SpawnItemConfig.AttachmentTypes)
                {
                    string type = attachType.Key;                    
                    var cmb = new RepositoryItemImageComboBox();

                    PopulateAttachmentNames(cmb, type);
                    AttachmentEditorsByType.Add(type, cmb);
                }
            }
            catch (Exception ex)
            {
                ((ILog)this).Write("Error while populating spawn attachment names combos: {0}", ex.Message);
            }
        }
        
        private int GetImageIndex(ImageList list, string fileName, bool canAdd)
        {
            // если путь к картинке идёт не от Root директории, добавляем в качестве Root папку приложения
            if (!Path.IsPathRooted(fileName))
                fileName = Path.Combine(Application.StartupPath, fileName);

            int index = list.Images.IndexOfKey(fileName);
            if (index == -1)
            {
                index = MissingImageIndex;
                if (canAdd)
                {
                    if (!File.Exists(fileName))
                    {
                        ((ILog)this).Write("File \"{0}\" not found", fileName);
                    }
                    else
                    {
                        list.Images.Add(fileName, Image.FromFile(fileName));
                        index = list.Images.IndexOfKey(fileName);
                    }
                }
            }

            return index;
        }

        private void UpdateTime()
        {
            hScrollBar.Maximum = (int)CurrentLevel.Time + (CurrentLevel.Time % 1 > 0 ? 1 : 0);
            pnlTimer.Refresh();
        }

        private void LoadLine(Panel pnlLine, SpawnLine line)
        {
            pnlLine.SuspendLayout();
            SuspendDrawing(pnlLine);
            BeginBinding();
            try
            {
                pnlLine.Controls.Clear();
                pnlLine.Tag = line;

                foreach (var item in line.Items)
                {
                    AddSpawnItemButton(pnlLine, item);
                }
            }
            finally
            {
                pnlLine.ResumeLayout();
                ResumeDrawing(pnlLine);
                EndBinding();
            }
        }

        private void BindAbsoluteTime()
        {
            if (_curItemButton == null)
                return;

            var item = _curItemButton.Item;
            var line = GetCurrentLine();

            txtSpawnItemAbsoluteTime.Value = (decimal)line.GetAbsoluteTime(item);
        }

        private void ConfigureListGrid<T>(GridView view, IList<T> dataSource)
        {
            var ds = new BindingList<T>(dataSource);
            ds.ListChanged += gridDataSource_ListChanged;

            view.GridControl.DataSource = ds;
        }

        private void BindSpawnItem(SpawnItemButton btnItem)
		{
            if (_curItemButton != null)
                _curItemButton.IsSelected = false;

            _curItemButton = btnItem;
            
            if (_curItemButton == null)
			{
                pnlSpawnItemBinding.Visible = false;
            }
			else
			{
                var item = _curItemButton.Item;
                var line = GetCurrentLine();
                BeginBinding();
                try
                {
                    _curItemButton.IsSelected = true;

                    pnlSpawnItemBinding.Visible = true;
                    
                    cmbSpawnItemType.EditValue = item.Type;
                    txtSpawnItemDelay.Value = (decimal)item.Delay;

                    // для не-первого айтема не даём ставить Delay меньше 1, т.к. в этом случае он сольётся с предыдущим. Для подобных настроек есть отдельные линии.
                    txtSpawnItemDelay.Properties.MinValue = line.Items.FirstOrDefault() == item ? 0 : 1;

                    BindAbsoluteTime();

                    gvAttachmentTypes.RefreshData();
                }
                finally
                {
                    EndBinding();
                }

                _curItemButton.Refresh();
            }
		}

        private void BeginBinding()
        {
            Interlocked.Increment(ref _unstableCounter);
        }

        private void EndBinding()
        {
            int counter = Interlocked.Decrement(ref _unstableCounter);
            if (counter < 0)
            {
                DialogUtility.ShowError("Unexpected unstable counter: {0}", counter);
                _unstableCounter = 0;
            }
        }

        #endregion Initialization and binding

        #region Editing methods

        private void ClearLine(Control pnlLine)
        {
            var line = GetLine(pnlLine);

            line.Clear();
            pnlLine.Controls.Clear();
            IsNeedSave = true;

            UpdatePrognosis();
        }

        private void AddSpawnItem(Control pnlLine, SpawnItemButton btnInsertAfter, bool isBindCurrent, SpawnItem newItem = null)
        {
            var line = GetLine(pnlLine);
            var insertAfterItem = btnInsertAfter == null ? null : btnInsertAfter.Item;

            if (newItem == null)
                newItem = line.InsertItem(Pack.SpawnItemConfig.Types.Keys.First(), insertAfterItem);
            else
                line.InsertItem(newItem, insertAfterItem);

            var newBtn = AddSpawnItemButton(pnlLine, newItem);
            if (isBindCurrent)
                BindSpawnItem(newBtn);

            IsNeedSave = true;

            UpdatePrognosis();
        }

        private SpawnItemButton AddSpawnItemButton(Control pnlLine, SpawnItem item)
        {
            var btnItem = new SpawnItemButton(item, 
                (fileName) => ilSpawnItemTypesLarge.Images[GetImageIndex(ilSpawnItemTypesLarge, fileName, false)],
                (fileName) => ilAttachmentNames.Images[GetImageIndex(ilAttachmentNames, fileName, false)]);

            btnItem.ContextMenu = cmButton;            
            btnItem.MouseDown += btnItem_MouseDown;

            pnlLine.Controls.Add(btnItem);
            pnlLine.Refresh();

            return btnItem;
        }

        private void RemoveSpawnItemButton(SpawnItemButton btnItem)
        {
            BindSpawnItem(null);
            var pnlLine = btnItem.Parent;

            var line = GetLine(pnlLine);

            line.RemoveItem(btnItem.Item);
            pnlLine.Controls.Remove(btnItem);
            pnlLine.Refresh();

            UpdatePrognosis();
        }

        private void ShowInsertWizard(Control pnlLine, SpawnItemButton btnPrev = null)
        {
            using (var frm = new WizardForm(this))
            {                
                if (frm.ShowDialog() != DialogResult.OK)
                    return;

                int totalProbability = frm.Items.Sum(itm => itm.Probability);
                var itemsByRange = new List<Tuple<int, int, WizardForm.Item>>(frm.Items.Count);
                int probabilityStart = 0;                
                foreach (var item in frm.Items)
                {
                    itemsByRange.Add(
                        new Tuple<int, int, WizardForm.Item>(probabilityStart, probabilityStart + item.Probability, item));

                    probabilityStart += item.Probability;
                }

                Random rnd = new Random(0);

                pnlLine.SuspendLayout();
                SuspendDrawing(pnlLine);
                BeginBinding();
                try
                {
                    for (int i = 0; i < frm.Count; i++)
                    {
                        int index = rnd.Next(0, totalProbability);
                        var wizardItem = itemsByRange.First(itm => itm.Item1 <= index && index < itm.Item2).Item3;
                        wizardItem.Delay = frm.Interval;

                        // NOTE: клонируем item, т.к. если копировать ссылку, то потом получим несколько ссылок на одно и то же и будут непонятные ошибки при редактировании
                        AddSpawnItem(pnlLine, btnPrev, false, wizardItem.Clone());
                    }

                    BindSpawnItem(null);
                }
                finally
                {
                    pnlLine.ResumeLayout();
                    ResumeDrawing(pnlLine);
                    EndBinding();
                }

                UpdatePrognosis();
            }
        }

        #endregion Editing methods

        #region Utility

        private SpawnLine GetCurrentLine()
        {
            if (_curItemButton == null)
                return null;

            return GetLine(_curItemButton.Parent);
        }

        private SpawnLine GetLine(Control pnlLine)
        {
            if (pnlLine == null)
                return null;

            var result = pnlLine.Tag as SpawnLine;
            if (result == null)
                throw new CommonException("Panel tag is not binded to SpawnLine. It is binded to {0}", pnlLine.Tag == null ? "<NULL>" : pnlLine.Tag.ToString());

            return result;
        }        

        private static SpawnItemButton GetNearButton(Control pnlLine, int locationX)
        {
            SpawnItemButton result = null;
            foreach (SpawnItemButton btn in pnlLine.Controls)
            {
                if (btn.Right <= locationX && (result == null || btn.Right > result.Right))
                    result = btn;
            }

            return result;
        }

        private static void SuspendDrawing(Control parent)
        {
            SendMessage(parent.Handle, WM_SETREDRAW, false, 0);
        }

        private static void ResumeDrawing(Control parent)
        {
            SendMessage(parent.Handle, WM_SETREDRAW, true, 0);
            parent.Refresh();
        }

        #endregion Utility

        #region Event handlers

        private void pnlTimer_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Graphics g = Graphics.FromHwnd(pnlTimer.Handle);
			Level lev = CurrentLevel;

			g.Clear(Color.LightGray);

			if (lev.Time != 0)
			{
				int sizeX = pnlTimer.Size.Width;
				int time = hScrollBar.Value;

				Font font = new Font("Arial", 10, FontStyle.Regular);
				Pen  pen1 = new Pen(Color.Black, 1);
				Pen  pen2 = new Pen(Color.Blue, 2);
				Pen  pen3 = new Pen(Color.Red, 2);
				SolidBrush brush = new SolidBrush(Color.Black);

				for (int x = 0; x < sizeX; x += PixelInSec)
				{
					if ((time % 10) == 0)
					{
						g.DrawLine(pen2, x, 0, x, 10);
						g.DrawString(time.ToString(), font, brush, x, 10);
					}
					else
					{
						g.DrawLine(pen1, x, 0, x, 5);
					}
					++time;
					if (time > lev.Time)
						break;
				}
			}
		}

		private void hScrollBar_ValueChanged(object sender, EventArgs e)
		{
            pnlTimer.Refresh();
            foreach (var pnl in _linePanels)
            {
                pnl.Refresh();
            }
        }

        private void cmLinePanel_ClickAddWizard(object sender, EventArgs e)
        {
            ShowInsertWizard(cmLinePanel.SourceControl, _insertAfterBtnItem);
        }

        private void cmLinePanel_ClickAdd(object sender, EventArgs e)
        {
            AddSpawnItem(cmLinePanel.SourceControl, _insertAfterBtnItem, true);
        }

        private void cmLinePanel_ClickDeleteAll(object sender, EventArgs e)
        {
            ClearLine(cmLinePanel.SourceControl);
        }

        private void cmButton_ClickInsert(object sender, EventArgs e)
        {
            AddSpawnItem(cmButton.SourceControl.Parent, (SpawnItemButton)cmButton.SourceControl, true);
        }

        private void cmButton_ClickInsertWizard(object sender, EventArgs e)
        {
            ShowInsertWizard(cmButton.SourceControl.Parent, (SpawnItemButton)cmButton.SourceControl);
		}

		private void cmButton_ClickDelete(object sender, EventArgs e)
		{
            RemoveSpawnItemButton((SpawnItemButton)cmButton.SourceControl);
		}

        private void cmButton_ClickDeleteAll(object sender, EventArgs e)
        {
            ClearLine(cmButton.SourceControl.Parent);
        }

        private void pnlLine_Paint(object sender, PaintEventArgs e)
		{
			var pnlLine = (Panel)sender;
			int scroll = hScrollBar.Value;

            // NOTE: Клонируем коллекцию контролов (ToArray()), т.к. в ней может измениться порядок
			foreach (var btnItem in pnlLine.Controls.OfType<SpawnItemButton>().ToArray())
			{
                var line = GetLine(btnItem.Parent);

                // элементы с нулевой задержкой стараемся держать наверху, чтобы их было легко выделить и переместить куда-то или изменить Delay
                if (btnItem.Item.Delay == 0)
                    pnlLine.Controls.SetChildIndex(btnItem, 0);

                int x = (int)line.GetAbsoluteTime(btnItem.Item) - scroll;
				btnItem.Location = new Point(x * PixelInSec, 1);
			}
		}
		
		private void pnlLine_MouseDown(object sender, MouseEventArgs e)
		{
			Panel pnlLine = (Panel)sender;

            _insertAfterBtnItem = null;
            if (e.Button == MouseButtons.Right && pnlLine.Controls.Count > 0)
			{
                // ищем ближайший слева контрол от позиции клика
                _insertAfterBtnItem = GetNearButton(pnlLine, e.X);
			}
            else if (e.Button == MouseButtons.Left)
            {
                DoDragDrop(pnlLine, DragDropEffects.Move);
            }

            BindSpawnItem(null);
		}

        private void btnItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                BindSpawnItem((SpawnItemButton)sender);
                DoDragDrop((SpawnItemButton)sender, DragDropEffects.Move);
            }
        }

        private void gvTasks_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            var task = (Task)e.Row;
            if (e.IsGetData)
                e.Value = task.Params[_taskParamMap[e.Column]];
            else if (e.IsSetData)
                task.Params[_taskParamMap[e.Column]] = (int)e.Value;
        }

        private void gridDataSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (IsUnstableState)
                return;

            IsNeedSave = true;
        }

        private void view_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (IsUnstableState)
                return;

            IsNeedSave = true;
        }

        private void cmbSpawnItemType_EditValueChanged(object sender, EventArgs e)
        {
            if (IsUnstableState || _curItemButton == null)
                return;

            IsNeedSave = true;
            _curItemButton.Item.Type = (string)cmbSpawnItemType.EditValue;            

            UpdatePrognosis();
            _curItemButton.Refresh();
        }

        private void gvAttachmentTypes_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (_curItemButton == null || _pack == null || CurrentLevel == null || !e.Column.Name.StartsWith(AttachmentColNamePrefix))
                return;
            
            SpawnItem item = _curItemButton.Item;
            string attachmentType = (string)e.Row;
            int index = int.Parse(e.Column.Name.Replace(AttachmentColNamePrefix, String.Empty));
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

                _curItemButton.Refresh();

                UpdatePrognosis();
                _curItemButton.Refresh();
            }
        }

        private void gvAttachmentTypes_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (!gvAttachmentTypes.IsDataRow(e.RowHandle) || !e.Column.Name.StartsWith(AttachmentColNamePrefix))
                return;

            string type = (string)gvAttachmentTypes.GetRowCellValue(e.RowHandle, gcAttachmentType);
            e.RepositoryItem = AttachmentEditorsByType[type];
        }

        private void txtTime_EditValueChanged(object sender, EventArgs e)
        {
            if (IsUnstableState)
                return;

            CurrentLevel.Time = (float)txtTime.Value;
            UpdateTime();

            IsNeedSave = true;
        }

        private void txtSpawnItemDelay_EditValueChanged(object sender, EventArgs e)
        {
            if (IsUnstableState || _curItemButton == null)
                return;

            _curItemButton.Item.Delay = (float)txtSpawnItemDelay.Value;
            BindAbsoluteTime();
            IsNeedSave = true;

            _curItemButton.Parent.Refresh();
        }

        private void pnlLine_DragEnter(object sender, DragEventArgs e)
        {
            var pnlLineTo = (Panel)sender;
            var pnlDragged = (Panel)e.Data.GetData(typeof(Panel));
            var btnDragged = (SpawnItemButton)e.Data.GetData(typeof(SpawnItemButton));

            if ((btnDragged == null || btnDragged.Parent == pnlLineTo) && (pnlDragged == null || pnlDragged == pnlLineTo || !(pnlDragged.Tag is SpawnLine)))
                return;

            e.Effect = DragDropEffects.Move;
        }

        private void pnlLine_DragDrop(object sender, DragEventArgs e)
        {            
            var pnlLineTo = (Panel)sender;
            var pnlLineDragged = (Panel)e.Data.GetData(typeof(Panel));
            var btnDragged = (SpawnItemButton)e.Data.GetData(typeof(SpawnItemButton));

            if (btnDragged != null)
            {
                // перенос кнопки (SpawnItem) на другую панель (линию)
                var pnlLineFrom = btnDragged.Parent;
                var lineFrom = GetLine(pnlLineFrom);
                var lineTo = GetLine(pnlLineTo);

                pnlLineFrom.SuspendLayout();
                pnlLineTo.SuspendLayout();
                SuspendDrawing(pnlLineFrom);
                SuspendDrawing(pnlLineTo);
                try
                {
                    pnlLineFrom.Controls.Remove(btnDragged);
                    lineFrom.RemoveItem(btnDragged.Item);

                    var pointInPnlTo = pnlLineTo.PointToClient(new Point(e.X, e.Y));
                    AddSpawnItem(pnlLineTo, GetNearButton(pnlLineTo, pointInPnlTo.X), true, btnDragged.Item);
                }
                finally
                {
                    pnlLineFrom.ResumeLayout();
                    pnlLineTo.ResumeLayout();
                    ResumeDrawing(pnlLineFrom);
                    ResumeDrawing(pnlLineTo);
                }
            }
            else if (pnlLineDragged != null)
            {
                // меняем панели местами через Drag/Drop
                SpawnLine lineTo = GetLine(pnlLineTo);
                SpawnLine lineDragged = GetLine(pnlLineDragged);

                pnlLines.SuspendLayout();
                try
                {
                    CurrentLevel.InterchangeLines(lineTo, lineDragged);
                    int lineТоIndex = _linePanels.IndexOf(pnlLineTo);
                    int lineDraggedIndex = _linePanels.IndexOf(pnlLineDragged);

                    _linePanels[lineТоIndex] = pnlLineDragged;
                    _linePanels[lineDraggedIndex] = pnlLineTo;

                    int childToIndex = pnlLines.Controls.IndexOf(pnlLineTo);
                    int childDraggedIndex = pnlLines.Controls.IndexOf(pnlLineDragged);

                    pnlLines.Controls.SetChildIndex(pnlLineTo, childDraggedIndex);
                    pnlLines.Controls.SetChildIndex(pnlLineDragged, childToIndex);                    
                }
                finally
                {
                    pnlLines.ResumeLayout();
                }
            }

            IsNeedSave = true;
        }
        
        #endregion Event handlers
    }
}
