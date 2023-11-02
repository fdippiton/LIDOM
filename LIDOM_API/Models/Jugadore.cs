using System;
using System.Collections.Generic;

namespace LIDOM_API.Models;

public partial class Jugadore
{
    public int JugId { get; set; }

    public string JugNombre { get; set; } = null!;

    public string JugApellidos { get; set; } = null!;

    public int? JugPosicion { get; set; }

    public int? JugEquipo { get; set; }

    public int JugEdad { get; set; }

    public int JugNumCamiseta { get; set; }

    public virtual Equipo? JugEquipoNavigation { get; set; } 

    public virtual Posicione? JugPosicionNavigation { get; set; } 
}
