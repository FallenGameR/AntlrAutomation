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
                @"d:\Archive\Projects\AntlrAutomation\Sample\ParserLibrary\bin\Debug\ParserLibrary.dll",
                "ParserLibrary.Loader");

            var filePath = @"d:\Archive\Projects\AntlrAutomation\Sample\Resources\simpleton.txt";
            var tree = loader.Parse(filePath);

            Console.WriteLine(tree.ToStringTree());

            AppDomain.Unload(newDomain);
        }
    }
}
