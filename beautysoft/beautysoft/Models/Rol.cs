using System;
using System.Collections.Generic;

namespace beautysoft.Models;

public partial class Rol
{
    public int IdRol { get; set; }

    public string? NombreRol { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<Estilistas> Estilista { get; set; } = new List<Estilistas>();
}
