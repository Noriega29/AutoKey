using System.Diagnostics;

namespace AutoKey
{
    public class ProcessManager
    {
        public List<ProcessInfo> ObtenerProcesosPrincipales(bool incluirSubprocesos = false)
        {
            return Process.GetProcesses()
                .Where(p =>
                {
                    try
                    {
                        if (incluirSubprocesos)
                            return !string.IsNullOrWhiteSpace(p.MainModule?.FileVersionInfo.FileDescription);

                        // solo procesos con ventana
                        return !string.IsNullOrWhiteSpace(p.MainWindowTitle) &&
                               !string.IsNullOrWhiteSpace(p.MainModule?.FileVersionInfo.FileDescription);
                    }
                    catch
                    {
                        return false;
                    }
                })
                .Select(p =>
                {
                    string? descripcion = null;
                    try
                    {
                        descripcion = p.MainModule?.FileVersionInfo.FileDescription;
                    }
                    catch { }

                    return new ProcessInfo
                    {
                        Nombre = descripcion ?? string.Empty,
                        ProcessName = p.ProcessName,
                        PID = p.Id
                    };
                })
                .ToList();
        }
    }
}
