using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace Automation.Core
{
    public static class TokenExtensions
    {
        public static bool IsEof(this IToken token)
        {
            return token.Type == -1;
        }
    }
}
