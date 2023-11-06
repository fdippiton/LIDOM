using System.ComponentModel;

namespace LIDOM_MVC.ViewModels
{
    public class EstadisticasViewModel
    {
        [DisplayName("Id de equipo")]
        public int EquipoId { get; set; }
        [DisplayName("Equipo")]
        public string EquipoNombre { get; set; }
        [DisplayName("Total Juegos")]
        public int TotalJuegos { get; set; }
        [DisplayName("Total Juegos Ganados")]
        public int TotalJuegosGanados { get; set; }
        [DisplayName("Total Juegos Perdidos")]
        public int TotalJuegosPerdidos { get; set; }
        [DisplayName("Total Juegos Empatados")]
        public int TotalJuegosEmpatados { get; set; }
        [DisplayName("Total Carreras")]
        public int TotalCarreras { get; set; }
        [DisplayName("Total Hits")]
        public int TotalHits { get; set; }
        [DisplayName("Total Errores")]
        public int TotalErrores { get; set; }
    }
}
