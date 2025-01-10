using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ThAmCo.Catering.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FoodItems",
                columns: table => new
                {
                    FoodItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UnitPrice = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodItems", x => x.FoodItemId);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    MenuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.MenuId);
                });

            migrationBuilder.CreateTable(
                name: "FoodBookings",
                columns: table => new
                {
                    FoodBookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientReferenceId = table.Column<int>(type: "int", nullable: true),
                    NumberOfGuests = table.Column<int>(type: "int", nullable: false),
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    FoodBookingDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodBookings", x => x.FoodBookingId);
                    table.ForeignKey(
                        name: "FK_FoodBookings_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "MenuId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MenuFoodItems",
                columns: table => new
                {
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    FoodItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuFoodItems", x => new { x.MenuId, x.FoodItemId });
                    table.ForeignKey(
                        name: "FK_MenuFoodItems_FoodItems_FoodItemId",
                        column: x => x.FoodItemId,
                        principalTable: "FoodItems",
                        principalColumn: "FoodItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuFoodItems_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "MenuId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "FoodItems",
                columns: new[] { "FoodItemId", "Description", "Name", "UnitPrice" },
                values: new object[,]
                {
                    { 1, "Grilled chicken with lettuce, tomato, and mayo", "Classic Chicken Sandwich", 5.99f },
                    { 2, "Whole-wheat wrap with hummus and fresh veggies", "Fresh Veggie Wrap", 4.49f },
                    { 3, "Romaine lettuce with Caesar dressing and croutons", "Caesar Salad", 3.99f },
                    { 4, "Juicy grilled steak seasoned perfectly", "Grilled Steak", 12.99f },
                    { 5, "Turkey, bacon, lettuce, tomato on toasted bread", "Turkey Club Sandwich", 6.49f },
                    { 6, "Ham, cheese, lettuce, and tomato on bread", "Ham and Cheese Sandwich", 5.19f },
                    { 7, "Melted cheddar cheese on toasted bread", "Grilled Cheese Sandwich", 4.99f },
                    { 8, "Bacon, lettuce, and tomato on toasted bread", "BLT Sandwich", 5.79f },
                    { 9, "Grilled fish tacos with slaw and spicy mayo", "Fish Tacos", 7.99f },
                    { 10, "Pasta with rich meat sauce", "Spaghetti Bolognese", 8.99f },
                    { 11, "Penne pasta in spicy tomato sauce with garlic", "Penne Arrabbiata", 7.49f },
                    { 12, "Creamy risotto with wild mushrooms", "Mushroom Risotto", 9.49f },
                    { 13, "Fettuccine with Alfredo sauce and grilled chicken", "Chicken Alfredo", 10.99f },
                    { 14, "Burritos filled with beef, rice, beans, and cheese", "Beef Burritos", 6.99f },
                    { 15, "Veggies, beans, and avocado in tacos", "Vegetarian Tacos", 5.49f },
                    { 16, "Pizza with BBQ chicken and mozzarella", "BBQ Chicken Pizza", 12.99f },
                    { 17, "Classic pepperoni pizza with marinara", "Pepperoni Pizza", 11.49f },
                    { 18, "Pizza with fresh tomatoes, mozzarella, and basil", "Margherita Pizza", 9.99f },
                    { 19, "Beef burger with cheddar, lettuce, and tomato", "Cheeseburger", 6.49f },
                    { 20, "Veggie patty with lettuce, tomato, and avocado", "Veggie Burger", 5.99f },
                    { 21, "Crispy wings with your choice of sauce", "Chicken Wings", 7.99f },
                    { 22, "Golden mozzarella sticks with marinara", "Mozzarella Sticks", 4.99f },
                    { 23, "Crispy onion rings with dipping sauce", "Onion Rings", 3.99f },
                    { 24, "Tortilla chips with cheese, beans, and toppings", "Nachos", 6.99f },
                    { 25, "Hummus served with warm pita bread", "Hummus and Pita Bread", 4.49f },
                    { 26, "Spaghetti with meatballs and marinara sauce", "Spaghetti and Meatballs", 8.99f },
                    { 27, "Layers of pasta, meat sauce, and cheese", "Lasagna", 9.99f },
                    { 28, "Breaded chicken with marinara sauce and cheese", "Chicken Parmesan", 10.99f },
                    { 29, "Grilled steak with your choice of sides", "Steak", 12.99f },
                    { 30, "Grilled salmon with lemon and dill", "Salmon", 11.99f },
                    { 31, "Crispy french fries", "French Fries", 2.99f },
                    { 32, "Creamy mashed potatoes", "Mashed Potatoes", 2.49f },
                    { 33, "Steamed green beans", "Green Beans", 1.99f },
                    { 34, "Buttered corn", "Corn", 1.99f },
                    { 35, "Fresh salad with dressing", "Salad", 2.99f },
                    { 36, "Your choice of ice cream", "Ice Cream", 2.99f },
                    { 37, "A slice of cake", "Cake", 3.99f },
                    { 38, "Chocolate brownies", "Brownies", 2.99f },
                    { 39, "Your choice of cookie", "Cookies", 1.99f },
                    { 40, "Fresh fruit salad", "Fruit Salad", 2.99f }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuId", "MenuName" },
                values: new object[,]
                {
                    { 1, "Basic Lunch" },
                    { 2, "Vegetarian Feast" },
                    { 3, "Deluxe Dinner" },
                    { 4, "Healthy Lunch" },
                    { 5, "Pizza Party" },
                    { 6, "BBQ Feast" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodBookings_MenuId",
                table: "FoodBookings",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuFoodItems_FoodItemId",
                table: "MenuFoodItems",
                column: "FoodItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodBookings");

            migrationBuilder.DropTable(
                name: "MenuFoodItems");

            migrationBuilder.DropTable(
                name: "FoodItems");

            migrationBuilder.DropTable(
                name: "Menus");
        }
    }
}
