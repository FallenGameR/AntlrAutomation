using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Automation.Module.Tests.TestUtils
{
    public sealed class Powershell : IDisposable
    {
        private static readonly string defaultFolder = @"..\..\..\..\Module\";
        private static readonly string scriptFolder;

        static Powershell()
        {
            scriptFolder = Path.GetTempPath() + @"AntlrAutomation\";

            if (!Directory.Exists(scriptFolder))
            {
                Directory.CreateDirectory(scriptFolder);
            }
        }

        private readonly string scriptPath;
        private readonly ProcessStartInfo startInfo;
        private Process process;
        private BlockingCollection<string> allLines;
        private BlockingCollection<string> outLines;
        private BlockingCollection<string> errLines;

        private Powershell()
        {
            this.scriptPath = GetTempScriptPath();
            this.startInfo = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = string.Format("-NoProfile -ExecutionPolicy RemoteSigned -File \"{0}\"", this.scriptPath),
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            };
        }

        public static string Out { get; private set; }

        public static string Err { get; private set; }

        public static Powershell GetInstance()
        {
            return new Powershell();
        }

        private static string GetTempScriptPath()
        {
            return scriptFolder + Path.ChangeExtension(Path.GetRandomFileName(), ".ps1");
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
            // Add default folder
            script = "cd " + defaultFolder + Environment.NewLine + script;

            // Save script content
            File.WriteAllText(this.scriptPath, script);

            // All lines as they arrive
            this.allLines = new BlockingCollection<string>();
            this.outLines = new BlockingCollection<string>();
            this.errLines = new BlockingCollection<string>();

            // Start powershell process that alls script
            using (this.process = Process.Start(this.startInfo))
            using (var taskOut = Task.Factory.StartNew(this.OutputStreamHandler))
            using (var taskErr = Task.Factory.StartNew(this.ErrorsStreamHandler))
            {
                try
                {
                    this.process.WaitForExit();
                    Task.WaitAll(taskOut, taskErr);
                }
                finally
                {
                    this.outLines.CompleteAdding();
                    this.errLines.CompleteAdding();
                    this.allLines.CompleteAdding();
                }
            }

            Out = string.Join(Environment.NewLine, this.outLines.ToArray());
            Err = string.Join(Environment.NewLine, this.errLines.ToArray());
            return string.Join(Environment.NewLine, this.allLines.ToArray());
        }

        private void OutputStreamHandler()
        {
            var line = this.process.StandardOutput.ReadLine();

            while (line != null)
            {
                this.allLines.Add(line);
                this.outLines.Add(line);
                Console.WriteLine("out: {0}", line);
                line = this.process.StandardOutput.ReadLine();
            }
        }

        private void ErrorsStreamHandler()
        {
            var line = this.process.StandardError.ReadLine();

            while (line != null)
            {
                this.allLines.Add(line);
                this.errLines.Add(line);
                Console.WriteLine("err: {0}", line);
                line = this.process.StandardError.ReadLine();
            }
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
