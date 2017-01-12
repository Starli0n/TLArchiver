using System;

namespace TLArchiver.Core
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
            : base(sMessage) { }
    }

    class TLCoreException : TLBaseException
    {
        public TLCoreException(string sMessage)
            : base(sMessage) { }
    }

    class TLAccessHashException : TLCoreException
    {
        public TLAccessHashException()
            : base("access_hash expected but is null") { }
    }
}
