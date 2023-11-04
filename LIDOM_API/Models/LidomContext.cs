using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LIDOM_API.Models;

public partial class LidomContext : DbContext
{
    public LidomContext()
    {
    }

    public LidomContext(DbContextOptions<LidomContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CalendarioJuego> CalendarioJuegos { get; set; }

    public virtual DbSet<Equipo> Equipos { get; set; }

    public virtual DbSet<Estadio> Estadios { get; set; }

    public virtual DbSet<FechaPartido> FechaPartidos { get; set; }

    public virtual DbSet<Jugadore> Jugadores { get; set; }

    public virtual DbSet<Partido> Partidos { get; set; }

    public virtual DbSet<Posicione> Posiciones { get; set; }

    public virtual DbSet<ResultadoEquipo> ResultadoEquipos { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Temporada> Temporadas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("server=localhost; database=LIDOM; integrated security=true; TrustServerCertificate=true;");


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CalendarioJuego>(entity =>
        {
            entity.HasKey(e => e.CalJuegoId).HasName("PK__Calendar__0F71E9F117ED9F91");

            entity.Property(e => e.CalJuegoId).HasColumnName("Cal_Juego_Id");
            entity.Property(e => e.CalEquipoLocal).HasColumnName("Cal_Equipo_Local");
            entity.Property(e => e.CalEquipoVisitante).HasColumnName("Cal_Equipo_Visitante");
            entity.Property(e => e.CalFechaPartido).HasColumnName("Cal_FechaPartido");

            entity.HasOne(d => d.CalEquipoLocalNavigation).WithMany(p => p.CalendarioJuegoCalEquipoLocalNavigations)
                .HasForeignKey(d => d.CalEquipoLocal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Calendari__Cal_E__49C3F6B7");

            entity.HasOne(d => d.CalEquipoVisitanteNavigation).WithMany(p => p.CalendarioJuegoCalEquipoVisitanteNavigations)
                .HasForeignKey(d => d.CalEquipoVisitante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Calendari__Cal_E__4AB81AF0");

            entity.HasOne(d => d.CalFechaPartidoNavigation).WithMany(p => p.CalendarioJuegos)
                .HasForeignKey(d => d.CalFechaPartido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Calendari__Cal_F__48CFD27E");
        });

        modelBuilder.Entity<Equipo>(entity =>
        {
            entity.HasKey(e => e.EqId).HasName("PK__Equipos__B2C8DEB05F954A2F");

            entity.Property(e => e.EqId).HasColumnName("Eq_Id");
            entity.Property(e => e.EqCiudad)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Eq_Ciudad");
            entity.Property(e => e.EqDescripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Eq_Descripcion");
            entity.Property(e => e.EqEstadio).HasColumnName("Eq_Estadio");
            entity.Property(e => e.EqEstatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("Eq_Estatus");
            entity.Property(e => e.EqNombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Eq_Nombre");

            entity.HasOne(d => d.EqEstadioNavigation).WithMany(p => p.Equipos)
                .HasForeignKey(d => d.EqEstadio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Equipos__Eq_Esta__4222D4EF");
        });

        modelBuilder.Entity<Estadio>(entity =>
        {
            entity.HasKey(e => e.EstId).HasName("PK__Estadios__345473BC2BA1C032");

            entity.Property(e => e.EstId).HasColumnName("Est_Id");
            entity.Property(e => e.EstNombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Est_Nombre");
            entity.Property(e => e.EstUbicacion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Est_Ubicacion");
        });

        modelBuilder.Entity<FechaPartido>(entity =>
        {
            entity.HasKey(e => e.FecId).HasName("PK__FechaPar__47F0F4F9C979EF57");

            entity.Property(e => e.FecId).HasColumnName("Fec_Id");
            entity.Property(e => e.FecFechaPartido)
                .HasColumnType("date")
                .HasColumnName("Fec_FechaPartido");
            entity.Property(e => e.FecHora).HasColumnName("Fec_Hora");
            entity.Property(e => e.FecTemporada).HasColumnName("Fec_Temporada");

            entity.HasOne(d => d.FecTemporadaNavigation).WithMany(p => p.FechaPartidos)
                .HasForeignKey(d => d.FecTemporada)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FechaPart__Fec_T__3D5E1FD2");
        });

        modelBuilder.Entity<Jugadore>(entity =>
        {
            entity.HasKey(e => e.JugId).HasName("PK__Jugadore__8F35D6776775AFC4");

            entity.Property(e => e.JugId).HasColumnName("Jug_Id");
            entity.Property(e => e.JugApellidos)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Jug_Apellidos");
            entity.Property(e => e.JugEdad).HasColumnName("Jug_Edad");
            entity.Property(e => e.JugEquipo).HasColumnName("Jug_Equipo");
            entity.Property(e => e.JugNombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Jug_Nombre");
            entity.Property(e => e.JugNumCamiseta).HasColumnName("Jug_Num_Camiseta");
            entity.Property(e => e.JugPosicion).HasColumnName("Jug_Posicion");

            entity.HasOne(d => d.JugEquipoNavigation).WithMany(p => p.Jugadores)
                .HasForeignKey(d => d.JugEquipo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Jugadores__Jug_E__45F365D3");

            entity.HasOne(d => d.JugPosicionNavigation).WithMany(p => p.Jugadores)
                .HasForeignKey(d => d.JugPosicion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Jugadores__Jug_P__44FF419A");
        });

        modelBuilder.Entity<Partido>(entity =>
        {
            entity.HasKey(e => e.ParId).HasName("PK__Partidos__620469CED870314A");

            entity.Property(e => e.ParId).HasColumnName("Par_Id");
            entity.Property(e => e.ParEquipoGanador).HasColumnName("Par_Equipo_Ganador");
            entity.Property(e => e.ParEquipoPerdedor).HasColumnName("Par_Equipo_Perdedor");
            entity.Property(e => e.ParJuego).HasColumnName("Par_Juego");

            entity.HasOne(d => d.ParEquipoGanadorNavigation).WithMany(p => p.PartidoParEquipoGanadorNavigations)
                .HasForeignKey(d => d.ParEquipoGanador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Partidos__Par_Eq__4E88ABD4");

            entity.HasOne(d => d.ParEquipoPerdedorNavigation).WithMany(p => p.PartidoParEquipoPerdedorNavigations)
                .HasForeignKey(d => d.ParEquipoPerdedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Partidos__Par_Eq__4F7CD00D");

            entity.HasOne(d => d.ParJuegoNavigation).WithMany(p => p.Partidos)
                .HasForeignKey(d => d.ParJuego)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Partidos__Par_Ju__4D94879B");
        });

        modelBuilder.Entity<Posicione>(entity =>
        {
            entity.HasKey(e => e.PosId).HasName("PK__Posicion__B4D5D4573E7C83AC");

            entity.Property(e => e.PosId).HasColumnName("Pos_Id");
            entity.Property(e => e.PosNombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Pos_Nombre");
        });

        modelBuilder.Entity<ResultadoEquipo>(entity =>
        {
            entity.HasKey(e => e.ResId).HasName("PK__Resultad__11B934A56CAA8243");

            entity.Property(e => e.ResId).HasColumnName("Res_Id");
            entity.Property(e => e.ResCarreras).HasColumnName("Res_Carreras");
            entity.Property(e => e.ResEquipo).HasColumnName("Res_Equipo");
            entity.Property(e => e.ResErrores).HasColumnName("Res_Errores");
            entity.Property(e => e.ResHits).HasColumnName("Res_Hits");
            entity.Property(e => e.ResJuegoEmpate).HasColumnName("Res_Juego_Empate");
            entity.Property(e => e.ResJuegoGanado).HasColumnName("Res_Juego_Ganado");
            entity.Property(e => e.ResJuegoPerdido).HasColumnName("Res_Juego_Perdido");
            entity.Property(e => e.ResPartido).HasColumnName("Res_Partido");

            entity.HasOne(d => d.ResEquipoNavigation).WithMany(p => p.ResultadoEquipos)
                .HasForeignKey(d => d.ResEquipo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Resultado__Res_E__534D60F1");

            entity.HasOne(d => d.ResPartidoNavigation).WithMany(p => p.ResultadoEquipos)
                .HasForeignKey(d => d.ResPartido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Resultado__Res_P__52593CB8");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("PK__Roles__795EBD49F995DF0E");

            entity.Property(e => e.RolId).HasColumnName("Rol_Id");
            entity.Property(e => e.RolEstatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Rol_Estatus");
            entity.Property(e => e.RolNombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Rol_Nombre");
        });

        modelBuilder.Entity<Temporada>(entity =>
        {
            entity.HasKey(e => e.TemId).HasName("PK__Temporad__0327B2C7F8DE911B");

            entity.Property(e => e.TemId).HasColumnName("Tem_Id");
            entity.Property(e => e.TemFecFinal)
                .HasColumnType("date")
                .HasColumnName("Tem_Fec_Final");
            entity.Property(e => e.TemFecInicio)
                .HasColumnType("date")
                .HasColumnName("Tem_Fec_Inicio");
            entity.Property(e => e.TemNombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Tem_Nombre");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuId).HasName("PK__Usuarios__B6173FCBDF718959");

            entity.Property(e => e.UsuId).HasColumnName("Usu_Id");
            entity.Property(e => e.UsuEstado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Usu_Estado");
            entity.Property(e => e.UsuNombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Usu_Nombre");
            entity.Property(e => e.UsuPassword)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Usu_Password");
            entity.Property(e => e.UsuRol).HasColumnName("Usu_Rol");

            entity.HasOne(d => d.UsuRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.UsuRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuarios__Usu_Ro__5812160E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
