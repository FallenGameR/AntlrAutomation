// -----------------------------------------------------------------------
// <copyright file="ILoader.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Automation.Core
{
    using Antlr.Runtime.Tree;

    public interface ILoader
    {
        AutomationTree Parse(string filePath);
    }
}
