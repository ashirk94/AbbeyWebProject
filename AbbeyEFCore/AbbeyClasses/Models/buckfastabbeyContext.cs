using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AbbeyClasses.Models
{
    public partial class buckfastabbeyContext : DbContext
    {
        public buckfastabbeyContext()
        {
        }

        public buckfastabbeyContext(DbContextOptions<buckfastabbeyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Monk> Monk { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=groyper*6673;database=buckfastabbey");
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
