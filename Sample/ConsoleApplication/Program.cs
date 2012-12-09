using ParserLibrary;
using Antlr.Runtime.Tree;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var file = @"d:\Archive\Projects\AntlrAutomation\Sample\Resources\simpleton.txt";
            var loader = new Loader();
            loader.Parse(file);
        }
    }
}
