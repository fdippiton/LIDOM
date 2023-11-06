namespace LIDOM_MVC.ViewModels
{
    public class StandingPorFechaViewModel
    {
        public DateTime Fecha { get; set; }
        public List<StandingEquipoViewModel> Equipos = new List<StandingEquipoViewModel>();
    }
}
