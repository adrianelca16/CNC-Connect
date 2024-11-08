using System;
using System.Diagnostics;
using System.Text;

public class ConsoleService
{
    public event Action<string> OutputReceived;

    public void ExecuteCommand(string workingDirectory, string command, string arguments)
    {
        var processInfo = new ProcessStartInfo
        {
            FileName = command,
            Arguments = arguments,
            WorkingDirectory = workingDirectory,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        var process = new Process { StartInfo = processInfo };

        process.OutputDataReceived += (sender, args) =>
        {
            if (args.Data != null)
            {
                OutputReceived?.Invoke(args.Data);
                Console.WriteLine(args.Data.ToString());
            }
        };

        process.ErrorDataReceived += (sender, args) =>
        {
            if (args.Data != null)
            {
                OutputReceived?.Invoke(args.Data);
            }
        };

        process.Start();
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
        process.WaitForExit();
    }
}
