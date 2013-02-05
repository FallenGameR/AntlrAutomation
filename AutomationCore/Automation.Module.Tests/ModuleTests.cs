using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Automation.Module.Tests
{
    [TestClass]
    public class ModuleTests
    {
        // Module can be imported without errors
        // Set Grammar accepts full text grammar without errors
        // Set Grammar accepts short text grammar without errors
        // Parse Item parses input with case insensitive grammar name
        // Parse Item parses input with regex matches grammar name
        // Parse Item chooses first matching grammar and print warning if grammar name ambiguity
        // Format custom is used by default
        // Format custom renders output as tree
        // Format table renders output as table
        // Dynamic children access works
    }
}
