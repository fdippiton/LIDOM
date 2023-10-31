using System.ComponentModel;

namespace LIDOM_MVC.Models
{
    public class Temporada
    {
        [DisplayName("Id")]
        public int TemId { get; set; }
        [DisplayName("Nombre")]
        public string TemNombre { get; set; } = null!;
        [DisplayName("Fecha de inicio")]
        public DateTime TemFecInicio { get; set; }
        [DisplayName("Fecha de termino")]
        public DateTime TemFecFinal { get; set; }
    }
}
