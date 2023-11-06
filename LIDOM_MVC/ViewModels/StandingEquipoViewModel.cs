namespace LIDOM_MVC.ViewModels
{
    public class StandingEquipoViewModel
    {
        public int EquipoId { get; set; }
        public DateTime Fecha { get; set; }
        public string EquipoNombre { get; set; }
        public int JuegosGanados { get; set; }
        public int JuegosPerdidos { get; set; }
        public int JuegosEmpatados { get; set; }
        public int Carreras { get; set; }
        public int Hits { get; set; }
        public int Errores { get; set; }
    }
}
