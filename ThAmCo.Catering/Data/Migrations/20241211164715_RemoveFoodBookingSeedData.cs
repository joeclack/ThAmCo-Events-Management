using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ThAmCo.Catering.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFoodBookingSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FoodBookings",
                keyColumn: "FoodBookingId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FoodBookings",
                keyColumn: "FoodBookingId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FoodBookings",
                keyColumn: "FoodBookingId",
                keyValue: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FoodBookings",
                columns: new[] { "FoodBookingId", "ClientReferenceId", "FoodBookingDate", "MenuId", "NumberOfGuests" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 11, 28, 10, 8, 27, 416, DateTimeKind.Local).AddTicks(900), 1, 1 },
                    { 2, 2, new DateTime(2024, 12, 8, 10, 8, 27, 416, DateTimeKind.Local).AddTicks(948), 2, 5 },
                    { 3, 3, new DateTime(2024, 12, 28, 10, 8, 27, 416, DateTimeKind.Local).AddTicks(951), 3, 2 }
                });
        }
    }
}
