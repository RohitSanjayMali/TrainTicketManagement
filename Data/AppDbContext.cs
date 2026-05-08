using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrainTicketManagement.Models;

namespace TrainTicketManagement.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // FIXED: PascalCase names (C# convention)
        // ToTable() ensures DB table names stay the same — existing data safe!
        public DbSet<Train> Trains { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Keep existing DB table names unchanged so data is safe
            modelBuilder.Entity<Train>().ToTable("trains");
            modelBuilder.Entity<Station>().ToTable("stations");
            modelBuilder.Entity<Payment>().ToTable("payments");
            modelBuilder.Entity<Passenger>().ToTable("passengers");
            modelBuilder.Entity<Booking>().ToTable("bookings");

            modelBuilder.Entity<Train>()
                .HasOne(t => t.FromStation)
                .WithMany(s => s.FromTrains)
                .HasForeignKey(t => t.FromStationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Train>()
                .HasOne(t => t.ToStation)
                .WithMany(s => s.ToTrains)
                .HasForeignKey(t => t.ToStationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Booking>()
                .HasMany(b => b.Passengers)
                .WithOne(p => p.Booking)
                .HasForeignKey(p => p.BookingId);
        }
    }
}
