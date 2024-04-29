using System.Diagnostics;

namespace FlowSynx.Environment;

public class ProcessHandler : IProcessHandler
{
    public bool IsRunning(string processName, string machineAddress)
    {
        var processes = Process.GetProcessesByName(processName, machineAddress);
        return processes.Length != 0;
    }

    public void Terminate(string processName, string machineAddress)
    {
        var processes = Process.GetProcessesByName(processName, machineAddress);
        if (processes.Length == 0) return;
        foreach (var process in processes)
        {
            process.Kill();
        }
    }

    public bool IsStopped(string processName, string machineAddress, bool force)
    {
        if (!IsRunning(processName, machineAddress)) 
            return true;

        if (!force) 
            return false;

        Terminate(processName, machineAddress);
        return true;
    }
}