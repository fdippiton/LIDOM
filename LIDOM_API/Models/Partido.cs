using System;
using System.Collections.Generic;

namespace LIDOM_API.Models;

public partial class Partido
{
    public int ParId { get; set; }

    public int ParJuego { get; set; }

    public int ParEquipoGanador { get; set; }

    public int ParEquipoPerdedor { get; set; }

    public virtual Equipo ParEquipoGanadorNavigation { get; set; } = null!;

    public virtual Equipo ParEquipoPerdedorNavigation { get; set; } = null!;

    public virtual CalendarioJuego ParJuegoNavigation { get; set; } = null!;

    public virtual ICollection<ResultadoEquipo> ResultadoEquipos { get; set; } = new List<ResultadoEquipo>();
}
