namespace LIDOM_API.ViewModels
{
    public class StandingViewModel
    {
        public DateTime FechaDelJuego { get; set; }
        public string NombreDelEquipo { get; set; }
        public int JuegosGanados { get; set; }
        public int JuegosPerdidos { get; set; }
        public int JuegosEmpatados { get; set; }
        public int Carreras { get; set; }
        public int Hits { get; set; }
        public int Errores { get; set; }
    }
}
