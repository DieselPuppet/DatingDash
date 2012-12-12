using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Linq;
using System.IO;
using System.Xml;
using LevelData.Utility;
using LevelData.Utility.Extensions;
using LevelEditor.Utility;

namespace LevelEditor.Forms
{
	public partial class MainForm : Form
	{
		#region Fields

		private string[] _recentPacks = new string[5];
		private string _lastDirectory;
        private string _levelTemplatePath;
        private readonly string _editorDirectory;

		#endregion Fields

		public MainForm(string path = null)
		{
			InitializeComponent();

			for (int i = 0; i < _recentPacks.Length; i++)
				_recentPacks[i] = String.Empty;

			_lastDirectory = Directory.GetCurrentDirectory();
			_editorDirectory = Application.StartupPath;

			LoadConfig();

            if (!path.IsNullOrWhiteSpace())
                OpenPack(path, false);
		}

		private void MainForm_Closed(object sender, System.EventArgs e)
		{
			SaveConfig();
		}

        private void UpdateFormState()
        {
            if (Disposing)
                return;

            bool hasPack = GetActivePack() != null;

            miPacksClose.Enabled = hasPack;
            miPacksSave.Enabled = hasPack;
            miPacksSaveAs.Enabled = hasPack;
            miLevels.Visible = hasPack;
            miNavigation.Visible = hasPack;
            miWindows.Visible = hasPack;

            tbbSavePack.Enabled = hasPack;
            tbbSeparator1.Enabled = hasPack;
            tbbFirstLevel.Enabled = hasPack;
            tbbPreviousLevel.Enabled = hasPack;
            tbbNextLevel.Enabled = hasPack;
            tbbLastLevel.Enabled = hasPack;
            tbbSeparator2.Enabled = hasPack;
            tbbAddLevel.Enabled = hasPack;
            tbbCopyLevel.Enabled = hasPack;
            tbbCutLevel.Enabled = hasPack;
            tbbPasteLevel.Enabled = hasPack;
            tbbInsertLevel.Enabled = hasPack;
            tbbMoveFirst.Enabled = hasPack;
            tbbMoveBack.Enabled = hasPack;
            tbbMoveForward.Enabled = hasPack;
            tbbMoveLast.Enabled = hasPack;
            tbbSeparator3.Enabled = hasPack;
            tbbSeparator4.Enabled = hasPack;
            cbNumber.Enabled = hasPack;
        }

		private void LoadConfig()
		{
			string path = Path.Combine(_editorDirectory, "config.xml");

			if (File.Exists(path))
			{
				XmlDocument doc = new XmlDocument();
				doc.Load(path);
				XmlElement xml = doc.DocumentElement;

				foreach (XmlAttribute attribute in xml.Attributes)
				{
					if (attribute.Name == "last_directory")
					{
						_lastDirectory = attribute.Value;
					}
					else if (attribute.Name == "recent_pack_1")
					{
						_recentPacks[0] = attribute.Value;
					}
					else if (attribute.Name == "recent_pack_2")
					{
						_recentPacks[1] = attribute.Value;
					}
					else if (attribute.Name == "recent_pack_3")
					{
						_recentPacks[2] = attribute.Value;
					}
					else if (attribute.Name == "recent_pack_4")
					{
						_recentPacks[3] = attribute.Value;
					}
					else if (attribute.Name == "recent_pack_5")
					{
						_recentPacks[4] = attribute.Value;
					}
                    else if (attribute.Name == "template_path")
                    {
                        _levelTemplatePath = attribute.Value;
                    }
				}

				miPacksRecent.MenuItems.Clear();
				for (int i = 0; i < 5; ++i)
				{
					if (_recentPacks[i] != "")
					{
						miPacksRecent.MenuItems.Add(_recentPacks[i], new EventHandler(miPackRecent_Click));
					}
				}

                if (_levelTemplatePath.IsNullOrWhiteSpace())
                    _levelTemplatePath = Path.Combine(_editorDirectory, "template.xml");
			}
		}

		private void SaveConfig()
		{
            string path = Path.Combine(_editorDirectory, "config.xml");

			XmlDocument doc = new XmlDocument();
			doc.AppendChild(doc.CreateXmlDeclaration("1.0", "utf-16", null));

			XmlElement root_element = doc.CreateElement("Config");
			root_element.SetAttribute("last_directory", _lastDirectory);
			root_element.SetAttribute("recent_pack_1", _recentPacks[0]);
			root_element.SetAttribute("recent_pack_2", _recentPacks[1]);
			root_element.SetAttribute("recent_pack_3", _recentPacks[2]);
			root_element.SetAttribute("recent_pack_4", _recentPacks[3]);
			root_element.SetAttribute("recent_pack_5", _recentPacks[4]);
            
            // не сохраняем полный путь к template, если он создан относительно папки редактора
            string templatePath = _levelTemplatePath;
            if (Path.IsPathRooted(templatePath) && templatePath.StartsWith(_editorDirectory))
            {
                templatePath = templatePath.Replace(_editorDirectory +
                    (_editorDirectory.EndsWith(Path.DirectorySeparatorChar.ToString()) ? String.Empty : Path.DirectorySeparatorChar.ToString()), String.Empty);
            }

            root_element.SetAttribute("template_path", templatePath);

			doc.AppendChild(root_element);

			doc.Save(path);
		}

        private void OpenPack(string fileName, bool isShowDialog)
        {
            string formCaption = null;
            if (isShowDialog)
            {
                openPackDialog.InitialDirectory = _lastDirectory;

                if (openPackDialog.ShowDialog() != DialogResult.OK)
                    return;

                fileName = openPackDialog.FileName;
                AddRecentPack(fileName);

                _lastDirectory = Path.GetDirectoryName(openPackDialog.FileName);
            }
            else if (!fileName.IsNullOrWhiteSpace())
            {
                if (!File.Exists(fileName))
                    return;

                var frmOpened = MdiChildren.FirstOrDefault(frm => frm.Text == fileName);
                if (frmOpened != null)
                {
                    frmOpened.Activate();
                    if (DialogUtility.ShowConfirm(true, "File is already opened, do you want to reopen it?") == DialogResult.Yes)
                    {
                        frmOpened.Close();

                        // проверяем, закрылось ли (могли предложить сохранить и закрытия не произошло)
                        if (MdiChildren.Contains(frmOpened))
                            return;
                    }
                    else
                    {
                        return;
                    }
                }
            }
            else
            {
                int index = MdiChildren.Length;
                do
                {
                    formCaption = String.Format("Noname{0}", ++index);
                } while (MdiChildren.Any(frm => frm.Text == formCaption));
            }

            try
            {
                var packForm = new PackForm(_levelTemplatePath, fileName);

                packForm.MdiParent = this;
                packForm.Activated += packForm_Activated;
                packForm.FormClosing += packForm_FormClosing;
                packForm.Disposed += packForm_Disposed;

                if (formCaption != null)
                    packForm.Text = formCaption;
                                
                packForm.Show();
                UpdateFormState();
            }
            catch (Exception ex)
            {
                DialogUtility.ShowError("Error while opening pack: {0}", ex.Message);
            }
        }

        private PackForm GetActivePack()
        {
            return (PackForm)ActiveMdiChild;
        }

        private void AddRecentPack(string path)
        {
            miPacksRecent.MenuItems.Clear();

            int index = -1;
            bool present = false;

            for (int i = 4; i > -1; --i)
            {
                if (_recentPacks[i] == "")
                {
                    index = i;
                }
                if (_recentPacks[i] == path)
                    present = true;
            }

            if (!present)
            {
                if (index == -1)
                {
                    for (int i = 1; i < 5; ++i)
                    {
                        _recentPacks[i - 1] = _recentPacks[i];
                    }
                    _recentPacks[4] = path;
                }
                else
                {
                    _recentPacks[index] = path;
                }
            }

            for (int i = 0; i < 5; ++i)
            {
                if (_recentPacks[i] != "")
                {
                    miPacksRecent.MenuItems.Add(_recentPacks[i], new EventHandler(miPackRecent_Click));
                }
            }
        }

        private bool SaveCurrentPack(bool requestFileName)
        {
            var packForm = GetActivePack();
            if (packForm == null)
                return false;

            if (packForm.IsNew || requestFileName)
            {
			    savePackDialog.InitialDirectory = _lastDirectory;
                bool result = false;

			    if (savePackDialog.ShowDialog() == DialogResult.OK)
			    {
                    result = packForm.Save(savePackDialog.FileName);
                    if (result)
                        AddRecentPack(savePackDialog.FileName);

				    _lastDirectory = Path.GetDirectoryName(savePackDialog.FileName);
			    }

                return result;
            }
            else
            {
                return packForm.Save(packForm.Text);
            }
        }

        private void UpdateLevelIndex()
        {
            var packForm = GetActivePack();
            if (packForm == null)
                return;

            cbNumber.Text = (packForm.LevelIndex + 1).ToString();
        }

        #region Event handlers

        private void miPacksNew_Click(object sender, System.EventArgs e)
		{
            OpenPack(null, false);
		}

		private void miPacksOpen_Click(object sender, System.EventArgs e)
		{
            OpenPack(null, true);
		}

		private void miPackRecent_Click(object sender, System.EventArgs e)
		{
            string fileName = ((MenuItem)sender).Text;
            OpenPack(fileName, false);
		}        

		private void miPacksClose_Click(object sender, System.EventArgs e)
		{
			if (ActiveMdiChild != null)
				ActiveMdiChild.Close();
		}

		private void miPacksSave_Click(object sender, System.EventArgs e)
		{
            SaveCurrentPack(false);
		}

		private void miPacksSaveAs_Click(object sender, System.EventArgs e)
		{
            SaveCurrentPack(true);
		}

		private void miPacksExit_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void packForm_Activated(object sender, EventArgs e)
		{
            var packForm = (PackForm)sender;
			cbNumber.Items.Clear();
            for (int i = 0; i < packForm.LevelCount; ++i)
			{
				cbNumber.Items.Add(i + 1);
			}

            UpdateLevelIndex();
		}

        private void packForm_FormClosing(object sender, FormClosingEventArgs e)
		{
            DialogResult res = DialogResult.Yes;
            var packForm = (PackForm)sender;
			if (packForm.IsNew || packForm.IsNeedSave)
			{                
                string message = String.Format("Save pack \"{0}\"?", Path.GetFileNameWithoutExtension(packForm.Text));
                res = DialogUtility.ShowConfirm(true, message);

                if (res == DialogResult.Yes && !SaveCurrentPack(false))
                    res = DialogResult.Cancel;
			}

            e.Cancel = (res == DialogResult.Cancel);
		}

        private void packForm_Disposed(object sender, EventArgs e)
        {
            UpdateFormState();
        }

		private void cbNumber_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            var packForm = GetActivePack();
            if (packForm != null)
                packForm.LevelIndex = Convert.ToInt32(cbNumber.Text) - 1;
		}

		private void miLevelsAdd_Click(object sender, System.EventArgs e)
		{
            var packForm = GetActivePack();
			if (packForm != null)
			{
				packForm.AddLevel();
				cbNumber.Items.Add(cbNumber.Items.Count + 1);
                UpdateLevelIndex();
			}
		}

		private void miLevelsInsert_Click(object sender, System.EventArgs e)
		{
            var packForm = GetActivePack();
			if (packForm != null)
			{
				packForm.InsertLevel();
				cbNumber.Items.Add(cbNumber.Items.Count + 1);
                UpdateLevelIndex();
			}
		}

		private void miLevelsCopyAdd_Click(object sender, System.EventArgs e)
		{
            var packForm = GetActivePack();
			if (packForm != null)
			{
				packForm.CopyAddLevel();
				cbNumber.Items.Add(cbNumber.Items.Count + 1);
                UpdateLevelIndex();
			}
		}

		private void miLevelsDelete_Click(object sender, System.EventArgs e)
		{
            var packForm = GetActivePack();
			if (ActiveMdiChild != null)
			{
                packForm.DeleteLevel();
				cbNumber.Items.Remove(cbNumber.Items.Count);
                UpdateLevelIndex();
			}
		}

		private void miLevelsCopy_Click(object sender, System.EventArgs e)
		{
            var packForm = GetActivePack();
            if (packForm != null)
                packForm.CopyLevel();
		}

		private void miLevelsPaste_Click(object sender, System.EventArgs e)
		{
            var packForm = GetActivePack();
            if (packForm != null)
                packForm.PasteLevel();
		}

		private void miLevelsCut_Click(object sender, System.EventArgs e)
		{
            var packForm = GetActivePack();
            if (packForm != null)
			{
                packForm.CutLevel();
				cbNumber.Items.Remove(cbNumber.Items.Count);
                UpdateLevelIndex();
			}
		}

		private void miLevelsMoveFirst_Click(object sender, System.EventArgs e)
		{
            var packForm = GetActivePack();
            if (packForm != null)
			{
                packForm.MoveLevelFirst();
                UpdateLevelIndex();
			}
		}

		private void miLevelsMoveBack_Click(object sender, System.EventArgs e)
		{
            var packForm = GetActivePack();
            if (packForm != null)
			{
                packForm.MoveLevelBack();
                UpdateLevelIndex();
			}
		}

		private void miLevelsMoveForward_Click(object sender, System.EventArgs e)
		{
            var packForm = GetActivePack();
            if (packForm != null)
			{
                packForm.MoveLevelForward();
                UpdateLevelIndex();
			}
		}

		private void miLevelsMoveLast_Click(object sender, System.EventArgs e)
		{
            var packForm = GetActivePack();
            if (packForm != null)
			{
                packForm.MoveLevelLast();
                UpdateLevelIndex();
			}
		}

		private void miLevelsClear_Click(object sender, System.EventArgs e)
		{
            var packForm = GetActivePack();
            if (packForm != null)
                packForm.ClearLevel();
		}

		private void miNavigationFirst_Click(object sender, System.EventArgs e)
		{
            var packForm = GetActivePack();
            if (packForm != null)
			{
                packForm.FirstLevel();
                UpdateLevelIndex();
			}
		}

		private void miNavigationPrev_Click(object sender, System.EventArgs e)
		{
            var packForm = GetActivePack();
            if (packForm != null)
			{
                packForm.PreviousLevel();
                UpdateLevelIndex();
			}
		}

		private void miNavigationNext_Click(object sender, System.EventArgs e)
		{
            var packForm = GetActivePack();
			if (ActiveMdiChild != null)
			{
                if (packForm.NextLevel())
				{
                    UpdateLevelIndex();
				}
				else
				{
                    DialogResult res = DialogUtility.ShowConfirm(true, "Current level is the last. Add new level?");

                    if (res == DialogResult.Yes)
						miLevelsAdd_Click(sender, e);
				}
			}
		}

		private void miNavigationLast_Click(object sender, System.EventArgs e)
		{
            var packForm = GetActivePack();
            if (packForm != null)
			{
                packForm.LastLevel();
                UpdateLevelIndex();
			}
		}

		private void toolBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if(((string)e.Button.Tag) == "NewPack")
			{
				miPacksNew_Click(sender, e);
			}
			else if(((string)e.Button.Tag) == "OpenPack")
			{
				miPacksOpen_Click(sender, e);
			}
			else if(((string)e.Button.Tag) == "SavePack")
			{
				miPacksSave_Click(sender, e);
			}
			else if(((string)e.Button.Tag) == "FirstLevel")
			{
				miNavigationFirst_Click(sender, e);
			}
			else if(((string)e.Button.Tag) == "PrevLevel")
			{
				miNavigationPrev_Click(sender, e);
			}
			else if(((string)e.Button.Tag) == "NextLevel")
			{
				miNavigationNext_Click(sender, e);
			}
			else if(((string)e.Button.Tag) == "LastLevel")
			{
				miNavigationLast_Click(sender, e);
			}
			else if(((string)e.Button.Tag) == "AddLevel")
			{
				miLevelsAdd_Click(sender, e);
			}
			else if(((string)e.Button.Tag) == "InsertLevel")
			{
				miLevelsInsert_Click(sender, e);
			}
			else if(((string)e.Button.Tag) == "CopyLevel")
			{
				miLevelsCopy_Click(sender, e);
			}
			else if(((string)e.Button.Tag) == "CutLevel")
			{
				miLevelsCut_Click(sender, e);
			}
			else if(((string)e.Button.Tag) == "PasteLevel")
			{
				miLevelsPaste_Click(sender, e);
			}
			else if(((string)e.Button.Tag) == "MoveFirst")
			{
				miLevelsMoveFirst_Click(sender, e);
			}
			else if(((string)e.Button.Tag) == "MoveBack")
			{
				miLevelsMoveBack_Click(sender, e);
			}
			else if(((string)e.Button.Tag) == "MoveForward")
			{
				miLevelsMoveForward_Click(sender, e);
			}
			else if(((string)e.Button.Tag) == "MoveLast")
			{
				miLevelsMoveLast_Click(sender, e);
			}
        }

        #endregion Event handlers
    }
}
