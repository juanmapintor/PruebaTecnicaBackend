using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Configuration;
using PruebaTecnica.Entities;
using PruebaTecnica.Helpers;

namespace PruebaTecnica.Repository;

public partial class PruebaTecnicaContext : DbContext
{
    private AppOptions LocalAppOptions { get; set; } = new AppOptions();
    public PruebaTecnicaContext()
    {
        ConfigurationHelper.GetConfiguration().GetSection(AppOptions.Section).Bind(LocalAppOptions);
    }

    public PruebaTecnicaContext(DbContextOptions<PruebaTecnicaContext> options)
        : base(options)
    {
        ConfigurationHelper.GetConfiguration().GetSection(AppOptions.Section).Bind(LocalAppOptions);
    }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Asignacion> Asignaciones { get; set; }
    public virtual DbSet<Curso> Cursos { get; set; }
    public virtual DbSet<Distrito> Distritos { get; set; }
    public virtual DbSet<Docente> Docentes { get; set; }
    public virtual DbSet<Estudiante> Estudiantes { get; set; }
    public virtual DbSet<Matricula> Matriculas { get; set; }
    public virtual DbSet<Profesion> Profesiones { get; set; }
    public virtual DbSet<Provincia> Provincias { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(LocalAppOptions.ConnectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asignacion>(entity =>
        {
            entity.HasKey(e => e.Idasignacion);

            entity.ToTable("TB_ASIGNACION");

            entity.Property(e => e.Idasignacion).HasColumnName("idasignacion");
            entity.Property(e => e.FechaAsi)
                .HasColumnType("date")
                .HasColumnName("fecha_asi");
            entity.Property(e => e.Idcurso).HasColumnName("idcurso");
            entity.Property(e => e.Iddocente).HasColumnName("iddocente");

            entity.HasOne(d => d.Curso).WithMany(p => p.Asignaciones)
                .HasForeignKey(d => d.Idcurso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ASIGNACION__CURSO");

            entity.HasOne(d => d.Docente).WithMany(p => p.Asignaciones)
                .HasForeignKey(d => d.Iddocente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ASIGNACION__DOCENTE");
        });

        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.Idcurso);

            entity.ToTable("TB_CURSO");

            entity.Property(e => e.Idcurso).HasColumnName("idcurso");
            entity.Property(e => e.CostCur).HasColumnName("cost_cur");
            entity.Property(e => e.DuraCur).HasColumnName("dura_cur");
            entity.Property(e => e.NombCur)
                .HasMaxLength(100)
                .HasColumnName("nomb_cur");
        });

        modelBuilder.Entity<Distrito>(entity =>
        {
            entity.HasKey(e => e.Iddistrito);

            entity.ToTable("TB_DISTRITO");

            entity.Property(e => e.Iddistrito).HasColumnName("iddistrito");
            entity.Property(e => e.Idprovincia).HasColumnName("idprovincia");
            entity.Property(e => e.NombDis)
                .HasMaxLength(100)
                .HasColumnName("nomb_dis");

            entity.HasOne(d => d.Provincia).WithMany(p => p.Distritos)
                .HasForeignKey(d => d.Idprovincia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DISTRITO__PROVINCIA");
        });

        modelBuilder.Entity<Docente>(entity =>
        {
            entity.HasKey(e => e.Iddocente);

            entity.ToTable("TB_DOCENTE");

            entity.Property(e => e.Iddocente).HasColumnName("iddocente");
            entity.Property(e => e.ApelDoc)
                .HasMaxLength(100)
                .HasColumnName("apel_doc");
            entity.Property(e => e.DireDoc)
                .HasMaxLength(100)
                .HasColumnName("dire_doc");
            entity.Property(e => e.GradDoc)
                .HasMaxLength(100)
                .HasColumnName("grad_doc");
            entity.Property(e => e.Idprofesion).HasColumnName("idprofesion");
            entity.Property(e => e.NcelDoc)
                .HasMaxLength(100)
                .HasColumnName("ncel_doc");
            entity.Property(e => e.NombDoc)
                .HasMaxLength(100)
                .HasColumnName("nomb_doc");
            entity.Property(e => e.NtelDoc)
                .HasMaxLength(100)
                .HasColumnName("ntel_doc");

            entity.HasOne(d => d.Profesion).WithMany(p => p.Docentes)
                .HasForeignKey(d => d.Idprofesion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DOCENTE__PROFESION");
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.Idestudiante);

            entity.ToTable("TB_ESTUDIANTE");

            entity.Property(e => e.Idestudiante).HasColumnName("idestudiante");
            entity.Property(e => e.ApelEst)
                .HasMaxLength(100)
                .HasColumnName("apel_est");
            entity.Property(e => e.DireEst)
                .HasMaxLength(100)
                .HasColumnName("dire_est");
            entity.Property(e => e.FnacEst)
                .HasColumnType("date")
                .HasColumnName("fnac_est");
            entity.Property(e => e.GinsEst)
                .HasMaxLength(100)
                .HasColumnName("gins_est");
            entity.Property(e => e.Iddistrito).HasColumnName("iddistrito");
            entity.Property(e => e.NombEst)
                .HasMaxLength(100)
                .HasColumnName("nomb_est");
            entity.Property(e => e.SexoEst)
                .HasMaxLength(100)
                .HasColumnName("sexo_est");
            entity.Property(e => e.TcolEst)
                .HasMaxLength(100)
                .HasColumnName("tcol_est");

            entity.HasOne(d => d.Distrito).WithMany(p => p.Estudiantes)
                .HasForeignKey(d => d.Iddistrito)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ESTUDIANTE__DISTRITO");
        });

        modelBuilder.Entity<Matricula>(entity =>
        {
            entity.HasKey(e => e.Idmatricula);

            entity.ToTable("TB_MATRICULA");

            entity.Property(e => e.Idmatricula).HasColumnName("idmatricula");
            entity.Property(e => e.FechaMat)
                .HasColumnType("date")
                .HasColumnName("fecha_mat");
            entity.Property(e => e.Idcurso).HasColumnName("idcurso");
            entity.Property(e => e.Idestudiante).HasColumnName("idestudiante");

            entity.HasOne(d => d.Curso).WithMany(p => p.Matriculas)
                .HasForeignKey(d => d.Idcurso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MATRICULA__CURSO");

            entity.HasOne(d => d.Estudiante).WithMany(p => p.Matriculas)
                .HasForeignKey(d => d.Idestudiante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MATRICULA__ESTUDIANTE");
        });

        modelBuilder.Entity<Profesion>(entity =>
        {
            entity.HasKey(e => e.Idprofesion);

            entity.ToTable("TB_PROFESION");

            entity.Property(e => e.Idprofesion).HasColumnName("idprofesion");
            entity.Property(e => e.NombPro)
                .HasMaxLength(100)
                .HasColumnName("nomb_pro");
        });

        modelBuilder.Entity<Provincia>(entity =>
        {
            entity.HasKey(e => e.Idprovincia);

            entity.ToTable("TB_PROVINCIA");

            entity.Property(e => e.Idprovincia).HasColumnName("idprovincia");
            entity.Property(e => e.NombPro)
                .HasMaxLength(100)
                .HasColumnName("nomb_pro");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Username);

            entity.ToTable("TB_USER");

            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
            entity.Property(e => e.Password)
                .HasMaxLength(64)
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
