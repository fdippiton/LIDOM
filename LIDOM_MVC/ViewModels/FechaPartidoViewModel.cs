using System.ComponentModel;

namespace LIDOM_MVC.ViewModels
{
    public class FechaPartidoViewModel
    {
        public int FecId { get; set; }

        public DateTime FecFechaPartido { get; set; }

        public string FecTemporada { get; set; }

        public TimeSpan FecHora { get; set; }
    }
}
