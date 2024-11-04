using Microsoft.EntityFrameworkCore;
using System.Net;

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
            // Set the database filename (inc. path) for SQLite to use
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define relationships
            modelBuilder.Entity<GuestBooking>()
                .HasOne<Guest>()
                .WithMany(g => g.GuestBookings)
                .HasForeignKey(gb => gb.GuestId);

            modelBuilder.Entity<GuestBooking>()
                .HasOne<Event>()
                .WithMany(e => e.GuestBookings)
                .HasForeignKey(gb => gb.EventId);

            modelBuilder.Entity<Staffing>()
                .HasOne<Event>()
                .WithMany(e => e.Staffing)
                .HasForeignKey(e => e.EventId);

            modelBuilder.Entity<Staffing>()
                .HasOne<Staff>()
                .WithMany(e => e.Staffing)
                .HasForeignKey(e => e.StaffId);

            // Seed data for Event
            modelBuilder.Entity<Event>().HasData(
                new Event { EventId = 1, EventTypeId = 101, FoodBookingId = 1001, ReservationId = 2001, Date = new DateTime(2024, 5, 15) },
                new Event { EventId = 2, EventTypeId = 102, FoodBookingId = 1002, ReservationId = 2002, Date = new DateTime(2024, 6, 20) }
            );

            // Seed data for Guest
            modelBuilder.Entity<Guest>().HasData(
                new Guest { GuestId = 1, FirstName = "John", LastName = "Doe", EventId = "1" },
                new Guest { GuestId = 2, FirstName = "Jane", LastName = "Smith", EventId = "2" }
            );

            // Seed data for GuestBooking
            modelBuilder.Entity<GuestBooking>().HasData(
                new GuestBooking { GuestBookingId = 1, GuestId = 1, EventId = 1 },
                new GuestBooking { GuestBookingId = 2, GuestId = 2, EventId = 2 }
            );

            // Seed data for Staff
            modelBuilder.Entity<Staff>().HasData(
                new Staff { StaffId = "S1", FirstName = "Alice", LastName = "Johnson", Role = "Chef" },
                new Staff { StaffId = "S2", FirstName = "Bob", LastName = "Williams", Role = "Server" }
            );

            // Seed data for Staffing
            modelBuilder.Entity<Staffing>().HasData(
                new Staffing { StaffId = 1, EventId = 1 },
                new Staffing { StaffId = 2, EventId = 2 }
            );
        }

    }
}
