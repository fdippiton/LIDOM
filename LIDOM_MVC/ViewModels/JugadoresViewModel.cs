using System.ComponentModel;

namespace LIDOM_MVC.ViewModels
{
    public class JugadoresViewModel
    {
        [DisplayName("Id")]
        public int JugId { get; set; }
        [DisplayName("Nombre")]
        public string JugNombre { get; set; } = null!;
        [DisplayName("Apellido")]
        public string JugApellidos { get; set; } = null!;
        [DisplayName("Posicion")]
        public string JugPosicion { get; set; }
        [DisplayName("Equipo")]
        public string JugEquipo { get; set; }
        [DisplayName("Edad")]
        public int JugEdad { get; set; }
        [DisplayName("Numero en camiseta")]
        public int JugNumCamiseta { get; set; }
    }
}
