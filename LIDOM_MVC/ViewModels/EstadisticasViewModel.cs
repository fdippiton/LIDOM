namespace LIDOM_MVC.ViewModels
{
    public class EstadisticasViewModel
    {
        public int EquipoId { get; set; }
        public string EquipoNombre { get; set; }
        public int TotalJuegos { get; set; }
        public int TotalJuegosGanados { get; set; }
        public int TotalJuegosPerdidos { get; set; }
        public int TotalJuegosEmpatados { get; set; }
        public int TotalCarreras { get; set; }
        public int TotalHits { get; set; }
        public int TotalErrores { get; set; }
    }
}
