using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Catering.Data;

public class CateringDbContext : DbContext
{
    public DbSet<Menu> Menus => Set<Menu>();
    public DbSet<MenuFoodItem> MenuFoodItems => Set<MenuFoodItem>();
    public DbSet<FoodItem> FoodItems => Set<FoodItem>();
    public DbSet<FoodBooking> FoodBookings => Set<FoodBooking>();

    private readonly IHostEnvironment _hostEnv;
    public string DbPath { get; }

	public CateringDbContext(DbContextOptions<CateringDbContext> options, IHostEnvironment env) : base(options)
    {
        _hostEnv = env;

        var folder = Environment.SpecialFolder.MyDocuments;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "ThAmCo.Catering.db");
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

        modelBuilder.Entity<FoodItem>().HasData(
            new FoodItem { FoodItemId = 1, Description = "Grilled Chicken", UnitPrice = 15.99f },
            new FoodItem { FoodItemId = 2, Description = "Caesar Salad", UnitPrice = 7.99f },
            new FoodItem { FoodItemId = 3, Description = "Pasta Primavera", UnitPrice = 12.99f }
        );

        // Define seed data for Menus
        modelBuilder.Entity<Menu>().HasData(
            new Menu { MenuId = 1, MenuName = "Lunch Specials" },
            new Menu { MenuId = 2, MenuName = "Dinner Delights" }
        );

        // Define seed data for MenuFoodItems (associating FoodItems with Menus)
        modelBuilder.Entity<MenuFoodItem>().HasData(
            new MenuFoodItem { MenuId = 1, FoodItemId = 1 },
            new MenuFoodItem { MenuId = 1, FoodItemId = 2 },
            new MenuFoodItem { MenuId = 2, FoodItemId = 2 },
            new MenuFoodItem { MenuId = 2, FoodItemId = 3 }
        );

        // Define seed data for FoodBookings
        modelBuilder.Entity<FoodBooking>().HasData(
            new FoodBooking { FoodBookingId = 1, ClientReferenceId = 1001, NumberOfGuests = 4, MenuId = 1 },
            new FoodBooking { FoodBookingId = 2, ClientReferenceId = 1002, NumberOfGuests = 2, MenuId = 2 }
        );

        // FoodBooking to Menu (Many-to-One relationship)
        modelBuilder.Entity<FoodBooking>()
            .HasOne<Menu>()
            .WithMany(menu => menu.FoodBookings)
            .HasForeignKey(foodBooking => foodBooking.MenuId)
            .OnDelete(DeleteBehavior.Cascade);

        // Menu to MenuFoodItem (One-to-Many relationship)
        modelBuilder.Entity<MenuFoodItem>()
            .HasOne<Menu>()
            .WithMany(menu => menu.MenuFoodItems)
            .HasForeignKey(mfi => mfi.MenuId)
            .OnDelete(DeleteBehavior.Cascade);

        // MenuFoodItem to FoodItem (Many-to-One relationship)
        modelBuilder.Entity<MenuFoodItem>()
            .HasOne<FoodItem>()
            .WithMany(foodItem => foodItem.MenuFoodItems)
            .HasForeignKey(mfi => mfi.FoodItemId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
