using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelData.Log
{
    public interface ILog
    {
        void Write(string message);
        void Write(string format, params object[] args);
    }
}
