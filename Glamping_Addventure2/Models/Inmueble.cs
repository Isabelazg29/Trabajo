using System;
using System.Collections.Generic;

namespace Glamping_Addventure2.Models;

public partial class Inmueble
{
    public int Idinmueble { get; set; }

    public string? NombreInmueble { get; set; }

    public byte[]? ImagenInmueble { get; set; }

    public int? CantidadInmueble { get; set; }

    public int? Precio { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<HabitacionInmueble> HabitacionInmuebles { get; set; } = new List<HabitacionInmueble>();
}
