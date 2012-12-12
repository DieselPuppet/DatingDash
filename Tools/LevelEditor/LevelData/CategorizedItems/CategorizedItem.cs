using LevelData.Log;
using LevelData.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelData.CategorizedItems
{
    public abstract class CategorizedItem
    {
        public string Category
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public CategorizedItem()
        {
        }

        public CategorizedItem(string category, string type)
        {
            Category = category;
            Type = type;
        }

        public virtual bool Validate(ILog log)
        {
            bool result = true;
            if (Type.IsNullOrWhiteSpace())
            {
                log.Write("{0} type is empty.", GetType().Name);
                result = false;
            }

            if (Category.IsNullOrWhiteSpace())
            {
                log.Write("{0} category is empty.", GetType().Name);
                result = false;
            }

            return result;
        }
    }
}
