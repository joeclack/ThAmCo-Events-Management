using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThAmCo.Catering.Models;

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

        modelBuilder.Entity<MenuFoodItem>()
            .HasKey(mfi => new { mfi.MenuId, mfi.FoodItemId });

        modelBuilder.Entity<MenuFoodItem>()
            .HasOne(mfi => mfi.Menu)
            .WithMany(m => m.MenuFoodItems)
            .HasForeignKey(mfi => mfi.MenuId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<MenuFoodItem>()
            .HasOne(mfi => mfi.FoodItem)
            .WithMany(fi => fi.MenuFoodItems)
            .HasForeignKey(mfi => mfi.FoodItemId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<FoodItem>().HasData(
            new FoodItem { FoodItemId = 1, Name = "Classic Chicken Sandwich", Description = "Grilled chicken breast with lettuce, tomato, and mayo on a toasted bun", UnitPrice = 5.99f },
            new FoodItem { FoodItemId = 2, Name = "Fresh Veggie Wrap", Description = "Whole-wheat wrap with hummus, fresh veggies, and a hint of vinaigrette", UnitPrice = 4.49f },
            new FoodItem { FoodItemId = 3, Name = "Caesar Salad", Description = "Crisp romaine lettuce with Caesar dressing, croutons, and Parmesan cheese", UnitPrice = 3.99f },
            new FoodItem { FoodItemId = 4, Name = "Grilled Steak", Description = "Juicy grilled steak seasoned to perfection", UnitPrice = 12.99f },
            new FoodItem { FoodItemId = 5, Name = "Turkey Club Sandwich", Description = "Turkey, bacon, lettuce, and tomato stacked on toasted bread", UnitPrice = 6.49f },
            new FoodItem { FoodItemId = 6, Name = "Ham and Cheese Sandwich", Description = "Classic ham and cheese sandwich with fresh lettuce and tomato", UnitPrice = 5.19f },
            new FoodItem { FoodItemId = 7, Name = "Grilled Cheese Sandwich", Description = "Melted cheddar cheese between crispy, toasted bread", UnitPrice = 4.99f },
            new FoodItem { FoodItemId = 8, Name = "BLT Sandwich", Description = "Bacon, lettuce, and tomato on toasted whole-grain bread", UnitPrice = 5.79f },
            new FoodItem { FoodItemId = 9, Name = "Fish Tacos", Description = "Two tacos with grilled fish, slaw, and a spicy mayo drizzle", UnitPrice = 7.99f },
            new FoodItem { FoodItemId = 10, Name = "Spaghetti Bolognese", Description = "Classic Italian pasta with rich meat sauce", UnitPrice = 8.99f },
            new FoodItem { FoodItemId = 11, Name = "Penne Arrabbiata", Description = "Penne pasta in a spicy tomato sauce with garlic and chili", UnitPrice = 7.49f },
            new FoodItem { FoodItemId = 12, Name = "Mushroom Risotto", Description = "Creamy risotto with wild mushrooms and Parmesan", UnitPrice = 9.49f },
            new FoodItem { FoodItemId = 13, Name = "Chicken Alfredo", Description = "Fettuccine pasta with creamy Alfredo sauce and grilled chicken", UnitPrice = 10.99f },
            new FoodItem { FoodItemId = 14, Name = "Beef Burritos", Description = "Two burritos filled with beef, rice, beans, and cheese", UnitPrice = 6.99f },
            new FoodItem { FoodItemId = 15, Name = "Vegetarian Tacos", Description = "Two tacos filled with seasoned veggies, beans, and avocado", UnitPrice = 5.49f },
            new FoodItem { FoodItemId = 16, Name = "BBQ Chicken Pizza", Description = "Pizza topped with BBQ chicken, red onions, and mozzarella", UnitPrice = 12.99f },
            new FoodItem { FoodItemId = 17, Name = "Pepperoni Pizza", Description = "Classic pepperoni pizza with marinara sauce and mozzarella", UnitPrice = 11.49f },
            new FoodItem { FoodItemId = 18, Name = "Margherita Pizza", Description = "Pizza with fresh tomatoes, mozzarella, and basil", UnitPrice = 9.99f },
            new FoodItem { FoodItemId = 19, Name = "Cheeseburger", Description = "Beef burger with melted cheddar, lettuce, and tomato on a brioche bun", UnitPrice = 6.49f },
            new FoodItem { FoodItemId = 20, Name = "Veggie Burger", Description = "Grilled veggie patty with lettuce, tomato, and avocado on a whole-wheat bun", UnitPrice = 5.99f }
        );


        modelBuilder.Entity<Menu>().HasData(
            new Menu { MenuId = 1, MenuName = "Basic Lunch" },
            new Menu { MenuId = 2, MenuName = "Vegetarian Feast" },
            new Menu { MenuId = 3, MenuName = "Deluxe Dinner" }
        );

        modelBuilder.Entity<MenuFoodItem>().HasData(
            new MenuFoodItem { MenuId = 1, FoodItemId = 1 }, 
            new MenuFoodItem { MenuId = 1, FoodItemId = 3 }, 
            new MenuFoodItem { MenuId = 1, FoodItemId = 5 }, 
            new MenuFoodItem { MenuId = 1, FoodItemId = 6 }, 
            new MenuFoodItem { MenuId = 2, FoodItemId = 2 }, 
            new MenuFoodItem { MenuId = 2, FoodItemId = 3 }, 
            new MenuFoodItem { MenuId = 2, FoodItemId = 7 }, 
            new MenuFoodItem { MenuId = 2, FoodItemId = 15 },
            new MenuFoodItem { MenuId = 3, FoodItemId = 4 }, 
            new MenuFoodItem { MenuId = 3, FoodItemId = 10 },
            new MenuFoodItem { MenuId = 3, FoodItemId = 14 },
            new MenuFoodItem { MenuId = 3, FoodItemId = 13 }
        );

        modelBuilder.Entity<FoodBooking>().HasData(
            new FoodBooking { FoodBookingId = 1, ClientReferenceId = 1, NumberOfGuests = 1, MenuId = 1, FoodBookingDate = DateTime.Now },
            new FoodBooking { FoodBookingId = 2, ClientReferenceId = 2, NumberOfGuests = 5, MenuId = 2, FoodBookingDate = DateTime.Now.AddDays(10) },
            new FoodBooking { FoodBookingId = 3, ClientReferenceId = 3, NumberOfGuests = 2, MenuId = 3, FoodBookingDate = DateTime.Now.AddMonths(1) }
        );
    }
}
