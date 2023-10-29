using System;
using System.Collections.Generic;

namespace LIDOM_API.Models;

public partial class Estadio
{
    public int EstId { get; set; }

    public string EstNombre { get; set; } = null!;

    public string EstUbicacion { get; set; } = null!;

    public virtual ICollection<Equipo> Equipos { get; set; } = new List<Equipo>();
}
