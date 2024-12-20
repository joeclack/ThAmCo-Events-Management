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
                    FoodBookingId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsCanceled = table.Column<bool>(type: "INTEGER", nullable: false)
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
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    IsAnonymised = table.Column<bool>(type: "INTEGER", nullable: false)
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
                    Role = table.Column<string>(type: "TEXT", nullable: false),
                    IsFirstAider = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsAnonymised = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.StaffId);
                });

            migrationBuilder.CreateTable(
                name: "GuestBookings",
                columns: table => new
                {
                    GuestId = table.Column<int>(type: "INTEGER", nullable: false),
                    EventId = table.Column<int>(type: "INTEGER", nullable: false),
                    DidAttend = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestBookings", x => new { x.GuestId, x.EventId });
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
                    EventId = table.Column<int>(type: "INTEGER", nullable: false),
                    DidAttend = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffing", x => new { x.EventId, x.StaffId });
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
                table: "Guests",
                columns: new[] { "GuestId", "Email", "FirstName", "IsAnonymised", "LastName" },
                values: new object[,]
                {
                    { 1, "alice.green@example.com", "Alice", false, "Green" },
                    { 2, "bob.smith@example.com", "Bob", false, "Smith" },
                    { 3, "carol.johnson@example.com", "Carol", false, "Johnson" },
                    { 4, "david.brown@example.com", "David", false, "Brown" },
                    { 5, "ella.davis@example.com", "Ella", false, "Davis" },
                    { 6, "frank.wilson@example.com", "Frank", false, "Wilson" },
                    { 7, "grace.martinez@example.com", "Grace", false, "Martinez" },
                    { 8, "henry.anderson@example.com", "Henry", false, "Anderson" },
                    { 9, "ivy.thomas@example.com", "Ivy", false, "Thomas" },
                    { 10, "jack.moore@example.com", "Jack", false, "Moore" },
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
                    { 1, "John", false, false, "Doe", "Chef" },
                    { 2, "Jane", false, false, "Smith", "Server" },
                    { 3, "Emily", false, true, "Johnson", "Manager" },
                    { 4, "Michael", false, true, "Williams", "Chef" },
                    { 5, "Sarah", false, true, "Brown", "Server" },
                    { 6, "David", false, false, "Jones", "Bartender" },
                    { 7, "Laura", false, false, "Garcia", "Host" },
                    { 8, "Daniel", false, true, "Martinez", "Sous Chef" },
                    { 9, "Sophia", false, true, "Anderson", "Server" },
                    { 10, "James", false, true, "Taylor", "Manager" },
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

            migrationBuilder.CreateIndex(
                name: "IX_GuestBookings_EventId",
                table: "GuestBookings",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Staffing_StaffId",
                table: "Staffing",
                column: "StaffId");
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
