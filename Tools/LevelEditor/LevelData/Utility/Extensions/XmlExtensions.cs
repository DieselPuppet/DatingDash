using LevelData.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml;

namespace LevelData.Utility.Extensions
{
    public static class XmlExtensions
    {
        public static void SetAttribute(this XmlElement elem, string name, int value)
        {
            elem.SetAttribute(name, value.ToString(CultureInfo.InvariantCulture));
        }

        public static void SetAttribute(this XmlElement elem, string name, float value)
        {
            elem.SetAttribute(name, value.ToString(CultureInfo.InvariantCulture));
        }

        public static string GetStringAttribute(this XmlElement elem, string name, bool isRequired = true)
        {
            string value = elem.GetAttribute(name);
            if (isRequired && value.IsNullOrWhiteSpace())
                throw new LevelXmlException("\"{0}\" attribute is missing for \"{1}\"", name, elem.Name);

            return value;
        }

        public static int GetIntAttribute(this XmlElement elem, string name, bool isRequired = true)
        {
            int result;
            string value = elem.GetStringAttribute(name, isRequired);

            if (!int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out result))
                throw new LevelXmlException("Invalid integer value \"{0}\" of attribute \"{1}\" for \"{2}\"", value, name, elem.Name);

            return result;
        }

        public static float GetFloatAttribute(this XmlElement elem, string name, bool isRequired = true)
        {
            float result;
            string value = elem.GetStringAttribute(name, isRequired);

            if (!float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result))
                throw new LevelXmlException("Invalid float value \"{0}\" of attribute \"{1}\" for \"{2}\"", value, name, elem.Name);

            return result;
        }

        public static XmlElement AppendChildElement(this XmlElement root, string name)
        {
            var elem = root.OwnerDocument.CreateElement(name);
            root.AppendChild(elem);

            return elem;
        }

        public static XmlElement GetNotEmptyChild(this XmlNode node, string elementName)
        {
            var result = node[elementName];
            if (result == null)
                throw new LevelXmlException("{0} was not found as a child of {1} in input XML", elementName, node.Name);

            if (!result.OfType<XmlElement>().Any())
                throw new LevelXmlException("{0} contains no child elements in input XML", elementName);

            return result;
        }

        public static XmlElement[] GetChildElements(this XmlElement element)
        {
            return element.OfType<XmlElement>().ToArray();
        }
    }
}
