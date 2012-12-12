using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelEditor.Forms
{
    public partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.miPacks = new System.Windows.Forms.MenuItem();
            this.miPacksNew = new System.Windows.Forms.MenuItem();
            this.miPacksOpen = new System.Windows.Forms.MenuItem();
            this.miPacksClose = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.miPacksSave = new System.Windows.Forms.MenuItem();
            this.miPacksSaveAs = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.miPacksRecent = new System.Windows.Forms.MenuItem();
            this.menuItem41 = new System.Windows.Forms.MenuItem();
            this.miPacksExit = new System.Windows.Forms.MenuItem();
            this.miLevels = new System.Windows.Forms.MenuItem();
            this.miLevelsAdd = new System.Windows.Forms.MenuItem();
            this.miLevelsInsert = new System.Windows.Forms.MenuItem();
            this.miLevelsCopyAdd = new System.Windows.Forms.MenuItem();
            this.miLevelsDelete = new System.Windows.Forms.MenuItem();
            this.menuItem15 = new System.Windows.Forms.MenuItem();
            this.miLevelsCopy = new System.Windows.Forms.MenuItem();
            this.miLevelsPaste = new System.Windows.Forms.MenuItem();
            this.miLevelsCut = new System.Windows.Forms.MenuItem();
            this.menuItem18 = new System.Windows.Forms.MenuItem();
            this.miLevelsMoveFirst = new System.Windows.Forms.MenuItem();
            this.miLevelsMoveBack = new System.Windows.Forms.MenuItem();
            this.miLevelsMoveForward = new System.Windows.Forms.MenuItem();
            this.miLevelsMoveLast = new System.Windows.Forms.MenuItem();
            this.menuItem45 = new System.Windows.Forms.MenuItem();
            this.miLevelsClear = new System.Windows.Forms.MenuItem();
            this.menuItem21 = new System.Windows.Forms.MenuItem();
            this.miNavigation = new System.Windows.Forms.MenuItem();
            this.miNavigationFirst = new System.Windows.Forms.MenuItem();
            this.miNavigationPrev = new System.Windows.Forms.MenuItem();
            this.miNavigationNext = new System.Windows.Forms.MenuItem();
            this.miNavigationLast = new System.Windows.Forms.MenuItem();
            this.miWindows = new System.Windows.Forms.MenuItem();
            this.miHelp = new System.Windows.Forms.MenuItem();
            this.miHelpAbout = new System.Windows.Forms.MenuItem();
            this.miHelpHelp = new System.Windows.Forms.MenuItem();
            this.toolBar = new System.Windows.Forms.ToolBar();
            this.tbbNewPack = new System.Windows.Forms.ToolBarButton();
            this.tbbOpenPack = new System.Windows.Forms.ToolBarButton();
            this.tbbSavePack = new System.Windows.Forms.ToolBarButton();
            this.tbbSeparator1 = new System.Windows.Forms.ToolBarButton();
            this.tbbFirstLevel = new System.Windows.Forms.ToolBarButton();
            this.tbbPreviousLevel = new System.Windows.Forms.ToolBarButton();
            this.tbbEmpty = new System.Windows.Forms.ToolBarButton();
            this.tbbEmpty1 = new System.Windows.Forms.ToolBarButton();
            this.tbbEmpty2 = new System.Windows.Forms.ToolBarButton();
            this.tbbNextLevel = new System.Windows.Forms.ToolBarButton();
            this.tbbLastLevel = new System.Windows.Forms.ToolBarButton();
            this.tbbSeparator2 = new System.Windows.Forms.ToolBarButton();
            this.tbbAddLevel = new System.Windows.Forms.ToolBarButton();
            this.tbbInsertLevel = new System.Windows.Forms.ToolBarButton();
            this.tbbCopyLevel = new System.Windows.Forms.ToolBarButton();
            this.tbbCutLevel = new System.Windows.Forms.ToolBarButton();
            this.tbbPasteLevel = new System.Windows.Forms.ToolBarButton();
            this.tbbSeparator3 = new System.Windows.Forms.ToolBarButton();
            this.tbbMoveFirst = new System.Windows.Forms.ToolBarButton();
            this.tbbMoveBack = new System.Windows.Forms.ToolBarButton();
            this.tbbMoveForward = new System.Windows.Forms.ToolBarButton();
            this.tbbMoveLast = new System.Windows.Forms.ToolBarButton();
            this.tbbSeparator4 = new System.Windows.Forms.ToolBarButton();
            this.ilToolButtons = new System.Windows.Forms.ImageList(this.components);
            this.cbNumber = new System.Windows.Forms.ComboBox();
            this.openPackDialog = new System.Windows.Forms.OpenFileDialog();
            this.savePackDialog = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miPacks,
            this.miLevels,
            this.miNavigation,
            this.miWindows,
            this.miHelp});
            // 
            // miPacks
            // 
            this.miPacks.Index = 0;
            this.miPacks.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miPacksNew,
            this.miPacksOpen,
            this.miPacksClose,
            this.menuItem5,
            this.miPacksSave,
            this.miPacksSaveAs,
            this.menuItem8,
            this.miPacksRecent,
            this.menuItem41,
            this.miPacksExit});
            this.miPacks.Text = "Packs";
            // 
            // miPacksNew
            // 
            this.miPacksNew.Index = 0;
            this.miPacksNew.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
            this.miPacksNew.Text = "New";
            this.miPacksNew.Click += new System.EventHandler(this.miPacksNew_Click);
            // 
            // miPacksOpen
            // 
            this.miPacksOpen.Index = 1;
            this.miPacksOpen.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.miPacksOpen.Text = "Open";
            this.miPacksOpen.Click += new System.EventHandler(this.miPacksOpen_Click);
            // 
            // miPacksClose
            // 
            this.miPacksClose.Enabled = false;
            this.miPacksClose.Index = 2;
            this.miPacksClose.Text = "Close";
            this.miPacksClose.Click += new System.EventHandler(this.miPacksClose_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 3;
            this.menuItem5.Text = "-";
            // 
            // miPacksSave
            // 
            this.miPacksSave.Enabled = false;
            this.miPacksSave.Index = 4;
            this.miPacksSave.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            this.miPacksSave.Text = "Save";
            this.miPacksSave.Click += new System.EventHandler(this.miPacksSave_Click);
            // 
            // miPacksSaveAs
            // 
            this.miPacksSaveAs.Enabled = false;
            this.miPacksSaveAs.Index = 5;
            this.miPacksSaveAs.Text = "Save as ...";
            this.miPacksSaveAs.Click += new System.EventHandler(this.miPacksSaveAs_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 6;
            this.menuItem8.Text = "-";
            // 
            // miPacksRecent
            // 
            this.miPacksRecent.Index = 7;
            this.miPacksRecent.Text = "Recent Packs";
            // 
            // menuItem41
            // 
            this.menuItem41.Index = 8;
            this.menuItem41.Text = "-";
            // 
            // miPacksExit
            // 
            this.miPacksExit.Index = 9;
            this.miPacksExit.Shortcut = System.Windows.Forms.Shortcut.AltF4;
            this.miPacksExit.Text = "Exit";
            this.miPacksExit.Click += new System.EventHandler(this.miPacksExit_Click);
            // 
            // miLevels
            // 
            this.miLevels.Index = 1;
            this.miLevels.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miLevelsAdd,
            this.miLevelsInsert,
            this.miLevelsCopyAdd,
            this.miLevelsDelete,
            this.menuItem15,
            this.miLevelsCopy,
            this.miLevelsPaste,
            this.miLevelsCut,
            this.menuItem18,
            this.miLevelsMoveFirst,
            this.miLevelsMoveBack,
            this.miLevelsMoveForward,
            this.miLevelsMoveLast,
            this.menuItem45,
            this.miLevelsClear,
            this.menuItem21});
            this.miLevels.Text = "Levels";
            this.miLevels.Visible = false;
            // 
            // miLevelsAdd
            // 
            this.miLevelsAdd.Index = 0;
            this.miLevelsAdd.Shortcut = System.Windows.Forms.Shortcut.CtrlA;
            this.miLevelsAdd.Text = "Add";
            this.miLevelsAdd.Click += new System.EventHandler(this.miLevelsAdd_Click);
            // 
            // miLevelsInsert
            // 
            this.miLevelsInsert.Index = 1;
            this.miLevelsInsert.Shortcut = System.Windows.Forms.Shortcut.CtrlI;
            this.miLevelsInsert.Text = "Insert";
            this.miLevelsInsert.Click += new System.EventHandler(this.miLevelsInsert_Click);
            // 
            // miLevelsCopyAdd
            // 
            this.miLevelsCopyAdd.Index = 2;
            this.miLevelsCopyAdd.Text = "Copy and Add";
            this.miLevelsCopyAdd.Click += new System.EventHandler(this.miLevelsCopyAdd_Click);
            // 
            // miLevelsDelete
            // 
            this.miLevelsDelete.Index = 3;
            this.miLevelsDelete.Text = "Delete";
            this.miLevelsDelete.Click += new System.EventHandler(this.miLevelsDelete_Click);
            // 
            // menuItem15
            // 
            this.menuItem15.Index = 4;
            this.menuItem15.Text = "-";
            // 
            // miLevelsCopy
            // 
            this.miLevelsCopy.Index = 5;
            this.miLevelsCopy.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
            this.miLevelsCopy.Text = "Copy";
            this.miLevelsCopy.Click += new System.EventHandler(this.miLevelsCopy_Click);
            // 
            // miLevelsPaste
            // 
            this.miLevelsPaste.Index = 6;
            this.miLevelsPaste.Shortcut = System.Windows.Forms.Shortcut.CtrlV;
            this.miLevelsPaste.Text = "Paste";
            this.miLevelsPaste.Click += new System.EventHandler(this.miLevelsPaste_Click);
            // 
            // miLevelsCut
            // 
            this.miLevelsCut.Index = 7;
            this.miLevelsCut.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
            this.miLevelsCut.Text = "Cut";
            this.miLevelsCut.Click += new System.EventHandler(this.miLevelsCut_Click);
            // 
            // menuItem18
            // 
            this.menuItem18.Index = 8;
            this.menuItem18.Text = "-";
            // 
            // miLevelsMoveFirst
            // 
            this.miLevelsMoveFirst.Index = 9;
            this.miLevelsMoveFirst.Text = "Move First";
            this.miLevelsMoveFirst.Click += new System.EventHandler(this.miLevelsMoveFirst_Click);
            // 
            // miLevelsMoveBack
            // 
            this.miLevelsMoveBack.Index = 10;
            this.miLevelsMoveBack.Text = "Move Back";
            this.miLevelsMoveBack.Click += new System.EventHandler(this.miLevelsMoveBack_Click);
            // 
            // miLevelsMoveForward
            // 
            this.miLevelsMoveForward.Index = 11;
            this.miLevelsMoveForward.Text = "Move Forward";
            this.miLevelsMoveForward.Click += new System.EventHandler(this.miLevelsMoveForward_Click);
            // 
            // miLevelsMoveLast
            // 
            this.miLevelsMoveLast.Index = 12;
            this.miLevelsMoveLast.Text = "Move Last";
            this.miLevelsMoveLast.Click += new System.EventHandler(this.miLevelsMoveLast_Click);
            // 
            // menuItem45
            // 
            this.menuItem45.Index = 13;
            this.menuItem45.Text = "-";
            // 
            // miLevelsClear
            // 
            this.miLevelsClear.Index = 14;
            this.miLevelsClear.Text = "Clear";
            this.miLevelsClear.Click += new System.EventHandler(this.miLevelsClear_Click);
            // 
            // menuItem21
            // 
            this.menuItem21.Index = 15;
            this.menuItem21.Text = "-";
            // 
            // miNavigation
            // 
            this.miNavigation.Index = 2;
            this.miNavigation.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miNavigationFirst,
            this.miNavigationPrev,
            this.miNavigationNext,
            this.miNavigationLast});
            this.miNavigation.Text = "Navigation";
            this.miNavigation.Visible = false;
            // 
            // miNavigationFirst
            // 
            this.miNavigationFirst.Index = 0;
            this.miNavigationFirst.Shortcut = System.Windows.Forms.Shortcut.F5;
            this.miNavigationFirst.Text = "First";
            this.miNavigationFirst.Click += new System.EventHandler(this.miNavigationFirst_Click);
            // 
            // miNavigationPrev
            // 
            this.miNavigationPrev.Index = 1;
            this.miNavigationPrev.Shortcut = System.Windows.Forms.Shortcut.F6;
            this.miNavigationPrev.Text = "Previous";
            this.miNavigationPrev.Click += new System.EventHandler(this.miNavigationPrev_Click);
            // 
            // miNavigationNext
            // 
            this.miNavigationNext.Index = 2;
            this.miNavigationNext.Shortcut = System.Windows.Forms.Shortcut.F7;
            this.miNavigationNext.Text = "Next";
            this.miNavigationNext.Click += new System.EventHandler(this.miNavigationNext_Click);
            // 
            // miNavigationLast
            // 
            this.miNavigationLast.Index = 3;
            this.miNavigationLast.Shortcut = System.Windows.Forms.Shortcut.F8;
            this.miNavigationLast.Text = "Last";
            this.miNavigationLast.Click += new System.EventHandler(this.miNavigationLast_Click);
            // 
            // miWindows
            // 
            this.miWindows.Index = 3;
            this.miWindows.MdiList = true;
            this.miWindows.Text = "Windows";
            this.miWindows.Visible = false;
            // 
            // miHelp
            // 
            this.miHelp.Index = 4;
            this.miHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miHelpAbout,
            this.miHelpHelp});
            this.miHelp.Text = "Help";
            // 
            // miHelpAbout
            // 
            this.miHelpAbout.Index = 0;
            this.miHelpAbout.Text = "About";
            // 
            // miHelpHelp
            // 
            this.miHelpHelp.Index = 1;
            this.miHelpHelp.Shortcut = System.Windows.Forms.Shortcut.F1;
            this.miHelpHelp.Text = "Help";
            // 
            // toolBar
            // 
            this.toolBar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.toolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.tbbNewPack,
            this.tbbOpenPack,
            this.tbbSavePack,
            this.tbbSeparator1,
            this.tbbFirstLevel,
            this.tbbPreviousLevel,
            this.tbbEmpty,
            this.tbbEmpty1,
            this.tbbEmpty2,
            this.tbbNextLevel,
            this.tbbLastLevel,
            this.tbbSeparator2,
            this.tbbAddLevel,
            this.tbbInsertLevel,
            this.tbbCopyLevel,
            this.tbbCutLevel,
            this.tbbPasteLevel,
            this.tbbSeparator3,
            this.tbbMoveFirst,
            this.tbbMoveBack,
            this.tbbMoveForward,
            this.tbbMoveLast,
            this.tbbSeparator4});
            this.toolBar.ButtonSize = new System.Drawing.Size(20, 20);
            this.toolBar.DropDownArrows = true;
            this.toolBar.ImageList = this.ilToolButtons;
            this.toolBar.Location = new System.Drawing.Point(0, 0);
            this.toolBar.Name = "toolBar";
            this.toolBar.ShowToolTips = true;
            this.toolBar.Size = new System.Drawing.Size(792, 28);
            this.toolBar.TabIndex = 1;
            this.toolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar_ButtonClick);
            // 
            // tbbNewPack
            // 
            this.tbbNewPack.ImageIndex = 0;
            this.tbbNewPack.Name = "tbbNewPack";
            this.tbbNewPack.Tag = "NewPack";
            this.tbbNewPack.ToolTipText = "New Pack";
            // 
            // tbbOpenPack
            // 
            this.tbbOpenPack.ImageIndex = 1;
            this.tbbOpenPack.Name = "tbbOpenPack";
            this.tbbOpenPack.Tag = "OpenPack";
            this.tbbOpenPack.ToolTipText = "Open Pack";
            // 
            // tbbSavePack
            // 
            this.tbbSavePack.Enabled = false;
            this.tbbSavePack.ImageIndex = 2;
            this.tbbSavePack.Name = "tbbSavePack";
            this.tbbSavePack.Tag = "SavePack";
            this.tbbSavePack.ToolTipText = "Save Pack";
            // 
            // tbbSeparator1
            // 
            this.tbbSeparator1.Enabled = false;
            this.tbbSeparator1.Name = "tbbSeparator1";
            this.tbbSeparator1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbbFirstLevel
            // 
            this.tbbFirstLevel.Enabled = false;
            this.tbbFirstLevel.ImageIndex = 3;
            this.tbbFirstLevel.Name = "tbbFirstLevel";
            this.tbbFirstLevel.Tag = "FirstLevel";
            this.tbbFirstLevel.ToolTipText = "First Level";
            // 
            // tbbPreviousLevel
            // 
            this.tbbPreviousLevel.Enabled = false;
            this.tbbPreviousLevel.ImageIndex = 4;
            this.tbbPreviousLevel.Name = "tbbPreviousLevel";
            this.tbbPreviousLevel.Tag = "PrevLevel";
            this.tbbPreviousLevel.ToolTipText = "Previous Level";
            // 
            // tbbEmpty
            // 
            this.tbbEmpty.Enabled = false;
            this.tbbEmpty.Name = "tbbEmpty";
            // 
            // tbbEmpty1
            // 
            this.tbbEmpty1.Enabled = false;
            this.tbbEmpty1.Name = "tbbEmpty1";
            // 
            // tbbEmpty2
            // 
            this.tbbEmpty2.Enabled = false;
            this.tbbEmpty2.Name = "tbbEmpty2";
            // 
            // tbbNextLevel
            // 
            this.tbbNextLevel.Enabled = false;
            this.tbbNextLevel.ImageIndex = 6;
            this.tbbNextLevel.Name = "tbbNextLevel";
            this.tbbNextLevel.Tag = "NextLevel";
            this.tbbNextLevel.ToolTipText = "Next Level";
            // 
            // tbbLastLevel
            // 
            this.tbbLastLevel.Enabled = false;
            this.tbbLastLevel.ImageIndex = 7;
            this.tbbLastLevel.Name = "tbbLastLevel";
            this.tbbLastLevel.Tag = "LastLevel";
            this.tbbLastLevel.ToolTipText = "Last Level";
            // 
            // tbbSeparator2
            // 
            this.tbbSeparator2.Enabled = false;
            this.tbbSeparator2.Name = "tbbSeparator2";
            this.tbbSeparator2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbbAddLevel
            // 
            this.tbbAddLevel.Enabled = false;
            this.tbbAddLevel.ImageIndex = 8;
            this.tbbAddLevel.Name = "tbbAddLevel";
            this.tbbAddLevel.Tag = "AddLevel";
            this.tbbAddLevel.ToolTipText = "Add Level";
            // 
            // tbbInsertLevel
            // 
            this.tbbInsertLevel.Enabled = false;
            this.tbbInsertLevel.ImageIndex = 9;
            this.tbbInsertLevel.Name = "tbbInsertLevel";
            this.tbbInsertLevel.Tag = "InsertLevel";
            this.tbbInsertLevel.ToolTipText = "Insert Level";
            // 
            // tbbCopyLevel
            // 
            this.tbbCopyLevel.Enabled = false;
            this.tbbCopyLevel.ImageIndex = 10;
            this.tbbCopyLevel.Name = "tbbCopyLevel";
            this.tbbCopyLevel.Tag = "CopyLevel";
            this.tbbCopyLevel.ToolTipText = "Copy Level";
            // 
            // tbbCutLevel
            // 
            this.tbbCutLevel.Enabled = false;
            this.tbbCutLevel.ImageIndex = 11;
            this.tbbCutLevel.Name = "tbbCutLevel";
            this.tbbCutLevel.Tag = "CutLevel";
            this.tbbCutLevel.ToolTipText = "Cut Level";
            // 
            // tbbPasteLevel
            // 
            this.tbbPasteLevel.Enabled = false;
            this.tbbPasteLevel.ImageIndex = 12;
            this.tbbPasteLevel.Name = "tbbPasteLevel";
            this.tbbPasteLevel.Tag = "PasteLevel";
            this.tbbPasteLevel.ToolTipText = "Paste Level";
            // 
            // tbbSeparator3
            // 
            this.tbbSeparator3.Enabled = false;
            this.tbbSeparator3.Name = "tbbSeparator3";
            this.tbbSeparator3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbbMoveFirst
            // 
            this.tbbMoveFirst.Enabled = false;
            this.tbbMoveFirst.ImageIndex = 13;
            this.tbbMoveFirst.Name = "tbbMoveFirst";
            this.tbbMoveFirst.Tag = "MoveFirst";
            this.tbbMoveFirst.ToolTipText = "Move First";
            // 
            // tbbMoveBack
            // 
            this.tbbMoveBack.Enabled = false;
            this.tbbMoveBack.ImageIndex = 14;
            this.tbbMoveBack.Name = "tbbMoveBack";
            this.tbbMoveBack.Tag = "MoveBack";
            this.tbbMoveBack.ToolTipText = "Move Back";
            // 
            // tbbMoveForward
            // 
            this.tbbMoveForward.Enabled = false;
            this.tbbMoveForward.ImageIndex = 15;
            this.tbbMoveForward.Name = "tbbMoveForward";
            this.tbbMoveForward.Tag = "MoveForward";
            this.tbbMoveForward.ToolTipText = "Move Forward";
            // 
            // tbbMoveLast
            // 
            this.tbbMoveLast.Enabled = false;
            this.tbbMoveLast.ImageIndex = 16;
            this.tbbMoveLast.Name = "tbbMoveLast";
            this.tbbMoveLast.Tag = "MoveLast";
            this.tbbMoveLast.ToolTipText = "Move Last";
            // 
            // tbbSeparator4
            // 
            this.tbbSeparator4.Enabled = false;
            this.tbbSeparator4.Name = "tbbSeparator4";
            this.tbbSeparator4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ilToolButtons
            // 
            this.ilToolButtons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilToolButtons.ImageStream")));
            this.ilToolButtons.TransparentColor = System.Drawing.Color.Silver;
            this.ilToolButtons.Images.SetKeyName(0, "");
            this.ilToolButtons.Images.SetKeyName(1, "");
            this.ilToolButtons.Images.SetKeyName(2, "");
            this.ilToolButtons.Images.SetKeyName(3, "");
            this.ilToolButtons.Images.SetKeyName(4, "");
            this.ilToolButtons.Images.SetKeyName(5, "");
            this.ilToolButtons.Images.SetKeyName(6, "");
            this.ilToolButtons.Images.SetKeyName(7, "");
            this.ilToolButtons.Images.SetKeyName(8, "");
            this.ilToolButtons.Images.SetKeyName(9, "");
            this.ilToolButtons.Images.SetKeyName(10, "");
            this.ilToolButtons.Images.SetKeyName(11, "");
            this.ilToolButtons.Images.SetKeyName(12, "");
            this.ilToolButtons.Images.SetKeyName(13, "");
            this.ilToolButtons.Images.SetKeyName(14, "");
            this.ilToolButtons.Images.SetKeyName(15, "");
            this.ilToolButtons.Images.SetKeyName(16, "");
            this.ilToolButtons.Images.SetKeyName(17, "");
            this.ilToolButtons.Images.SetKeyName(18, "");
            this.ilToolButtons.Images.SetKeyName(19, "");
            this.ilToolButtons.Images.SetKeyName(20, "");
            this.ilToolButtons.Images.SetKeyName(21, "");
            // 
            // cbNumber
            // 
            this.cbNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNumber.Enabled = false;
            this.cbNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbNumber.Location = new System.Drawing.Point(128, 2);
            this.cbNumber.MaxDropDownItems = 20;
            this.cbNumber.Name = "cbNumber";
            this.cbNumber.Size = new System.Drawing.Size(56, 21);
            this.cbNumber.TabIndex = 5;
            this.cbNumber.TabStop = false;
            this.cbNumber.SelectedIndexChanged += new System.EventHandler(this.cbNumber_SelectedIndexChanged);
            // 
            // openPackDialog
            // 
            this.openPackDialog.AddExtension = false;
            this.openPackDialog.CheckFileExists = false;
            this.openPackDialog.DefaultExt = "pack";
            this.openPackDialog.Filter = "Pack files (*.xml)|*.xml";
            this.openPackDialog.Title = "Open Packs";
            // 
            // savePackDialog
            // 
            this.savePackDialog.DefaultExt = "pack";
            this.savePackDialog.Filter = "Pack files (*.xml)|*.xml";
            this.savePackDialog.Title = "Save Packs";
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(792, 553);
            this.Controls.Add(this.cbNumber);
            this.Controls.Add(this.toolBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Menu = this.mainMenu;
            this.Name = "MainForm";
            this.Text = "KBTR Level Editor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Closed += new System.EventHandler(this.MainForm_Closed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.ComboBox cbNumber;
        private System.Windows.Forms.ImageList ilToolButtons;
        private System.Windows.Forms.OpenFileDialog openPackDialog;
        private System.Windows.Forms.SaveFileDialog savePackDialog;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ToolBar toolBar;
        private System.Windows.Forms.ToolBarButton tbbNewPack;
        private System.Windows.Forms.ToolBarButton tbbOpenPack;
        private System.Windows.Forms.ToolBarButton tbbSavePack;
        private System.Windows.Forms.ToolBarButton tbbSeparator1;
        private System.Windows.Forms.ToolBarButton tbbFirstLevel;
        private System.Windows.Forms.ToolBarButton tbbPreviousLevel;
        private System.Windows.Forms.ToolBarButton tbbEmpty;
        private System.Windows.Forms.ToolBarButton tbbEmpty1;
        private System.Windows.Forms.ToolBarButton tbbEmpty2;
        private System.Windows.Forms.ToolBarButton tbbNextLevel;
        private System.Windows.Forms.ToolBarButton tbbLastLevel;
        private System.Windows.Forms.ToolBarButton tbbSeparator2;
        private System.Windows.Forms.ToolBarButton tbbAddLevel;
        private System.Windows.Forms.ToolBarButton tbbCopyLevel;
        private System.Windows.Forms.ToolBarButton tbbCutLevel;
        private System.Windows.Forms.ToolBarButton tbbPasteLevel;
        private System.Windows.Forms.ToolBarButton tbbInsertLevel;
        private System.Windows.Forms.ToolBarButton tbbMoveFirst;
        private System.Windows.Forms.ToolBarButton tbbMoveBack;
        private System.Windows.Forms.ToolBarButton tbbMoveForward;
        private System.Windows.Forms.ToolBarButton tbbMoveLast;
        private System.Windows.Forms.ToolBarButton tbbSeparator3;
        private System.Windows.Forms.ToolBarButton tbbSeparator4;
        private System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem menuItem8;
        private System.Windows.Forms.MenuItem menuItem15;
        private System.Windows.Forms.MenuItem menuItem18;
        private System.Windows.Forms.MenuItem menuItem21;
        private System.Windows.Forms.MenuItem menuItem41;
        private System.Windows.Forms.MenuItem menuItem45;
        private System.Windows.Forms.MenuItem miPacks;
        private System.Windows.Forms.MenuItem miPacksNew;
        private System.Windows.Forms.MenuItem miPacksOpen;
        private System.Windows.Forms.MenuItem miPacksClose;
        private System.Windows.Forms.MenuItem miPacksSave;
        private System.Windows.Forms.MenuItem miPacksSaveAs;
        private System.Windows.Forms.MenuItem miPacksRecent;
        private System.Windows.Forms.MenuItem miPacksExit;
        private System.Windows.Forms.MenuItem miLevels;
        private System.Windows.Forms.MenuItem miLevelsAdd;
        private System.Windows.Forms.MenuItem miLevelsInsert;
        private System.Windows.Forms.MenuItem miLevelsCopyAdd;
        private System.Windows.Forms.MenuItem miLevelsDelete;
        private System.Windows.Forms.MenuItem miLevelsCopy;
        private System.Windows.Forms.MenuItem miLevelsPaste;
        private System.Windows.Forms.MenuItem miLevelsCut;
        private System.Windows.Forms.MenuItem miLevelsClear;
        private System.Windows.Forms.MenuItem miNavigation;
        private System.Windows.Forms.MenuItem miNavigationFirst;
        private System.Windows.Forms.MenuItem miNavigationPrev;
        private System.Windows.Forms.MenuItem miNavigationNext;
        private System.Windows.Forms.MenuItem miNavigationLast;
        private System.Windows.Forms.MenuItem miWindows;
        private System.Windows.Forms.MenuItem miLevelsMoveFirst;
        private System.Windows.Forms.MenuItem miLevelsMoveBack;
        private System.Windows.Forms.MenuItem miLevelsMoveForward;
        private System.Windows.Forms.MenuItem miLevelsMoveLast;
        private System.Windows.Forms.MenuItem miHelp;
        private System.Windows.Forms.MenuItem miHelpAbout;
        private System.Windows.Forms.MenuItem miHelpHelp;
    }
}
