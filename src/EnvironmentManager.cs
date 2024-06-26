﻿using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Linux_EnvironmentVariables_Demo;

public class EnvironmentManager
{
    public string? Get(string variableName)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return System.Environment.GetEnvironmentVariable(variableName, EnvironmentVariableTarget.User);

        string? result = null;
        var proc = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "/bin/bash",
                Arguments = $"-c \"echo ${variableName}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            }
        };

        proc.Start();
        while (!proc.StandardOutput.EndOfStream)
            result = proc.StandardOutput.ReadLine();

        proc.WaitForExit();
        return result;
    }

    public void Set(string variableName, string value)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            System.Environment.SetEnvironmentVariable(variableName, value, EnvironmentVariableTarget.User);
            return;
        }

        var proc = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "/bin/bash",
                Arguments = $"-c \"echo export {variableName}={value}>>~/.bashrc; source ~/.bashrc\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            }
        };
        proc.Start();
        proc.WaitForExit();
    }
}