using System.ComponentModel;

namespace LIDOM_MVC.Models
{
    public class Equipo
    {
        [DisplayName("Id")]
        public int EqId { get; set; }
        [DisplayName("Nombre")]
        public string EqNombre { get; set; } = null!;
        [DisplayName("Descripcion")]
        public string EqDescripcion { get; set; } = null!;
        [DisplayName("Ciudad")]
        public string EqCiudad { get; set; } = null!;
        [DisplayName("Estadio")]
        public int? EqEstadio { get; set; }
        [DisplayName("Estatus")]
        public string EqEstatus { get; set; } = null!;
    }
}
