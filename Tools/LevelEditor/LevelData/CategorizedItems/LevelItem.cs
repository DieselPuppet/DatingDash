using LevelData.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelData.CategorizedItems
{   
    public class LevelItem : CategorizedItem
    {
        public int Value
        {
            get;
            set;
        }

        public LevelItem()
            : base()
        {
        }

        public LevelItem(string category, string type, int value = 0)
            : base(category, type)
        {
            Value = value;
        }

        public LevelItem Clone()
        {
            return new LevelItem(Category, Type, Value);
        }
    }
}
