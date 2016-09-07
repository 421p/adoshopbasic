using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

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
                if (args.Data == null) return;
                if (Regex.IsMatch(args.Data, @"server\sstarted")) {
                    _stdin.WriteLine(@"select 1 from pg_roles where rolname = 'shop_ado';");
                }

                if (Regex.IsMatch(args.Data, @"0\srows")) {
                    _stdin.WriteLine("create user shop_ado with password 'shop_ado' createdb;");
                }

                if (Regex.IsMatch(args.Data, @"1\srow") ||
                    Regex.IsMatch(args.Data, @"CREATE\sROLE")) {
                    Load?.Invoke(this, new EventArgs());
                }
            };

            _process.ErrorDataReceived += (sender, args) => Console.WriteLine(args.Data);

            _process.BeginOutputReadLine();
            _process.BeginErrorReadLine();

            return _process;
        }

        public void StopPostgres()
        {
            _stdin.WriteLine(@"\q");
        }
    }
}


