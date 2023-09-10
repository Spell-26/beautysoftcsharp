using System;
using System.Collections.Generic;

namespace beautysoft.Models;

public partial class Servicio
{
    public int IdServicio { get; set; }

    public string? Nombre { get; set; }

    public int? Precio { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Citas> Cita { get; set; } = new List<Citas>();
}
