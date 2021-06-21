using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using AbbeyClasses;

namespace AbbeyClasses.Models
{
    public partial class BuckfastabbeyContext : DbContext
    {
        public BuckfastabbeyContext()
        {
        }

        public BuckfastabbeyContext(DbContextOptions<BuckfastabbeyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Monk> Monk { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL(ConfigDB.GetMySqlConnectionString());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Monk>(entity =>
            {
                entity.ToTable("monk");

                entity.Property(e => e.MonkId).HasColumnName("monk_id");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(40);

                entity.Property(e => e.Job)
                    .IsRequired()
                    .HasColumnName("job")
                    .HasMaxLength(40);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(40);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
