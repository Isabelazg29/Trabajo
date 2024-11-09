using System;
using System.Collections.Generic;

namespace Glamping_Addventure2.Models;

public partial class MetodoPago
{
    public int IdmetodoPago { get; set; }

    public string? Nombre { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
