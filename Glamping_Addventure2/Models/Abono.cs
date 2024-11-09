using System;
using System.Collections.Generic;

namespace Glamping_Addventure2.Models;

public partial class Abono
{
    public int Idabono { get; set; }

    public int? Idreserva { get; set; }

    public DateOnly? Fecha { get; set; }

    public int? Subtotal { get; set; }

    public int? Iva { get; set; }

    public int? Total { get; set; }

    public string? Estado { get; set; }

    public virtual Reserva? IdreservaNavigation { get; set; }
}
