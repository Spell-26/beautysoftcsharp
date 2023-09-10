using System;
using System.Collections.Generic;

namespace beautysoft.Models;

public partial class Citas
{
    public int IdCita { get; set; }

    public int? IdCliente { get; set; }

    public int? IdEstilista { get; set; }

    public int? IdServicio { get; set; }

    public DateTime? Fecha { get; set; }

    public TimeSpan? Hora { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Estilistas? IdEstilistaNavigation { get; set; }

    public virtual Servicio? IdServicioNavigation { get; set; }
}
