using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThAmCo.Catering.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                });

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
                name: "MenuFoodItems",
                columns: table => new
                {
                    MenuFoodItemId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MenuId = table.Column<int>(type: "INTEGER", nullable: false),
                    FoodItemId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuFoodItems", x => x.MenuFoodItemId);
                    table.ForeignKey(
                        name: "FK_MenuFoodItems_FoodItems_FoodItemId",
                        column: x => x.FoodItemId,
                        principalTable: "FoodItems",
                        principalColumn: "FoodItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodItemMenuFoodItem",
                columns: table => new
                {
                    FoodItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    MenuFoodItemsMenuFoodItemId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodItemMenuFoodItem", x => new { x.FoodItemId, x.MenuFoodItemsMenuFoodItemId });
                    table.ForeignKey(
                        name: "FK_FoodItemMenuFoodItem_FoodItems_FoodItemId",
                        column: x => x.FoodItemId,
                        principalTable: "FoodItems",
                        principalColumn: "FoodItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodItemMenuFoodItem_MenuFoodItems_MenuFoodItemsMenuFoodItemId",
                        column: x => x.MenuFoodItemsMenuFoodItemId,
                        principalTable: "MenuFoodItems",
                        principalColumn: "MenuFoodItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    MenuId = table.Column<int>(type: "INTEGER", nullable: false),
                    MenuName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.MenuId);
                    table.ForeignKey(
                        name: "FK_Menus_FoodBookings_MenuId",
                        column: x => x.MenuId,
                        principalTable: "FoodBookings",
                        principalColumn: "FoodBookingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Menus_MenuFoodItems_MenuId",
                        column: x => x.MenuId,
                        principalTable: "MenuFoodItems",
                        principalColumn: "MenuFoodItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodBookingMenu",
                columns: table => new
                {
                    FoodBookingsFoodBookingId = table.Column<int>(type: "INTEGER", nullable: false),
                    MenuId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodBookingMenu", x => new { x.FoodBookingsFoodBookingId, x.MenuId });
                    table.ForeignKey(
                        name: "FK_FoodBookingMenu_FoodBookings_FoodBookingsFoodBookingId",
                        column: x => x.FoodBookingsFoodBookingId,
                        principalTable: "FoodBookings",
                        principalColumn: "FoodBookingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodBookingMenu_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "MenuId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MenuMenuFoodItem",
                columns: table => new
                {
                    MenuFoodItemsMenuFoodItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    MenuId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuMenuFoodItem", x => new { x.MenuFoodItemsMenuFoodItemId, x.MenuId });
                    table.ForeignKey(
                        name: "FK_MenuMenuFoodItem_MenuFoodItems_MenuFoodItemsMenuFoodItemId",
                        column: x => x.MenuFoodItemsMenuFoodItemId,
                        principalTable: "MenuFoodItems",
                        principalColumn: "MenuFoodItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuMenuFoodItem_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "MenuId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodBookingMenu_MenuId",
                table: "FoodBookingMenu",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodItemMenuFoodItem_MenuFoodItemsMenuFoodItemId",
                table: "FoodItemMenuFoodItem",
                column: "MenuFoodItemsMenuFoodItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuFoodItems_FoodItemId",
                table: "MenuFoodItems",
                column: "FoodItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MenuMenuFoodItem_MenuId",
                table: "MenuMenuFoodItem",
                column: "MenuId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodBookingMenu");

            migrationBuilder.DropTable(
                name: "FoodItemMenuFoodItem");

            migrationBuilder.DropTable(
                name: "MenuMenuFoodItem");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "FoodBookings");

            migrationBuilder.DropTable(
                name: "MenuFoodItems");

            migrationBuilder.DropTable(
                name: "FoodItems");
        }
    }
}
