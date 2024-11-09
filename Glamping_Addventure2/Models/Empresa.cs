using System;
using System.Collections.Generic;

namespace Glamping_Addventure2.Models;

public partial class Empresa
{
    public int Idempresa { get; set; }

    public string? PronombrePrimero { get; set; }

    public int? Telefono { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
