using LevelData.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using LevelData.Utility.Extensions;

namespace LevelData.Config
{
    public class TaskConfig
    {
        private const string TypeRootName = "TaskType";
        private const string CategoryRootName = "TaskCategory";

        public readonly string[] Types;
        public readonly string[] Categories;

        public TaskConfig(XmlElement xml)
        {
            Types = GetList(xml, TypeRootName);
            Categories = GetList(xml, CategoryRootName);
        }

        public TaskConfig(string[] types, string[] categories)
        {
            if (types == null || types.Length == 0)
                throw new ArgumentException("Task types must be non-empty");

            if (categories == null || categories.Length == 0)
                throw new ArgumentException("Task categories must be non-empty");

            Types = types;
            Categories = categories;
        }

        private static string[] GetList(XmlElement xml, string rootName)
        {
            var listRoot = xml.GetNotEmptyChild(rootName);
            var childElements = listRoot.GetChildElements();
            var result = new string[childElements.Length];

            for (int i = 0; i < childElements.Length; i++)
            {
                string name = childElements[i].Name;

                if (result.Contains(name))
                    throw new LevelXmlException("\"{0}\" contained multiple times in {1} list.", name, rootName);

                result[i] = childElements[i].Name;
            }

            return result;
        }
    }
}
