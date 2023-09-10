using System;
using System.Collections.Generic;

namespace beautysoft.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdRol { get; set; }

    public virtual ICollection<Citas> Cita { get; set; } = new List<Citas>();

    public virtual Rol? IdRolNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
