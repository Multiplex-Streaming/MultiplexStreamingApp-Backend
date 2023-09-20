﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Multiplex.Domain.Models;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Multiplex.Domain.Context.Autogenerated
{
    public partial class MultiplexContext : DbContext
    {
        public MultiplexContext()
        {
        }

        public MultiplexContext(DbContextOptions<MultiplexContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Canales> Canales { get; set; }
        public virtual DbSet<CapituloSerie> CapituloSerie { get; set; }
        public virtual DbSet<EstadosCuentas> EstadosCuentas { get; set; }
        public virtual DbSet<FavoritosPelicula> FavoritosPelicula { get; set; }
        public virtual DbSet<FavoritosSeries> FavoritosSeries { get; set; }
        public virtual DbSet<Generos> Generos { get; set; }
        public virtual DbSet<GenerosPeliculas> GenerosPeliculas { get; set; }
        public virtual DbSet<GenerosSeries> GenerosSeries { get; set; }
        public virtual DbSet<HistorialPeliculas> HistorialPeliculas { get; set; }
        public virtual DbSet<HistorialSeries> HistorialSeries { get; set; }
        public virtual DbSet<Peliculas> Peliculas { get; set; }
        public virtual DbSet<Series> Series { get; set; }
        public virtual DbSet<TiposCuentas> TiposCuentas { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=default");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Canales>(entity =>
            {
                entity.HasKey(e => e.IdCn)
                    .HasName("PK__CANALES__8B622F9993D86E37");

                entity.ToTable("CANALES");

                entity.HasIndex(e => e.IdCn)
                    .HasName("CANALES_PK")
                    .IsUnique();

                entity.Property(e => e.IdCn).HasColumnName("ID_CN");

                entity.Property(e => e.NombreCn)
                    .IsRequired()
                    .HasColumnName("NOMBRE_CN")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CapituloSerie>(entity =>
            {
                entity.HasKey(e => new { e.IdSr, e.IdCp })
                    .HasName("PK__CAPITULO__23D58B78B479C9E6");

                entity.ToTable("CAPITULO_SERIE");

                entity.HasIndex(e => e.IdSr)
                    .HasName("RELATION_91_FK");

                entity.HasIndex(e => new { e.IdSr, e.IdCp })
                    .HasName("CAPITULO_SERIE_PK")
                    .IsUnique();

                entity.Property(e => e.IdSr).HasColumnName("ID_SR");

                entity.Property(e => e.IdCp).HasColumnName("ID_CP");

                entity.Property(e => e.DescripcionCp)
                    .HasColumnName("DESCRIPCION_CP")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DuracionCp)
                    .HasColumnName("DURACION_CP")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.NombreCp)
                    .IsRequired()
                    .HasColumnName("NOMBRE_CP")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PortadaCp)
                    .HasColumnName("PORTADA_CP")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UrlCp)
                    .IsRequired()
                    .HasColumnName("URL_CP")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdSrNavigation)
                    .WithMany(p => p.CapituloSerie)
                    .HasForeignKey(d => d.IdSr)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CAPITULO___ID_SR__33D4B598");
            });

            modelBuilder.Entity<EstadosCuentas>(entity =>
            {
                entity.HasKey(e => e.IdEc)
                    .HasName("PK__ESTADOS___8B62DF43B2FFE2A5");

                entity.ToTable("ESTADOS_CUENTAS");

                entity.HasIndex(e => e.IdEc)
                    .HasName("ESTADOS_CUENTAS_PK")
                    .IsUnique();

                entity.Property(e => e.IdEc).HasColumnName("ID_EC");

                entity.Property(e => e.DescripcionEc)
                    .IsRequired()
                    .HasColumnName("DESCRIPCION_EC")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FavoritosPelicula>(entity =>
            {
                entity.HasKey(e => new { e.IdPl, e.IdUsr })
                    .HasName("PK__FAVORITO__39CB56BD33362D55");

                entity.ToTable("FAVORITOS_PELICULA");

                entity.HasIndex(e => e.IdPl)
                    .HasName("FAVORITOS_PELICULA_FK2");

                entity.HasIndex(e => e.IdUsr)
                    .HasName("FAVORITOS_PELICULA_FK");

                entity.HasIndex(e => new { e.IdPl, e.IdUsr })
                    .HasName("FAVORITOS_PELICULA_PK")
                    .IsUnique();

                entity.Property(e => e.IdPl).HasColumnName("ID_PL");

                entity.Property(e => e.IdUsr).HasColumnName("ID_USR");

                entity.HasOne(d => d.IdPlNavigation)
                    .WithMany(p => p.FavoritosPelicula)
                    .HasForeignKey(d => d.IdPl)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FAVORITOS__ID_PL__4222D4EF");

                entity.HasOne(d => d.IdUsrNavigation)
                    .WithMany(p => p.FavoritosPelicula)
                    .HasForeignKey(d => d.IdUsr)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FAVORITOS__ID_US__4316F928");
            });

            modelBuilder.Entity<FavoritosSeries>(entity =>
            {
                entity.HasKey(e => new { e.IdSr, e.IdUsr })
                    .HasName("PK__FAVORITO__39CB6F139E0A786F");

                entity.ToTable("FAVORITOS_SERIES");

                entity.HasIndex(e => e.IdSr)
                    .HasName("FAVORITOS_SERIES_FK2");

                entity.HasIndex(e => e.IdUsr)
                    .HasName("FAVORITOS_SERIES_FK");

                entity.HasIndex(e => new { e.IdSr, e.IdUsr })
                    .HasName("FAVORITOS_SERIES_PK")
                    .IsUnique();

                entity.Property(e => e.IdSr).HasColumnName("ID_SR");

                entity.Property(e => e.IdUsr).HasColumnName("ID_USR");

                entity.HasOne(d => d.IdSrNavigation)
                    .WithMany(p => p.FavoritosSeries)
                    .HasForeignKey(d => d.IdSr)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FAVORITOS__ID_SR__49C3F6B7");

                entity.HasOne(d => d.IdUsrNavigation)
                    .WithMany(p => p.FavoritosSeries)
                    .HasForeignKey(d => d.IdUsr)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FAVORITOS__ID_US__4AB81AF0");
            });

            modelBuilder.Entity<Generos>(entity =>
            {
                entity.HasKey(e => e.IdGn)
                    .HasName("PK__GENEROS__8B62CF0A43F16FDB");

                entity.ToTable("GENEROS");

                entity.HasIndex(e => e.IdGn)
                    .HasName("GENEROS_PK")
                    .IsUnique();

                entity.Property(e => e.IdGn).HasColumnName("ID_GN");

                entity.Property(e => e.DescripcionGn)
                    .IsRequired()
                    .HasColumnName("DESCRIPCION_GN")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GenerosPeliculas>(entity =>
            {
                entity.HasKey(e => new { e.IdGn, e.IdPl })
                    .HasName("PK__GENEROS___63D4F608D6760CDD");

                entity.ToTable("GENEROS_PELICULAS");

                entity.HasIndex(e => e.IdGn)
                    .HasName("RELATION_88_FK2");

                entity.HasIndex(e => e.IdPl)
                    .HasName("RELATION_88_FK");

                entity.HasIndex(e => new { e.IdGn, e.IdPl })
                    .HasName("RELATION_88_PK")
                    .IsUnique();

                entity.Property(e => e.IdGn).HasColumnName("ID_GN");

                entity.Property(e => e.IdPl).HasColumnName("ID_PL");

                entity.HasOne(d => d.IdGnNavigation)
                    .WithMany(p => p.GenerosPeliculas)
                    .HasForeignKey(d => d.IdGn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GENEROS_P__ID_GN__36B12243");

                entity.HasOne(d => d.IdPlNavigation)
                    .WithMany(p => p.GenerosPeliculas)
                    .HasForeignKey(d => d.IdPl)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GENEROS_P__ID_PL__37A5467C");
            });

            modelBuilder.Entity<GenerosSeries>(entity =>
            {
                entity.HasKey(e => new { e.IdGn, e.IdSr })
                    .HasName("PK__GENEROS___83D4F592FB5D004C");

                entity.ToTable("GENEROS_SERIES");

                entity.HasIndex(e => e.IdGn)
                    .HasName("RELATION_89_FK2");

                entity.HasIndex(e => e.IdSr)
                    .HasName("RELATION_89_FK");

                entity.HasIndex(e => new { e.IdGn, e.IdSr })
                    .HasName("RELATION_89_PK")
                    .IsUnique();

                entity.Property(e => e.IdGn).HasColumnName("ID_GN");

                entity.Property(e => e.IdSr).HasColumnName("ID_SR");

                entity.HasOne(d => d.IdGnNavigation)
                    .WithMany(p => p.GenerosSeries)
                    .HasForeignKey(d => d.IdGn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GENEROS_S__ID_GN__3A81B327");

                entity.HasOne(d => d.IdSrNavigation)
                    .WithMany(p => p.GenerosSeries)
                    .HasForeignKey(d => d.IdSr)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GENEROS_S__ID_SR__3B75D760");
            });

            modelBuilder.Entity<HistorialPeliculas>(entity =>
            {
                entity.HasKey(e => new { e.IdPl, e.IdUsr })
                    .HasName("PK__HISTORIA__39CB56BDB9DB2C2F");

                entity.ToTable("HISTORIAL_PELICULAS");

                entity.HasIndex(e => e.IdPl)
                    .HasName("HISTORIAL_PELICULAS_FK2");

                entity.HasIndex(e => e.IdUsr)
                    .HasName("HISTORIAL_PELICULAS_FK");

                entity.HasIndex(e => new { e.IdPl, e.IdUsr })
                    .HasName("HISTORIAL_PELICULAS_PK")
                    .IsUnique();

                entity.Property(e => e.IdPl).HasColumnName("ID_PL");

                entity.Property(e => e.IdUsr).HasColumnName("ID_USR");

                entity.HasOne(d => d.IdPlNavigation)
                    .WithMany(p => p.HistorialPeliculas)
                    .HasForeignKey(d => d.IdPl)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HISTORIAL__ID_PL__3E52440B");

                entity.HasOne(d => d.IdUsrNavigation)
                    .WithMany(p => p.HistorialPeliculas)
                    .HasForeignKey(d => d.IdUsr)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HISTORIAL__ID_US__3F466844");
            });

            modelBuilder.Entity<HistorialSeries>(entity =>
            {
                entity.HasKey(e => new { e.IdSr, e.IdUsr })
                    .HasName("PK__HISTORIA__39CB6F1366C29DB5");

                entity.ToTable("HISTORIAL_SERIES");

                entity.HasIndex(e => e.IdSr)
                    .HasName("HISTORIAL_SERIES_FK2");

                entity.HasIndex(e => e.IdUsr)
                    .HasName("HISTORIAL_SERIES_FK");

                entity.HasIndex(e => new { e.IdSr, e.IdUsr })
                    .HasName("HISTORIAL_SERIES_PK")
                    .IsUnique();

                entity.Property(e => e.IdSr).HasColumnName("ID_SR");

                entity.Property(e => e.IdUsr).HasColumnName("ID_USR");

                entity.HasOne(d => d.IdSrNavigation)
                    .WithMany(p => p.HistorialSeries)
                    .HasForeignKey(d => d.IdSr)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HISTORIAL__ID_SR__45F365D3");

                entity.HasOne(d => d.IdUsrNavigation)
                    .WithMany(p => p.HistorialSeries)
                    .HasForeignKey(d => d.IdUsr)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HISTORIAL__ID_US__46E78A0C");
            });

            modelBuilder.Entity<Peliculas>(entity =>
            {
                entity.HasKey(e => e.IdPl)
                    .HasName("PK__PELICULA__8B63902F7434654B");

                entity.ToTable("PELICULAS");

                entity.HasIndex(e => e.IdPl)
                    .HasName("PELICULAS_PK")
                    .IsUnique();

                entity.Property(e => e.IdPl).HasColumnName("ID_PL");

                entity.Property(e => e.DescripcionPl)
                    .HasColumnName("DESCRIPCION_PL")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.DuracionPl)
                    .HasColumnName("DURACION_PL")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ElencoPl)
                    .HasColumnName("ELENCO_PL")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.PortadaPl)
                    .HasColumnName("PORTADA_PL")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TituloPl)
                    .IsRequired()
                    .HasColumnName("TITULO_PL")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UrlPl)
                    .IsRequired()
                    .HasColumnName("URL_PL")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Series>(entity =>
            {
                entity.HasKey(e => e.IdSr)
                    .HasName("PK__SERIES__8B63A981490F9C24");

                entity.ToTable("SERIES");

                entity.HasIndex(e => e.IdSr)
                    .HasName("SERIES_PK")
                    .IsUnique();

                entity.Property(e => e.IdSr).HasColumnName("ID_SR");

                entity.Property(e => e.CantCapitulosSr).HasColumnName("CANT_CAPITULOS_SR");

                entity.Property(e => e.DescripcionSr)
                    .HasColumnName("DESCRIPCION_SR")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.NombreSr)
                    .IsRequired()
                    .HasColumnName("NOMBRE_SR")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TiposCuentas>(entity =>
            {
                entity.HasKey(e => e.IdTc)
                    .HasName("PK__TIPOS_CU__8B63B1B142CB1630");

                entity.ToTable("TIPOS_CUENTAS");

                entity.HasIndex(e => e.IdTc)
                    .HasName("TIPOS_CUENTAS_PK")
                    .IsUnique();

                entity.Property(e => e.IdTc).HasColumnName("ID_TC");

                entity.Property(e => e.DescripcionTc)
                    .IsRequired()
                    .HasColumnName("DESCRIPCION_TC")
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasKey(e => e.IdUsr)
                    .HasName("PK__USUARIOS__2A8C692A42065764");

                entity.ToTable("USUARIOS");

                entity.HasIndex(e => e.IdEc)
                    .HasName("RELATION_31_FK");

                entity.HasIndex(e => e.IdTc)
                    .HasName("RELATION_23_FK");

                entity.HasIndex(e => e.IdUsr)
                    .HasName("USUARIOS_PK")
                    .IsUnique();

                entity.Property(e => e.IdUsr).HasColumnName("ID_USR");

                entity.Property(e => e.ApellidoUsr)
                    .IsRequired()
                    .HasColumnName("APELLIDO_USR")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CorreoUsr)
                    .IsRequired()
                    .HasColumnName("CORREO_USR")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaAltaUsr)
                    .HasColumnName("FECHA_ALTA_USR")
                    .HasColumnType("date");

                entity.Property(e => e.FechaModificacionUsr)
                    .HasColumnName("FECHA_MODIFICACION_USR")
                    .HasColumnType("date");

                entity.Property(e => e.IdEc).HasColumnName("ID_EC");

                entity.Property(e => e.IdTc).HasColumnName("ID_TC");

                entity.Property(e => e.NombreUsr)
                    .IsRequired()
                    .HasColumnName("NOMBRE_USR")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordUsr)
                    .IsRequired()
                    .HasColumnName("PASSWORD_USR")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VerificacionUsr)
                    .HasColumnName("VERIFICACION_USR")
                    .HasColumnType("numeric(1, 0)");

                entity.HasOne(d => d.IdEcNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdEc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__USUARIOS__ID_EC__30F848ED");

                entity.HasOne(d => d.IdTcNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdTc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__USUARIOS__ID_TC__300424B4");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
