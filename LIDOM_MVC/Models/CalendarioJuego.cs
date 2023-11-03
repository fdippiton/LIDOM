using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIDOM_MVC.Models
{
    public class CalendarioJuego
    {
        [DisplayName("Id")]
        public int CalJuegoId { get; set; }
        [DisplayName("Equipo Local")]
        public int CalEquipoLocal { get; set; }
        [DisplayName("Equipo Visitante")]
        public int CalEquipoVisitante { get; set; }
        
        [DisplayName("Fecha de partido")]
        public int CalFechaPartido { get; set; }
    }
}
