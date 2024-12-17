using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ThAmCo.Events.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Guests",
                columns: new[] { "GuestId", "Email", "FirstName", "IsAnonymised", "LastName" },
                values: new object[,]
                {
                    { 11, "olivia.miller@example.com", "Olivia", false, "Miller" },
                    { 12, "william.garcia@example.com", "William", false, "Garcia" },
                    { 13, "noah.rodriguez@example.com", "Noah", false, "Rodriguez" },
                    { 14, "sophia.lee@example.com", "Sophia", false, "Lee" },
                    { 15, "jacob.nguyen@example.com", "Jacob", false, "Nguyen" },
                    { 16, "emma.clark@example.com", "Emma", false, "Clark" },
                    { 17, "ethan.hall@example.com", "Ethan", false, "Hall" },
                    { 18, "ava.walker@example.com", "Ava", false, "Walker" },
                    { 19, "liam.harris@example.com", "Liam", false, "Harris" },
                    { 20, "isabella.wright@example.com", "Isabella", false, "Wright" }
                });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "StaffId", "FirstName", "IsAnonymised", "IsFirstAider", "LastName", "Role" },
                values: new object[,]
                {
                    { 11, "Olivia", false, false, "Nelson", "Chef" },
                    { 12, "William", false, true, "Perez", "Server" },
                    { 13, "Abigail", false, true, "Campbell", "Manager" },
                    { 14, "Ethan", false, false, "Parker", "Chef" },
                    { 15, "Sophia", false, true, "Evans", "Server" },
                    { 16, "Mason", false, false, "Edwards", "Bartender" },
                    { 17, "Ava", false, true, "Collins", "Host" },
                    { 18, "Jacob", false, false, "Stewart", "Sous Chef" },
                    { 19, "Emily", false, true, "Sanchez", "Server" },
                    { 20, "Michael", false, true, "Morris", "Manager" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Guests",
                keyColumn: "GuestId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Guests",
                keyColumn: "GuestId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Guests",
                keyColumn: "GuestId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Guests",
                keyColumn: "GuestId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Guests",
                keyColumn: "GuestId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Guests",
                keyColumn: "GuestId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Guests",
                keyColumn: "GuestId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Guests",
                keyColumn: "GuestId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Guests",
                keyColumn: "GuestId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Guests",
                keyColumn: "GuestId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "StaffId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "StaffId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "StaffId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "StaffId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "StaffId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "StaffId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "StaffId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "StaffId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "StaffId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "StaffId",
                keyValue: 20);
        }
    }
}
