using System;
using System.Collections.Generic;

namespace LIDOM_API.Models;

public partial class Temporada
{
    public int TemId { get; set; }

    public string TemNombre { get; set; } = null!;

    public DateTime TemFecInicio { get; set; }

    public DateTime TemFecFinal { get; set; }

    public virtual ICollection<FechaPartido> FechaPartidos { get; set; } = new List<FechaPartido>();
}
