using System;
using System.Collections.Generic;

namespace Glamping_Addventure2.Models;

public partial class Reserva
{
    public int Idreserva { get; set; }

    public DateOnly? FechaReserva { get; set; }

    public DateOnly? FechaInicio { get; set; }

    public DateOnly? FechaFin { get; set; }

    public int? SubTotal { get; set; }

    public int? Iva { get; set; }

    public int? Total { get; set; }

    public int? Idempresa { get; set; }

    public int? Idcliente { get; set; }

    public int? Idusuario { get; set; }

    public int? IdmetodoPago { get; set; }

    public int? Estado { get; set; }

    public virtual ICollection<Abono> Abonos { get; set; } = new List<Abono>();

    public virtual ICollection<DetalleReservaHabitacion> DetalleReservaHabitacions { get; set; } = new List<DetalleReservaHabitacion>();

    public virtual ICollection<DetalleReservaPaquete> DetalleReservaPaquetes { get; set; } = new List<DetalleReservaPaquete>();

    public virtual ICollection<DetalleReservaServicio> DetalleReservaServicios { get; set; } = new List<DetalleReservaServicio>();

    public virtual Cliente? IdclienteNavigation { get; set; }

    public virtual Empresa? IdempresaNavigation { get; set; }

    public virtual MetodoPago? IdmetodoPagoNavigation { get; set; }

    public virtual Usuario? IdusuarioNavigation { get; set; }
}
