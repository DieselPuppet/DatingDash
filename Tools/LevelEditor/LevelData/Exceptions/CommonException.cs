using System;
using System.Runtime.Serialization;

namespace LevelData.Exceptions
{
    public class CommonException : Exception
    {
        public CommonException(string format, params object[] args)
            : this(null, format, args)
        {
        }

        public CommonException(string message)
            : this(null, message)
        {
        }

        public CommonException(Exception inner, string format, params object[] args)
            : this(inner, String.Format(format, args))
        {
        }

        public CommonException(Exception inner, string message)
            : base(message, inner)
        {
            // main constructor, all others just call this one
        }
    }
}