namespace LIDOM_API.ViewModels
{
    public class CalendarioJuegoViewModel
    {
        public int CalJuegoId { get; set; }
        public DateTime FecFechaPartido { get; set; }
        public TimeSpan FecHora { get; set; }
        public string TemNombre { get; set; }
        public string EquipoLocal { get; set; }
        public string EquipoVisitante { get; set; }
        public string EstNombre { get; set; }
    }
}
