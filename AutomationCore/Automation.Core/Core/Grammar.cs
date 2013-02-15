namespace Automation.Core
{
    public class Grammar
    {
        public string Name { get; private set; }
        public string Folder { get; private set; }
        public string Assembly { get; private set; }
        public string FullText { get { return null; } }
        public string ShortText { get { return null; } }

        public AutomationTree Parse(string text)
        {
            return null;
        }

        public string[] Tokenize(string text)
        {
            return null;
        }

        public void UnloadParserAppDomain()
        {
        }
    }
}
