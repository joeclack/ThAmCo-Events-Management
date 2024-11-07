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
        optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Define composite primary key for MenuFoodItem
        modelBuilder.Entity<MenuFoodItem>()
            .HasKey(mfi => new { mfi.MenuId, mfi.FoodItemId });

        // Seed data for FoodItems (20 Food Items)
        modelBuilder.Entity<FoodItem>().HasData(
            new FoodItem { FoodItemId = 1, Description = "Chicken Sandwich", UnitPrice = 5.99f },
            new FoodItem { FoodItemId = 2, Description = "Veggie Wrap", UnitPrice = 4.49f },
            new FoodItem { FoodItemId = 3, Description = "Caesar Salad", UnitPrice = 3.99f },
            new FoodItem { FoodItemId = 4, Description = "Steak", UnitPrice = 12.99f },
            new FoodItem { FoodItemId = 5, Description = "Turkey Club", UnitPrice = 6.49f },
            new FoodItem { FoodItemId = 6, Description = "Ham and Cheese", UnitPrice = 5.19f },
            new FoodItem { FoodItemId = 7, Description = "Grilled Cheese", UnitPrice = 4.99f },
            new FoodItem { FoodItemId = 8, Description = "BLT Sandwich", UnitPrice = 5.79f },
            new FoodItem { FoodItemId = 9, Description = "Fish Tacos", UnitPrice = 7.99f },
            new FoodItem { FoodItemId = 10, Description = "Spaghetti Bolognese", UnitPrice = 8.99f },
            new FoodItem { FoodItemId = 11, Description = "Penne Arrabbiata", UnitPrice = 7.49f },
            new FoodItem { FoodItemId = 12, Description = "Mushroom Risotto", UnitPrice = 9.49f },
            new FoodItem { FoodItemId = 13, Description = "Chicken Alfredo", UnitPrice = 10.99f },
            new FoodItem { FoodItemId = 14, Description = "Beef Burritos", UnitPrice = 6.99f },
            new FoodItem { FoodItemId = 15, Description = "Vegetarian Tacos", UnitPrice = 5.49f },
            new FoodItem { FoodItemId = 16, Description = "BBQ Chicken Pizza", UnitPrice = 12.99f },
            new FoodItem { FoodItemId = 17, Description = "Pepperoni Pizza", UnitPrice = 11.49f },
            new FoodItem { FoodItemId = 18, Description = "Margherita Pizza", UnitPrice = 9.99f },
            new FoodItem { FoodItemId = 19, Description = "Cheeseburger", UnitPrice = 6.49f },
            new FoodItem { FoodItemId = 20, Description = "Veggie Burger", UnitPrice = 5.99f }
        );

        // Seed data for Menus (5 Menus)
        modelBuilder.Entity<Menu>().HasData(
            new Menu { MenuId = 1, MenuName = "Basic Lunch" },
            new Menu { MenuId = 2, MenuName = "Vegetarian Feast" },
            new Menu { MenuId = 3, MenuName = "Deluxe Dinner" },
            new Menu { MenuId = 4, MenuName = "Italian Specialties" },
            new Menu { MenuId = 5, MenuName = "Mexican Fiesta" }
        );

        // Seed data for MenuFoodItems (Mapping FoodItems to Menus)
        modelBuilder.Entity<MenuFoodItem>().HasData(
            new MenuFoodItem { MenuId = 1, FoodItemId = 1 },  // Chicken Sandwich in Basic Lunch
            new MenuFoodItem { MenuId = 1, FoodItemId = 3 },  // Caesar Salad in Basic Lunch
            new MenuFoodItem { MenuId = 1, FoodItemId = 5 },  // Turkey Club in Basic Lunch
            new MenuFoodItem { MenuId = 1, FoodItemId = 6 },  // Ham and Cheese in Basic Lunch
            new MenuFoodItem { MenuId = 2, FoodItemId = 2 },  // Veggie Wrap in Vegetarian Feast
            new MenuFoodItem { MenuId = 2, FoodItemId = 3 },  // Caesar Salad in Vegetarian Feast
            new MenuFoodItem { MenuId = 2, FoodItemId = 7 },  // Grilled Cheese in Vegetarian Feast
            new MenuFoodItem { MenuId = 2, FoodItemId = 15 }, // Vegetarian Tacos in Vegetarian Feast
            new MenuFoodItem { MenuId = 3, FoodItemId = 4 },  // Steak in Deluxe Dinner
            new MenuFoodItem { MenuId = 3, FoodItemId = 10 }, // Spaghetti Bolognese in Deluxe Dinner
            new MenuFoodItem { MenuId = 3, FoodItemId = 14 }, // Beef Burritos in Deluxe Dinner
            new MenuFoodItem { MenuId = 3, FoodItemId = 13 }, // Chicken Alfredo in Deluxe Dinner
            new MenuFoodItem { MenuId = 4, FoodItemId = 12 }, // Mushroom Risotto in Italian Specialties
            new MenuFoodItem { MenuId = 4, FoodItemId = 16 }, // BBQ Chicken Pizza in Italian Specialties
            new MenuFoodItem { MenuId = 4, FoodItemId = 17 }, // Pepperoni Pizza in Italian Specialties
            new MenuFoodItem { MenuId = 4, FoodItemId = 18 }, // Margherita Pizza in Italian Specialties
            new MenuFoodItem { MenuId = 5, FoodItemId = 19 }, // Cheeseburger in Mexican Fiesta
            new MenuFoodItem { MenuId = 5, FoodItemId = 20 }, // Veggie Burger in Mexican Fiesta
            new MenuFoodItem { MenuId = 5, FoodItemId = 9 },  // Fish Tacos in Mexican Fiesta
            new MenuFoodItem { MenuId = 5, FoodItemId = 15 }  // Vegetarian Tacos in Mexican Fiesta
        );

        // Seed data for FoodBookings
        modelBuilder.Entity<FoodBooking>().HasData(
            new FoodBooking { FoodBookingId = 1, ClientReferenceId = 101, NumberOfGuests = 50, MenuId = 1 },
            new FoodBooking { FoodBookingId = 2, ClientReferenceId = 102, NumberOfGuests = 30, MenuId = 2 },
            new FoodBooking { FoodBookingId = 3, ClientReferenceId = 103, NumberOfGuests = 75, MenuId = 3 },
            new FoodBooking { FoodBookingId = 4, ClientReferenceId = 104, NumberOfGuests = 100, MenuId = 4 },
            new FoodBooking { FoodBookingId = 5, ClientReferenceId = 105, NumberOfGuests = 60, MenuId = 5 }
        );

        // Additional constraints and relationships can be configured here

        modelBuilder.Entity<FoodBooking>()
            .HasOne<Menu>()
            .WithMany(menu => menu.FoodBookings)
            .HasForeignKey(foodBooking => foodBooking.MenuId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<MenuFoodItem>()
            .HasOne<Menu>()
            .WithMany(menu => menu.MenuFoodItems)
            .HasForeignKey(mfi => mfi.MenuId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<MenuFoodItem>()
            .HasOne<FoodItem>()
            .WithMany(foodItem => foodItem.MenuFoodItems)
            .HasForeignKey(mfi => mfi.FoodItemId)
            .OnDelete(DeleteBehavior.Cascade);
    }




}
