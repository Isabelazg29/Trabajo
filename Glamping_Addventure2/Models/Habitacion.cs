using System;
using System.Collections.Generic;

namespace Glamping_Addventure2.Models;

public partial class Habitacion
{
    public int Idhabitacion { get; set; }

    public string? NombreHabitacion { get; set; }

    public byte[]? ImagenHabitacion { get; set; }

    public string? Descripcion { get; set; }

    public string? Piso { get; set; }

    public int? CantidadPersonas { get; set; }

    public decimal? Precio { get; set; }

    public int? IdtipodeHabitacion { get; set; }

    public virtual ICollection<DetalleReservaHabitacion> DetalleReservaHabitacions { get; set; } = new List<DetalleReservaHabitacion>();

    public virtual ICollection<HabitacionInmueble> HabitacionInmuebles { get; set; } = new List<HabitacionInmueble>();

    public virtual TipodeHabitacion? IdtipodeHabitacionNavigation { get; set; }

    public virtual ICollection<PaquetesHabitacion> PaquetesHabitacions { get; set; } = new List<PaquetesHabitacion>();
}
