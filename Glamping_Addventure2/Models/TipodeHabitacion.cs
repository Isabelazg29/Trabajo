using System;
using System.Collections.Generic;

namespace Glamping_Addventure2.Models;

public partial class TipodeHabitacion
{
    public int IdtipodeHabitacion { get; set; }

    public string? NombreTipodeHabitacion { get; set; }

    public byte[]? ImagenTipodeHabitacion { get; set; }

    public string? Descripcion { get; set; }

    public decimal? Precio { get; set; }

    public virtual ICollection<Habitacion> Habitacions { get; set; } = new List<Habitacion>();
}
