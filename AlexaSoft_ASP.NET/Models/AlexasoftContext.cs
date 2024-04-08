using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AlexaSoft_ASP.NET.Models;

public partial class AlexasoftContext : DbContext
{
    public AlexasoftContext()
    {
    }

    public AlexasoftContext(DbContextOptions<AlexasoftContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CategoriaProducto> CategoriaProductos { get; set; }

    public virtual DbSet<Cita> Citas { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Colaboradore> Colaboradores { get; set; }

    public virtual DbSet<Compra> Compras { get; set; }

    public virtual DbSet<Detallespedidosproducto> Detallespedidosproductos { get; set; }

    public virtual DbSet<Detallespedidosservicio> Detallespedidosservicios { get; set; }

    public virtual DbSet<Detallesproductosxcompra> Detallesproductosxcompras { get; set; }

    public virtual DbSet<Horario> Horarios { get; set; }

    public virtual DbSet<Motivoscancelacion> Motivoscancelacions { get; set; }

    public virtual DbSet<Paquete> Paquetes { get; set; }

    public virtual DbSet<PaquetesServicio> PaquetesServicios { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedore> Proveedores { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolesPermiso> RolesPermisos { get; set; }

    public virtual DbSet<SalidaInsumo> SalidaInsumos { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<ServiciosProducto> ServiciosProductos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    public virtual DbSet<Ventasdetalleproducto> Ventasdetalleproductos { get; set; }

    public virtual DbSet<Ventasdetallesservicio> Ventasdetallesservicios { get; set; }

    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("");*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoriaProducto>(entity =>
        {
            entity.HasKey(e => e.IdCategoriaProducto).HasName("PK_categoria_productos_idCategoriaProducto");

            entity.ToTable("categoria_productos", "alexa_soft");

            entity.Property(e => e.IdCategoriaProducto).HasColumnName("idCategoriaProducto");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Cita>(entity =>
        {
            entity.HasKey(e => e.IdCita).HasName("PK_citas_idCita");

            entity.ToTable("citas", "alexa_soft");

            entity.HasIndex(e => e.IdCliente, "idCliente");

            entity.HasIndex(e => e.IdColaborador, "idColaborador");

            entity.HasIndex(e => e.IdHorario, "idHorario");

            entity.HasIndex(e => e.IdMotivoCancelacion, "idMotivoCancelacion");

            entity.HasIndex(e => e.IdPaquete, "idPaquete");

            entity.Property(e => e.IdCita).HasColumnName("idCita");
            entity.Property(e => e.Detalle)
                .HasMaxLength(200)
                .HasColumnName("detalle");
            entity.Property(e => e.Estado)
                .HasMaxLength(9)
                .HasColumnName("estado");
            entity.Property(e => e.Fecha)
                .HasColumnType("date")
                .HasColumnName("fecha");
            entity.Property(e => e.Hora).HasColumnName("hora");
            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.IdColaborador).HasColumnName("idColaborador");
            entity.Property(e => e.IdHorario).HasColumnName("idHorario");
            entity.Property(e => e.IdMotivoCancelacion).HasColumnName("idMotivoCancelacion");
            entity.Property(e => e.IdPaquete).HasColumnName("idPaquete");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("citas$citas_ibfk_10");

            entity.HasOne(d => d.IdColaboradorNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdColaborador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("citas$citas_ibfk_8");

            entity.HasOne(d => d.IdHorarioNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdHorario)
                .HasConstraintName("citas$citas_ibfk_5");

            entity.HasOne(d => d.IdMotivoCancelacionNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdMotivoCancelacion)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("citas$citas_ibfk_9");

            entity.HasOne(d => d.IdPaqueteNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdPaquete)
                .HasConstraintName("citas$citas_ibfk_11");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK_clientes_idCliente");

            entity.ToTable("clientes", "alexa_soft");

            entity.HasIndex(e => e.Correo, "clientes$correo").IsUnique();

            entity.HasIndex(e => e.Instagram, "clientes$instagram").IsUnique();

            entity.HasIndex(e => e.Telefono, "clientes$telefono").IsUnique();

            entity.HasIndex(e => e.IdRol, "idRol");

            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(20)
                .HasColumnName("contrasena");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .HasColumnName("correo");
            entity.Property(e => e.Estado)
                .HasMaxLength(11)
                .HasColumnName("estado");
            entity.Property(e => e.FechaInteraccion)
                .HasColumnType("date")
                .HasColumnName("fechaInteraccion");
            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.Instagram)
                .HasMaxLength(30)
                .HasColumnName("instagram");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(10)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("clientes$clientes_ibfk_1");
        });

        modelBuilder.Entity<Colaboradore>(entity =>
        {
            entity.HasKey(e => e.IdColaborador).HasName("PK_colaboradores_idColaborador");

            entity.ToTable("colaboradores", "alexa_soft");

            entity.HasIndex(e => e.Cedula, "colaboradores$cedula").IsUnique();

            entity.HasIndex(e => e.Correo, "colaboradores$correo").IsUnique();

            entity.HasIndex(e => e.Telefono, "colaboradores$telefono").IsUnique();

            entity.HasIndex(e => e.IdRol, "idRol");

            entity.Property(e => e.IdColaborador).HasColumnName("idColaborador");
            entity.Property(e => e.Cedula)
                .HasMaxLength(15)
                .HasColumnName("cedula");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(20)
                .HasColumnName("contrasena");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .HasColumnName("correo");
            entity.Property(e => e.Estado)
                .HasMaxLength(11)
                .HasColumnName("estado");
            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(10)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Colaboradores)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("colaboradores$colaboradores_ibfk_1");
        });

        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => e.IdCompra).HasName("PK_compras_idCompra");

            entity.ToTable("compras", "alexa_soft");

            entity.HasIndex(e => e.IdProveedor, "idProveedor");

            entity.Property(e => e.IdCompra).HasColumnName("idCompra");
            entity.Property(e => e.Fecha)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("fecha");
            entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");
            entity.Property(e => e.MotivoAnular)
                .HasMaxLength(200)
                .HasColumnName("motivoAnular");
            entity.Property(e => e.Precio).HasColumnName("precio");
            entity.Property(e => e.Subtotal).HasColumnName("subtotal");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("compras$compras_ibfk_1");
        });

        modelBuilder.Entity<Detallespedidosproducto>(entity =>
        {
            entity.HasKey(e => e.IdDetallePedidoProducto).HasName("PK_detallespedidosproductos_idDetallePedidoProducto");

            entity.ToTable("detallespedidosproductos", "alexa_soft");

            entity.HasIndex(e => e.IdPedido, "idCotizacion");

            entity.HasIndex(e => e.IdProducto, "idProducto");

            entity.Property(e => e.IdDetallePedidoProducto).HasColumnName("idDetallePedidoProducto");
            entity.Property(e => e.IdPedido).HasColumnName("idPedido");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.UnidadesXproducto).HasColumnName("unidadesXproducto");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.Detallespedidosproductos)
                .HasForeignKey(d => d.IdPedido)
                .HasConstraintName("detallespedidosproductos$detallespedidosproductos_ibfk_1");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Detallespedidosproductos)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("detallespedidosproductos$detallespedidosproductos_ibfk_2");
        });

        modelBuilder.Entity<Detallespedidosservicio>(entity =>
        {
            entity.HasKey(e => e.IdDetallePedidoServicio).HasName("PK_detallespedidosservicio_idDetallePedidoServicio");

            entity.ToTable("detallespedidosservicio", "alexa_soft");

            entity.HasIndex(e => e.IdPaquete, "idPaquete");

            entity.HasIndex(e => e.IdPedido, "idPedido");

            entity.Property(e => e.IdDetallePedidoServicio).HasColumnName("idDetallePedidoServicio");
            entity.Property(e => e.IdPaquete).HasColumnName("idPaquete");
            entity.Property(e => e.IdPedido).HasColumnName("idPedido");

            entity.HasOne(d => d.IdPaqueteNavigation).WithMany(p => p.Detallespedidosservicios)
                .HasForeignKey(d => d.IdPaquete)
                .HasConstraintName("detallespedidosservicio$detallespedidosservicio_ibfk_1");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.Detallespedidosservicios)
                .HasForeignKey(d => d.IdPedido)
                .HasConstraintName("detallespedidosservicio$detallespedidosservicio_ibfk_2");
        });

        modelBuilder.Entity<Detallesproductosxcompra>(entity =>
        {
            entity.HasKey(e => e.IdDetalleProductoXcompra).HasName("PK_detallesproductosxcompras_idDetalleProductoXCompra");

            entity.ToTable("detallesproductosxcompras", "alexa_soft");

            entity.HasIndex(e => e.IdCompra, "idCompra");

            entity.HasIndex(e => e.IdProducto, "idProducto");

            entity.Property(e => e.IdDetalleProductoXcompra)
                .ValueGeneratedNever()
                .HasColumnName("idDetalleProductoXCompra");
            entity.Property(e => e.IdCompra).HasColumnName("idCompra");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.Unidades).HasColumnName("unidades");

            entity.HasOne(d => d.IdCompraNavigation).WithMany(p => p.Detallesproductosxcompras)
                .HasForeignKey(d => d.IdCompra)
                .HasConstraintName("detallesproductosxcompras$detallesproductosxcompras_ibfk_2");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Detallesproductosxcompras)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("detallesproductosxcompras$detallesproductosxcompras_ibfk_1");
        });

        modelBuilder.Entity<Horario>(entity =>
        {
            entity.HasKey(e => e.IdHorario).HasName("PK_horarios_idHorario");

            entity.ToTable("horarios", "alexa_soft");

            entity.Property(e => e.IdHorario).HasColumnName("idHorario");
            entity.Property(e => e.Estado)
                .HasMaxLength(11)
                .HasColumnName("estado");
            entity.Property(e => e.FinJornada).HasColumnName("finJornada");
            entity.Property(e => e.InicioJornada).HasColumnName("inicioJornada");
            entity.Property(e => e.NumeroDia).HasColumnName("numeroDia");
        });

        modelBuilder.Entity<Motivoscancelacion>(entity =>
        {
            entity.HasKey(e => e.IdMotivo).HasName("PK_motivoscancelacion_idMotivo");

            entity.ToTable("motivoscancelacion", "alexa_soft");

            entity.Property(e => e.IdMotivo).HasColumnName("idMotivo");
            entity.Property(e => e.Motivo)
                .HasMaxLength(100)
                .HasColumnName("motivo");
        });

        modelBuilder.Entity<Paquete>(entity =>
        {
            entity.HasKey(e => e.IdPaquete).HasName("PK_paquetes_idPaquete");

            entity.ToTable("paquetes", "alexa_soft");

            entity.Property(e => e.IdPaquete).HasColumnName("idPaquete");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(11)
                .HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<PaquetesServicio>(entity =>
        {
            entity.HasKey(e => e.IdPaqueteXservicio).HasName("PK_paquetes_servicios_idPaqueteXServicio");

            entity.ToTable("paquetes_servicios", "alexa_soft");

            entity.HasIndex(e => e.IdPaquete, "idPaquete");

            entity.HasIndex(e => e.IdServicio, "idServicio");

            entity.Property(e => e.IdPaqueteXservicio).HasColumnName("idPaqueteXServicio");
            entity.Property(e => e.IdPaquete).HasColumnName("idPaquete");
            entity.Property(e => e.IdServicio).HasColumnName("idServicio");

            entity.HasOne(d => d.IdPaqueteNavigation).WithMany(p => p.PaquetesServicios)
                .HasForeignKey(d => d.IdPaquete)
                .HasConstraintName("paquetes_servicios$paquetes_servicios_ibfk_2");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.PaquetesServicios)
                .HasForeignKey(d => d.IdServicio)
                .HasConstraintName("paquetes_servicios$paquetes_servicios_ibfk_1");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.IdPedido).HasName("PK_pedidos_idPedido");

            entity.ToTable("pedidos", "alexa_soft");

            entity.HasIndex(e => e.IdColaborador, "IdColaborador");

            entity.HasIndex(e => e.IdCliente, "idCliente");

            entity.Property(e => e.IdPedido).HasColumnName("idPedido");
            entity.Property(e => e.Estado)
                .HasMaxLength(9)
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasPrecision(0)
                .HasColumnName("fechaCreacion");
            entity.Property(e => e.FechaFinalizacion)
                .HasPrecision(0)
                .HasColumnName("fechaFinalizacion");
            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.Iva)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("iva");
            entity.Property(e => e.Total).HasColumnName("total");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("pedidos$pedidos_ibfk_1");

            entity.HasOne(d => d.IdColaboradorNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdColaborador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pedidos$pedidos_ibfk_2");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.IdPermiso).HasName("PK_permisos_idPermiso");

            entity.ToTable("permisos", "alexa_soft");

            entity.Property(e => e.IdPermiso).HasColumnName("idPermiso");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK_productos_idProducto");

            entity.ToTable("productos", "alexa_soft");

            entity.HasIndex(e => e.IdCategoriaProducto, "idCategoriaProducto");

            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.Estado)
                .HasMaxLength(11)
                .HasColumnName("estado");
            entity.Property(e => e.IdCategoriaProducto).HasColumnName("idCategoriaProducto");
            entity.Property(e => e.Marca)
                .HasMaxLength(50)
                .HasColumnName("marca");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio).HasColumnName("precio");
            entity.Property(e => e.Unidades).HasColumnName("unidades");

            entity.HasOne(d => d.IdCategoriaProductoNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoriaProducto)
                .HasConstraintName("productos$productos_ibfk_1");
        });

        modelBuilder.Entity<Proveedore>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("PK_proveedores_idProveedor");

            entity.ToTable("proveedores", "alexa_soft");

            entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(300)
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(10)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK_roles_idRol");

            entity.ToTable("roles", "alexa_soft");

            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.Estado)
                .HasMaxLength(11)
                .HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<RolesPermiso>(entity =>
        {
            entity.HasKey(e => e.IdPermisoXrol).HasName("PK_roles_permisos_idPermisoXRol");

            entity.ToTable("roles_permisos", "alexa_soft");

            entity.HasIndex(e => e.IdPermiso, "idPermiso");

            entity.HasIndex(e => e.IdRol, "idRol");

            entity.Property(e => e.IdPermisoXrol).HasColumnName("idPermisoXRol");
            entity.Property(e => e.IdPermiso).HasColumnName("idPermiso");
            entity.Property(e => e.IdRol).HasColumnName("idRol");

            entity.HasOne(d => d.IdPermisoNavigation).WithMany(p => p.RolesPermisos)
                .HasForeignKey(d => d.IdPermiso)
                .HasConstraintName("roles_permisos$roles_permisos_ibfk_2");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.RolesPermisos)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("roles_permisos$roles_permisos_ibfk_1");
        });

        modelBuilder.Entity<SalidaInsumo>(entity =>
        {
            entity.HasKey(e => e.IdInsumo).HasName("PK_salida_insumos_idInsumo");

            entity.ToTable("salida_insumos", "alexa_soft");

            entity.HasIndex(e => e.IdProducto, "idProducto");

            entity.Property(e => e.IdInsumo).HasColumnName("idInsumo");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.FechaRetiro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRetiro");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.MotivoAnular)
                .HasMaxLength(200)
                .HasColumnName("motivoAnular");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.SalidaInsumos)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("salida_insumos$salida_insumos_ibfk_1");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.IdServicio).HasName("PK_servicios_idServicio");

            entity.ToTable("servicios", "alexa_soft");

            entity.Property(e => e.IdServicio).HasColumnName("idServicio");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(300)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(11)
                .HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.TiempoMinutos).HasColumnName("tiempoMinutos");
        });

        modelBuilder.Entity<ServiciosProducto>(entity =>
        {
            entity.HasKey(e => e.IdProductoXservicio).HasName("PK_servicios_productos_idProductoXServicio");

            entity.ToTable("servicios_productos", "alexa_soft");

            entity.HasIndex(e => e.IdProducto, "idProducto");

            entity.HasIndex(e => e.IdServicio, "idServicio");

            entity.Property(e => e.IdProductoXservicio).HasColumnName("idProductoXServicio");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.IdServicio).HasColumnName("idServicio");
            entity.Property(e => e.UnidadMedida)
                .HasMaxLength(2)
                .HasColumnName("unidadMedida");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.ServiciosProductos)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("servicios_productos$servicios_productos_ibfk_2");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.ServiciosProductos)
                .HasForeignKey(d => d.IdServicio)
                .HasConstraintName("servicios_productos$servicios_productos_ibfk_1");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK_usuarios_idUsuario");

            entity.ToTable("usuarios", "alexa_soft");

            entity.HasIndex(e => e.IdRol, "idRol");

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(20)
                .HasColumnName("contrasena");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .HasColumnName("correo");
            entity.Property(e => e.Estado)
                .HasMaxLength(11)
                .HasColumnName("estado");
            entity.Property(e => e.FechaInteraccion)
                .HasColumnType("date")
                .HasColumnName("fechaInteraccion");
            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.Instagram)
                .HasMaxLength(30)
                .HasColumnName("instagram");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(10)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("usuarios$usuarios_ibfk_1");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK_ventas_idVenta");

            entity.ToTable("ventas", "alexa_soft");

            entity.HasIndex(e => e.IdColaborador, "idColaborador");

            entity.HasIndex(e => e.IdPedido, "idCotizacion");

            entity.HasIndex(e => e.IdUsuario, "idUsuario");

            entity.HasIndex(e => e.NumeroFactura, "ventas$numeroFactura").IsUnique();

            entity.Property(e => e.IdVenta).HasColumnName("idVenta");
            entity.Property(e => e.Fecha)
                .HasPrecision(0)
                .HasColumnName("fecha");
            entity.Property(e => e.IdColaborador).HasColumnName("idColaborador");
            entity.Property(e => e.IdPedido).HasColumnName("idPedido");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Iva).HasColumnName("iva");
            entity.Property(e => e.MotivoAnular)
                .HasMaxLength(200)
                .HasColumnName("motivoAnular");
            entity.Property(e => e.NumeroFactura)
                .HasMaxLength(20)
                .HasColumnName("numeroFactura");
            entity.Property(e => e.Total).HasColumnName("total");

            entity.HasOne(d => d.IdColaboradorNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdColaborador)
                .HasConstraintName("ventas$ventas_ibfk_2");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ventas$ventas_ibfk_1");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ventas$ventas_ibfk_3");
        });

        modelBuilder.Entity<Ventasdetalleproducto>(entity =>
        {
            entity.HasKey(e => e.IdVentaDetalleProducto).HasName("PK_ventasdetalleproductos_idVentaDetalleProducto");

            entity.ToTable("ventasdetalleproductos", "alexa_soft");

            entity.HasIndex(e => e.IdProducto, "idProducto");

            entity.HasIndex(e => e.IdVenta, "idVenta");

            entity.Property(e => e.IdVentaDetalleProducto).HasColumnName("idVentaDetalleProducto");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.IdVenta).HasColumnName("idVenta");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Ventasdetalleproductos)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("ventasdetalleproductos$ventasdetalleproductos_ibfk_3");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.Ventasdetalleproductos)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("ventasdetalleproductos$ventasdetalleproductos_ibfk_2");
        });

        modelBuilder.Entity<Ventasdetallesservicio>(entity =>
        {
            entity.HasKey(e => e.IdVentaDetalleServicio).HasName("PK_ventasdetallesservicios_idVentaDetalleServicio");

            entity.ToTable("ventasdetallesservicios", "alexa_soft");

            entity.HasIndex(e => e.IdVenta, "idDetalleCotizacionServicio");

            entity.HasIndex(e => e.IdPaquete, "idPaquete");

            entity.HasIndex(e => e.IdVenta, "idVenta");

            entity.Property(e => e.IdVentaDetalleServicio).HasColumnName("idVentaDetalleServicio");
            entity.Property(e => e.IdPaquete).HasColumnName("idPaquete");
            entity.Property(e => e.IdVenta).HasColumnName("idVenta");
            entity.Property(e => e.Subtotal).HasColumnName("subtotal");

            entity.HasOne(d => d.IdPaqueteNavigation).WithMany(p => p.Ventasdetallesservicios)
                .HasForeignKey(d => d.IdPaquete)
                .HasConstraintName("ventasdetallesservicios$ventasdetallesservicios_ibfk_1");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.Ventasdetallesservicios)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("ventasdetallesservicios$ventasdetallesservicios_ibfk_2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
