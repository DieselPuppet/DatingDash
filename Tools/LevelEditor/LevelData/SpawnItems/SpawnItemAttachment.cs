using LevelData.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using LevelData.Utility.Extensions;

namespace LevelData.SpawnItems
{
    public class SpawnItemAttachment
    {
        private const string NameAttribName = "Name";
        private string _name;

        public string Type
        {
            get;
            private set;
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (!Pack.SpawnItemConfig.AttachmentTypes[Type].ContainsKey(value))
                    throw new CommonException("Attachment name \"{0}\" is unknown for type \"{1}\"", value, Type);

                _name = value;
            }
        }

        public SpawnItemAttachment(string type, string name)
        {
            if (type.IsNullOrWhiteSpace())
                throw new ArgumentException("type must be non-empty string");

            if (!Pack.SpawnItemConfig.AttachmentTypes.Keys.Contains(type))
                throw new CommonException("Unknown attachment type: {0}", type);

            if (name.IsNullOrWhiteSpace())
                throw new ArgumentException("name must be non-empty string");

            Type = type;
            Name = name;
        }

        public SpawnItemAttachment Clone()
        {
            return new SpawnItemAttachment(Type, Name);
        }

        public void Save(XmlElement root)
        {
            var elem = root.AppendChildElement(Type);
            elem.SetAttribute(NameAttribName, Name);
        }

        public static SpawnItemAttachment Load(XmlElement elem)
        {
            return new SpawnItemAttachment(elem.Name, elem.GetStringAttribute(NameAttribName));
        }
    }
}
