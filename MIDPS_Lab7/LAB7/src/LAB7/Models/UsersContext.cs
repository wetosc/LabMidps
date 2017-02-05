using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LAB7.Models
{
    public partial class UsersContext : DbContext
    {
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-FNFDKC7\SQLEXPRESS;Initial Catalog=Users;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CanEdit).HasDefaultValueSql("0");

                entity.Property(e => e.CanViewExtra).HasDefaultValueSql("0");

                entity.Property(e => e.IsAdmin).HasDefaultValueSql("0");

                entity.Property(e => e.Password).HasColumnType("varchar(256)");

                entity.Property(e => e.UserName).HasColumnType("varchar(256)");
            });
        }
    }
}