using System;
using System.Xml;
using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using LevelData.Exceptions;
using LevelData.Log;
using LevelData.Utility.Extensions;
using LevelData.Config;


namespace LevelData
{
	public class Pack
	{
        private static bool _hasTemplateErrors = false;

        public static Level LevelTemplate
        {
            get;
            private set;
        }

        public static TaskConfig TaskConfig
        {
            get;
            private set;
        }

        public static SpawnItemConfig SpawnItemConfig
        {
            get;
            private set;
        }

        public List<Level> Levels
        {
            get;
            private set;
        }

        public ILog Log
        {
            get;
            private set;
        }

		public Pack(string levelTemplateXml, string packXml, ILog log = null)
		{
            Log = log ?? new LogStub();

            // не загружаем template повторно, чтобы если он был изменён вне редактора, не плодить паки с разными версиями template
            // TODO: можно добавить кнопку перезагрузки template, но при условии, что все паки закрыты.
            LoadTemplate(levelTemplateXml);

            Levels = new List<Level>();

            if (!packXml.IsNullOrWhiteSpace())
                Load(packXml);
            else
                Levels.Add(new Level(this));
		}

#if LEVEL_EDITOR

        public bool Save(string path)
        {
            if (!Validate())
                return false;

            Log.Write("Saving...");
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.AppendChild(doc.CreateXmlDeclaration("1.0", "utf-8", null));
                
                XmlElement rootElement = doc.CreateElement("Levels");
                for (int i = 0; i < Levels.Count; ++i)
                {
                    Log.Write("Level {0}", i + 1);

                    XmlElement level_element = doc.CreateElement(string.Format("Level{0}", i + 1));
                    Levels[i].Save(level_element);
                    rootElement.AppendChild(level_element);
                }

                doc.AppendChild(rootElement);

                doc.Save(path);
            }
            catch (Exception ex)
            {
                Log.Write("Error while saving: {0}", ex.Message);
                return false;
            }

            return true;
        }

		public Level AddLevel()
		{
            var level = new Level(this);
            Levels.Add(level);

            return level;
		}

		public Level InsertLevel(int index)
		{
            var level = new Level(this);
			Levels.Insert(index, level);

            return level;
		}

        public bool Validate()
        {
            Log.Write("Validating...");

            bool isValid = true;
            for (int i = 0; i < Levels.Count; i++)
            {
                Log.Write("Level {0}", i + 1);

                if (!Levels[i].Validate())
                    isValid = false;
            }

            return isValid;
        }
#endif
        private void Load(string xml)
        {
            Log.Write("Loading levels...");

            try
            {
                var doc = new XmlDocument();
                doc.LoadXml(xml);

                foreach (XmlElement elem in doc.DocumentElement.GetChildElements())
                {
                    if (elem.Name.StartsWith("Level"))
                    {
                        Log.Write(elem.Name);
                        Levels.Add(new Level(this, elem));
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Critical error while loading levels: {0}", ex.Message);
            }
        }

        private void LoadTemplate(string xml)
        {
            if (LevelTemplate != null && !_hasTemplateErrors)
                return;

            if (LevelTemplate == null)
                Log.Write("Loading template...");
            else
                Log.Write("Reloading template (previous attempt failed)...");

            try
            {
                if (xml.IsNullOrWhiteSpace())
                    throw new CommonException("Level template is empty");

                var docTemplate = new XmlDocument();
                docTemplate.LoadXml(xml);

                TaskConfig = new TaskConfig(docTemplate.DocumentElement);
                SpawnItemConfig = new SpawnItemConfig(docTemplate.DocumentElement);
                LevelTemplate = new Level(this, docTemplate.DocumentElement["Level"], true);

                _hasTemplateErrors = false;
            }
            catch (Exception ex)
            {
                // загружаем пустой, если не удалось получить из файла
                if (TaskConfig == null)
                    TaskConfig = new TaskConfig(new string[1], new string[1]);

                if (LevelTemplate == null)
                    LevelTemplate = new Level(this, true);

                if (SpawnItemConfig == null)
                    SpawnItemConfig = new SpawnItemConfig();

                _hasTemplateErrors = true;

                Log.Write("Error while loading level template: {0}", ex.Message);
            }
        }     
	}
}
