// -----------------------------------------------------------------------
// <copyright file="ILoader.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Automation.Core
{
    using System.Collections.Generic;

    public interface ILoader
    {
        AutomationTree Parse(string filePath);
        IEnumerable<string> Tokenize(string filePath);
    }
}
