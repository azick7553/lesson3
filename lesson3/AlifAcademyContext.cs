using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace lesson3
{
    public partial class AlifAcademyContext : DbContext
    {
        public AlifAcademyContext()
        {
            Database.EnsureCreated();
        }

        public AlifAcademyContext(DbContextOptions<AlifAcademyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Models> Models { get; set; }
        public virtual DbSet<TestTable> TestTable { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=AlifAcademy;User ID=Sa;Password=Root123.");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Models>(entity =>
            {
                entity.Property(e => e.ModelName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Models)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Models_Company");
            });

            modelBuilder.Entity<TestTable>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Test)
                    .HasColumnName("test")
                    .HasMaxLength(50);

                entity.Property(e => e.TestNew)
                    .HasColumnName("testNew")
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
