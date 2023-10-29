using System;
using System.Collections.Generic;

namespace LIDOM_API.Models;

public partial class Usuario
{
    public int UsuId { get; set; }

    public string UsuNombre { get; set; } = null!;

    public int UsuRol { get; set; }

    public string UsuPassword { get; set; } = null!;

    public string UsuEstado { get; set; } = null!;

    public virtual Role UsuRolNavigation { get; set; } = null!;
}
