namespace LIDOM_MVC.Models
{
    public class Equipo
    {
        public int EqId { get; set; }

        public string EqNombre { get; set; } = null!;

        public string EqDescripcion { get; set; } = null!;

        public string EqCiudad { get; set; } = null!;

        public int EqEstadio { get; set; }

        public string EqEstatus { get; set; } = null!;
    }
}
