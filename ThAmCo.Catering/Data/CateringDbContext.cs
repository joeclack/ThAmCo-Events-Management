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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<FoodItem>()
            .HasMany<MenuFoodItem>(fi => fi.MenuFoodItems)
            .WithMany();

        builder.Entity<FoodItem>()
            .HasKey(fi => fi.FoodItemId);

        builder.Entity<FoodBooking>()
            .HasKey(fb => fb.FoodBookingId);

        builder.Entity<MenuFoodItem>()
            .HasKey(mfi => mfi.MenuFoodItemId);

        builder.Entity<Menu>()
            .HasKey(m => m.MenuId);

        builder.Entity<MenuFoodItem>()
            .HasOne<FoodItem>()
            .WithOne();

        builder.Entity<MenuFoodItem>()
            .HasOne<Menu>()
            .WithOne()
            .HasForeignKey<Menu>(m => m.MenuId);

        builder.Entity<Menu>()
            .HasMany<MenuFoodItem>(m => m.MenuFoodItems)
            .WithMany();

        builder.Entity<Menu>()
            .HasMany<FoodBooking>(m => m.FoodBookings)
            .WithMany();

        builder.Entity<FoodBooking>()
            .HasOne<Menu>()
            .WithOne()
            .HasForeignKey<Menu>(m => m.MenuId);

        //builder.Entity<MenuFoodItem>().HasData(
        //    new MenuFoodItem { MenuFoodItemId = 1, MenuId = 1, FoodItemId = 1 },
        //    new MenuFoodItem { MenuFoodItemId = 2, MenuId = 2, FoodItemId = 2 },
        //    new MenuFoodItem { MenuFoodItemId = 3, MenuId = 3, FoodItemId = 3 },
        //    new MenuFoodItem { MenuFoodItemId = 4, MenuId = 4, FoodItemId = 4 },
        //    new MenuFoodItem { MenuFoodItemId = 5, MenuId = 5, FoodItemId = 5 },
        //    new MenuFoodItem { MenuFoodItemId = 6, MenuId = 6, FoodItemId = 6 },
        //    new MenuFoodItem { MenuFoodItemId = 7, MenuId = 7, FoodItemId = 7 },
        //    new MenuFoodItem { MenuFoodItemId = 8, MenuId = 8, FoodItemId = 8 },
        //    new MenuFoodItem { MenuFoodItemId = 9, MenuId = 9, FoodItemId = 9 },
        //    new MenuFoodItem { MenuFoodItemId = 10, MenuId = 10, FoodItemId = 10 }
        //    );

        //builder.Entity<Menu>().HasData(
        //    new Menu { MenuId = 1, MenuName = "Menu 1" },
        //    new Menu { MenuId = 2, MenuName = "Menu 2" },
        //    new Menu { MenuId = 3, MenuName = "Menu 3" },
        //    new Menu { MenuId = 4, MenuName = "Menu 4" },
        //    new Menu { MenuId = 5, MenuName = "Menu 5" },
        //    new Menu { MenuId = 6, MenuName = "Menu 6" },
        //    new Menu { MenuId = 7, MenuName = "Menu 7" },
        //    new Menu { MenuId = 8, MenuName = "Menu 8" },
        //    new Menu { MenuId = 9, MenuName = "Menu 9" },
        //    new Menu { MenuId = 10, MenuName = "Menu 10" }
        //    );

        //builder.Entity<FoodItem>().HasData(
        //        new FoodItem { FoodItemId = 1, Description = "Food Item 1", UnitPrice = 20 },
        //        new FoodItem { FoodItemId = 2, Description = "Food Item 2", UnitPrice = 30 },
        //        new FoodItem { FoodItemId = 3, Description = "Food Item 3", UnitPrice = 40 },
        //        new FoodItem { FoodItemId = 4, Description = "Food Item 4", UnitPrice = 10 },
        //        new FoodItem { FoodItemId = 5, Description = "Food Item 5", UnitPrice = 15 },
        //        new FoodItem { FoodItemId = 6, Description = "Food Item 6", UnitPrice = 25 },
        //        new FoodItem { FoodItemId = 7, Description = "Food Item 7", UnitPrice = 19 },
        //        new FoodItem { FoodItemId = 8, Description = "Food Item 8", UnitPrice = 22 },
        //        new FoodItem { FoodItemId = 9, Description = "Food Item 9", UnitPrice = 35 },
        //        new FoodItem { FoodItemId = 10, Description = "Food Item 10", UnitPrice = 45 }
        //    );

        //builder.Entity<FoodBooking>().HasData(
        //    new FoodBooking { FoodBookingId = 1, ClientReferenceId = 1, NumberOfGuests = 11, MenuId = 1 },
        //    new FoodBooking { FoodBookingId = 2, ClientReferenceId = 2, NumberOfGuests = 12, MenuId = 2 },
        //    new FoodBooking { FoodBookingId = 3, ClientReferenceId = 3, NumberOfGuests = 13, MenuId = 3 },
        //    new FoodBooking { FoodBookingId = 4, ClientReferenceId = 4, NumberOfGuests = 20, MenuId = 4 },
        //    new FoodBooking { FoodBookingId = 5, ClientReferenceId = 5, NumberOfGuests = 21, MenuId = 5 },
        //    new FoodBooking { FoodBookingId = 6, ClientReferenceId = 6, NumberOfGuests = 6, MenuId = 6 },
        //    new FoodBooking { FoodBookingId = 7, ClientReferenceId = 7, NumberOfGuests = 7, MenuId = 7 },
        //    new FoodBooking { FoodBookingId = 8, ClientReferenceId = 8, NumberOfGuests = 8, MenuId = 8 },
        //    new FoodBooking { FoodBookingId = 9, ClientReferenceId = 9, NumberOfGuests = 9, MenuId = 9 },
        //    new FoodBooking { FoodBookingId = 10, ClientReferenceId = 10, NumberOfGuests = 10, MenuId = 10 }
        //    );
    }
}
