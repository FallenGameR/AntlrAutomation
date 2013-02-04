using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Module.Tests.TestUtils
{
    public sealed class Powershell: IDisposable
    {
        private Powershell()
        {
            // create temp file
        }

        public static string Out { get; private set; }

        public static string Err { get; private set; }

        public static Powershell GetInstance()
        {
            return new Powershell();
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
            // Write to temp file, call, process out and err
            return null;
        }


        public void Dispose()
        {
            // remove temp file
        }
    }
}
