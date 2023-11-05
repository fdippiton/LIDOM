using System.ComponentModel;

namespace LIDOM_MVC.ViewModels
{
    public class PartidoView
    {
        [DisplayName("Id")]
        public int ParId { get; set; }

        [DisplayName("Id de Juego")]
        public int ParJuego { get; set; }
        [DisplayName("Fecha Id")]
        public int FecId { get; set; }
        [DisplayName("Fecha del partido")]
        public DateTime FecFechaPartido { get; set; }
        [DisplayName("Hora")]
        public TimeSpan FecHora { get; set; }
        [DisplayName("Equipo ganador")]
        public string ParEquipoGanador { get; set; }
        [DisplayName("Equipo perdedor")]
        public string ParEquipoPerdedor { get; set; }


        [DisplayName("Id de equipo perdedor")]
        public int ParEquipoGanadorId { get; set; }
        [DisplayName("Id de equipo ganador")]
        public int ParEquipoPerdedorId { get; set; }

        [DisplayName("Temporada")]
        public string TemNombre { get; set; }
        [DisplayName("Equipo local")]
        public string EquipoLocal { get; set; }
        [DisplayName("Equipo visitante")]
        public string EquipoVisitante { get; set; }
        [DisplayName("Estadio")]
        public string EstNombre { get; set; }

    }
}

