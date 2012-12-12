using System;
using System.Linq;
using System.Xml;
using System.Collections;

using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Globalization;
using LevelData.CategorizedItems;
using LevelData.Exceptions;
using LevelData.SpawnItems;
using LevelData.Utility.Extensions;

namespace LevelData
{
    public class Level
    {
        #region Consts

        private const int LineCount = 5;

        private const string TimeAttribName = "Time";
        private const string ValueAttriName = "Value";
        private const string ScoreAttriName = "Score";
        private const string ParamAttribNameFormat = "Param{0}";
        private const string LinesElementName = "Lines";
        private static readonly string LevelItemsElementName = typeof(LevelItem).Name;
        private static readonly string TasksElementName = typeof(Task).Name;

        #endregion Consts

        #region Properties

        public float Time
        {
            get;
            set;
        }

        public List<LevelItem> Items
        {
            get;
            private set;
        }

        public List<Task> Tasks
        {
            get;
            private set;
        }

        public SpawnLine[] Lines
        {
            get;
            private set;
        }

        public Pack Pack
        {
            get;
            private set;
        }

        #endregion Properties

        public Level(Pack pack, bool isTemplate = false)
        {
            if (pack == null)
                throw new ArgumentNullException("pack");

            if (!isTemplate && Pack.LevelTemplate == null)
                throw new ArgumentException("Pack must have a template level.");

            Pack = pack;
            Items = new List<LevelItem>();
            Tasks = new List<Task>();

            Lines = new SpawnLine[LineCount];
            for (int i = 0; i < LineCount; ++i)
            {
                Lines[i] = new SpawnLine();
            }

            Clear(isTemplate);
        }

        public Level(Pack pack, XmlElement xml, bool isTemplate = false)
            : this(pack, isTemplate)
        {
            Load(xml, isTemplate);
        }

        public void CopyFrom(Level original)
        {
            // убрал, т.к. темплейт пока один на всех
            //if (original.Pack != Pack)
            //    throw new CommonException("Level can be copied only from same pack, because templates for different packs may not match.");

            Time = original.Time;

            Items.Clear();
            Tasks.Clear();

            Items.AddRange(original.Items.Select(itm => itm.Clone()));
            Tasks.AddRange(original.Tasks.Select(tsk => tsk.Clone()));

            for (int i = 0; i < LineCount; i++)
            {
                Lines[i].Copy(original.Lines[i]);
            }
        }

        public void Clear(bool isTemplate = false)
        {
            if (isTemplate)
            {
                Items.Clear();
                Tasks.Clear();                
                ClearLines();

                Time = 0;
            }
            else
            {
                CopyFrom(Pack.LevelTemplate);
            }
        }

        public void ClearLines()
        {
            foreach (var line in Lines)
            {
                line.Clear();
            }
        }

#if LEVEL_EDITOR

        public Level Clone()
        {
            var result = new Level(Pack);
            result.CopyFrom(this);

            return result;
        }

        public void Save(XmlElement xml)
        {
            Action<XmlElement, LevelItem> saveItem = (itemElem, item) =>
            {
                itemElem.SetAttribute(ValueAttriName, item.Value);
            };

            Action<XmlElement, Task> saveTask = (taskElem, task) =>
            {
                taskElem.SetAttribute(ScoreAttriName, task.Score);

                for (int i = 0; i < Task.ParamsCount; i++)
                {
                    string attribName = String.Format(ParamAttribNameFormat, i + 1);
                    taskElem.SetAttribute(attribName, task.Params[i]);
                }
            };

            SaveCategorizedItems(xml, LevelItemsElementName, Items, saveItem);
            SaveCategorizedItems(xml, TasksElementName, Tasks, saveTask);

            xml.SetAttribute(TimeAttribName, Time);

            var linesRoot = xml.AppendChildElement(LinesElementName);
            foreach (var line in Lines)
            {
                line.Save(linesRoot);
            }
        }

        public bool Validate()
        {
            bool result = true;
            foreach (var task in Tasks)
            {
                if (!task.Validate(Pack.Log))
                    result = false;
            }

            foreach (var item in Items)
            {
                if (!item.Validate(Pack.Log))
                    result = false;
            }

            // TODO: lines validation

            return result;
        }

        public void InterchangeLines(SpawnLine line1, SpawnLine line2)
        {
            if (line1 == line2)
                return;

            int index1 = Array.IndexOf(Lines, line1);
            int index2 = Array.IndexOf(Lines, line2);

            if (index1 == -1 || index2 == -1)
                throw new CommonException("Lines are not contained within collection");

            Lines[index1] = line2;
            Lines[index2] = line1;
        }

#endif

        private void Load(XmlElement xml, bool isTemplate)
        {
            // NOTE: перед load должен быть обязательно выполнен сброс к Template (если таковой имеется), иначе останутся значения из предыдущего (те, которые не были сохранены в загружаемый XML).

            Action<XmlElement, LevelItem> initLevelItem = (itemElem, item) =>
            {
                item.Value = itemElem.GetIntAttribute(ValueAttriName);
            };

            Action<XmlElement, Task> initTask = (itemElem, item) =>
            {
                item.Score = itemElem.GetIntAttribute(ScoreAttriName);

                for (int i = 0; i < Task.ParamsCount; i++)
                {
                    string attribName = String.Format(ParamAttribNameFormat, i + 1);
                    item.Params[i] = itemElem.GetIntAttribute(attribName);
                }
            };

            Func<string, bool> validateTaskType = (type) =>
                ValidateAllowedValue(type, Pack.TaskConfig.Types, "task type", isTemplate);

            Func<string, bool> validateTaskCat = (category) =>
                ValidateAllowedValue(category, Pack.TaskConfig.Categories, "task category", isTemplate);

            Func<string, string, bool> canAddItem = (category, type) =>
            {
                if (isTemplate)
                    return true;

                Pack.Log.Write("The item \"{0}.{1}\" was skipped because it is no longer contained in the template.", category, type);
                return false;
            };

            LoadCategorizedItems(xml, LevelItemsElementName, Items, null, null, canAddItem, initLevelItem);
            LoadCategorizedItems(xml, TasksElementName, Tasks, validateTaskCat, validateTaskType, null, initTask);

            Time = xml.GetFloatAttribute(TimeAttribName);

            if (!isTemplate)
            {
                // игнорируем линии в шаблоне, т.к. непонятно, как их мержить, могут появиться дубли, если одно и то же 
                // будет и в шаблоне и во входном файле

                var linesRoot = xml[LinesElementName];
                if (linesRoot == null)
                    throw new LevelXmlException("Lines root element \"{0}\" was not found", LinesElementName);

                var lineElements = linesRoot.GetChildElements();
                if (lineElements.Length > LineCount)
                    Pack.Log.Write("Number of lines in XML {0} is greater than expected {1}. Additional lines will be skipped.", lineElements.Length, LineCount);

                ClearLines();
                for (int i = 0; i < LineCount && i < lineElements.Length; i++)
                {
                    Lines[i].Load(lineElements[i], Pack.Log);
                }
            }
        }

        private static void LoadCategorizedItems<TItem>(XmlElement root, string elementName, IList<TItem> items,
            Func<string, bool> validateCategory, Func<string, bool> validateType, Func<string, string, bool> canAdd, Action<XmlElement, TItem> init)
                where TItem : CategorizedItem, new()
        {
            var node = root[elementName];
            if (node == null)
                return;

            var categories = new List<string>();
            var typesInCategory = new List<string>();
            foreach (XmlElement categoryNode in node.GetChildElements())
            {
                string category = categoryNode.Name;
                if (validateCategory != null && !validateCategory(category))
                    continue;

                if (categories.Contains(category))
                    throw new LevelXmlException("Category {0} for root {1} contained multiple times.", category, elementName);

                categories.Add(category);
                typesInCategory.Clear();                
                
                foreach (XmlElement itemElement in categoryNode.GetChildElements())
                {
                    string type = itemElement.Name;
                    if (validateType != null && !validateType(type))
                        continue;

                    if (typesInCategory.Contains(type))
                        throw new LevelXmlException("Type {0} for root {1}.{2} contained multiple times.", type, elementName, category);

                    typesInCategory.Add(type);

                    var item = items.SingleOrDefault(tsk => tsk.Category == category && tsk.Type == type);

                    if (item == null)
                    {
                        if (canAdd == null || canAdd(category, type))
                        {
                            item = new TItem();
                            items.Add(item);
                        }
                        else
                        {
                            continue;
                        }
                    }

                    item.Type = type;
                    item.Category = category;

                    init(itemElement, item);
                }                
            }
        }

#if LEVEL_EDITOR

        private static void SaveCategorizedItems<TItem>(XmlElement root, string elementName, IEnumerable<TItem> items,
            Action<XmlElement, TItem> saveSpecific)
                where TItem : CategorizedItem
        {
            var categorizedItems = items.GroupBy(item => item.Category,
                (Category, MatchingItems) => new
                {
                    Category,
                    MatchingItems
                }).ToArray();

            XmlElement catRoot = root.AppendChildElement(elementName);
            foreach (var cat in categorizedItems)
            {
                var catElement = catRoot.AppendChildElement(cat.Category);
                foreach (var item in cat.MatchingItems)
                {
                    int count = cat.MatchingItems.Count(it => it.Type == item.Type);
                    if (count > 1)
                        throw new LevelXmlException("Type {0} for root {1}.{2} contained {3} times. Expected single.", item.Type, elementName, cat.Category, count);

                    var itemElem = catElement.AppendChildElement(item.Type);
                    saveSpecific(itemElem, item);
                }
            }
        }
#endif

        private bool ValidateAllowedValue(string value, string[] allowedValues, string name, bool isTemplate)
        {
            if (allowedValues.Contains(value))
                return true;

            Pack.Log.Write("{0}\"{1}\" {2} skipped, because is not defined in the template.", 
                isTemplate ? "Template parsing: " : "", value, name);
            return false;
        }
    }
}
