namespace LIDOM_API.ViewModels
{
    public class PartidoViewModel
    {

        public int ParId { get; set; }
        public int ParJuego { get; set; }
        public int FecId { get; set; }

        public DateTime FecFechaPartido { get; set; }
        public TimeSpan FecHora { get; set; }

        public string ParEquipoGanador { get; set; }

        public string ParEquipoPerdedor { get; set; }

        public int ParEquipoGanadorId { get; set; }

        public int ParEquipoPerdedorId { get; set; }
        public string TemNombre { get; set; }
        public string EquipoLocal { get; set; }
        public string EquipoVisitante { get; set; }
        public string EstNombre { get; set; }
    }
}
