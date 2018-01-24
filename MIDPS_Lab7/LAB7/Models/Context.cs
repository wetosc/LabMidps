using LAB7.Models;
using Microsoft.EntityFrameworkCore;

namespace LAB7.Data
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }

        public DbSet<Ring> Ring { get; set; }
        public DbSet<Wizard> Wizard { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Ring>().ToTable("Ring");
            // modelBuilder.Entity<Wizard>().ToTable("Wizard");
            // modelBuilder.Entity<WizardRing>().ToTable("WizardRing");
            
            modelBuilder.Entity<WizardRing>()
                .HasKey(t => new { t.WizardID, t.RingID });
            
            modelBuilder.Entity<WizardRing>()
                .HasOne(pt => pt.Wizard)
                .WithMany(p => p.WizardRings)
                .HasForeignKey(pt => pt.WizardID);

            modelBuilder.Entity<WizardRing>()
                .HasOne(pt => pt.Ring)
                .WithMany(t => t.WizardRings)
                .HasForeignKey(pt => pt.RingID);
        }
    }
}