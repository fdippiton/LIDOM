using System;
using System.Collections.Generic;

namespace LIDOM_API.Models;

public partial class Role
{
    public int RolId { get; set; }

    public string RolNombre { get; set; } = null!;

    public string RolEstatus { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
