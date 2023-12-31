﻿using System.ComponentModel;

namespace LIDOM_MVC.Models
{
    public class ResultadoEquipo
    {
        public int ResId { get; set; }
        [DisplayName("Id del Partido")]
        public int ResPartido { get; set; }
        [DisplayName("Equipo")]
        public int ResEquipo { get; set; }
        [DisplayName("Carreras")]
        public int ResCarreras { get; set; }
        [DisplayName("Hits")]
        public int ResHits { get; set; }
        [DisplayName("Errores")]
        public int ResErrores { get; set; }
        [DisplayName("Juego ganado")]
        public int ResJuegoGanado { get; set; }
        [DisplayName("Juego perdido")]
        public int ResJuegoPerdido { get; set; }
        [DisplayName("Juego empatado")]
        public int ResJuegoEmpate { get; set; }
    }
}
