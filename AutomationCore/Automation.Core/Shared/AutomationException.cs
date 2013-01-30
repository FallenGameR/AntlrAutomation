using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

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
