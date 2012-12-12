using LevelData.Exceptions;
using LevelData.Log;
using LevelData.Utility.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;

namespace LevelData.SpawnItems
{
	public class SpawnLine
	{
        public const float DefaultDelay = 10;

        private static readonly string ElementName = typeof(SpawnLine).Name;

        private List<SpawnItem> _items = new List<SpawnItem>();

        // TODO: should be IReadonlyList, but it is only in .NET 4.0
        public IEnumerable<SpawnItem> Items
        {
            get { return _items; }
        }

		public SpawnLine()
		{
		}

        public void Copy(SpawnLine original)
        {
            _items.Clear();

            foreach (var item in original.Items)
            {
                _items.Add(item.Clone());
            }
        }

        public void Clear()
        {
            _items.Clear();
        }

        public void Save(XmlElement root)
        {
            var elem = root.AppendChildElement(ElementName);

            foreach (var item in Items)
            {
                item.Save(elem);
            }
        }

        public void Load(XmlElement elem, ILog log)
        {
            if (elem.Name != ElementName)
                throw new LevelXmlException("Unexpected element name: \"{0}\". Expected: \"{1}\".", elem.Name, ElementName);

            Clear();
            foreach (XmlElement itemElem in elem.GetChildElements())
            {
                try
                {
                    _items.Add(SpawnItem.Load(itemElem, log));
                }
                catch(Exception ex)
                {
                    log.Write("Error while loading spawn item: {0}. Item is skipped.", ex.Message);
                }
            }
        }

        public SpawnItem InsertItem(string type, SpawnItem prevItem = null)
        {
            return InsertItem(type, GetNewItemIndex(prevItem));
        }

        public SpawnItem InsertItem(SpawnItem item, SpawnItem prevItem = null)
        {
            return InsertItem(item, GetNewItemIndex(prevItem));
        }

        public SpawnItem InsertItem(string type, int index)
        {
            float delay = index == 0 ? 0 : DefaultDelay;
            var item = new SpawnItem(type, delay);

            return InsertItem(item, index);
        }

        public SpawnItem InsertItem(SpawnItem item, int index)
        {
            if (_items.Contains(item))
                throw new ArgumentException("The item is already contained in the collection");

            _items.Insert(index, item);
            return item;
        }

        public void RemoveItem(SpawnItem item)
        {
            if (!_items.Remove(item))
                throw new CommonException("Item is not contained within current line");
        }

        public float GetAbsoluteTime(SpawnItem item)
        {
            float result = 0;
            foreach (var curItem in Items)
            {
                result += curItem.Delay;

                if (curItem == item)
                    return result;
            }

            throw new CommonException("Item is not contained within current line");
        }

        private int GetNewItemIndex(SpawnItem prevItem = null)
        {
            int index = 0;
            if (prevItem != null)
            {
                index = _items.IndexOf(prevItem);
                if (index == -1)
                    throw new CommonException("Item is not contained within current line");
                else
                    index += 1;
            }

            return index;
        }
	}
}
