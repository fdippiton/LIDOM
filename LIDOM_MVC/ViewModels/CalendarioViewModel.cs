using System.ComponentModel;

namespace LIDOM_MVC.ViewModels
{
    public class CalendarioViewModel
    {
        [DisplayName("Id")]
        public int CalJuegoId { get; set; }
        [DisplayName("Fecha de partido")]
        public DateTime FecFechaPartido { get; set; }
        [DisplayName("Hora")]
        public TimeSpan FecHora { get; set; }
        [DisplayName("Temporada")]
        public string TemNombre { get; set; }
        [DisplayName("Equipo Local")]
        public string EquipoLocal { get; set; }
        [DisplayName("Equipo Visitante")]
        public string EquipoVisitante { get; set; }
        [DisplayName("Estadio")]
        public string EstNombre { get; set; }

    }
}
