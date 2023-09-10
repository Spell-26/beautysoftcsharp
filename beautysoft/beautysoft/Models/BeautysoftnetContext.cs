using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace beautysoft.Models;

public partial class BeautysoftnetContext : DbContext
{
    public BeautysoftnetContext()
    {
    }

    public BeautysoftnetContext(DbContextOptions<BeautysoftnetContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Citas> Cita { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Estilistas> Estilista { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
           // optionsBuilder.UseSqlServer("server=localhost; database=beautysoftnet; integrated security=true; Encrypt = False");
        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Citas>(entity =>
        {
            entity.HasKey(e => e.IdCita).HasName("PK__cita__6AEC3C091D4EEC2B");

            entity.ToTable("cita");

            entity.Property(e => e.IdCita).HasColumnName("id_cita");
            entity.Property(e => e.Fecha)
                .HasColumnType("date")
                .HasColumnName("fecha");
            entity.Property(e => e.Hora).HasColumnName("hora");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdEstilista).HasColumnName("id_estilista");
            entity.Property(e => e.IdServicio).HasColumnName("id_servicio");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__cita__id_cliente__45F365D3");

            entity.HasOne(d => d.IdEstilistaNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdEstilista)
                .HasConstraintName("FK__cita__id_estilis__46E78A0C");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.Cita)
                .HasForeignKey(d => d.IdServicio)
                .HasConstraintName("FK__cita__id_servici__44FF419A");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__cliente__677F38F50F91C9CA");

            entity.ToTable("cliente");

            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__cliente__id_rol__4316F928");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__cliente__id_usua__412EB0B6");
        });

        modelBuilder.Entity<Estilistas>(entity =>
        {
            entity.HasKey(e => e.IdEstilista).HasName("PK__estilist__0FF69737540E3BD0");

            entity.ToTable("estilista");

            entity.Property(e => e.IdEstilista).HasColumnName("id_estilista");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Estilista)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__estilista__id_ro__440B1D61");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Estilista)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__estilista__id_us__4222D4EF");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__rol__6ABCB5E034B94F56");

            entity.ToTable("rol");

            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.NombreRol)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_rol");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.IdServicio).HasName("PK__servicio__6FD07FDC6793ABC0");

            entity.ToTable("servicio");

            entity.Property(e => e.IdServicio).HasColumnName("id_servicio");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio).HasColumnName("precio");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__usuario__4E3E04AD9AFD78BE");

            entity.ToTable("usuario");

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Contrasenna)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("contrasenna");
            entity.Property(e => e.Correo)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
