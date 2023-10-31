using LIDOM_MVC.Models;
using System.ComponentModel;

namespace LIDOM_MVC.ViewModels
{
    public class EquipoViewModel
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
        public string EqEstadioNombre { get; set; }
        [DisplayName("Estatus")]
        public string EqEstatus { get; set; } = null!;
    }
}
