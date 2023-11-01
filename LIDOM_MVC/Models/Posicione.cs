using System.ComponentModel;

namespace LIDOM_MVC.Models
{
    public class Posicione
    {
        [DisplayName("Id")]
        public int PosId { get; set; }
        [DisplayName("Nombre de la posicion")]
        public string PosNombre { get; set; } = null!;
    }
}
