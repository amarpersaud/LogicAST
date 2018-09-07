using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Base.Exceptions
{
    [System.Serializable]
    public class UnexpectedSymbolException : Exception
    {
        public UnexpectedSymbolException() { }
        public UnexpectedSymbolException(string message) : base(message) { }
        public UnexpectedSymbolException(string message, Exception inner) : base(message, inner) { }
        protected UnexpectedSymbolException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
