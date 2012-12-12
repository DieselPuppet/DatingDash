using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevelData.Log
{
    public class LogStub : ILog
    {
        public void Write(string message)
        {
        }

        public void Write(string format, params object[] args)
        {
        }
    }
}
