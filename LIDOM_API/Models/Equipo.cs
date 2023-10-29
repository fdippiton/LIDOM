using System;
using System.Collections.Generic;

namespace LIDOM_API.Models;

public partial class Equipo
{
    public int EqId { get; set; }

    public string EqNombre { get; set; } = null!;

    public string EqDescripcion { get; set; } = null!;

    public string EqCiudad { get; set; } = null!;

    public int EqEstadio { get; set; }

    public string EqEstatus { get; set; } = null!;

    public virtual ICollection<CalendarioJuego> CalendarioJuegoCalEquipoLocalNavigations { get; set; } = new List<CalendarioJuego>();

    public virtual ICollection<CalendarioJuego> CalendarioJuegoCalEquipoVisitanteNavigations { get; set; } = new List<CalendarioJuego>();

    public virtual Estadio EqEstadioNavigation { get; set; } = null!;

    public virtual ICollection<Jugadore> Jugadores { get; set; } = new List<Jugadore>();

    public virtual ICollection<Partido> PartidoParEquipoGanadorNavigations { get; set; } = new List<Partido>();

    public virtual ICollection<Partido> PartidoParEquipoPerdedorNavigations { get; set; } = new List<Partido>();

    public virtual ICollection<ResultadoEquipo> ResultadoEquipos { get; set; } = new List<ResultadoEquipo>();
}
