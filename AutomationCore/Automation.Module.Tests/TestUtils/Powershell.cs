using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Module.Tests.TestUtils
{
    public class Powershell
    {
        private Powershell()
        {

        }

        public static Powershell GetInstance()
        {
            return new Powershell();
        }

        internal static string Script(string p)
        {
            throw new NotImplementedException();
        }

        public static string Out { get; set; }

        public static string Err { get; set; }
    }
}
