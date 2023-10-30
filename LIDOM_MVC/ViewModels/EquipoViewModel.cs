namespace LIDOM_MVC.ViewModels
{
    public class EquipoViewModel
    {
        public int EqId { get; set; }

        public string EqNombre { get; set; } = null!;

        public string EqDescripcion { get; set; } = null!;

        public string EqCiudad { get; set; } = null!;

        public string EqEstadioNombre { get; set; }

        public string EqEstatus { get; set; } = null!;
    }
}
