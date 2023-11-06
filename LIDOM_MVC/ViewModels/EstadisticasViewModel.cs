using System.ComponentModel;

namespace LIDOM_MVC.ViewModels
{
    public class EstadisticasViewModel
    {
        [DisplayName("Id de equipo")]
        public int EquipoId { get; set; }
        [DisplayName("Equipo")]
        public string EquipoNombre { get; set; }
        [DisplayName("Total juegos")]
        public int TotalJuegos { get; set; }
        [DisplayName("Total juegos ganados")]
        public int TotalJuegosGanados { get; set; }
        [DisplayName("Total juegos perdidos")]
        public int TotalJuegosPerdidos { get; set; }
        [DisplayName("Total juegos empatados")]
        public int TotalJuegosEmpatados { get; set; }
        [DisplayName("Total carreras")]
        public int TotalCarreras { get; set; }
        [DisplayName("Total hits")]
        public int TotalHits { get; set; }
        [DisplayName("Total errores")]
        public int TotalErrores { get; set; }
    }
}
