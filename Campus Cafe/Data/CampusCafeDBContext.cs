using System;
using System.Collections.Generic;
using Campus_Cafe.Models;
using Microsoft.EntityFrameworkCore;

namespace Campus_Cafe.Data;

public partial class CampusCafeDBContext : DbContext
{
    public CampusCafeDBContext()
    {
    }

    public CampusCafeDBContext(DbContextOptions<CampusCafeDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CafeOrder> CafeOrders { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CampusCafeDB;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CafeOrder>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__CafeOrde__C3905BCF0F78C8C9");

            entity.HasOne(d => d.Product).WithMany(p => p.CafeOrders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CafeOrder__Produ__59063A47");

            entity.HasOne(d => d.Student).WithMany(p => p.CafeOrders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CafeOrder__Stude__5812160E");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A0B744BEDC9");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6CD7F4DFBC5");

            entity.Property(e => e.IsAvailable).HasDefaultValue(true);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Product__Categor__4E88ABD4");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Student__32C52B991FEB3E87");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
