using System.ComponentModel;

namespace LIDOM_MVC.Models
{
    public class FechaPartido
    {
        public int FecId { get; set; }

        [DisplayName("Fecha de partido")]
        public DateTime FecFechaPartido { get; set; }
        [DisplayName("Temporada")]
        public int FecTemporada { get; set; }
        [DisplayName("Hora")]
        public TimeSpan FecHora { get; set; }
    }
}
