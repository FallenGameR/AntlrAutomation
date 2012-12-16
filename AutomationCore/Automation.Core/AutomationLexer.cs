// -----------------------------------------------------------------------
// <copyright file="Lexer.cs" company="">
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

    public abstract class AutomationLexer : Lexer
    {
        public AutomationLexer()
            : base()
        {
        }

        public AutomationLexer(ICharStream input)
            : base(input)
        {
        }

        public AutomationLexer(ICharStream input, RecognizerSharedState state)
            : base(input, state)
        {
        }
    }
}
