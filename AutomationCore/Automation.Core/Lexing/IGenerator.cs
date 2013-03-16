using System.Collections.Generic;
using Antlr.Runtime;

namespace Automation.Core
{
    public interface IGenerator
    {
        bool IsTrigger(IToken token);
        IEnumerable<IToken> Generate(IToken token);
    }
}
