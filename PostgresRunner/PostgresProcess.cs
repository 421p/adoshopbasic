using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PostgresRunner {
    public class PostgresProcess {

        private StreamWriter _stdin;
        private Process _process;

        public event EventHandler Load;
        public event EventHandler Halted;

        public Process StartPostgres()
        {
            _process = new Process {
                StartInfo = {
                    FileName = @"..\..\..\pgsql.cmd",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    CreateNoWindow = true,
                    UseShellExecute = false
                },
                EnableRaisingEvents = true
            };

            _process.Start();

            _process.Exited += Halted;

            _stdin = _process.StandardInput;

            _process.OutputDataReceived += (sender, args) => {
                Console.WriteLine(args.Data);

                if (args.Data == null) return;
                if (Regex.IsMatch(args.Data, @"quit\sand\sshutdown\sserver")) {
                    Load?.Invoke(this, new EventArgs());
                }
            };

            _process.BeginOutputReadLine();

            return _process;
        }

        public void StopPostgres()
        {
            _stdin.WriteLine(@"\q");
        }
    }
}


