using System;
using System.Collections.Generic;

namespace Glamping_Addventure2.Models;

public partial class Paquete
{
    public int Idpaquete { get; set; }

    public string? NombrePaquete { get; set; }

    public DateOnly? FechaInicio { get; set; }

    public DateOnly? FechaFin { get; set; }

    public int? Precio { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<DetalleReservaPaquete> DetalleReservaPaquetes { get; set; } = new List<DetalleReservaPaquete>();

    public virtual ICollection<PaquetesHabitacion> PaquetesHabitacions { get; set; } = new List<PaquetesHabitacion>();

    public virtual ICollection<PaquetesServicio> PaquetesServicios { get; set; } = new List<PaquetesServicio>();
}
