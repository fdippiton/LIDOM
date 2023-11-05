using System;
using System.Collections.Generic;

namespace LIDOM_API.Models;

public partial class ResultadoEquipo
{
    public int ResId { get; set; }

    public int? ResPartido { get; set; }

    public int? ResEquipo { get; set; }

    public int ResCarreras { get; set; }

    public int ResHits { get; set; }

    public int ResErrores { get; set; }

    public int ResJuegoGanado { get; set; }

    public int ResJuegoPerdido { get; set; }

    public int ResJuegoEmpate { get; set; }

    public virtual Equipo? ResEquipoNavigation { get; set; } 

    public virtual Partido? ResPartidoNavigation { get; set; } 
}
