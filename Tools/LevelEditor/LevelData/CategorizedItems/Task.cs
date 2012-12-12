using LevelData.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelData.CategorizedItems
{   
    public class Task : CategorizedItem
    {
        public const int ParamsCount = 3;

        public int[] Params
        {
            get;
            private set;
        }

        public int Score
        {
            get;
            set;
        }

        public Task() : this(null, null)
        {
        }

        public Task(string category, string type, int score = 0, int[] parameters = null)
            : base(category, type)
        {
            Score = score;
            Params = new int[ParamsCount];

            if (parameters != null)
                parameters.CopyTo(Params, 0);
        }

        public Task Clone()
        {
            return new Task(Category, Type, Score, Params);
        }
    }
}
