using Microsoft.EntityFrameworkCore;
using System.Net;
using ThAmCo.Events.Models;

namespace ThAmCo.Events.Data
{
    public class EventsDbContext : DbContext
    {
        public DbSet<Event> Events => Set<Event>();
        public DbSet<Guest> Guests => Set<Guest>();
        public DbSet<GuestBooking> GuestBookings => Set<GuestBooking>();
        public DbSet<Staff> Staff => Set<Staff>();
        public DbSet<Staffing> Staffing => Set<Staffing>();

        private readonly IHostEnvironment _hostEnv;
        public string DbPath { get; }

        public EventsDbContext(DbContextOptions<EventsDbContext> options, IHostEnvironment env) : base(options)
        {
            _hostEnv = env;

            var folder = Environment.SpecialFolder.MyDocuments;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "ThAmCo.Events.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define key to avoid confusion with other Id's
            modelBuilder.Entity<Event>()
                .HasKey(e => e.EventId);

            // Guests bookings
            modelBuilder.Entity<GuestBooking>()
                .HasKey(gb => new { gb.GuestId, gb.EventId });

            modelBuilder.Entity<GuestBooking>()
                .HasOne(gb => gb.Guest)
                .WithMany(g => g.GuestBookings)
                .HasForeignKey(gb => gb.GuestId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GuestBooking>()
                .HasOne(gb => gb.Event)
                .WithMany(g => g.GuestBookings)
                .HasForeignKey(gb => gb.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            // Staffings
            modelBuilder.Entity<Staffing>()
                .HasKey(s => new { s.EventId, s.StaffId });

            modelBuilder.Entity<Staffing>()
                .HasOne(s => s.Staff)
                .WithMany(staff => staff.Staffings)
                .HasForeignKey(s => s.StaffId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Staffing>()
                .HasOne(s => s.Event)
                .WithMany(e => e.Staffings)
                .HasForeignKey(s => s.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed data

            modelBuilder.Entity<Guest>().HasData(
                new Guest { GuestId = 1, FirstName = "Alice", LastName = "Green", Email = "alice.green@example.com" },
                new Guest { GuestId = 2, FirstName = "Bob", LastName = "Smith", Email = "bob.smith@example.com" },
                new Guest { GuestId = 3, FirstName = "Carol", LastName = "Johnson", Email = "carol.johnson@example.com" },
                new Guest { GuestId = 4, FirstName = "David", LastName = "Brown", Email = "david.brown@example.com" },
                new Guest { GuestId = 5, FirstName = "Ella", LastName = "Davis", Email = "ella.davis@example.com" },
                new Guest { GuestId = 6, FirstName = "Frank", LastName = "Wilson", Email = "frank.wilson@example.com" },
                new Guest { GuestId = 7, FirstName = "Grace", LastName = "Martinez", Email = "grace.martinez@example.com" },
                new Guest { GuestId = 8, FirstName = "Henry", LastName = "Anderson", Email = "henry.anderson@example.com" },
                new Guest { GuestId = 9, FirstName = "Ivy", LastName = "Thomas", Email = "ivy.thomas@example.com" },
                new Guest { GuestId = 10, FirstName = "Jack", LastName = "Moore", Email = "jack.moore@example.com" }
            );

            modelBuilder.Entity<Staff>().HasData(
                new Staff { StaffId = 1, FirstName = "John", LastName = "Doe", Role = "Chef" },
                new Staff { StaffId = 2, FirstName = "Jane", LastName = "Smith", Role = "Server" },
                new Staff { StaffId = 3, FirstName = "Emily", LastName = "Johnson", Role = "Manager" },
                new Staff { StaffId = 4, FirstName = "Michael", LastName = "Williams", Role = "Chef" },
                new Staff { StaffId = 5, FirstName = "Sarah", LastName = "Brown", Role = "Server" },
                new Staff { StaffId = 6, FirstName = "David", LastName = "Jones", Role = "Bartender" },
                new Staff { StaffId = 7, FirstName = "Laura", LastName = "Garcia", Role = "Host" },
                new Staff { StaffId = 8, FirstName = "Daniel", LastName = "Martinez", Role = "Sous Chef" },
                new Staff { StaffId = 9, FirstName = "Sophia", LastName = "Anderson", Role = "Server" },
                new Staff { StaffId = 10, FirstName = "James", LastName = "Taylor", Role = "Manager" }
            );
        }

    }
}
