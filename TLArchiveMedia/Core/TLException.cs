using System;

namespace TLArchiveMedia
{
    abstract class TLBaseException : Exception
    {
        public TLBaseException() : base() { }
        public TLBaseException(string message) : base(message) { }
        public TLBaseException(string message, System.Exception inner) : base(message, inner) { }
    }

    class TLUIException : TLBaseException
    {
        public TLUIException(string sMessage)
            : base() { }
    }

    class TLCoreException : TLBaseException
    {
        public TLCoreException(string sMessage)
            : base() { }
    }
}
