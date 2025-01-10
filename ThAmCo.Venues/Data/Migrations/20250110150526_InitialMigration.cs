using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ThAmCo.Venues.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nchar(3)", fixedLength: true, maxLength: 3, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Venues",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nchar(5)", fixedLength: true, maxLength: 5, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Availabilities",
                columns: table => new
                {
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VenueCode = table.Column<string>(type: "nchar(5)", fixedLength: true, maxLength: 5, nullable: false),
                    CostPerHour = table.Column<double>(type: "float", nullable: false)
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
                    EventTypeId = table.Column<string>(type: "nchar(3)", fixedLength: true, nullable: false),
                    VenueCode = table.Column<string>(type: "nchar(5)", fixedLength: true, maxLength: 5, nullable: false)
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
                    Reference = table.Column<string>(type: "nchar(13)", fixedLength: true, maxLength: 13, nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VenueCode = table.Column<string>(type: "nchar(5)", fixedLength: true, maxLength: 5, nullable: false),
                    WhenMade = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StaffId = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    { new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 51.789999999999999 },
                    { new DateTime(2025, 1, 12, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 86.450000000000003 },
                    { new DateTime(2025, 1, 12, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 79.379999999999995 },
                    { new DateTime(2025, 1, 14, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 61.43 },
                    { new DateTime(2025, 1, 14, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 36.18 },
                    { new DateTime(2025, 1, 16, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 57.18 },
                    { new DateTime(2025, 1, 16, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 137.63 },
                    { new DateTime(2025, 1, 17, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 64.0 },
                    { new DateTime(2025, 1, 17, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 116.25 },
                    { new DateTime(2025, 1, 18, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 74.230000000000004 },
                    { new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 38.759999999999998 },
                    { new DateTime(2025, 1, 21, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 70.799999999999997 },
                    { new DateTime(2025, 1, 22, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 89.709999999999994 },
                    { new DateTime(2025, 1, 23, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 59.640000000000001 },
                    { new DateTime(2025, 1, 23, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 80.709999999999994 },
                    { new DateTime(2025, 1, 25, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 30.91 },
                    { new DateTime(2025, 1, 26, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 89.709999999999994 },
                    { new DateTime(2025, 1, 27, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 87.790000000000006 },
                    { new DateTime(2025, 1, 28, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 105.06 },
                    { new DateTime(2025, 1, 30, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 120.2 },
                    { new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 59.759999999999998 },
                    { new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 30.98 },
                    { new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 104.73 },
                    { new DateTime(2025, 2, 3, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 88.900000000000006 },
                    { new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 85.0 },
                    { new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 110.03 },
                    { new DateTime(2025, 2, 11, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 95.290000000000006 },
                    { new DateTime(2025, 2, 12, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 70.480000000000004 },
                    { new DateTime(2025, 2, 12, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 59.659999999999997 },
                    { new DateTime(2025, 2, 12, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 133.22999999999999 },
                    { new DateTime(2025, 2, 13, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 90.780000000000001 },
                    { new DateTime(2025, 2, 13, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 49.280000000000001 },
                    { new DateTime(2025, 2, 14, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 79.060000000000002 },
                    { new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 30.91 },
                    { new DateTime(2025, 2, 16, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 128.63999999999999 },
                    { new DateTime(2025, 2, 17, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 40.289999999999999 },
                    { new DateTime(2025, 2, 17, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 121.62 },
                    { new DateTime(2025, 2, 19, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 103.33 },
                    { new DateTime(2025, 2, 23, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 98.299999999999997 },
                    { new DateTime(2025, 2, 23, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 103.76000000000001 },
                    { new DateTime(2025, 2, 24, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 72.159999999999997 },
                    { new DateTime(2025, 2, 24, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 53.840000000000003 },
                    { new DateTime(2025, 2, 25, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 71.5 },
                    { new DateTime(2025, 2, 26, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 43.719999999999999 },
                    { new DateTime(2025, 2, 26, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 76.109999999999999 },
                    { new DateTime(2025, 2, 27, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 52.75 },
                    { new DateTime(2025, 2, 27, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 34.399999999999999 },
                    { new DateTime(2025, 2, 28, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 82.030000000000001 },
                    { new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 93.269999999999996 },
                    { new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 119.92 },
                    { new DateTime(2025, 3, 4, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 44.640000000000001 },
                    { new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 124.63 },
                    { new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 71.049999999999997 },
                    { new DateTime(2025, 3, 7, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 107.68000000000001 },
                    { new DateTime(2025, 3, 8, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 84.439999999999998 },
                    { new DateTime(2025, 3, 8, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 52.060000000000002 },
                    { new DateTime(2025, 3, 9, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 55.75 },
                    { new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 50.390000000000001 },
                    { new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 72.590000000000003 },
                    { new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 66.870000000000005 },
                    { new DateTime(2025, 3, 13, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 90.799999999999997 },
                    { new DateTime(2025, 3, 14, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 56.890000000000001 },
                    { new DateTime(2025, 3, 14, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 95.230000000000004 },
                    { new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 32.600000000000001 },
                    { new DateTime(2025, 3, 16, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 71.349999999999994 },
                    { new DateTime(2025, 3, 17, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 75.510000000000005 },
                    { new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 69.230000000000004 },
                    { new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 76.049999999999997 },
                    { new DateTime(2025, 3, 26, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 96.329999999999998 },
                    { new DateTime(2025, 3, 27, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 122.59 },
                    { new DateTime(2025, 3, 29, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 70.969999999999999 },
                    { new DateTime(2025, 3, 31, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 95.810000000000002 },
                    { new DateTime(2025, 3, 31, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 35.670000000000002 },
                    { new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 103.92 },
                    { new DateTime(2025, 4, 4, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 61.840000000000003 },
                    { new DateTime(2025, 4, 4, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 122.68000000000001 },
                    { new DateTime(2025, 4, 5, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 91.939999999999998 },
                    { new DateTime(2025, 4, 5, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 44.049999999999997 },
                    { new DateTime(2025, 4, 7, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 85.689999999999998 },
                    { new DateTime(2025, 4, 8, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 87.140000000000001 },
                    { new DateTime(2025, 4, 10, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 79.430000000000007 },
                    { new DateTime(2025, 4, 10, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 102.34 },
                    { new DateTime(2025, 4, 11, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 38.850000000000001 },
                    { new DateTime(2025, 4, 11, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 93.219999999999999 },
                    { new DateTime(2025, 4, 13, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 49.899999999999999 },
                    { new DateTime(2025, 4, 13, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 119.03 },
                    { new DateTime(2025, 4, 14, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 39.979999999999997 },
                    { new DateTime(2025, 4, 14, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 90.310000000000002 },
                    { new DateTime(2025, 4, 16, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 54.619999999999997 },
                    { new DateTime(2025, 4, 17, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 72.25 },
                    { new DateTime(2025, 4, 18, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 82.069999999999993 },
                    { new DateTime(2025, 4, 18, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 86.519999999999996 },
                    { new DateTime(2025, 4, 19, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 102.41 },
                    { new DateTime(2025, 4, 21, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 52.270000000000003 },
                    { new DateTime(2025, 4, 21, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 50.789999999999999 },
                    { new DateTime(2025, 4, 21, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 108.02 },
                    { new DateTime(2025, 4, 24, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 62.859999999999999 },
                    { new DateTime(2025, 4, 24, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 36.640000000000001 },
                    { new DateTime(2025, 4, 24, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 71.359999999999999 },
                    { new DateTime(2025, 4, 26, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 58.340000000000003 },
                    { new DateTime(2025, 4, 26, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 32.0 },
                    { new DateTime(2025, 4, 26, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 120.05 },
                    { new DateTime(2025, 4, 28, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 50.359999999999999 },
                    { new DateTime(2025, 4, 29, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 63.100000000000001 },
                    { new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 78.379999999999995 },
                    { new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 33.210000000000001 },
                    { new DateTime(2025, 5, 3, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 133.65000000000001 },
                    { new DateTime(2025, 5, 4, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 138.90000000000001 },
                    { new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 99.760000000000005 },
                    { new DateTime(2025, 5, 6, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 50.329999999999998 },
                    { new DateTime(2025, 5, 8, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 85.159999999999997 },
                    { new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 48.549999999999997 },
                    { new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 81.319999999999993 },
                    { new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 30.149999999999999 },
                    { new DateTime(2025, 5, 12, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 55.509999999999998 },
                    { new DateTime(2025, 5, 13, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 31.870000000000001 },
                    { new DateTime(2025, 5, 13, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 91.890000000000001 },
                    { new DateTime(2025, 5, 14, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 96.620000000000005 },
                    { new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 66.040000000000006 },
                    { new DateTime(2025, 5, 16, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 55.399999999999999 },
                    { new DateTime(2025, 5, 17, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 104.97 },
                    { new DateTime(2025, 5, 20, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 58.43 },
                    { new DateTime(2025, 5, 21, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 96.319999999999993 },
                    { new DateTime(2025, 5, 24, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 38.420000000000002 },
                    { new DateTime(2025, 5, 25, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 44.880000000000003 },
                    { new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 61.509999999999998 },
                    { new DateTime(2025, 5, 27, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 48.390000000000001 },
                    { new DateTime(2025, 5, 30, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 83.599999999999994 },
                    { new DateTime(2025, 5, 30, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 42.060000000000002 },
                    { new DateTime(2025, 5, 31, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 58.0 },
                    { new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 33.920000000000002 },
                    { new DateTime(2025, 6, 2, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 120.31 },
                    { new DateTime(2025, 6, 3, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 58.130000000000003 },
                    { new DateTime(2025, 6, 7, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 53.75 },
                    { new DateTime(2025, 6, 9, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 98.450000000000003 },
                    { new DateTime(2025, 6, 9, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 131.58000000000001 },
                    { new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 62.189999999999998 },
                    { new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 37.520000000000003 },
                    { new DateTime(2025, 6, 11, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 59.909999999999997 },
                    { new DateTime(2025, 6, 14, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 73.219999999999999 },
                    { new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 70.170000000000002 },
                    { new DateTime(2025, 6, 17, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 49.380000000000003 },
                    { new DateTime(2025, 6, 18, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 96.819999999999993 },
                    { new DateTime(2025, 6, 18, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 45.490000000000002 },
                    { new DateTime(2025, 6, 19, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 86.0 },
                    { new DateTime(2025, 6, 21, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 45.390000000000001 },
                    { new DateTime(2025, 6, 21, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 98.090000000000003 },
                    { new DateTime(2025, 6, 23, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 54.409999999999997 },
                    { new DateTime(2025, 6, 23, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 92.420000000000002 },
                    { new DateTime(2025, 6, 24, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 49.280000000000001 },
                    { new DateTime(2025, 6, 25, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 47.259999999999998 },
                    { new DateTime(2025, 6, 26, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 66.109999999999999 },
                    { new DateTime(2025, 6, 27, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 93.900000000000006 },
                    { new DateTime(2025, 6, 30, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 31.93 },
                    { new DateTime(2025, 7, 2, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 74.170000000000002 },
                    { new DateTime(2025, 7, 2, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 106.91 },
                    { new DateTime(2025, 7, 4, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 32.149999999999999 },
                    { new DateTime(2025, 7, 5, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 36.909999999999997 },
                    { new DateTime(2025, 7, 6, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 58.049999999999997 },
                    { new DateTime(2025, 7, 8, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 95.569999999999993 },
                    { new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 30.600000000000001 },
                    { new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 41.670000000000002 },
                    { new DateTime(2025, 7, 11, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 85.799999999999997 },
                    { new DateTime(2025, 7, 11, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 54.479999999999997 },
                    { new DateTime(2025, 7, 11, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 124.84999999999999 },
                    { new DateTime(2025, 7, 13, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 57.409999999999997 },
                    { new DateTime(2025, 7, 14, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 96.25 },
                    { new DateTime(2025, 7, 14, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 30.890000000000001 },
                    { new DateTime(2025, 7, 14, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 94.379999999999995 },
                    { new DateTime(2025, 7, 16, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 77.810000000000002 },
                    { new DateTime(2025, 7, 18, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 30.510000000000002 },
                    { new DateTime(2025, 7, 19, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 121.25 },
                    { new DateTime(2025, 7, 20, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 67.010000000000005 },
                    { new DateTime(2025, 7, 20, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 38.350000000000001 },
                    { new DateTime(2025, 7, 20, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 75.140000000000001 },
                    { new DateTime(2025, 7, 21, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 75.799999999999997 },
                    { new DateTime(2025, 7, 21, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 59.020000000000003 },
                    { new DateTime(2025, 7, 22, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 81.5 },
                    { new DateTime(2025, 7, 22, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 47.630000000000003 },
                    { new DateTime(2025, 7, 23, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 53.170000000000002 },
                    { new DateTime(2025, 7, 25, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 42.609999999999999 },
                    { new DateTime(2025, 7, 26, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 53.670000000000002 },
                    { new DateTime(2025, 7, 28, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 59.189999999999998 },
                    { new DateTime(2025, 7, 29, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 61.850000000000001 },
                    { new DateTime(2025, 7, 29, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 37.659999999999997 },
                    { new DateTime(2025, 7, 30, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 33.969999999999999 },
                    { new DateTime(2025, 7, 31, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 57.549999999999997 },
                    { new DateTime(2025, 8, 2, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 56.490000000000002 },
                    { new DateTime(2025, 8, 2, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 109.37 },
                    { new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 90.879999999999995 },
                    { new DateTime(2025, 8, 5, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 71.140000000000001 },
                    { new DateTime(2025, 8, 5, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 45.420000000000002 },
                    { new DateTime(2025, 8, 8, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 76.989999999999995 },
                    { new DateTime(2025, 8, 8, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 81.040000000000006 },
                    { new DateTime(2025, 8, 9, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 62.119999999999997 },
                    { new DateTime(2025, 8, 9, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 121.13 },
                    { new DateTime(2025, 8, 10, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 69.819999999999993 },
                    { new DateTime(2025, 8, 11, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 35.850000000000001 },
                    { new DateTime(2025, 8, 15, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 39.310000000000002 },
                    { new DateTime(2025, 8, 16, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 77.310000000000002 },
                    { new DateTime(2025, 8, 16, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 76.790000000000006 },
                    { new DateTime(2025, 8, 17, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 138.03999999999999 },
                    { new DateTime(2025, 8, 18, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 87.5 },
                    { new DateTime(2025, 8, 19, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 98.079999999999998 },
                    { new DateTime(2025, 8, 20, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 59.240000000000002 },
                    { new DateTime(2025, 8, 20, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 46.780000000000001 },
                    { new DateTime(2025, 8, 21, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 89.359999999999999 },
                    { new DateTime(2025, 8, 21, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 49.670000000000002 },
                    { new DateTime(2025, 8, 21, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 127.37 },
                    { new DateTime(2025, 8, 22, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 81.400000000000006 },
                    { new DateTime(2025, 8, 22, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 121.98999999999999 },
                    { new DateTime(2025, 8, 25, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 56.340000000000003 },
                    { new DateTime(2025, 8, 27, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 103.7 },
                    { new DateTime(2025, 8, 28, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 91.439999999999998 },
                    { new DateTime(2025, 8, 28, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 103.04000000000001 },
                    { new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 88.680000000000007 },
                    { new DateTime(2025, 8, 31, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 98.159999999999997 },
                    { new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 83.719999999999999 },
                    { new DateTime(2025, 9, 2, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 87.349999999999994 },
                    { new DateTime(2025, 9, 2, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 56.630000000000003 },
                    { new DateTime(2025, 9, 3, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 112.2 },
                    { new DateTime(2025, 9, 5, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 84.079999999999998 },
                    { new DateTime(2025, 9, 5, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 110.84999999999999 },
                    { new DateTime(2025, 9, 6, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 85.200000000000003 },
                    { new DateTime(2025, 9, 7, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 55.359999999999999 },
                    { new DateTime(2025, 9, 7, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 118.73999999999999 },
                    { new DateTime(2025, 9, 8, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 59.859999999999999 },
                    { new DateTime(2025, 9, 9, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 93.459999999999994 },
                    { new DateTime(2025, 9, 9, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 48.32 },
                    { new DateTime(2025, 9, 9, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 101.45999999999999 },
                    { new DateTime(2025, 9, 10, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 30.050000000000001 },
                    { new DateTime(2025, 9, 11, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 99.400000000000006 },
                    { new DateTime(2025, 9, 12, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 70.120000000000005 },
                    { new DateTime(2025, 9, 13, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 34.710000000000001 },
                    { new DateTime(2025, 9, 15, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 32.740000000000002 },
                    { new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 138.31999999999999 },
                    { new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 81.810000000000002 },
                    { new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 31.879999999999999 },
                    { new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 95.670000000000002 },
                    { new DateTime(2025, 9, 19, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 51.979999999999997 },
                    { new DateTime(2025, 9, 19, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 92.510000000000005 },
                    { new DateTime(2025, 9, 20, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 60.950000000000003 },
                    { new DateTime(2025, 9, 22, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 135.44 },
                    { new DateTime(2025, 9, 23, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 75.489999999999995 },
                    { new DateTime(2025, 9, 27, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 67.5 },
                    { new DateTime(2025, 9, 27, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 128.66 },
                    { new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 51.079999999999998 },
                    { new DateTime(2025, 9, 30, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 43.810000000000002 },
                    { new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 91.909999999999997 },
                    { new DateTime(2025, 10, 4, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 71.650000000000006 },
                    { new DateTime(2025, 10, 4, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 92.780000000000001 },
                    { new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 50.799999999999997 },
                    { new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 59.969999999999999 },
                    { new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 99.530000000000001 },
                    { new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 123.45 },
                    { new DateTime(2025, 10, 7, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 88.810000000000002 },
                    { new DateTime(2025, 10, 8, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 48.719999999999999 },
                    { new DateTime(2025, 10, 10, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 92.120000000000005 },
                    { new DateTime(2025, 10, 12, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 50.75 },
                    { new DateTime(2025, 10, 13, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 54.619999999999997 },
                    { new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 46.719999999999999 },
                    { new DateTime(2025, 10, 16, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 121.87 },
                    { new DateTime(2025, 10, 17, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 55.990000000000002 },
                    { new DateTime(2025, 10, 17, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 72.870000000000005 },
                    { new DateTime(2025, 10, 18, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 39.75 },
                    { new DateTime(2025, 10, 18, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 123.73 },
                    { new DateTime(2025, 10, 19, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 61.479999999999997 },
                    { new DateTime(2025, 10, 19, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 53.600000000000001 },
                    { new DateTime(2025, 10, 20, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 45.600000000000001 },
                    { new DateTime(2025, 10, 20, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 118.95 },
                    { new DateTime(2025, 10, 21, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 54.890000000000001 },
                    { new DateTime(2025, 10, 22, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 46.240000000000002 },
                    { new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 39.439999999999998 },
                    { new DateTime(2025, 10, 28, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 37.5 },
                    { new DateTime(2025, 11, 1, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 40.119999999999997 },
                    { new DateTime(2025, 11, 4, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 97.129999999999995 },
                    { new DateTime(2025, 11, 5, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 117.31999999999999 },
                    { new DateTime(2025, 11, 6, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 46.049999999999997 },
                    { new DateTime(2025, 11, 9, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 62.5 },
                    { new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 43.159999999999997 },
                    { new DateTime(2025, 11, 12, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 33.579999999999998 },
                    { new DateTime(2025, 11, 12, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 129.53999999999999 },
                    { new DateTime(2025, 11, 14, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 46.869999999999997 },
                    { new DateTime(2025, 11, 15, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 95.150000000000006 },
                    { new DateTime(2025, 11, 15, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 51.07 },
                    { new DateTime(2025, 11, 18, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 78.25 },
                    { new DateTime(2025, 11, 18, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 103.22 },
                    { new DateTime(2025, 11, 19, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 97.439999999999998 },
                    { new DateTime(2025, 11, 19, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 123.03 },
                    { new DateTime(2025, 11, 21, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 33.670000000000002 },
                    { new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 89.590000000000003 },
                    { new DateTime(2025, 11, 24, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 58.270000000000003 },
                    { new DateTime(2025, 11, 25, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 53.979999999999997 },
                    { new DateTime(2025, 11, 26, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 77.269999999999996 },
                    { new DateTime(2025, 11, 26, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 37.670000000000002 },
                    { new DateTime(2025, 11, 26, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 107.79000000000001 },
                    { new DateTime(2025, 11, 27, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 95.730000000000004 },
                    { new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 69.400000000000006 },
                    { new DateTime(2025, 12, 2, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 76.959999999999994 },
                    { new DateTime(2025, 12, 2, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 54.700000000000003 },
                    { new DateTime(2025, 12, 3, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 99.489999999999995 },
                    { new DateTime(2025, 12, 4, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 74.420000000000002 },
                    { new DateTime(2025, 12, 4, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 58.549999999999997 },
                    { new DateTime(2025, 12, 5, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 95.870000000000005 },
                    { new DateTime(2025, 12, 10, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 55.329999999999998 },
                    { new DateTime(2025, 12, 14, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 93.079999999999998 },
                    { new DateTime(2025, 12, 14, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 139.75 },
                    { new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 41.729999999999997 },
                    { new DateTime(2025, 12, 18, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 69.819999999999993 },
                    { new DateTime(2025, 12, 21, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 96.209999999999994 },
                    { new DateTime(2025, 12, 23, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 90.030000000000001 },
                    { new DateTime(2025, 12, 24, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 63.380000000000003 },
                    { new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 61.479999999999997 },
                    { new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 53.979999999999997 },
                    { new DateTime(2025, 12, 27, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 57.270000000000003 },
                    { new DateTime(2025, 12, 29, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 74.75 },
                    { new DateTime(2025, 12, 29, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 73.849999999999994 },
                    { new DateTime(2025, 12, 30, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 58.189999999999998 },
                    { new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 44.909999999999997 },
                    { new DateTime(2026, 1, 2, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 126.51000000000001 },
                    { new DateTime(2026, 1, 3, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 35.640000000000001 },
                    { new DateTime(2026, 1, 3, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 129.69999999999999 },
                    { new DateTime(2026, 1, 4, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 80.909999999999997 },
                    { new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 48.460000000000001 },
                    { new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 89.200000000000003 },
                    { new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 47.670000000000002 },
                    { new DateTime(2026, 1, 7, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 57.240000000000002 },
                    { new DateTime(2026, 1, 8, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 59.119999999999997 },
                    { new DateTime(2026, 1, 9, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 61.75 },
                    { new DateTime(2026, 1, 9, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 52.700000000000003 },
                    { new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 53.670000000000002 },
                    { new DateTime(2026, 1, 11, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 88.260000000000005 },
                    { new DateTime(2026, 1, 11, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 129.25 },
                    { new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 80.159999999999997 },
                    { new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 77.969999999999999 },
                    { new DateTime(2026, 1, 16, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 90.609999999999999 },
                    { new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 55.93 },
                    { new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 90.099999999999994 },
                    { new DateTime(2026, 1, 19, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 63.310000000000002 },
                    { new DateTime(2026, 1, 19, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 41.32 },
                    { new DateTime(2026, 1, 21, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 125.20999999999999 },
                    { new DateTime(2026, 1, 23, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 70.040000000000006 },
                    { new DateTime(2026, 1, 24, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 59.859999999999999 },
                    { new DateTime(2026, 1, 25, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 54.369999999999997 },
                    { new DateTime(2026, 1, 26, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 44.420000000000002 },
                    { new DateTime(2026, 1, 28, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 34.57 },
                    { new DateTime(2026, 1, 30, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 71.629999999999995 },
                    { new DateTime(2026, 1, 31, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 50.079999999999998 },
                    { new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 31.02 },
                    { new DateTime(2026, 2, 2, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 96.310000000000002 },
                    { new DateTime(2026, 2, 2, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 45.520000000000003 },
                    { new DateTime(2026, 2, 3, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 90.280000000000001 },
                    { new DateTime(2026, 2, 5, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 61.18 },
                    { new DateTime(2026, 2, 6, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 62.009999999999998 },
                    { new DateTime(2026, 2, 7, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 132.56999999999999 },
                    { new DateTime(2026, 2, 8, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 53.270000000000003 },
                    { new DateTime(2026, 2, 10, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 92.620000000000005 },
                    { new DateTime(2026, 2, 11, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 60.700000000000003 },
                    { new DateTime(2026, 2, 11, 0, 0, 0, 0, DateTimeKind.Local), "FDLCK", 37.210000000000001 },
                    { new DateTime(2026, 2, 12, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 95.359999999999999 },
                    { new DateTime(2026, 2, 12, 0, 0, 0, 0, DateTimeKind.Local), "TNDMR", 131.53999999999999 },
                    { new DateTime(2026, 2, 13, 0, 0, 0, 0, DateTimeKind.Local), "CRKHL", 74.870000000000005 }
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
