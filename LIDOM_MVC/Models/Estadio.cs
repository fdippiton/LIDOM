using System.ComponentModel;

namespace LIDOM_MVC.Models
{
    public class Estadio
    {
        [DisplayName("Id")]
        public int EstId { get; set; }

        [DisplayName("Nombre")]
        public string EstNombre { get; set; } = null!;

        [DisplayName("Ubicacion")]
        public string EstUbicacion { get; set; } = null!;
    }
}
