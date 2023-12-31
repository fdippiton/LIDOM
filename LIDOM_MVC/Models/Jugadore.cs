﻿using System.ComponentModel;

namespace LIDOM_MVC.Models
{
    public class Jugadore
    {
        [DisplayName("Id")]
        public int JugId { get; set; }
        [DisplayName("Nombre")]
        public string JugNombre { get; set; } = null!;
        [DisplayName("Apellido")]
        public string JugApellidos { get; set; } = null!;
        [DisplayName("Posicion")]
        public int JugPosicion { get; set; }
        [DisplayName("Equipo")]
        public int JugEquipo { get; set; }
        [DisplayName("Edad")]
        public int JugEdad { get; set; }
        [DisplayName("Numero en camiseta")]
        public int JugNumCamiseta { get; set; }

    }
}
