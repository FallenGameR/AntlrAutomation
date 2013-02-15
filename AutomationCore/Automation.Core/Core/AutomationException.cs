using System;
using System.Runtime.Serialization;

namespace Automation.Core
{
    [Serializable]
    public class AutomationException : Exception
    {
        public AutomationException() { }
        public AutomationException(string message) : base(message) { }
        public AutomationException(string message, Exception inner) : base(message, inner) { }
        protected AutomationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
