using LevelData.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using LevelData.Utility.Extensions;

namespace LevelData.Config
{
    public class SpawnItemConfig
    {
        private const string SpawnItemRootName = "SpawnItem";
        private const string SpawnItemAttachmentRootName = "SpawnItemAttachment";

        public readonly Dictionary<string, string> Types = new Dictionary<string, string>();
        public readonly Dictionary<string, Dictionary<string, string>> AttachmentTypes = new Dictionary<string, Dictionary<string,string>>();

        public SpawnItemConfig(XmlElement xml)
        {
            LoadTypes(xml);
            LoadAttachments(xml);
        }

        public SpawnItemConfig()
        {
        }

        private void LoadTypes(XmlElement xml)
        {
            var root = xml.GetNotEmptyChild(SpawnItemRootName);
            foreach (XmlElement elemType in root.GetChildElements())
            {
                string type = elemType.Name;
                string imageFileName = null;

#if LEVEL_EDITOR
                if (Types.ContainsKey(type))
                    throw new LevelXmlException("Multiple \"{0}\" have the same type \"{1}\"", SpawnItemRootName, type);                

                imageFileName = elemType.GetStringAttribute("Image");
#endif

                Types.Add(type, imageFileName);
            }
        }

        private void LoadAttachments(XmlElement xml)
        {
            var root = xml.GetNotEmptyChild(SpawnItemAttachmentRootName);

            foreach (XmlElement elemType in root.GetChildElements())
            {
                string type = elemType.Name;
                if (AttachmentTypes.ContainsKey(type))
                    throw new LevelXmlException("\"{0}\" contains multiple \"{1}\" elements", SpawnItemAttachmentRootName, type);

                var attachmentNames = new Dictionary<string, string>();
                AttachmentTypes[type] = attachmentNames;
                foreach (var elemName in elemType.GetChildElements())
                {
                    string name = elemName.Name;
                    string imageFileName = null;

#if LEVEL_EDITOR
                    if (attachmentNames.ContainsKey(name))
                        throw new LevelXmlException("Multiple \"{0}\" of type \"{1}\" have the same name \"{2}\"", SpawnItemAttachmentRootName, type, name);

                    imageFileName = elemName.GetStringAttribute("Image");
#endif
                    attachmentNames.Add(name, imageFileName);
                }
            }
        }
    }
}
