using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ThAmCo.Catering.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FoodItems",
                columns: new[] { "FoodItemId", "Description", "Name", "UnitPrice" },
                values: new object[,]
                {
                    { 21, "Crispy chicken wings with your choice of sauce", "Chicken Wings", 7.99f },
                    { 22, "Golden-brown mozzarella sticks with marinara sauce", "Mozzarella Sticks", 4.99f },
                    { 23, "Crispy onion rings with a tangy dipping sauce", "Onion Rings", 3.99f },
                    { 24, "Crispy tortilla chips topped with cheese, beans, and your choice of toppings", "Nachos", 6.99f },
                    { 25, "Creamy hummus served with warm pita bread", "Hummus and Pita Bread", 4.49f },
                    { 26, "Classic spaghetti and meatballs with marinara sauce", "Spaghetti and Meatballs", 8.99f },
                    { 27, "Layers of pasta, meat sauce, and cheese", "Lasagna", 9.99f },
                    { 28, "Breaded chicken cutlet topped with marinara sauce and cheese", "Chicken Parmesan", 10.99f },
                    { 29, "Grilled steak with your choice of sides", "Steak", 12.99f },
                    { 30, "Grilled salmon with lemon and dill", "Salmon", 11.99f },
                    { 31, "Crispy french fries", "French Fries", 2.99f },
                    { 32, "Creamy mashed potatoes", "Mashed Potatoes", 2.49f },
                    { 33, "Steamed green beans", "Green Beans", 1.99f },
                    { 34, "Buttered corn", "Corn", 1.99f },
                    { 35, "Fresh salad with your choice of dressing", "Salad", 2.99f },
                    { 36, "Your choice of flavor", "Ice Cream", 2.99f },
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
                    { 4, "Healthy Lunch" },
                    { 5, "Pizza Party" },
                    { 6, "BBQ Feast" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "FoodItemId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "FoodItemId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "FoodItemId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "FoodItemId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "FoodItemId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "FoodItemId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "FoodItemId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "FoodItemId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "FoodItemId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "FoodItemId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "FoodItemId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "FoodItemId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "FoodItemId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "FoodItemId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "FoodItemId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "FoodItemId",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "FoodItemId",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "FoodItemId",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "FoodItemId",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "FoodItems",
                keyColumn: "FoodItemId",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "MenuId",
                keyValue: 6);
        }
    }
}
