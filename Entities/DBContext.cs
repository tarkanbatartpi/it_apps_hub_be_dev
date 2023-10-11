using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace apps_hub.Entities;

public partial class DBContext : DbContext
{
    public DBContext()
    {
    }

    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TempTable> TempTables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=root;database=appshub_dev");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TempTable>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.ToTable("temp_table");

            entity.Property(e => e.id).HasColumnName("id");
            entity.Property(e => e.img)
                .HasMaxLength(255)
                .HasColumnName("img");
            entity.Property(e => e.link)
                .HasMaxLength(255)
                .HasColumnName("link");
            entity.Property(e => e.title)
                .HasMaxLength(45)
                .HasColumnName("title");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
