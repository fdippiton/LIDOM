using System.ComponentModel;

namespace LIDOM_MVC.ViewModels
{
    public class EqEstadioNombreViewModel
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
        public string EqEstadio { get; set; }
        [DisplayName("Estatus")]
        public string EqEstatus { get; set; } = null!;
    }
}
