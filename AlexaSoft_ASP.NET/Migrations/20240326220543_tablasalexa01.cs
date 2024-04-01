using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlexaSoft_ASP.NET.Migrations
{
    /// <inheritdoc />
    public partial class tablasalexa01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "alexa_soft");

            migrationBuilder.CreateTable(
                name: "categoria_productos",
                schema: "alexa_soft",
                columns: table => new
                {
                    idCategoriaProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categoria_productos_idCategoriaProducto", x => x.idCategoriaProducto);
                });

            migrationBuilder.CreateTable(
                name: "horarios",
                schema: "alexa_soft",
                columns: table => new
                {
                    idHorario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numeroDia = table.Column<int>(type: "int", nullable: false),
                    inicioJornada = table.Column<TimeSpan>(type: "time", nullable: false),
                    finJornada = table.Column<TimeSpan>(type: "time", nullable: false),
                    estado = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_horarios_idHorario", x => x.idHorario);
                });

            migrationBuilder.CreateTable(
                name: "motivoscancelacion",
                schema: "alexa_soft",
                columns: table => new
                {
                    idMotivo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    motivo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_motivoscancelacion_idMotivo", x => x.idMotivo);
                });

            migrationBuilder.CreateTable(
                name: "paquetes",
                schema: "alexa_soft",
                columns: table => new
                {
                    idPaquete = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    estado = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paquetes_idPaquete", x => x.idPaquete);
                });

            migrationBuilder.CreateTable(
                name: "permisos",
                schema: "alexa_soft",
                columns: table => new
                {
                    idPermiso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 200, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permisos_idPermiso", x => x.idPermiso);
                });

            migrationBuilder.CreateTable(
                name: "proveedores",
                schema: "alexa_soft",
                columns: table => new
                {
                    idProveedor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    telefono = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_proveedores_idProveedor", x => x.idProveedor);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                schema: "alexa_soft",
                columns: table => new
                {
                    idRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    estado = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles_idRol", x => x.idRol);
                });

            migrationBuilder.CreateTable(
                name: "servicios",
                schema: "alexa_soft",
                columns: table => new
                {
                    idServicio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    tiempoMinutos = table.Column<int>(type: "int", nullable: false),
                    estado = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_servicios_idServicio", x => x.idServicio);
                });

            migrationBuilder.CreateTable(
                name: "productos",
                schema: "alexa_soft",
                columns: table => new
                {
                    idProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    marca = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    precio = table.Column<int>(type: "int", nullable: false),
                    unidades = table.Column<int>(type: "int", nullable: false),
                    estado = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    idCategoriaProducto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productos_idProducto", x => x.idProducto);
                    table.ForeignKey(
                        name: "productos$productos_ibfk_1",
                        column: x => x.idCategoriaProducto,
                        principalSchema: "alexa_soft",
                        principalTable: "categoria_productos",
                        principalColumn: "idCategoriaProducto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "compras",
                schema: "alexa_soft",
                columns: table => new
                {
                    idCompra = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idProveedor = table.Column<int>(type: "int", nullable: true),
                    precio = table.Column<int>(type: "int", nullable: false),
                    fecha = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(getdate())"),
                    subtotal = table.Column<int>(type: "int", nullable: false),
                    motivoAnular = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_compras_idCompra", x => x.idCompra);
                    table.ForeignKey(
                        name: "compras$compras_ibfk_1",
                        column: x => x.idProveedor,
                        principalSchema: "alexa_soft",
                        principalTable: "proveedores",
                        principalColumn: "idProveedor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "clientes",
                schema: "alexa_soft",
                columns: table => new
                {
                    idCliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    correo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    telefono = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    instagram = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    contrasena = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    estado = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    fechaInteraccion = table.Column<DateTime>(type: "date", nullable: false),
                    idRol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientes_idCliente", x => x.idCliente);
                    table.ForeignKey(
                        name: "clientes$clientes_ibfk_1",
                        column: x => x.idRol,
                        principalSchema: "alexa_soft",
                        principalTable: "roles",
                        principalColumn: "idRol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "colaboradores",
                schema: "alexa_soft",
                columns: table => new
                {
                    idColaborador = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    cedula = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    correo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    telefono = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    contrasena = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    estado = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    fechaInteraccion = table.Column<DateTime>(type: "date", nullable: false),
                    idRol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_colaboradores_idColaborador", x => x.idColaborador);
                    table.ForeignKey(
                        name: "colaboradores$colaboradores_ibfk_1",
                        column: x => x.idRol,
                        principalSchema: "alexa_soft",
                        principalTable: "roles",
                        principalColumn: "idRol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "roles_permisos",
                schema: "alexa_soft",
                columns: table => new
                {
                    idPermisoXRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idRol = table.Column<int>(type: "int", nullable: false),
                    idPermiso = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles_permisos_idPermisoXRol", x => x.idPermisoXRol);
                    table.ForeignKey(
                        name: "roles_permisos$roles_permisos_ibfk_1",
                        column: x => x.idRol,
                        principalSchema: "alexa_soft",
                        principalTable: "roles",
                        principalColumn: "idRol",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "roles_permisos$roles_permisos_ibfk_2",
                        column: x => x.idPermiso,
                        principalSchema: "alexa_soft",
                        principalTable: "permisos",
                        principalColumn: "idPermiso",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                schema: "alexa_soft",
                columns: table => new
                {
                    idUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    correo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    telefono = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    instagram = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    contrasena = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    estado = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    fechaInteraccion = table.Column<DateTime>(type: "date", nullable: false),
                    idRol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios_idUsuario", x => x.idUsuario);
                    table.ForeignKey(
                        name: "usuarios$usuarios_ibfk_1",
                        column: x => x.idRol,
                        principalSchema: "alexa_soft",
                        principalTable: "roles",
                        principalColumn: "idRol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "paquetes_servicios",
                schema: "alexa_soft",
                columns: table => new
                {
                    idPaqueteXServicio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idPaquete = table.Column<int>(type: "int", nullable: false),
                    idServicio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paquetes_servicios_idPaqueteXServicio", x => x.idPaqueteXServicio);
                    table.ForeignKey(
                        name: "paquetes_servicios$paquetes_servicios_ibfk_1",
                        column: x => x.idServicio,
                        principalSchema: "alexa_soft",
                        principalTable: "servicios",
                        principalColumn: "idServicio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "paquetes_servicios$paquetes_servicios_ibfk_2",
                        column: x => x.idPaquete,
                        principalSchema: "alexa_soft",
                        principalTable: "paquetes",
                        principalColumn: "idPaquete",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "salida_insumos",
                schema: "alexa_soft",
                columns: table => new
                {
                    idInsumo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idProducto = table.Column<int>(type: "int", nullable: false),
                    fechaRetiro = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    cantidad = table.Column<int>(type: "int", nullable: false),
                    motivoAnular = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_salida_insumos_idInsumo", x => x.idInsumo);
                    table.ForeignKey(
                        name: "salida_insumos$salida_insumos_ibfk_1",
                        column: x => x.idProducto,
                        principalSchema: "alexa_soft",
                        principalTable: "productos",
                        principalColumn: "idProducto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "servicios_productos",
                schema: "alexa_soft",
                columns: table => new
                {
                    idProductoXServicio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idServicio = table.Column<int>(type: "int", nullable: false),
                    idProducto = table.Column<int>(type: "int", nullable: false),
                    cantidad = table.Column<int>(type: "int", nullable: false),
                    unidadMedida = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_servicios_productos_idProductoXServicio", x => x.idProductoXServicio);
                    table.ForeignKey(
                        name: "servicios_productos$servicios_productos_ibfk_1",
                        column: x => x.idServicio,
                        principalSchema: "alexa_soft",
                        principalTable: "servicios",
                        principalColumn: "idServicio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "servicios_productos$servicios_productos_ibfk_2",
                        column: x => x.idProducto,
                        principalSchema: "alexa_soft",
                        principalTable: "productos",
                        principalColumn: "idProducto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "detallesproductosxcompras",
                schema: "alexa_soft",
                columns: table => new
                {
                    idDetalleProductoXCompra = table.Column<int>(type: "int", nullable: false),
                    idProducto = table.Column<int>(type: "int", nullable: false),
                    idCompra = table.Column<int>(type: "int", nullable: false),
                    unidades = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detallesproductosxcompras_idDetalleProductoXCompra", x => x.idDetalleProductoXCompra);
                    table.ForeignKey(
                        name: "detallesproductosxcompras$detallesproductosxcompras_ibfk_1",
                        column: x => x.idProducto,
                        principalSchema: "alexa_soft",
                        principalTable: "productos",
                        principalColumn: "idProducto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "detallesproductosxcompras$detallesproductosxcompras_ibfk_2",
                        column: x => x.idCompra,
                        principalSchema: "alexa_soft",
                        principalTable: "compras",
                        principalColumn: "idCompra",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "citas",
                schema: "alexa_soft",
                columns: table => new
                {
                    idCita = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fecha = table.Column<DateTime>(type: "date", nullable: false),
                    hora = table.Column<TimeSpan>(type: "time", nullable: false),
                    detalle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    estado = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    idMotivoCancelacion = table.Column<int>(type: "int", nullable: true),
                    idCliente = table.Column<int>(type: "int", nullable: false),
                    idPaquete = table.Column<int>(type: "int", nullable: false),
                    idHorario = table.Column<int>(type: "int", nullable: false),
                    idColaborador = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_citas_idCita", x => x.idCita);
                    table.ForeignKey(
                        name: "citas$citas_ibfk_10",
                        column: x => x.idCliente,
                        principalSchema: "alexa_soft",
                        principalTable: "clientes",
                        principalColumn: "idCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "citas$citas_ibfk_11",
                        column: x => x.idPaquete,
                        principalSchema: "alexa_soft",
                        principalTable: "paquetes",
                        principalColumn: "idPaquete",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "citas$citas_ibfk_5",
                        column: x => x.idHorario,
                        principalSchema: "alexa_soft",
                        principalTable: "horarios",
                        principalColumn: "idHorario",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "citas$citas_ibfk_8",
                        column: x => x.idColaborador,
                        principalSchema: "alexa_soft",
                        principalTable: "colaboradores",
                        principalColumn: "idColaborador");
                    table.ForeignKey(
                        name: "citas$citas_ibfk_9",
                        column: x => x.idMotivoCancelacion,
                        principalSchema: "alexa_soft",
                        principalTable: "motivoscancelacion",
                        principalColumn: "idMotivo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pedidos",
                schema: "alexa_soft",
                columns: table => new
                {
                    idPedido = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    fechaFinalizacion = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    estado = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    total = table.Column<int>(type: "int", nullable: false),
                    iva = table.Column<decimal>(type: "decimal(10,0)", nullable: false),
                    idCliente = table.Column<int>(type: "int", nullable: false),
                    IdColaborador = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pedidos_idPedido", x => x.idPedido);
                    table.ForeignKey(
                        name: "pedidos$pedidos_ibfk_1",
                        column: x => x.idCliente,
                        principalSchema: "alexa_soft",
                        principalTable: "clientes",
                        principalColumn: "idCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "pedidos$pedidos_ibfk_2",
                        column: x => x.IdColaborador,
                        principalSchema: "alexa_soft",
                        principalTable: "colaboradores",
                        principalColumn: "idColaborador");
                });

            migrationBuilder.CreateTable(
                name: "detallespedidosproductos",
                schema: "alexa_soft",
                columns: table => new
                {
                    idDetallePedidoProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idPedido = table.Column<int>(type: "int", nullable: false),
                    idProducto = table.Column<int>(type: "int", nullable: false),
                    unidadesXproducto = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detallespedidosproductos_idDetallePedidoProducto", x => x.idDetallePedidoProducto);
                    table.ForeignKey(
                        name: "detallespedidosproductos$detallespedidosproductos_ibfk_1",
                        column: x => x.idPedido,
                        principalSchema: "alexa_soft",
                        principalTable: "pedidos",
                        principalColumn: "idPedido",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "detallespedidosproductos$detallespedidosproductos_ibfk_2",
                        column: x => x.idProducto,
                        principalSchema: "alexa_soft",
                        principalTable: "productos",
                        principalColumn: "idProducto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "detallespedidosservicio",
                schema: "alexa_soft",
                columns: table => new
                {
                    idDetallePedidoServicio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idPaquete = table.Column<int>(type: "int", nullable: false),
                    idPedido = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detallespedidosservicio_idDetallePedidoServicio", x => x.idDetallePedidoServicio);
                    table.ForeignKey(
                        name: "detallespedidosservicio$detallespedidosservicio_ibfk_1",
                        column: x => x.idPaquete,
                        principalSchema: "alexa_soft",
                        principalTable: "paquetes",
                        principalColumn: "idPaquete",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "detallespedidosservicio$detallespedidosservicio_ibfk_2",
                        column: x => x.idPedido,
                        principalSchema: "alexa_soft",
                        principalTable: "pedidos",
                        principalColumn: "idPedido",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ventas",
                schema: "alexa_soft",
                columns: table => new
                {
                    idVenta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numeroFactura = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    idPedido = table.Column<int>(type: "int", nullable: false),
                    idColaborador = table.Column<int>(type: "int", nullable: false),
                    fecha = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    motivoAnular = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    idUsuario = table.Column<int>(type: "int", nullable: false),
                    total = table.Column<int>(type: "int", nullable: false),
                    iva = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ventas_idVenta", x => x.idVenta);
                    table.ForeignKey(
                        name: "ventas$ventas_ibfk_1",
                        column: x => x.idPedido,
                        principalSchema: "alexa_soft",
                        principalTable: "pedidos",
                        principalColumn: "idPedido");
                    table.ForeignKey(
                        name: "ventas$ventas_ibfk_2",
                        column: x => x.idColaborador,
                        principalSchema: "alexa_soft",
                        principalTable: "colaboradores",
                        principalColumn: "idColaborador",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ventas$ventas_ibfk_3",
                        column: x => x.idUsuario,
                        principalSchema: "alexa_soft",
                        principalTable: "usuarios",
                        principalColumn: "idUsuario");
                });

            migrationBuilder.CreateTable(
                name: "ventasdetalleproductos",
                schema: "alexa_soft",
                columns: table => new
                {
                    idVentaDetalleProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idVenta = table.Column<int>(type: "int", nullable: false),
                    idProducto = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ventasdetalleproductos_idVentaDetalleProducto", x => x.idVentaDetalleProducto);
                    table.ForeignKey(
                        name: "ventasdetalleproductos$ventasdetalleproductos_ibfk_2",
                        column: x => x.idVenta,
                        principalSchema: "alexa_soft",
                        principalTable: "ventas",
                        principalColumn: "idVenta",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ventasdetalleproductos$ventasdetalleproductos_ibfk_3",
                        column: x => x.idProducto,
                        principalSchema: "alexa_soft",
                        principalTable: "productos",
                        principalColumn: "idProducto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ventasdetallesservicios",
                schema: "alexa_soft",
                columns: table => new
                {
                    idVentaDetalleServicio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idVenta = table.Column<int>(type: "int", nullable: false),
                    idPaquete = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    subtotal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ventasdetallesservicios_idVentaDetalleServicio", x => x.idVentaDetalleServicio);
                    table.ForeignKey(
                        name: "ventasdetallesservicios$ventasdetallesservicios_ibfk_1",
                        column: x => x.idPaquete,
                        principalSchema: "alexa_soft",
                        principalTable: "paquetes",
                        principalColumn: "idPaquete",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ventasdetallesservicios$ventasdetallesservicios_ibfk_2",
                        column: x => x.idVenta,
                        principalSchema: "alexa_soft",
                        principalTable: "ventas",
                        principalColumn: "idVenta",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "idCliente",
                schema: "alexa_soft",
                table: "citas",
                column: "idCliente");

            migrationBuilder.CreateIndex(
                name: "idColaborador",
                schema: "alexa_soft",
                table: "citas",
                column: "idColaborador");

            migrationBuilder.CreateIndex(
                name: "idHorario",
                schema: "alexa_soft",
                table: "citas",
                column: "idHorario");

            migrationBuilder.CreateIndex(
                name: "idMotivoCancelacion",
                schema: "alexa_soft",
                table: "citas",
                column: "idMotivoCancelacion");

            migrationBuilder.CreateIndex(
                name: "idPaquete",
                schema: "alexa_soft",
                table: "citas",
                column: "idPaquete");

            migrationBuilder.CreateIndex(
                name: "clientes$correo",
                schema: "alexa_soft",
                table: "clientes",
                column: "correo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "clientes$instagram",
                schema: "alexa_soft",
                table: "clientes",
                column: "instagram",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "clientes$telefono",
                schema: "alexa_soft",
                table: "clientes",
                column: "telefono",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idRol",
                schema: "alexa_soft",
                table: "clientes",
                column: "idRol");

            migrationBuilder.CreateIndex(
                name: "colaboradores$cedula",
                schema: "alexa_soft",
                table: "colaboradores",
                column: "cedula",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "colaboradores$correo",
                schema: "alexa_soft",
                table: "colaboradores",
                column: "correo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "colaboradores$telefono",
                schema: "alexa_soft",
                table: "colaboradores",
                column: "telefono",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idRol",
                schema: "alexa_soft",
                table: "colaboradores",
                column: "idRol");

            migrationBuilder.CreateIndex(
                name: "idProveedor",
                schema: "alexa_soft",
                table: "compras",
                column: "idProveedor");

            migrationBuilder.CreateIndex(
                name: "idCotizacion",
                schema: "alexa_soft",
                table: "detallespedidosproductos",
                column: "idPedido");

            migrationBuilder.CreateIndex(
                name: "idProducto",
                schema: "alexa_soft",
                table: "detallespedidosproductos",
                column: "idProducto");

            migrationBuilder.CreateIndex(
                name: "idPaquete",
                schema: "alexa_soft",
                table: "detallespedidosservicio",
                column: "idPaquete");

            migrationBuilder.CreateIndex(
                name: "idPedido",
                schema: "alexa_soft",
                table: "detallespedidosservicio",
                column: "idPedido");

            migrationBuilder.CreateIndex(
                name: "idCompra",
                schema: "alexa_soft",
                table: "detallesproductosxcompras",
                column: "idCompra");

            migrationBuilder.CreateIndex(
                name: "idProducto",
                schema: "alexa_soft",
                table: "detallesproductosxcompras",
                column: "idProducto");

            migrationBuilder.CreateIndex(
                name: "idPaquete",
                schema: "alexa_soft",
                table: "paquetes_servicios",
                column: "idPaquete");

            migrationBuilder.CreateIndex(
                name: "idServicio",
                schema: "alexa_soft",
                table: "paquetes_servicios",
                column: "idServicio");

            migrationBuilder.CreateIndex(
                name: "idCliente",
                schema: "alexa_soft",
                table: "pedidos",
                column: "idCliente");

            migrationBuilder.CreateIndex(
                name: "IdColaborador",
                schema: "alexa_soft",
                table: "pedidos",
                column: "IdColaborador");

            migrationBuilder.CreateIndex(
                name: "idCategoriaProducto",
                schema: "alexa_soft",
                table: "productos",
                column: "idCategoriaProducto");

            migrationBuilder.CreateIndex(
                name: "idPermiso",
                schema: "alexa_soft",
                table: "roles_permisos",
                column: "idPermiso");

            migrationBuilder.CreateIndex(
                name: "idRol",
                schema: "alexa_soft",
                table: "roles_permisos",
                column: "idRol");

            migrationBuilder.CreateIndex(
                name: "idProducto",
                schema: "alexa_soft",
                table: "salida_insumos",
                column: "idProducto");

            migrationBuilder.CreateIndex(
                name: "idProducto",
                schema: "alexa_soft",
                table: "servicios_productos",
                column: "idProducto");

            migrationBuilder.CreateIndex(
                name: "idServicio",
                schema: "alexa_soft",
                table: "servicios_productos",
                column: "idServicio");

            migrationBuilder.CreateIndex(
                name: "idRol",
                schema: "alexa_soft",
                table: "usuarios",
                column: "idRol");

            migrationBuilder.CreateIndex(
                name: "idColaborador",
                schema: "alexa_soft",
                table: "ventas",
                column: "idColaborador");

            migrationBuilder.CreateIndex(
                name: "idCotizacion",
                schema: "alexa_soft",
                table: "ventas",
                column: "idPedido");

            migrationBuilder.CreateIndex(
                name: "idUsuario",
                schema: "alexa_soft",
                table: "ventas",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "ventas$numeroFactura",
                schema: "alexa_soft",
                table: "ventas",
                column: "numeroFactura",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idProducto",
                schema: "alexa_soft",
                table: "ventasdetalleproductos",
                column: "idProducto");

            migrationBuilder.CreateIndex(
                name: "idVenta",
                schema: "alexa_soft",
                table: "ventasdetalleproductos",
                column: "idVenta");

            migrationBuilder.CreateIndex(
                name: "idDetalleCotizacionServicio",
                schema: "alexa_soft",
                table: "ventasdetallesservicios",
                column: "idVenta");

            migrationBuilder.CreateIndex(
                name: "idPaquete",
                schema: "alexa_soft",
                table: "ventasdetallesservicios",
                column: "idPaquete");

            migrationBuilder.CreateIndex(
                name: "idVenta",
                schema: "alexa_soft",
                table: "ventasdetallesservicios",
                column: "idVenta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "citas",
                schema: "alexa_soft");

            migrationBuilder.DropTable(
                name: "detallespedidosproductos",
                schema: "alexa_soft");

            migrationBuilder.DropTable(
                name: "detallespedidosservicio",
                schema: "alexa_soft");

            migrationBuilder.DropTable(
                name: "detallesproductosxcompras",
                schema: "alexa_soft");

            migrationBuilder.DropTable(
                name: "paquetes_servicios",
                schema: "alexa_soft");

            migrationBuilder.DropTable(
                name: "roles_permisos",
                schema: "alexa_soft");

            migrationBuilder.DropTable(
                name: "salida_insumos",
                schema: "alexa_soft");

            migrationBuilder.DropTable(
                name: "servicios_productos",
                schema: "alexa_soft");

            migrationBuilder.DropTable(
                name: "ventasdetalleproductos",
                schema: "alexa_soft");

            migrationBuilder.DropTable(
                name: "ventasdetallesservicios",
                schema: "alexa_soft");

            migrationBuilder.DropTable(
                name: "horarios",
                schema: "alexa_soft");

            migrationBuilder.DropTable(
                name: "motivoscancelacion",
                schema: "alexa_soft");

            migrationBuilder.DropTable(
                name: "compras",
                schema: "alexa_soft");

            migrationBuilder.DropTable(
                name: "permisos",
                schema: "alexa_soft");

            migrationBuilder.DropTable(
                name: "servicios",
                schema: "alexa_soft");

            migrationBuilder.DropTable(
                name: "productos",
                schema: "alexa_soft");

            migrationBuilder.DropTable(
                name: "paquetes",
                schema: "alexa_soft");

            migrationBuilder.DropTable(
                name: "ventas",
                schema: "alexa_soft");

            migrationBuilder.DropTable(
                name: "proveedores",
                schema: "alexa_soft");

            migrationBuilder.DropTable(
                name: "categoria_productos",
                schema: "alexa_soft");

            migrationBuilder.DropTable(
                name: "pedidos",
                schema: "alexa_soft");

            migrationBuilder.DropTable(
                name: "usuarios",
                schema: "alexa_soft");

            migrationBuilder.DropTable(
                name: "clientes",
                schema: "alexa_soft");

            migrationBuilder.DropTable(
                name: "colaboradores",
                schema: "alexa_soft");

            migrationBuilder.DropTable(
                name: "roles",
                schema: "alexa_soft");
        }
    }
}
