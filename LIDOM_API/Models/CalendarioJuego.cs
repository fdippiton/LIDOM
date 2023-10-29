using System;
using System.Collections.Generic;

namespace LIDOM_API.Models;

public partial class CalendarioJuego
{
    public int CalJuegoId { get; set; }

    public int CalFechaPartido { get; set; }

    public int CalEquipoLocal { get; set; }

    public int CalEquipoVisitante { get; set; }

    public virtual Equipo CalEquipoLocalNavigation { get; set; } = null!;

    public virtual Equipo CalEquipoVisitanteNavigation { get; set; } = null!;

    public virtual FechaPartido CalFechaPartidoNavigation { get; set; } = null!;

    public virtual ICollection<Partido> Partidos { get; set; } = new List<Partido>();
}
