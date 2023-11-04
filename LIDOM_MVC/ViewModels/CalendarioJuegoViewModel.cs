using LIDOM_MVC.Models;
using System.ComponentModel;

namespace LIDOM_MVC.ViewModels
{
    public class CalendarioJuegoViewModel
    {
        [DisplayName("Id")]
        public int CalJuegoId { get; set; }
        [DisplayName("Fecha de partido")]
        public DateTime CalFechaPartido { get; set; }
        [DisplayName("Equipo Local")]
        public string CalEquipoLocal { get; set; }
        [DisplayName("Equipo visitante")]
        public string CalEquipoVisitante { get; set; }
        //[DisplayName("Hora")]
        //public TimeSpan FecHora { get; set; }
        public int FecId { get; set; }

        [DisplayName("Fecha de partido")]
        public DateTime FecFechaPartido { get; set; }
        [DisplayName("Temporada")]
        public int FecTemporada { get; set; }
        [DisplayName("Hora")]
        public TimeSpan FecHora { get; set; }

        [DisplayName("Id")]
        public int EstId { get; set; }

        [DisplayName("Nombre")]
        public string EstNombre { get; set; } = null!;

        [DisplayName("Ubicacion")]
        public string EstUbicacion { get; set; } = null!;

        [DisplayName("Id")]
        public int TemId { get; set; }
        [DisplayName("Nombre")]
        public string TemNombre { get; set; } = null!;
        [DisplayName("Fecha de inicio")]
        public DateTime TemFecInicio { get; set; }
        [DisplayName("Fecha de termino")]
        public DateTime TemFecFinal { get; set; }

    }
}
