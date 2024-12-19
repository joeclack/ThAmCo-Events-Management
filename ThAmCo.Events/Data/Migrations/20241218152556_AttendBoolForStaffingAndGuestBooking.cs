using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThAmCo.Events.Data.Migrations
{
    /// <inheritdoc />
    public partial class AttendBoolForStaffingAndGuestBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsCancled",
                table: "Staffing",
                newName: "DidAttend");

            migrationBuilder.RenameColumn(
                name: "IsCancled",
                table: "GuestBookings",
                newName: "DidAttend");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DidAttend",
                table: "Staffing",
                newName: "IsCancled");

            migrationBuilder.RenameColumn(
                name: "DidAttend",
                table: "GuestBookings",
                newName: "IsCancled");
        }
    }
}
