using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace Automation.Core
{
    public class Grammar
    {
        public string Name { get; private set; }
        public string Folder { get; private set; }
        public string Assembly { get; private set; }
        public string FullText { get { return null; } }
        public string ShortText { get { return null; } }

        public AutomationTree Parse(string text)
        {
            return null;
        }

        public IToken[] Tokenize(string text)
        {
            return null;
        }
    }
}
