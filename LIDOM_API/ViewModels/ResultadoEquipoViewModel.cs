namespace LIDOM_API.ViewModels
{
    public class ResultadoEquipoViewModel
    {
        public int ResId { get; set; }

        public int ResPartido { get; set; }
        public DateTime ResPartidoFecha { get; set; }

        public int ResEquipo { get; set; }
        public string ResEquipoNombre { get; set; }

        public int ResCarreras { get; set; }

        public int ResHits { get; set; }

        public int ResErrores { get; set; }

        public int ResJuegoGanado { get; set; }

        public int ResJuegoPerdido { get; set; }

        public int ResJuegoEmpate { get; set; }
    }
}
