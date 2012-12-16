// -----------------------------------------------------------------------
// <copyright file="Parser.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Automation.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Antlr.Runtime;

    public abstract class AutomationParser : Parser
    {
        public AutomationParser(ITokenStream input)
            : base(input)
        {
        }

        public AutomationParser(ITokenStream input, RecognizerSharedState state)
            : base(input, state)
        {
        }
    }
}
