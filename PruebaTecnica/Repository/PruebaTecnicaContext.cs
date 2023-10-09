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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(LocalAppOptions.ConnectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
