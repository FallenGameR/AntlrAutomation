﻿// -----------------------------------------------------------------------
// <copyright file="ILoader.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Automation.Core
{
    public interface ILoader
    {
        AutomationTree Parse(string text);
        string[] Tokenize(string text);
    }
}
