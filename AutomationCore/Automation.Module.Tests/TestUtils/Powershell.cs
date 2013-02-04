using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Module.Tests.TestUtils
{
    public sealed class Powershell: IDisposable
    {
        private readonly string scriptPath;

        private Powershell()
        {
            this.scriptPath = GetTempScriptPath();
        }

        public static string Out { get; private set; }

        public static string Err { get; private set; }

        public static Powershell GetInstance()
        {
            return new Powershell();
        }

        private static string GetTempScriptPath()
        {
            return
                Path.GetTempPath() +
                @"AntlrAutomation\" +
                Path.ChangeExtension(Path.GetRandomFileName(), ".ps1");
        }

        public static string Script(string text)
        {
            using (var powershell = GetInstance())
            {
                return powershell.Execute(text);
            }
        }

        public string Execute(string script)
        {
            File.WriteAllText(this.scriptPath, script);
            // Write to temp file, call, process out and err
            return null;
        }

        public void Dispose()
        {
            if (File.Exists(this.scriptPath))
            {
                File.Delete(this.scriptPath);
            }
        }
    }
}
