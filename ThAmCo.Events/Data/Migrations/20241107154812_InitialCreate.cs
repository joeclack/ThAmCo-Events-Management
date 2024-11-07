using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ThAmCo.Events.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: false),
                    EventTypeId = table.Column<string>(type: "TEXT", nullable: false),
                    ReservationId = table.Column<string>(type: "TEXT", nullable: false),
                    FoodBookingId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                });

            migrationBuilder.CreateTable(
                name: "Guests",
                columns: table => new
                {
                    GuestId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guests", x => x.GuestId);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    StaffId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.StaffId);
                });

            migrationBuilder.CreateTable(
                name: "GuestBookings",
                columns: table => new
                {
                    GuestBookingId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GuestId = table.Column<int>(type: "INTEGER", nullable: false),
                    EventId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestBookings", x => x.GuestBookingId);
                    table.ForeignKey(
                        name: "FK_GuestBookings_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GuestBookings_Guests_GuestId",
                        column: x => x.GuestId,
                        principalTable: "Guests",
                        principalColumn: "GuestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Staffing",
                columns: table => new
                {
                    StaffId = table.Column<int>(type: "INTEGER", nullable: false),
                    EventId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffing", x => new { x.StaffId, x.EventId });
                    table.ForeignKey(
                        name: "FK_Staffing_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Staffing_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "StaffId", "FirstName", "LastName", "Role" },
                values: new object[,]
                {
                    { 1, "FirstName1", "LastName1", "Manager" },
                    { 2, "FirstName2", "LastName2", "Chef" },
                    { 3, "FirstName3", "LastName3", "Bartender" },
                    { 4, "FirstName4", "LastName4", "Security" },
                    { 5, "FirstName5", "LastName5", "Coordinator" },
                    { 6, "FirstName6", "LastName6", "Event Planner" },
                    { 7, "FirstName7", "LastName7", "Cleaner" },
                    { 8, "FirstName8", "LastName8", "Technician" },
                    { 9, "FirstName9", "LastName9", "DJ" },
                    { 10, "FirstName10", "LastName10", "Photographer" },
                    { 11, "FirstName11", "LastName11", "Logistics Manager" },
                    { 12, "FirstName12", "LastName12", "Driver" },
                    { 13, "FirstName13", "LastName13", "Greeter" },
                    { 14, "FirstName14", "LastName14", "Marketing Specialist" },
                    { 15, "FirstName15", "LastName15", "Ticketing Agent" },
                    { 16, "FirstName16", "LastName16", "Host" },
                    { 17, "FirstName17", "LastName17", "VIP Assistant" },
                    { 18, "FirstName18", "LastName18", "Customer Support" },
                    { 19, "FirstName19", "LastName19", "Videographer" },
                    { 20, "FirstName20", "LastName20", "Stage Hand" },
                    { 21, "FirstName21", "LastName21", "Waiter" },
                    { 22, "FirstName22", "LastName22", "Manager" },
                    { 23, "FirstName23", "LastName23", "Chef" },
                    { 24, "FirstName24", "LastName24", "Bartender" },
                    { 25, "FirstName25", "LastName25", "Security" },
                    { 26, "FirstName26", "LastName26", "Coordinator" },
                    { 27, "FirstName27", "LastName27", "Event Planner" },
                    { 28, "FirstName28", "LastName28", "Cleaner" },
                    { 29, "FirstName29", "LastName29", "Technician" },
                    { 30, "FirstName30", "LastName30", "DJ" },
                    { 31, "FirstName31", "LastName31", "Photographer" },
                    { 32, "FirstName32", "LastName32", "Logistics Manager" },
                    { 33, "FirstName33", "LastName33", "Driver" },
                    { 34, "FirstName34", "LastName34", "Greeter" },
                    { 35, "FirstName35", "LastName35", "Marketing Specialist" },
                    { 36, "FirstName36", "LastName36", "Ticketing Agent" },
                    { 37, "FirstName37", "LastName37", "Host" },
                    { 38, "FirstName38", "LastName38", "VIP Assistant" },
                    { 39, "FirstName39", "LastName39", "Customer Support" },
                    { 40, "FirstName40", "LastName40", "Videographer" },
                    { 41, "FirstName41", "LastName41", "Stage Hand" },
                    { 42, "FirstName42", "LastName42", "Waiter" },
                    { 43, "FirstName43", "LastName43", "Manager" },
                    { 44, "FirstName44", "LastName44", "Chef" },
                    { 45, "FirstName45", "LastName45", "Bartender" },
                    { 46, "FirstName46", "LastName46", "Security" },
                    { 47, "FirstName47", "LastName47", "Coordinator" },
                    { 48, "FirstName48", "LastName48", "Event Planner" },
                    { 49, "FirstName49", "LastName49", "Cleaner" },
                    { 50, "FirstName50", "LastName50", "Technician" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GuestBookings_EventId",
                table: "GuestBookings",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_GuestBookings_GuestId",
                table: "GuestBookings",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_Staffing_EventId",
                table: "Staffing",
                column: "EventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuestBookings");

            migrationBuilder.DropTable(
                name: "Staffing");

            migrationBuilder.DropTable(
                name: "Guests");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Staff");
        }
    }
}
