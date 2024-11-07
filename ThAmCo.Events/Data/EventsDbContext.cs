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
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Staffing>()
                .HasKey(s => new { s.StaffId, s.EventId });

            modelBuilder.Entity<GuestBooking>()
                .HasOne<Guest>()
                .WithMany(g => g.GuestBookings)
                .HasForeignKey(gb => gb.GuestId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GuestBooking>()
                .HasOne<Event>()
                .WithMany(e => e.GuestBookings)
                .HasForeignKey(gb => gb.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Staffing>()
                .HasOne<Event>()
                .WithMany(e => e.Staffings)
                .HasForeignKey(s => s.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Staffing>()
                .HasOne<Staff>()
                .WithMany(s => s.Staffings)
                .HasForeignKey(s => s.StaffId)
                .OnDelete(DeleteBehavior.Cascade);

            // List of possible roles for staff members
            var roles = new[] {
                "Waiter", "Manager", "Chef", "Bartender", "Security",
                "Coordinator", "Event Planner", "Cleaner", "Technician", "DJ",
                "Photographer", "Logistics Manager", "Driver", "Greeter",
                "Marketing Specialist", "Ticketing Agent", "Host", "VIP Assistant",
                "Customer Support", "Videographer", "Stage Hand"
            };

            // Seed Staff data
            modelBuilder.Entity<Staff>().HasData(
                Enumerable.Range(1, 50).Select(i => new Staff
                {
                    StaffId = i,
                    FirstName = $"FirstName{i}",
                    LastName = $"LastName{i}",
                    Role = roles[i % roles.Length] // Assign roles cyclically from the roles array
                })
            );
        }

    }
}
