namespace AutoKey
{
    public class AutoKeyConfig
    {
        public int PID { get; set; }
        public string CombinationVisual { get; set; } // Lo que verá el usuario en el TextBox
        public List<Keys> CombinacionInterna { get; set; } = new List<Keys>(); // Lista interna de teclas, usada por AutoKeyRunner

        // Constructor
        public AutoKeyConfig(int pid, string combinationVisual, List<Keys> combinacionInterna)
        {
            PID = pid;
            CombinationVisual = combinationVisual;
            CombinacionInterna = combinacionInterna;
        }
    }
}
