using System;
using System.Collections.Generic;

namespace Glamping_Addventure2.Models;

public partial class HabitacionInmueble
{
    public int IdhabitacionInmuebles { get; set; }

    public int? Idhabitacion { get; set; }

    public int? Idinmueble { get; set; }

    public virtual Habitacion? IdhabitacionNavigation { get; set; }

    public virtual Inmueble? IdinmuebleNavigation { get; set; }
}
