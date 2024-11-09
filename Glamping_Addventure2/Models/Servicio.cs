using System;
using System.Collections.Generic;

namespace Glamping_Addventure2.Models;

public partial class Servicio
{
    public int Idservicio { get; set; }

    public string? NombreServicio { get; set; }

    public byte[]? ImagenServicio { get; set; }

    public string? Descripcion { get; set; }

    public decimal? Precio { get; set; }

    public TimeOnly? Duracion { get; set; }

    public int? CantidadMaximaPersonas { get; set; }

    public virtual ICollection<DetalleReservaServicio> DetalleReservaServicios { get; set; } = new List<DetalleReservaServicio>();

    public virtual ICollection<PaquetesServicio> PaquetesServicios { get; set; } = new List<PaquetesServicio>();
}
