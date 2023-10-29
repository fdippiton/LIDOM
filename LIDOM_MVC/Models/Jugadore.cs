namespace LIDOM_MVC.Models
{
    public class Jugadore
    {
        public int JugId { get; set; }

        public string JugNombre { get; set; } = null!;

        public string JugApellidos { get; set; } = null!;

        public int JugPosicion { get; set; }

        public int JugEquipo { get; set; }

        public int JugEdad { get; set; }

        public int JugNumCamiseta { get; set; }

    }
}
