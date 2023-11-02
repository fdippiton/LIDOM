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
    }
}
