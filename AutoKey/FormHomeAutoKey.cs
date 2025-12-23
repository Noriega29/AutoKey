using Gma.System.MouseKeyHook;
using System.Diagnostics;
using Timer = System.Windows.Forms.Timer;

namespace AutoKey
{
    public partial class FormHomeAutoKey : Form
    {
        // Manejador de procesos
        private readonly ProcessManager processManager = new ProcessManager();
        // Timer para refrescar la lista de procesos
        private readonly Timer timer;
        // Manejador de combinación de teclas
        private readonly KeyCombinationHandler keyHandler;
        // Configuración actual
        private AutoKeyConfig? currentConfig;
        // Manejador de AutoKey
        private readonly AutoKeyRunner autoKeyRunner = new AutoKeyRunner();
        // Intervalos permitidos
        private const int INTERVALO_MIN = 100;
        private const int INTERVALO_MAX = 5000;
        // Hook global de teclado
        private IKeyboardMouseEvents globalHook;

        public FormHomeAutoKey()
        {
            InitializeComponent(); // Inicializa los controles

            // ------------------------------
            // Hook global para F1
            // ------------------------------
            globalHook = Hook.GlobalEvents();
            globalHook.KeyDown += GlobalHook_KeyDown;

            // Estado inicial
            labelEstado.Text = "AutoKey inactivo";

            // Configurar ListView
            listViewProcesos.View = View.Details;
            listViewProcesos.FullRowSelect = true;
            listViewProcesos.GridLines = true;
            listViewProcesos.Columns.Clear();
            listViewProcesos.Columns.Add("Nombre", 200);
            listViewProcesos.Columns.Add("ProcessName", 150);
            listViewProcesos.Columns.Add("PID", 70);
            listViewProcesos.MultiSelect = false;
            listViewProcesos.SelectedIndexChanged += (s, e) => UpdateStartButtonState();

            // Timer de refresco de procesos
            timer = new Timer();
            timer.Interval = 500;
            timer.Tick += (s, e) => CargarProcesos();
            timer.Start();

            // Refrescar lista cuando cambie el checkbox
            checkBoxVerSubprocesos.CheckedChanged += (s, e) => CargarProcesos();

            // Key handler para TextBox de asignación de teclas
            keyHandler = new KeyCombinationHandler(textBoxAsignacionTeclas);
            textBoxAsignacionTeclas.KeyDown += keyHandler.OnKeyDown;
            textBoxAsignacionTeclas.KeyUp += keyHandler.OnKeyUp;
            textBoxAsignacionTeclas.TextChanged += (s, e) => UpdateStartButtonState();

            // Velocidad de intervalos
            ActualizarLabelIntervalo();

            // Estado inicial del botón de inicio
            UpdateStartButtonState();

            // Asignar estos eventos al label (puedes hacerlo desde el diseñador o en el constructor)
            label2.MouseEnter += Label2_MouseEnter;
            label2.MouseLeave += Label2_MouseLeave;
        }

        // ------------------------------
        // Evento global de teclado
        // ------------------------------
        private void GlobalHook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                e.Handled = true;
                ToggleAutoKey();
            }
        }

        // ------------------------------
        // Método para iniciar/detener AutoKey
        // ------------------------------
        private void ToggleAutoKey()
        {
            Invoke((MethodInvoker)(() =>
            {
                if (!autoKeyRunner.IsRunning)
                {
                    // Iniciar AutoKey
                    if (listViewProcesos.SelectedItems.Count > 0 && keyHandler.GetCombinacionInterna().Count > 0)
                    {
                        var item = listViewProcesos.SelectedItems[0];
                        int pid = int.Parse(item.SubItems[2].Text);

                        var combinacionInterna = keyHandler.GetCombinacionInterna();
                        string combinacionVisual = textBoxAsignacionTeclas.Text;

                        currentConfig = new AutoKeyConfig(pid, combinacionVisual, combinacionInterna);
                        autoKeyRunner.Iniciar(currentConfig);

                        labelEstado.Text = "AutoKey activo";
                        labelEstado.ForeColor = Color.Green;
                        buttonIniciarAutoKey.Text = "Detener";
                        SoundManager.ReproducirInicio();
                    }
                }
                else
                {
                    // Detener AutoKey
                    autoKeyRunner.Detener();
                    currentConfig = null;

                    labelEstado.Text = "AutoKey inactivo";
                    labelEstado.ForeColor = Color.Red;
                    buttonIniciarAutoKey.Text = "Iniciar";
                    SoundManager.ReproducirDetener();
                }
            }));
        }

        // ------------------------------
        // Botón limpiar texto
        // ------------------------------
        private void buttonLimpiarTexto_Click(object? sender, EventArgs e)
        {
            autoKeyRunner.Detener();
            currentConfig = null;

            labelEstado.Text = "AutoKey inactivo";
            labelEstado.ForeColor = Color.Red;
            buttonIniciarAutoKey.Text = "Iniciar";

            textBoxAsignacionTeclas.Clear();
            keyHandler.Reiniciar();

            UpdateStartButtonState();
        }

        // ------------------------------
        // Cargar lista de procesos
        // ------------------------------
        private void CargarProcesos()
        {
            bool incluirSub = checkBoxVerSubprocesos.Checked;
            var procesos = processManager.ObtenerProcesosPrincipales(incluirSub);
            MostrarProcesos(procesos);
        }

        private void MostrarProcesos(List<ProcessInfo> procesos)
        {
            var procesosActuales = procesos.ToDictionary(p => p.PID);

            // Eliminar procesos que ya no están
            for (int i = listViewProcesos.Items.Count - 1; i >= 0; i--)
            {
                if (int.TryParse(listViewProcesos.Items[i].SubItems[2].Text, out int pid))
                {
                    if (!procesosActuales.ContainsKey(pid))
                        listViewProcesos.Items.RemoveAt(i);
                }
            }

            // Agregar o actualizar procesos
            foreach (var p in procesosActuales.Values)
            {
                bool encontrada = false;
                foreach (ListViewItem item in listViewProcesos.Items)
                {
                    if (int.TryParse(item.SubItems[2].Text, out int pid) && pid == p.PID)
                    {
                        item.SubItems[0].Text = p.Nombre;
                        item.SubItems[1].Text = p.ProcessName;
                        encontrada = true;
                        break;
                    }
                }

                if (!encontrada)
                {
                    var newItem = new ListViewItem(p.Nombre);
                    newItem.SubItems.Add(p.ProcessName);
                    newItem.SubItems.Add(p.PID.ToString());
                    listViewProcesos.Items.Add(newItem);
                }
            }
        }

        // ------------------------------
        // Actualizar estado botón inicio
        // ------------------------------
        private void UpdateStartButtonState()
        {
            bool hayCombinacion = keyHandler.GetCombinacionInterna().Count > 0;
            bool haySeleccion = listViewProcesos.SelectedItems.Count > 0;

            buttonIniciarAutoKey.Enabled = hayCombinacion && haySeleccion;
        }

        // ------------------------------
        // Botón iniciar/detener AutoKey
        // ------------------------------
        private void buttonIniciarAutoKey_Click(object sender, EventArgs e)
        {
            ToggleAutoKey();
        }

        // ------------------------------
        // Botones de velocidad
        // ------------------------------
        private void buttonMasVelocidad_Click(object sender, EventArgs e)
        {
            int nuevoIntervalo = autoKeyRunner.Intervalo - 100;
            if (nuevoIntervalo < INTERVALO_MIN)
                nuevoIntervalo = INTERVALO_MIN;

            autoKeyRunner.AjustarVelocidad(nuevoIntervalo);
            ActualizarLabelIntervalo();
        }

        private void buttonMenosVelocidad_Click(object sender, EventArgs e)
        {
            int nuevoIntervalo = autoKeyRunner.Intervalo + 100;
            if (nuevoIntervalo > INTERVALO_MAX)
                nuevoIntervalo = INTERVALO_MAX;

            autoKeyRunner.AjustarVelocidad(nuevoIntervalo);
            ActualizarLabelIntervalo();
        }

        private void ActualizarLabelIntervalo()
        {
            int ms = autoKeyRunner.Intervalo;

            if (ms == 1000)
                labelIntervalos.Text = "1 segundo";
            else if (ms > 1000)
                labelIntervalos.Text = $"{ms / 1000.0:0.##} segundos";
            else
                labelIntervalos.Text = $"{ms} ms";
        }

        // ------------------------------
        // Botón ayuda
        // ------------------------------
        private void buttonHelp_Iniciar_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "1. Selecciona un proceso de la lista.\n" +
                "2. Asigna una tecla o combinación de teclas.\n" +
                "3. Ajusta el intervalo de velocidad si lo deseas.\n" +
                "4. Presiona 'Iniciar' para activar AutoKey.\n\n" +
                "💡 Extra: Puedes iniciar o detener AutoKey presionando F1 directamente en la ventana del proceso destino.",
                "¿Cómo funciona la app?",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }


        // ------------------------------
        // Liberar hook al cerrar la app
        // ------------------------------
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            SoundManager.ReproducirCerrar();
            globalHook.KeyDown -= GlobalHook_KeyDown;
            globalHook.Dispose();
            base.OnFormClosing(e);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            try
            {
                // Reemplaza la URL con la de tu GitHub
                string url = "https://github.com/Noriega29";

                // Abrir en el navegador predeterminado
                System.Diagnostics.Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true // Esto es importante para abrir URLs
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo abrir el enlace: " + ex.Message);
            }
        }

        // Cuando el cursor entra al label
        private void Label2_MouseEnter(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Green; // Cambia a azul
            label2.Cursor = Cursors.Hand;  // Cambia el cursor a mano
        }

        // Cuando el cursor sale del label
        private void Label2_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Black; // Vuelve al color original
            label2.Cursor = Cursors.Default; // Cursor normal
        }
    }
}
