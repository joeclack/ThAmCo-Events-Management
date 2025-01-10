using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Configuration;
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
	private readonly IConfiguration _configuration;


	public CateringDbContext(DbContextOptions<CateringDbContext> options, IHostEnvironment env, IConfiguration configuration) : base(options)
	{
        _hostEnv = env;
		_configuration = configuration;

	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		base.OnConfiguring(optionsBuilder);
		var connectionString = _configuration.GetConnectionString("DefaultConnection");
		optionsBuilder.UseSqlServer(connectionString);
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

        modelBuilder.Entity<FoodBooking>()
            .HasOne(fb => fb.Menu)
            .WithMany(m => m.FoodBookings)
            .HasForeignKey(fb => fb.MenuId)
            .OnDelete(DeleteBehavior.Cascade);

		modelBuilder.Entity<FoodItem>().HasData(
			new FoodItem { FoodItemId = 1, Name = "Classic Chicken Sandwich", Description = "Grilled chicken with lettuce, tomato, and mayo", UnitPrice = 5.99f },
			new FoodItem { FoodItemId = 2, Name = "Fresh Veggie Wrap", Description = "Whole-wheat wrap with hummus and fresh veggies", UnitPrice = 4.49f },
			new FoodItem { FoodItemId = 3, Name = "Caesar Salad", Description = "Romaine lettuce with Caesar dressing and croutons", UnitPrice = 3.99f },
			new FoodItem { FoodItemId = 4, Name = "Grilled Steak", Description = "Juicy grilled steak seasoned perfectly", UnitPrice = 12.99f },
			new FoodItem { FoodItemId = 5, Name = "Turkey Club Sandwich", Description = "Turkey, bacon, lettuce, tomato on toasted bread", UnitPrice = 6.49f },
			new FoodItem { FoodItemId = 6, Name = "Ham and Cheese Sandwich", Description = "Ham, cheese, lettuce, and tomato on bread", UnitPrice = 5.19f },
			new FoodItem { FoodItemId = 7, Name = "Grilled Cheese Sandwich", Description = "Melted cheddar cheese on toasted bread", UnitPrice = 4.99f },
			new FoodItem { FoodItemId = 8, Name = "BLT Sandwich", Description = "Bacon, lettuce, and tomato on toasted bread", UnitPrice = 5.79f },
			new FoodItem { FoodItemId = 9, Name = "Fish Tacos", Description = "Grilled fish tacos with slaw and spicy mayo", UnitPrice = 7.99f },
			new FoodItem { FoodItemId = 10, Name = "Spaghetti Bolognese", Description = "Pasta with rich meat sauce", UnitPrice = 8.99f },
			new FoodItem { FoodItemId = 11, Name = "Penne Arrabbiata", Description = "Penne pasta in spicy tomato sauce with garlic", UnitPrice = 7.49f },
			new FoodItem { FoodItemId = 12, Name = "Mushroom Risotto", Description = "Creamy risotto with wild mushrooms", UnitPrice = 9.49f },
			new FoodItem { FoodItemId = 13, Name = "Chicken Alfredo", Description = "Fettuccine with Alfredo sauce and grilled chicken", UnitPrice = 10.99f },
			new FoodItem { FoodItemId = 14, Name = "Beef Burritos", Description = "Burritos filled with beef, rice, beans, and cheese", UnitPrice = 6.99f },
			new FoodItem { FoodItemId = 15, Name = "Vegetarian Tacos", Description = "Veggies, beans, and avocado in tacos", UnitPrice = 5.49f },
			new FoodItem { FoodItemId = 16, Name = "BBQ Chicken Pizza", Description = "Pizza with BBQ chicken and mozzarella", UnitPrice = 12.99f },
			new FoodItem { FoodItemId = 17, Name = "Pepperoni Pizza", Description = "Classic pepperoni pizza with marinara", UnitPrice = 11.49f },
			new FoodItem { FoodItemId = 18, Name = "Margherita Pizza", Description = "Pizza with fresh tomatoes, mozzarella, and basil", UnitPrice = 9.99f },
			new FoodItem { FoodItemId = 19, Name = "Cheeseburger", Description = "Beef burger with cheddar, lettuce, and tomato", UnitPrice = 6.49f },
			new FoodItem { FoodItemId = 20, Name = "Veggie Burger", Description = "Veggie patty with lettuce, tomato, and avocado", UnitPrice = 5.99f },
			new FoodItem { FoodItemId = 21, Name = "Chicken Wings", Description = "Crispy wings with your choice of sauce", UnitPrice = 7.99f },
			new FoodItem { FoodItemId = 22, Name = "Mozzarella Sticks", Description = "Golden mozzarella sticks with marinara", UnitPrice = 4.99f },
			new FoodItem { FoodItemId = 23, Name = "Onion Rings", Description = "Crispy onion rings with dipping sauce", UnitPrice = 3.99f },
			new FoodItem { FoodItemId = 24, Name = "Nachos", Description = "Tortilla chips with cheese, beans, and toppings", UnitPrice = 6.99f },
			new FoodItem { FoodItemId = 25, Name = "Hummus and Pita Bread", Description = "Hummus served with warm pita bread", UnitPrice = 4.49f },
			new FoodItem { FoodItemId = 26, Name = "Spaghetti and Meatballs", Description = "Spaghetti with meatballs and marinara sauce", UnitPrice = 8.99f },
			new FoodItem { FoodItemId = 27, Name = "Lasagna", Description = "Layers of pasta, meat sauce, and cheese", UnitPrice = 9.99f },
			new FoodItem { FoodItemId = 28, Name = "Chicken Parmesan", Description = "Breaded chicken with marinara sauce and cheese", UnitPrice = 10.99f },
			new FoodItem { FoodItemId = 29, Name = "Steak", Description = "Grilled steak with your choice of sides", UnitPrice = 12.99f },
			new FoodItem { FoodItemId = 30, Name = "Salmon", Description = "Grilled salmon with lemon and dill", UnitPrice = 11.99f },
			new FoodItem { FoodItemId = 31, Name = "French Fries", Description = "Crispy french fries", UnitPrice = 2.99f },
			new FoodItem { FoodItemId = 32, Name = "Mashed Potatoes", Description = "Creamy mashed potatoes", UnitPrice = 2.49f },
			new FoodItem { FoodItemId = 33, Name = "Green Beans", Description = "Steamed green beans", UnitPrice = 1.99f },
			new FoodItem { FoodItemId = 34, Name = "Corn", Description = "Buttered corn", UnitPrice = 1.99f },
			new FoodItem { FoodItemId = 35, Name = "Salad", Description = "Fresh salad with dressing", UnitPrice = 2.99f },
			new FoodItem { FoodItemId = 36, Name = "Ice Cream", Description = "Your choice of ice cream", UnitPrice = 2.99f },
			new FoodItem { FoodItemId = 37, Name = "Cake", Description = "A slice of cake", UnitPrice = 3.99f },
			new FoodItem { FoodItemId = 38, Name = "Brownies", Description = "Chocolate brownies", UnitPrice = 2.99f },
			new FoodItem { FoodItemId = 39, Name = "Cookies", Description = "Your choice of cookie", UnitPrice = 1.99f },
			new FoodItem { FoodItemId = 40, Name = "Fruit Salad", Description = "Fresh fruit salad", UnitPrice = 2.99f }
		);



		modelBuilder.Entity<Menu>().HasData(
            new Menu { MenuId = 1, MenuName = "Basic Lunch" },
            new Menu { MenuId = 2, MenuName = "Vegetarian Feast" },
            new Menu { MenuId = 3, MenuName = "Deluxe Dinner" },
		    new Menu { MenuId = 4, MenuName = "Healthy Lunch" },
            new Menu { MenuId = 5, MenuName = "Pizza Party" },
            new Menu { MenuId = 6, MenuName = "BBQ Feast" }
		);
    }
}
