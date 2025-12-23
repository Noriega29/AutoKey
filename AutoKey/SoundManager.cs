using System.Media;

namespace AutoKey
{
    public static class SoundManager
    {
        private static readonly string pathInicio = "Sounds/inicio.wav";
        private static readonly string pathDetener = "Sounds/detener.wav";
        private static readonly string pathCerrar = "Sounds/cerrar.wav";

        public static void ReproducirInicio()
        {
            PlaySound(pathInicio);
        }

        public static void ReproducirDetener()
        {
            PlaySound(pathDetener);
        }

        public static void ReproducirCerrar()
        {
            try
            {
                using (SoundPlayer player = new SoundPlayer(pathCerrar))
                {
                    // PlaySync reproduce el sonido y espera hasta que termine
                    player.PlaySync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al reproducir sonido de cierre: " + ex.Message);
            }
        }

        private static void PlaySound(string filePath)
        {
            try
            {
                using var player = new SoundPlayer(filePath);
                player.Play();
            }
            catch
            {
                // Opcional: manejar errores si el archivo no existe
            }
        }
    }
}
