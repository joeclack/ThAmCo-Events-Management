using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThAmCo.Events.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingCancelingAndDeletions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCancled",
                table: "Staffing",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAnonymised",
                table: "Staff",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAnonymised",
                table: "Guests",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCancled",
                table: "GuestBookings",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCanceled",
                table: "Events",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "GuestId",
                keyValue: 1,
                column: "IsAnonymised",
                value: false);

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "GuestId",
                keyValue: 2,
                column: "IsAnonymised",
                value: false);

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "GuestId",
                keyValue: 3,
                column: "IsAnonymised",
                value: false);

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "GuestId",
                keyValue: 4,
                column: "IsAnonymised",
                value: false);

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "GuestId",
                keyValue: 5,
                column: "IsAnonymised",
                value: false);

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "GuestId",
                keyValue: 6,
                column: "IsAnonymised",
                value: false);

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "GuestId",
                keyValue: 7,
                column: "IsAnonymised",
                value: false);

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "GuestId",
                keyValue: 8,
                column: "IsAnonymised",
                value: false);

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "GuestId",
                keyValue: 9,
                column: "IsAnonymised",
                value: false);

            migrationBuilder.UpdateData(
                table: "Guests",
                keyColumn: "GuestId",
                keyValue: 10,
                column: "IsAnonymised",
                value: false);

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "StaffId",
                keyValue: 1,
                column: "IsAnonymised",
                value: false);

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "StaffId",
                keyValue: 2,
                column: "IsAnonymised",
                value: false);

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "StaffId",
                keyValue: 3,
                column: "IsAnonymised",
                value: false);

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "StaffId",
                keyValue: 4,
                column: "IsAnonymised",
                value: false);

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "StaffId",
                keyValue: 5,
                column: "IsAnonymised",
                value: false);

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "StaffId",
                keyValue: 6,
                column: "IsAnonymised",
                value: false);

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "StaffId",
                keyValue: 7,
                column: "IsAnonymised",
                value: false);

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "StaffId",
                keyValue: 8,
                column: "IsAnonymised",
                value: false);

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "StaffId",
                keyValue: 9,
                column: "IsAnonymised",
                value: false);

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "StaffId",
                keyValue: 10,
                column: "IsAnonymised",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCancled",
                table: "Staffing");

            migrationBuilder.DropColumn(
                name: "IsAnonymised",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "IsAnonymised",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "IsCancled",
                table: "GuestBookings");

            migrationBuilder.DropColumn(
                name: "IsCanceled",
                table: "Events");
        }
    }
}
