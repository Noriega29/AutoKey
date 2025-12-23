using System;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace AutoKey
{
    public class ProcessUpdater
    {
        private readonly Timer timer;
        private readonly ProcessManager processManager;

        public event Action<List<ProcessInfo>>? ProcesosActualizados;

        public ProcessUpdater(int intervaloMs = 2000)
        {
            processManager = new ProcessManager();
            timer = new System.Windows.Forms.Timer();
            timer.Interval = intervaloMs;
            timer.Tick += Timer_Tick;
        }

        public void Iniciar() => timer.Start();
        public void Detener() => timer.Stop();

        private void Timer_Tick(object? sender, EventArgs e)
        {
            List<ProcessInfo> procesos = processManager.ObtenerProcesosPrincipales();
            ProcesosActualizados?.Invoke(procesos);
        }
    }
}
