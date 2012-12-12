using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using LevelData.Utility.Extensions;
using LevelData.Exceptions;
using LevelData.Log;

namespace LevelData.SpawnItems
{
    public class SpawnItem
    {
        private const string DelayAttribName = "Delay";
        private const string AbsoluteTimeAttribName = "AbsoluteTime";        

        public float Delay
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public List<SpawnItemAttachment> Attachments
        {
            get;
            private set;
        }

        public SpawnItem(string type, float delay)
        {
            if (type.IsNullOrWhiteSpace())
                throw new ArgumentException("type must be non-empty string");

            if (!Pack.SpawnItemConfig.Types.Keys.Contains(type))
                throw new CommonException("Unknown spawn item type: {0}", type);

            Type = type;
            Delay = delay;
            Attachments = new List<SpawnItemAttachment>();
        }

        public SpawnItem Clone()
        {
            var result = new SpawnItem(Type, Delay);
            foreach (var attach in Attachments)
            {
                result.Attachments.Add(attach.Clone());
            }

            return result;
        }

        public void Save(XmlElement root)
        {
            var elem = root.AppendChildElement(Type);

            elem.SetAttribute(DelayAttribName, Delay);
            foreach (var attach in Attachments)
            {
                attach.Save(elem);
            }
        }

        public static SpawnItem Load(XmlElement elem, ILog log)
        {
            string type = elem.Name;
            if (!Pack.SpawnItemConfig.Types.Keys.Contains(type))
                throw new CommonException("Unknown type: {0}", type);            

            var result = new SpawnItem(type, elem.GetFloatAttribute(DelayAttribName));

            foreach (XmlElement attachElem in elem.GetChildElements())
            {
                try
                {
                    result.Attachments.Add(SpawnItemAttachment.Load(attachElem));
                }
                catch (Exception ex)
                {
                    log.Write("Error while loading attachment: {0}. Skipped.", ex.Message);
                }
            }

            return result;
        }
    }
}
