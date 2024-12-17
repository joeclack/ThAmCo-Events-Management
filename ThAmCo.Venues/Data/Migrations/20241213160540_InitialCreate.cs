using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ThAmCo.Venues.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", fixedLength: true, maxLength: 3, nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Venues",
                columns: table => new
                {
                    Code = table.Column<string>(type: "TEXT", fixedLength: true, maxLength: 5, nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Capacity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Availabilities",
                columns: table => new
                {
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    VenueCode = table.Column<string>(type: "TEXT", fixedLength: true, maxLength: 5, nullable: false),
                    CostPerHour = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Availabilities", x => new { x.Date, x.VenueCode });
                    table.ForeignKey(
                        name: "FK_Availabilities_Venues_VenueCode",
                        column: x => x.VenueCode,
                        principalTable: "Venues",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Suitabilities",
                columns: table => new
                {
                    EventTypeId = table.Column<string>(type: "TEXT", fixedLength: true, nullable: false),
                    VenueCode = table.Column<string>(type: "TEXT", fixedLength: true, maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suitabilities", x => new { x.EventTypeId, x.VenueCode });
                    table.ForeignKey(
                        name: "FK_Suitabilities_EventTypes_EventTypeId",
                        column: x => x.EventTypeId,
                        principalTable: "EventTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Suitabilities_Venues_VenueCode",
                        column: x => x.VenueCode,
                        principalTable: "Venues",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Reference = table.Column<string>(type: "TEXT", fixedLength: true, maxLength: 13, nullable: false),
                    EventDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    VenueCode = table.Column<string>(type: "TEXT", fixedLength: true, maxLength: 5, nullable: false),
                    WhenMade = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StaffId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Reference);
                    table.ForeignKey(
                        name: "FK_Reservations_Availabilities_EventDate_VenueCode",
                        columns: x => new { x.EventDate, x.VenueCode },
                        principalTable: "Availabilities",
                        principalColumns: new[] { "Date", "VenueCode" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "EventTypes",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { "CON", "Conference" },
                    { "MET", "Meeting" },
                    { "PTY", "Party" },
                    { "WED", "Wedding" }
                });

            migrationBuilder.InsertData(
                table: "Venues",
                columns: new[] { "Code", "Capacity", "Description", "Name" },
                values: new object[,]
                {
                    { "CRKHL", 150, "Once the residence of Lord and Lady Crackling, this lavish dwelling remains a prime example of 18th century fine living.", "Crackling Hall" },
                    { "FDLCK", 85, "Rustic pub set in ideallic countryside, the original venue of a notorious local musician and his parrot.", "The Fiddler's Cockatoo" },
                    { "TNDMR", 450, "Refurbished manor house with fully equipped facilities ready to help you have a good time in business or pleasure.", "Tinder Manor" }
                });

            migrationBuilder.InsertData(
                table: "Availabilities",
                columns: new[] { "Date", "VenueCode", "CostPerHour" },
                values: new object[,]
                {
                    { new DateTime(2024, 12, 13, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 51.789999999999999 },
                    { new DateTime(2024, 12, 18, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 46.770000000000003 },
                    { new DateTime(2024, 12, 18, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 72.069999999999993 },
                    { new DateTime(2024, 12, 19, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 91.030000000000001 },
                    { new DateTime(2024, 12, 19, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 57.18 },
                    { new DateTime(2024, 12, 20, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 61.130000000000003 },
                    { new DateTime(2024, 12, 21, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 139.55000000000001 },
                    { new DateTime(2024, 12, 26, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 96.379999999999995 },
                    { new DateTime(2024, 12, 28, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 92.689999999999998 },
                    { new DateTime(2024, 12, 30, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 95.010000000000005 },
                    { new DateTime(2024, 12, 30, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 59.859999999999999 },
                    { new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 57.450000000000003 },
                    { new DateTime(2025, 1, 3, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 55.439999999999998 },
                    { new DateTime(2025, 1, 3, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 79.260000000000005 },
                    { new DateTime(2025, 1, 4, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 80.489999999999995 },
                    { new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 97.650000000000006 },
                    { new DateTime(2025, 1, 7, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 57.399999999999999 },
                    { new DateTime(2025, 1, 8, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 58.020000000000003 },
                    { new DateTime(2025, 1, 9, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 78.489999999999995 },
                    { new DateTime(2025, 1, 9, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 50.630000000000003 },
                    { new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 94.670000000000002 },
                    { new DateTime(2025, 1, 11, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 32.43 },
                    { new DateTime(2025, 1, 13, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 68.049999999999997 },
                    { new DateTime(2025, 1, 14, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 92.519999999999996 },
                    { new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 53.119999999999997 },
                    { new DateTime(2025, 1, 16, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 49.280000000000001 },
                    { new DateTime(2025, 1, 16, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 104.76000000000001 },
                    { new DateTime(2025, 1, 18, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 30.91 },
                    { new DateTime(2025, 1, 19, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 99.439999999999998 },
                    { new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 69.359999999999999 },
                    { new DateTime(2025, 1, 21, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 76.140000000000001 },
                    { new DateTime(2025, 1, 22, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 64.019999999999996 },
                    { new DateTime(2025, 1, 23, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 51.479999999999997 },
                    { new DateTime(2025, 1, 23, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 112.88 },
                    { new DateTime(2025, 1, 24, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 109.15000000000001 },
                    { new DateTime(2025, 1, 25, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 115.89 },
                    { new DateTime(2025, 1, 26, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 64.030000000000001 },
                    { new DateTime(2025, 1, 27, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 53.840000000000003 },
                    { new DateTime(2025, 1, 28, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 92.319999999999993 },
                    { new DateTime(2025, 1, 28, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 40.109999999999999 },
                    { new DateTime(2025, 1, 28, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 76.810000000000002 },
                    { new DateTime(2025, 1, 29, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 80.659999999999997 },
                    { new DateTime(2025, 1, 29, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 43.719999999999999 },
                    { new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 42.299999999999997 },
                    { new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 51.560000000000002 },
                    { new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 87.870000000000005 },
                    { new DateTime(2025, 2, 3, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 48.590000000000003 },
                    { new DateTime(2025, 2, 4, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 84.980000000000004 },
                    { new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 80.769999999999996 },
                    { new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 35.850000000000001 },
                    { new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 83.709999999999994 },
                    { new DateTime(2025, 2, 6, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 91.530000000000001 },
                    { new DateTime(2025, 2, 6, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 132.13 },
                    { new DateTime(2025, 2, 7, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 104.41 },
                    { new DateTime(2025, 2, 8, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 114.65000000000001 },
                    { new DateTime(2025, 2, 10, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 50.390000000000001 },
                    { new DateTime(2025, 2, 10, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 95.829999999999998 },
                    { new DateTime(2025, 2, 12, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 62.539999999999999 },
                    { new DateTime(2025, 2, 14, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 78.430000000000007 },
                    { new DateTime(2025, 2, 22, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 77.700000000000003 },
                    { new DateTime(2025, 2, 26, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 33.030000000000001 },
                    { new DateTime(2025, 2, 27, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 110.11 },
                    { new DateTime(2025, 2, 28, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 77.640000000000001 },
                    { new DateTime(2025, 3, 2, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 130.16999999999999 },
                    { new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 35.670000000000002 },
                    { new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 101.31999999999999 },
                    { new DateTime(2025, 3, 4, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 53.219999999999999 },
                    { new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 74.150000000000006 },
                    { new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 102.22 },
                    { new DateTime(2025, 3, 8, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 44.049999999999997 },
                    { new DateTime(2025, 3, 9, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 99.879999999999995 },
                    { new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 115.3 },
                    { new DateTime(2025, 3, 11, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 72.340000000000003 },
                    { new DateTime(2025, 3, 11, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 53.810000000000002 },
                    { new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 52.939999999999998 },
                    { new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 112.63 }
                });

            migrationBuilder.InsertData(
                table: "Suitabilities",
                columns: new[] { "EventTypeId", "VenueCode" },
                values: new object[,]
                {
                    { "CON", "CRKHL" },
                    { "CON", "TNDMR" },
                    { "MET", "TNDMR" },
                    { "PTY", "CRKHL" },
                    { "PTY", "FDLCK" },
                    { "WED", "CRKHL" },
                    { "WED", "FDLCK" },
                    { "WED", "TNDMR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Availabilities_VenueCode",
                table: "Availabilities",
                column: "VenueCode");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_EventDate_VenueCode",
                table: "Reservations",
                columns: new[] { "EventDate", "VenueCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suitabilities_VenueCode",
                table: "Suitabilities",
                column: "VenueCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Suitabilities");

            migrationBuilder.DropTable(
                name: "Availabilities");

            migrationBuilder.DropTable(
                name: "EventTypes");

            migrationBuilder.DropTable(
                name: "Venues");
        }
    }
}
