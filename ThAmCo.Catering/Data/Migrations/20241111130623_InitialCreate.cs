using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ThAmCo.Catering.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FoodItems",
                columns: table => new
                {
                    FoodItemId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    UnitPrice = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodItems", x => x.FoodItemId);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    MenuId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MenuName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.MenuId);
                });

            migrationBuilder.CreateTable(
                name: "FoodBookings",
                columns: table => new
                {
                    FoodBookingId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClientReferenceId = table.Column<int>(type: "INTEGER", nullable: true),
                    NumberOfGuests = table.Column<int>(type: "INTEGER", nullable: false),
                    MenuId = table.Column<int>(type: "INTEGER", nullable: false),
                    FoodBookingDate = table.Column<DateTime>(type: "TEXT", nullable: false)
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
                    MenuId = table.Column<int>(type: "INTEGER", nullable: false),
                    FoodItemId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuFoodItems", x => new { x.MenuId, x.FoodItemId });
                    table.ForeignKey(
                        name: "FK_MenuFoodItems_FoodItems_FoodItemId",
                        column: x => x.FoodItemId,
                        principalTable: "FoodItems",
                        principalColumn: "FoodItemId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuFoodItems_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "MenuId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "FoodItems",
                columns: new[] { "FoodItemId", "Description", "Name", "UnitPrice" },
                values: new object[,]
                {
                    { 1, "Grilled chicken breast with lettuce, tomato, and mayo on a toasted bun", "Classic Chicken Sandwich", 5.99f },
                    { 2, "Whole-wheat wrap with hummus, fresh veggies, and a hint of vinaigrette", "Fresh Veggie Wrap", 4.49f },
                    { 3, "Crisp romaine lettuce with Caesar dressing, croutons, and Parmesan cheese", "Caesar Salad", 3.99f },
                    { 4, "Juicy grilled steak seasoned to perfection", "Grilled Steak", 12.99f },
                    { 5, "Turkey, bacon, lettuce, and tomato stacked on toasted bread", "Turkey Club Sandwich", 6.49f },
                    { 6, "Classic ham and cheese sandwich with fresh lettuce and tomato", "Ham and Cheese Sandwich", 5.19f },
                    { 7, "Melted cheddar cheese between crispy, toasted bread", "Grilled Cheese Sandwich", 4.99f },
                    { 8, "Bacon, lettuce, and tomato on toasted whole-grain bread", "BLT Sandwich", 5.79f },
                    { 9, "Two tacos with grilled fish, slaw, and a spicy mayo drizzle", "Fish Tacos", 7.99f },
                    { 10, "Classic Italian pasta with rich meat sauce", "Spaghetti Bolognese", 8.99f },
                    { 11, "Penne pasta in a spicy tomato sauce with garlic and chili", "Penne Arrabbiata", 7.49f },
                    { 12, "Creamy risotto with wild mushrooms and Parmesan", "Mushroom Risotto", 9.49f },
                    { 13, "Fettuccine pasta with creamy Alfredo sauce and grilled chicken", "Chicken Alfredo", 10.99f },
                    { 14, "Two burritos filled with beef, rice, beans, and cheese", "Beef Burritos", 6.99f },
                    { 15, "Two tacos filled with seasoned veggies, beans, and avocado", "Vegetarian Tacos", 5.49f },
                    { 16, "Pizza topped with BBQ chicken, red onions, and mozzarella", "BBQ Chicken Pizza", 12.99f },
                    { 17, "Classic pepperoni pizza with marinara sauce and mozzarella", "Pepperoni Pizza", 11.49f },
                    { 18, "Pizza with fresh tomatoes, mozzarella, and basil", "Margherita Pizza", 9.99f },
                    { 19, "Beef burger with melted cheddar, lettuce, and tomato on a brioche bun", "Cheeseburger", 6.49f },
                    { 20, "Grilled veggie patty with lettuce, tomato, and avocado on a whole-wheat bun", "Veggie Burger", 5.99f }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuId", "MenuName" },
                values: new object[,]
                {
                    { 1, "Basic Lunch" },
                    { 2, "Vegetarian Feast" },
                    { 3, "Deluxe Dinner" }
                });

            migrationBuilder.InsertData(
                table: "FoodBookings",
                columns: new[] { "FoodBookingId", "ClientReferenceId", "FoodBookingDate", "MenuId", "NumberOfGuests" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 11, 11, 13, 6, 22, 821, DateTimeKind.Local).AddTicks(1568), 1, 1 },
                    { 2, 2, new DateTime(2024, 11, 21, 13, 6, 22, 821, DateTimeKind.Local).AddTicks(1629), 2, 5 },
                    { 3, 3, new DateTime(2024, 12, 11, 13, 6, 22, 821, DateTimeKind.Local).AddTicks(1634), 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "MenuFoodItems",
                columns: new[] { "FoodItemId", "MenuId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 3, 1 },
                    { 5, 1 },
                    { 6, 1 },
                    { 2, 2 },
                    { 3, 2 },
                    { 7, 2 },
                    { 15, 2 },
                    { 4, 3 },
                    { 10, 3 },
                    { 13, 3 },
                    { 14, 3 }
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
