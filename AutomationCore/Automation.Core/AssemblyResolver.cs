// -----------------------------------------------------------------------
// <copyright file="AssemblyResolver.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Automation.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// From http://stackoverflow.com/questions/2664028/how-can-i-get-powershell-added-types-to-use-added-types/2669782#2669782
    /// </summary>
    public static class AssemblyResolver
    {
        private static Dictionary<string, string> KnownAssemblies = new Dictionary<string, string>();

        public static void AddKnownAssembly(string path)
        {
            var name = Path.GetFileNameWithoutExtension(path);
            KnownAssemblies.Add(name, path);
        }

        public static Assembly AssemblyResolveHandler(object sender, ResolveEventArgs args)
        {
            var assemblyName = new AssemblyName(args.Name);

            if (KnownAssemblies.ContainsKey(assemblyName.Name))
            {
                return Assembly.LoadFrom(KnownAssemblies[assemblyName.Name]);
            }

            return null;
        }
    }
}
