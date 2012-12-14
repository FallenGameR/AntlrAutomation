using System;
using Automation.Core;

namespace Sample.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var newDomain = AppDomain.CreateDomain("NewDomain");

            var loader = (ILoader)newDomain.CreateInstanceFromAndUnwrap(
                @"d:\Archive\Projects\AntlrAutomation\AutomationCore\Sample.Parser\bin\Debug\Sample.Parser.dll",
                "Sample.Parser.Loader");

            var filePath = @"d:\Archive\Projects\AntlrAutomation\Info\simpleton.txt";
            var tree = loader.Parse(filePath);

            Console.WriteLine(tree.ToStringTree());

            AppDomain.Unload(newDomain);
        }
    }
}
