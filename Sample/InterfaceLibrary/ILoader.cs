// -----------------------------------------------------------------------
// <copyright file="ILoader.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace InterfaceLibrary
{
    using Antlr.Runtime.Tree;

    public interface ILoader
    {
        CommonTree Parse(string filePath);
    }
}
