using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace LabWeb_02.Models
{
    public partial class VentasIDATContext : DbContext
    {
        public VentasIDATContext()
        {
        }

        public VentasIDATContext(DbContextOptions<VentasIDATContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<CompañiasDeEnvio> CompañiasDeEnvios { get; set; }
        public virtual DbSet<DetallesDePedido> DetallesDePedidos { get; set; }
        public virtual DbSet<Empleado> Empleados { get; set; }
        public virtual DbSet<Pedido> Pedidos { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Proveedore> Proveedores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=VentasIDAT;Integrated Security=SSPI; User ID=sa;Password=********;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.IdCategoria);

                entity.Property(e => e.IdCategoria).ValueGeneratedNever();

                entity.Property(e => e.Imagen).HasColumnType("image");

                entity.Property(e => e.NombreCategoria)
                    .IsRequired()
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente);

                entity.Property(e => e.IdCliente).HasMaxLength(5);

                entity.Property(e => e.CargoContacto).HasMaxLength(30);

                entity.Property(e => e.Ciudad).HasMaxLength(15);

                entity.Property(e => e.CodPostal).HasMaxLength(10);

                entity.Property(e => e.Direccion).HasMaxLength(60);

                entity.Property(e => e.Fax).HasMaxLength(24);

                entity.Property(e => e.NombreCompañia)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.NombreContacto).HasMaxLength(30);

                entity.Property(e => e.Pais).HasMaxLength(15);

                entity.Property(e => e.Region).HasMaxLength(15);

                entity.Property(e => e.Telefono).HasMaxLength(24);
            });

            modelBuilder.Entity<CompañiasDeEnvio>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Compañias de envios");

                entity.Property(e => e.NombreCompañia)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.Telefono).HasMaxLength(24);
            });

            modelBuilder.Entity<DetallesDePedido>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Detalles de pedidos");

                entity.Property(e => e.PrecioUnidad).HasColumnType("money");

                entity.HasOne(d => d.IdPedidoNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdPedido)
                    .HasConstraintName("FK_Detalles de pedidos_Pedidos");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Detalles de pedidos_Productos");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.IdEmpleado);

                entity.Property(e => e.IdEmpleado).ValueGeneratedNever();

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Cargo).HasMaxLength(30);

                entity.Property(e => e.Ciudad).HasMaxLength(15);

                entity.Property(e => e.CodPostal).HasMaxLength(10);

                entity.Property(e => e.Direccion).HasMaxLength(60);

                entity.Property(e => e.Extension).HasMaxLength(4);

                entity.Property(e => e.FechaContratacion).HasColumnType("datetime");

                entity.Property(e => e.FechaNacimiento).HasColumnType("datetime");

                entity.Property(e => e.Foto).HasColumnType("image");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Pais).HasMaxLength(15);

                entity.Property(e => e.Region).HasMaxLength(15);

                entity.Property(e => e.TelDomicilio).HasMaxLength(24);

                entity.Property(e => e.Tratamiento).HasMaxLength(25);
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(e => e.IdPedido);

                entity.Property(e => e.IdPedido).ValueGeneratedNever();

                entity.Property(e => e.Cargo).HasColumnType("money");

                entity.Property(e => e.CiudadDestinatario).HasMaxLength(15);

                entity.Property(e => e.CodPostalDestinatario).HasMaxLength(10);

                entity.Property(e => e.Destinatario).HasMaxLength(40);

                entity.Property(e => e.DireccionDestinatario).HasMaxLength(60);

                entity.Property(e => e.FechaEntrega).HasColumnType("datetime");

                entity.Property(e => e.FechaEnvio).HasColumnType("datetime");

                entity.Property(e => e.FechaPedido).HasColumnType("datetime");

                entity.Property(e => e.IdCliente)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.PaisDestinatario).HasMaxLength(15);

                entity.Property(e => e.RegionDestinatario).HasMaxLength(15);

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pedidos_Clientes");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.IdEmpleado)
                    .HasConstraintName("FK_Pedidos_Empleados");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto);

                entity.Property(e => e.IdProducto).ValueGeneratedNever();

                entity.Property(e => e.CantidadPorUnidad).HasMaxLength(20);

                entity.Property(e => e.NombreProducto)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.PrecioUnidad).HasColumnType("money");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("FK_Productos_Categorias");

                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdProveedor)
                    .HasConstraintName("FK_Productos_Proveedores");
            });

            modelBuilder.Entity<Proveedore>(entity =>
            {
                entity.HasKey(e => e.IdProveedor);

                entity.Property(e => e.IdProveedor).ValueGeneratedNever();

                entity.Property(e => e.CargoContacto).HasMaxLength(30);

                entity.Property(e => e.Ciudad).HasMaxLength(15);

                entity.Property(e => e.CodPostal).HasMaxLength(10);

                entity.Property(e => e.Direccion).HasMaxLength(60);

                entity.Property(e => e.Fax).HasMaxLength(24);

                entity.Property(e => e.NombreCompañia)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.NombreContacto).HasMaxLength(30);

                entity.Property(e => e.Pais).HasMaxLength(15);

                entity.Property(e => e.Region).HasMaxLength(15);

                entity.Property(e => e.Telefono).HasMaxLength(24);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
