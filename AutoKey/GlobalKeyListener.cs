using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AutoKey
{
    /// <summary>
    /// Clase para escuchar teclas globalmente (incluso si la app no tiene foco)
    /// y alternar el inicio/pausa de AutoKeyRunner usando F1.
    /// </summary>
    public class GlobalKeyListener : IDisposable
    {
        private readonly AutoKeyRunner runner; // Referencia al AutoKeyRunner que vamos a controlar
        private IntPtr hookId = IntPtr.Zero;   // Identificador del hook
        private LowLevelKeyboardProc proc;     // Delegate del callback del hook

        public GlobalKeyListener(AutoKeyRunner runner)
        {
            this.runner = runner;
            proc = HookCallback;
            hookId = SetHook(proc); // Activamos el hook global al crear la clase
        }

        // =========================
        // DLL IMPORTS
        // =========================

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private const int WH_KEYBOARD_LL = 13; // Hook global de teclado
        private const int WM_KEYDOWN = 0x0100; // Evento de tecla presionada
        private const int WM_SYSKEYDOWN = 0x0104; // Evento de tecla presionada con Alt

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        // =========================
        // ACTIVACIÓN DEL HOOK
        // =========================
        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using var curProcess = Process.GetCurrentProcess();
            using var curModule = curProcess.MainModule!;
            // Se pasa el módulo actual para registrar el hook global de teclado
            return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
        }

        // =========================
        // CALLBACK DEL HOOK
        // =========================
        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            // Solo procesamos si el hook es válido y es un evento de tecla presionada
            if (nCode >= 0 && (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN))
            {
                int vkCode = Marshal.ReadInt32(lParam); // Código virtual de la tecla

                // Detectamos si se presionó F1
                if (vkCode == (int)Keys.F1)
                {
                    if (runner == null) return CallNextHookEx(hookId, nCode, wParam, lParam);

                    // Alternamos entre iniciar y detener AutoKeyRunner
                    if (runner.IsRunning)
                        runner.Detener();
                    else
                        runner.Iniciar(runner.CurrentConfig!);

                    // Retornamos 1 para evitar que F1 haga otra acción
                    return (IntPtr)1;
                }
            }

            // Pasamos el evento al siguiente hook
            return CallNextHookEx(hookId, nCode, wParam, lParam);
        }

        // =========================
        // LIMPIEZA
        // =========================
        public void Dispose()
        {
            if (hookId != IntPtr.Zero)
            {
                UnhookWindowsHookEx(hookId);
                hookId = IntPtr.Zero;
            }
        }
    }
}
