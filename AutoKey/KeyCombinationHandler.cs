namespace AutoKey
{
    public class KeyCombinationHandler
    {
        private readonly HashSet<Keys> teclasActivas = new();
        private readonly List<Keys> ordenVisual = new();
        private readonly List<Keys> combinacionInterna = new();

        private readonly TextBox targetTextBox;

        public KeyCombinationHandler(TextBox textBox)
        {
            targetTextBox = textBox;
        }

        public void OnKeyDown(object? sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;

            if (!teclasActivas.Contains(e.KeyCode))
            {
                teclasActivas.Add(e.KeyCode);
                ordenVisual.Add(e.KeyCode);

                ActualizarCombinacionInterna();
            }

            targetTextBox.Text = CrearCombinacionVisual();
        }

        public void OnKeyUp(object? sender, KeyEventArgs e)
        {
            if (teclasActivas.Remove(e.KeyCode))
                ordenVisual.Remove(e.KeyCode);
        }

        private string CrearCombinacionVisual()
        {
            var nombres = ordenVisual.Select(NormalizarNombre);
            return string.Join(" + ", nombres);
        }

        private string NormalizarNombre(Keys key)
        {
            return key switch
            {
                Keys.ControlKey => "Ctrl",
                Keys.ShiftKey => "Shift",
                Keys.Menu => "Alt",
                _ => key.ToString()
            };
        }

        private void ActualizarCombinacionInterna()
        {
            combinacionInterna.Clear();

            if (teclasActivas.Contains(Keys.ControlKey)) combinacionInterna.Add(Keys.ControlKey);
            if (teclasActivas.Contains(Keys.ShiftKey)) combinacionInterna.Add(Keys.ShiftKey);
            if (teclasActivas.Contains(Keys.Menu)) combinacionInterna.Add(Keys.Menu);

            var otras = ordenVisual.Where(k =>
                k != Keys.ControlKey &&
                k != Keys.ShiftKey &&
                k != Keys.Menu);

            combinacionInterna.AddRange(otras);
        }

        public List<Keys> GetCombinacionInterna()
        {
            return new List<Keys>(combinacionInterna);
        }

        public void Reiniciar()
        {
            teclasActivas.Clear();
            ordenVisual.Clear();
            combinacionInterna.Clear();
            targetTextBox.Clear();
        }
    }
}
