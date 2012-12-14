using Antlr.Runtime.Tree;
using System;
using InterfaceLibrary;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var newDomain = AppDomain.CreateDomain("NewDomain");

            var loader = (ILoader)newDomain.CreateInstanceFromAndUnwrap(
                @"d:\Archive\Projects\AntlrAutomation\AutomationCore\Sample.Parser\bin\Debug\Sample.Parser.dll",
                "ParserLibrary.Loader");

            var filePath = @"d:\Archive\Projects\AntlrAutomation\AutomationCore\Resources\simpleton.txt";
            var tree = loader.Parse(filePath);

            Console.WriteLine(tree.ToStringTree());

            AppDomain.Unload(newDomain);
        }
    }
}
