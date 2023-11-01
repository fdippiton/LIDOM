using System.ComponentModel;

namespace LIDOM_MVC.ViewModels
{
    public class FechaPartidoViewModel
    {
        public int FecId { get; set; }
        [DisplayName("Fecha de partido")]
        public DateTime FecFechaPartido { get; set; }
        [DisplayName("Temporada")]
        public string FecTemporada { get; set; }
        [DisplayName("Hora")]
        public TimeSpan FecHora { get; set; }
    }
}
