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
                    ClientReferenceId = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberOfGuests = table.Column<int>(type: "INTEGER", nullable: false),
                    MenuId = table.Column<int>(type: "INTEGER", nullable: false)
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
                columns: new[] { "FoodItemId", "Description", "UnitPrice" },
                values: new object[,]
                {
                    { 1, "Chicken Sandwich", 5.99f },
                    { 2, "Veggie Wrap", 4.49f },
                    { 3, "Caesar Salad", 3.99f },
                    { 4, "Steak", 12.99f },
                    { 5, "Turkey Club", 6.49f },
                    { 6, "Ham and Cheese", 5.19f },
                    { 7, "Grilled Cheese", 4.99f },
                    { 8, "BLT Sandwich", 5.79f },
                    { 9, "Fish Tacos", 7.99f },
                    { 10, "Spaghetti Bolognese", 8.99f },
                    { 11, "Penne Arrabbiata", 7.49f },
                    { 12, "Mushroom Risotto", 9.49f },
                    { 13, "Chicken Alfredo", 10.99f },
                    { 14, "Beef Burritos", 6.99f },
                    { 15, "Vegetarian Tacos", 5.49f },
                    { 16, "BBQ Chicken Pizza", 12.99f },
                    { 17, "Pepperoni Pizza", 11.49f },
                    { 18, "Margherita Pizza", 9.99f },
                    { 19, "Cheeseburger", 6.49f },
                    { 20, "Veggie Burger", 5.99f }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuId", "MenuName" },
                values: new object[,]
                {
                    { 1, "Basic Lunch" },
                    { 2, "Vegetarian Feast" },
                    { 3, "Deluxe Dinner" },
                    { 4, "Italian Specialties" },
                    { 5, "Mexican Fiesta" }
                });

            migrationBuilder.InsertData(
                table: "FoodBookings",
                columns: new[] { "FoodBookingId", "ClientReferenceId", "MenuId", "NumberOfGuests" },
                values: new object[,]
                {
                    { 1, 101, 1, 50 },
                    { 2, 102, 2, 30 },
                    { 3, 103, 3, 75 },
                    { 4, 104, 4, 100 },
                    { 5, 105, 5, 60 }
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
                    { 14, 3 },
                    { 12, 4 },
                    { 16, 4 },
                    { 17, 4 },
                    { 18, 4 },
                    { 9, 5 },
                    { 15, 5 },
                    { 19, 5 },
                    { 20, 5 }
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
