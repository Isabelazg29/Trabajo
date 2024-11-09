using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Glamping_Addventure2.Models;

public partial class GlampingAddventureContext : DbContext
{
    public GlampingAddventureContext()
    {
    }

    public GlampingAddventureContext(DbContextOptions<GlampingAddventureContext> options)
        : base(options)
    {
    }


    public virtual DbSet<Abono> Abonos { get; set; }

    public DbSet<TokenRecuperacion> TokenRecuperacion { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<DetalleReservaHabitacion> DetalleReservaHabitacions { get; set; }

    public virtual DbSet<DetalleReservaPaquete> DetalleReservaPaquetes { get; set; }

    public virtual DbSet<DetalleReservaServicio> DetalleReservaServicios { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<Habitacion> Habitacions { get; set; }

    public virtual DbSet<HabitacionInmueble> HabitacionInmuebles { get; set; }

    public virtual DbSet<Inmueble> Inmuebles { get; set; }

    public virtual DbSet<MetodoPago> MetodoPagos { get; set; }

    public virtual DbSet<Paquete> Paquetes { get; set; }

    public virtual DbSet<PaquetesHabitacion> PaquetesHabitacions { get; set; }

    public virtual DbSet<PaquetesServicio> PaquetesServicios { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolesPermiso> RolesPermisos { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<TipodeHabitacion> TipodeHabitacions { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=LAPTOP-OFAVMRQL;Initial Catalog=Glamping_Addventure;integrated security=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Abono>(entity =>
        {
            entity.HasKey(e => e.Idabono).HasName("PK__Abono__8647F8A9B65C89B5");

            entity.ToTable("Abono");

            entity.Property(e => e.Idabono).HasColumnName("IDAbono");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Idreserva).HasColumnName("IDReserva");
            entity.Property(e => e.Iva).HasColumnName("IVA");

            entity.HasOne(d => d.IdreservaNavigation).WithMany(p => p.Abonos)
                .HasForeignKey(d => d.Idreserva)
                .HasConstraintName("FK_Abono_Reserva");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Idcliente).HasName("PK__Clientes__9B8553FC77E5836A");

            entity.Property(e => e.Idcliente).HasColumnName("IDCliente");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Idrol).HasColumnName("IDRol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TipoDocumento)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdrolNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.Idrol)
                .HasConstraintName("FK_Clientes_Roles");
        });

        modelBuilder.Entity<DetalleReservaHabitacion>(entity =>
        {
            entity.HasKey(e => e.IddetalleReservaHabitacion).HasName("PK__DetalleR__8CA3B406866AC630");

            entity.ToTable("DetalleReservaHabitacion");

            entity.Property(e => e.IddetalleReservaHabitacion).HasColumnName("IDDetalleReservaHabitacion");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Idhabitacion).HasColumnName("IDHabitacion");
            entity.Property(e => e.Idreserva).HasColumnName("IDReserva");

            entity.HasOne(d => d.IdhabitacionNavigation).WithMany(p => p.DetalleReservaHabitacions)
                .HasForeignKey(d => d.Idhabitacion)
                .HasConstraintName("FK_DetalleReservaHabitacion_Habitacion");

            entity.HasOne(d => d.IdreservaNavigation).WithMany(p => p.DetalleReservaHabitacions)
                .HasForeignKey(d => d.Idreserva)
                .HasConstraintName("FK_DetalleReservaHabitacion_Reserva");
        });

        modelBuilder.Entity<DetalleReservaPaquete>(entity =>
        {
            entity.HasKey(e => e.IddetalleReservaPaquetes).HasName("PK__DetalleR__64F9FDAEF183D840");

            entity.Property(e => e.IddetalleReservaPaquetes).HasColumnName("IDDetalleReservaPaquetes");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Idpaquete).HasColumnName("IDPaquete");
            entity.Property(e => e.Idreserva).HasColumnName("IDReserva");

            entity.HasOne(d => d.IdpaqueteNavigation).WithMany(p => p.DetalleReservaPaquetes)
                .HasForeignKey(d => d.Idpaquete)
                .HasConstraintName("FK_DetalleReservaPaquetes_Paquete");

            entity.HasOne(d => d.IdreservaNavigation).WithMany(p => p.DetalleReservaPaquetes)
                .HasForeignKey(d => d.Idreserva)
                .HasConstraintName("FK_DetalleReservaPaquetes_Reserva");
        });

        modelBuilder.Entity<DetalleReservaServicio>(entity =>
        {
            entity.HasKey(e => e.IddetalleReservaServicio).HasName("PK__DetalleR__B52B22F81FEA71BF");

            entity.ToTable("DetalleReservaServicio");

            entity.Property(e => e.IddetalleReservaServicio).HasColumnName("IDDetalleReservaServicio");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Idreserva).HasColumnName("IDReserva");
            entity.Property(e => e.Idservicio).HasColumnName("IDServicio");

            entity.HasOne(d => d.IdreservaNavigation).WithMany(p => p.DetalleReservaServicios)
                .HasForeignKey(d => d.Idreserva)
                .HasConstraintName("FK_DetalleReservaServicio_Reserva");

            entity.HasOne(d => d.IdservicioNavigation).WithMany(p => p.DetalleReservaServicios)
                .HasForeignKey(d => d.Idservicio)
                .HasConstraintName("FK_DetalleReservaServicio_Servicio");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.Idempresa).HasName("PK__Empresa__ED09F0D5F1EE16D2");

            entity.ToTable("Empresa");

            entity.Property(e => e.Idempresa).HasColumnName("IDEmpresa");
            entity.Property(e => e.PronombrePrimero)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Habitacion>(entity =>
        {
            entity.HasKey(e => e.Idhabitacion).HasName("PK__Habitaci__6B4757DAE0A23709");

            entity.ToTable("Habitacion");

            entity.Property(e => e.Idhabitacion).HasColumnName("IDHabitacion");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IdtipodeHabitacion).HasColumnName("IDTipodeHabitacion");
            entity.Property(e => e.NombreHabitacion)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Piso)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.IdtipodeHabitacionNavigation).WithMany(p => p.Habitacions)
                .HasForeignKey(d => d.IdtipodeHabitacion)
                .HasConstraintName("FK_Habitacion_TipodeHabitacion");
        });

        modelBuilder.Entity<HabitacionInmueble>(entity =>
        {
            entity.HasKey(e => e.IdhabitacionInmuebles).HasName("PK__Habitaci__8FBCBDB4862007C1");

            entity.Property(e => e.IdhabitacionInmuebles).HasColumnName("IDHabitacionInmuebles");
            entity.Property(e => e.Idhabitacion).HasColumnName("IDHabitacion");
            entity.Property(e => e.Idinmueble).HasColumnName("IDInmueble");

            entity.HasOne(d => d.IdhabitacionNavigation).WithMany(p => p.HabitacionInmuebles)
                .HasForeignKey(d => d.Idhabitacion)
                .HasConstraintName("FK_HabitacionInmuebles_Habitacion");

            entity.HasOne(d => d.IdinmuebleNavigation).WithMany(p => p.HabitacionInmuebles)
                .HasForeignKey(d => d.Idinmueble)
                .HasConstraintName("FK_HabitacionInmuebles_Inmuebles");
        });

        modelBuilder.Entity<Inmueble>(entity =>
        {
            entity.HasKey(e => e.Idinmueble).HasName("PK__Inmueble__8FFA1C88E82C900F");

            entity.Property(e => e.Idinmueble).HasColumnName("IDInmueble");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreInmueble)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MetodoPago>(entity =>
        {
            entity.HasKey(e => e.IdmetodoPago).HasName("PK__MetodoPa__8D99F33886309C22");

            entity.ToTable("MetodoPago");

            entity.Property(e => e.IdmetodoPago).HasColumnName("IDMetodoPago");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Paquete>(entity =>
        {
            entity.HasKey(e => e.Idpaquete).HasName("PK__Paquetes__4C29513B2EA3D042");

            entity.Property(e => e.Idpaquete).HasColumnName("IDPaquete");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.NombrePaquete)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PaquetesHabitacion>(entity =>
        {
            entity.HasKey(e => e.IdpaquetesHabitacion).HasName("PK__Paquetes__2EBD7781381D14C8");

            entity.ToTable("PaquetesHabitacion");

            entity.Property(e => e.IdpaquetesHabitacion).HasColumnName("IDPaquetesHabitacion");
            entity.Property(e => e.Idhabitacion).HasColumnName("IDHabitacion");
            entity.Property(e => e.Idpaquete).HasColumnName("IDPaquete");

            entity.HasOne(d => d.IdhabitacionNavigation).WithMany(p => p.PaquetesHabitacions)
                .HasForeignKey(d => d.Idhabitacion)
                .HasConstraintName("FK_PaquetesHabitacion_Habitacion");

            entity.HasOne(d => d.IdpaqueteNavigation).WithMany(p => p.PaquetesHabitacions)
                .HasForeignKey(d => d.Idpaquete)
                .HasConstraintName("FK_PaquetesHabitacion_Paquetes");
        });

        modelBuilder.Entity<PaquetesServicio>(entity =>
        {
            entity.HasKey(e => e.IdpaquetesServicio).HasName("PK__Paquetes__C5795B25439B50EF");

            entity.ToTable("PaquetesServicio");

            entity.Property(e => e.IdpaquetesServicio).HasColumnName("IDPaquetesServicio");
            entity.Property(e => e.Idpaquete).HasColumnName("IDPaquete");
            entity.Property(e => e.Idservicio).HasColumnName("IDServicio");

            entity.HasOne(d => d.IdpaqueteNavigation).WithMany(p => p.PaquetesServicios)
                .HasForeignKey(d => d.Idpaquete)
                .HasConstraintName("FK_PaquetesServicio_Paquetes");

            entity.HasOne(d => d.IdservicioNavigation).WithMany(p => p.PaquetesServicios)
                .HasForeignKey(d => d.Idservicio)
                .HasConstraintName("FK_PaquetesServicio_Servicio");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.Idpermiso).HasName("PK__Permisos__F11D75F302FF0A29");

            entity.Property(e => e.Idpermiso).HasColumnName("IDPermiso");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EstadoPermisos)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombrePermisos)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.Idreserva).HasName("PK__Reserva__D9F2FA671CC2091A");

            entity.ToTable("Reserva");

            entity.Property(e => e.Idreserva).HasColumnName("IDReserva");
            entity.Property(e => e.Idcliente).HasColumnName("IDCliente");
            entity.Property(e => e.Idempresa).HasColumnName("IDEmpresa");
            entity.Property(e => e.IdmetodoPago).HasColumnName("IDMetodoPago");
            entity.Property(e => e.Idusuario).HasColumnName("IDUsuario");
            entity.Property(e => e.Iva).HasColumnName("IVA");

            entity.HasOne(d => d.IdclienteNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.Idcliente)
                .HasConstraintName("FK_Reserva_Cliente");

            entity.HasOne(d => d.IdempresaNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.Idempresa)
                .HasConstraintName("FK_Reserva_Empresa");

            entity.HasOne(d => d.IdmetodoPagoNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdmetodoPago)
                .HasConstraintName("FK_Reserva_MetodoPago");

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.Idusuario)
                .HasConstraintName("FK_Reserva_Usuario");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Idrol).HasName("PK__Roles__A681ACB6CDE3C190");

            entity.Property(e => e.Idrol).HasColumnName("IDRol");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RolesPermiso>(entity =>
        {
            entity.HasKey(e => e.IdrolPermiso).HasName("PK__RolesPer__3F09FABFAC4CF063");

            entity.Property(e => e.IdrolPermiso).HasColumnName("IDRolPermiso");
            entity.Property(e => e.Idpermiso).HasColumnName("IDPermiso");
            entity.Property(e => e.Idrol).HasColumnName("IDRol");

            entity.HasOne(d => d.IdpermisoNavigation).WithMany(p => p.RolesPermisos)
                .HasForeignKey(d => d.Idpermiso)
                .HasConstraintName("FK_RolesPermisos_Permisos");

            entity.HasOne(d => d.IdrolNavigation).WithMany(p => p.RolesPermisos)
                .HasForeignKey(d => d.Idrol)
                .HasConstraintName("FK_RolesPermisos_Roles");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.Idservicio).HasName("PK__Servicio__3CCE7416760CD8A0");

            entity.Property(e => e.Idservicio).HasColumnName("IDServicio");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreServicio)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<TipodeHabitacion>(entity =>
        {
            entity.HasKey(e => e.IdtipodeHabitacion).HasName("PK__TipodeHa__9BD4086C72AEB476");

            entity.ToTable("TipodeHabitacion");

            entity.Property(e => e.IdtipodeHabitacion).HasColumnName("IDTipodeHabitacion");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreTipodeHabitacion)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Idusuario).HasName("PK__Usuarios__5231116905826D68");

            entity.Property(e => e.Idusuario).HasColumnName("IDUsuario");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Contrasena)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Idrol).HasColumnName("IDRol");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TipoDocumento)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdrolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.Idrol)
                .HasConstraintName("FK_Usuarios_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
