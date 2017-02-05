using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LAB7.Models
{
    public partial class MiddleEarthContext : DbContext
    {
        public virtual DbSet<Elf> Elf { get; set; }
        public virtual DbSet<Hobbit> Hobbit { get; set; }
        public virtual DbSet<Orc> Orc { get; set; }
        public virtual DbSet<Ring> Ring { get; set; }
        public virtual DbSet<Wizard> Wizard { get; set; }

        // Unable to generate entity type for table 'dbo.Master2Ring'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-FNFDKC7\SQLEXPRESS;Initial Catalog=MiddleEarth;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Elf>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Category).HasColumnType("varchar(256)");

                entity.Property(e => e.HobbitFriend).HasColumnName("Hobbit_Friend");

                entity.Property(e => e.Image).HasColumnType("image");

                entity.Property(e => e.Name).HasColumnType("varchar(256)");

                entity.HasOne(d => d.HobbitFriendNavigation)
                    .WithMany(p => p.Elf)
                    .HasForeignKey(d => d.HobbitFriend)
                    .HasConstraintName("Hobbit_Friend_FK2");
            });

            modelBuilder.Entity<Hobbit>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasColumnType("varchar(256)");

                entity.Property(e => e.Region).HasColumnType("varchar(256)");
            });

            modelBuilder.Entity<Orc>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MasterId).HasColumnName("Master_ID");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.Orc)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("Master_ID_FK2");
            });

            modelBuilder.Entity<Ring>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Material).HasColumnType("varchar(256)");

                entity.Property(e => e.Name).HasColumnType("varchar(256)");
            });

            modelBuilder.Entity<Wizard>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Color).HasColumnType("varchar(256)");

                entity.Property(e => e.HobbitFriend).HasColumnName("Hobbit_Friend");

                entity.Property(e => e.Name).HasColumnType("varchar(256)");

                entity.HasOne(d => d.HobbitFriendNavigation)
                    .WithMany(p => p.Wizard)
                    .HasForeignKey(d => d.HobbitFriend)
                    .HasConstraintName("Hobbit_Friend_FK");
            });
        }
    }
}