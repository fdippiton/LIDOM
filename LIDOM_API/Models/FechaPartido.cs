using System;
using System.Collections.Generic;

namespace LIDOM_API.Models;

public partial class FechaPartido
{
    public int FecId { get; set; }

    public DateTime FecFechaPartido { get; set; }

    public int FecTemporada { get; set; }

    public TimeSpan FecHora { get; set; }

    public virtual ICollection<CalendarioJuego> CalendarioJuegos { get; set; } = new List<CalendarioJuego>();

    public virtual Temporada FecTemporadaNavigation { get; set; } = null!;
}
