using System;

namespace LevelData.Exceptions
{
    public class LevelXmlException : CommonException
    {
        public LevelXmlException(string format, params object[] args)
            : this(null, format, args)
        {
        }

        public LevelXmlException(string message)
            : this(null, message)
        {
        }

        public LevelXmlException(Exception inner, string format, params object[] args)
            : this(inner, String.Format(format, args))
        {
        }

        public LevelXmlException(Exception inner, string message)
            : base(inner, message)
        {
            // main constructor, all others just call this one
        }
    }
}