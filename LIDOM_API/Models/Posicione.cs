using System;
using System.Collections.Generic;

namespace LIDOM_API.Models;

public partial class Posicione
{
    public int PosId { get; set; }

    public string PosNombre { get; set; } = null!;

    public virtual ICollection<Jugadore> Jugadores { get; set; } = new List<Jugadore>();
}
