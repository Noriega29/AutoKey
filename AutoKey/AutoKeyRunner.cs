using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace AutoKey
{
    public class AutoKeyRunner
    {
        // Timer de Windows Forms que disparará los eventos de envío de teclas
        private readonly Timer timer;

        // Configuración actual del AutoKey (qué teclas enviar, a qué proceso)
        private AutoKeyConfig? config;

        // Intervalo de envío de teclas en milisegundos (por defecto 1000 ms = 1 segundo)
        public int Intervalo { get; private set; } = 1000;

        // Constructor: inicializa el timer y lo asocia con el método OnTick
        public AutoKeyRunner()
        {
            timer = new Timer();
            timer.Interval = Intervalo;
            timer.Tick += (s, e) => OnTick(); // Cada tick llamará al envío de teclas
        }

        // =========================
        // API DE WINDOWS
        // =========================

        // Permite obtener la ventana actualmente activa (foreground)
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        // =========================
        // MÉTODOS PRINCIPALES
        // =========================

        // Inicia el envío de teclas con una configuración determinada
        public void Iniciar(AutoKeyConfig config)
        {
            this.config = config;
            timer.Interval = Intervalo; // Asegura que el timer tenga el intervalo actual
            timer.Start(); // Activa el timer
        }

        // Detiene el envío de teclas y limpia la configuración
        public void Detener()
        {
            timer.Stop();
            config = null;
        }

        // Permite ajustar la velocidad de envío de teclas
        public void AjustarVelocidad(int nuevoIntervalo)
        {
            Intervalo = nuevoIntervalo;
            timer.Interval = Intervalo;
        }

        // =========================
        // MÉTODO QUE SE EJECUTA CADA TICK
        // =========================

        private void OnTick()
        {
            // Si no hay configuración o no hay teclas para enviar, no hacer nada
            if (config == null || config.CombinacionInterna == null || config.CombinacionInterna.Count == 0)
                return;

            // =========================
            // COMPROBACIÓN DE FOCO
            // =========================

            Process proc;
            try
            {
                // Intentamos obtener el proceso por su PID
                proc = Process.GetProcessById(config.PID);
            }
            catch
            {
                return; // Si el proceso ya no existe, salimos
            }

            // Si el proceso no tiene ventana principal, no hacemos nada
            if (proc.MainWindowHandle == IntPtr.Zero)
                return;

            // Solo enviamos teclas si la ventana está en primer plano
            if (GetForegroundWindow() != proc.MainWindowHandle)
                return;

            // =========================
            // ENVÍO DE COMBINACIÓN
            // =========================

            // Convertimos la lista de teclas a un string compatible con SendKeys
            string comando = CrearStringSendKeys(config.CombinacionInterna);
            if (!string.IsNullOrEmpty(comando))
            {
                try
                {
                    // Enviamos la combinación de teclas al proceso activo
                    SendKeys.SendWait(comando);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error SendKeys: " + ex.Message);
                }
            }
        }

        // =========================
        // MÉTODO AUXILIAR PARA CREAR STRING DE SENDKEYS
        // =========================
        private string CrearStringSendKeys(List<Keys> teclas)
        {
            var modificadores = new List<Keys>(); // Ctrl, Shift, Alt
            var normales = new List<Keys>();      // Letras, números, etc.

            // Separamos modificadores de teclas normales
            foreach (var k in teclas)
            {
                if (k == Keys.ControlKey || k == Keys.ShiftKey || k == Keys.Menu)
                    modificadores.Add(k);
                else
                    normales.Add(k);
            }

            // Convertimos modificadores a la sintaxis de SendKeys
            string modString = "";
            foreach (var mod in modificadores)
            {
                modString += mod switch
                {
                    Keys.ControlKey => "^", // Ctrl
                    Keys.ShiftKey => "+",   // Shift
                    Keys.Menu => "%",       // Alt
                    _ => ""
                };
            }

            // Convertimos teclas normales a string (entre {} si es más de un carácter)
            string normString = "";
            foreach (var n in normales)
            {
                string s = n.ToString();
                if (s.Length > 1)
                    s = "{" + s + "}";

                normString += s;
            }

            // Combinamos modificadores + teclas normales
            return modString + normString;
        }

        // =========================
        // NUEVAS PROPIEDADES DE SOLO LECTURA
        // =========================

        // Permite consultar si el AutoKeyRunner está activo (timer encendido)
        public bool IsRunning => timer.Enabled;

        // Permite obtener la configuración actual que se está usando
        public AutoKeyConfig? CurrentConfig => config;
    }
}
