using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tour.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddSeeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "CountryName" },
                values: new object[,]
                {
                    { "1", "việt nam" },
                    { "2", "hàn quốc" },
                    { "3", "nhật bản" },
                    { "4", "thái lan" },
                    { "5", "singapor" }
                });

            migrationBuilder.InsertData(
                table: "Transport",
                columns: new[] { "Id", "TransportName" },
                values: new object[] { "1", "Xe Khách" });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "Id", "CityName", "CountryId" },
                values: new object[,]
                {
                    { "1", "tphcm", "1" },
                    { "2", "vũng tàu", "1" },
                    { "3", "tokyo", "3" },
                    { "4", "bangkok", "4" },
                    { "5", "seoul", "2" },
                    { "6", "busan", "2" }
                });

            migrationBuilder.InsertData(
                table: "Sight",
                columns: new[] { "Id", "CityId", "Picture", "SightForMoney", "SightName" },
                values: new object[] { "1", "1", null, 140000.0, "Đầm Sen" });

            migrationBuilder.InsertData(
                table: "Tour",
                columns: new[] { "Id", "CityId", "EndDate", "MaxTourists", "Name", "Price", "SightId", "StartDate", "TransportId" },
                values: new object[] { "1", "1", new DateTime(2023, 4, 16, 15, 58, 20, 373, DateTimeKind.Utc).AddTicks(3574), 50, "Du Lịch TPHCM", 1000000.0, "1", new DateTime(2023, 4, 16, 15, 58, 20, 373, DateTimeKind.Utc).AddTicks(3571), "1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "4");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "5");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "6");

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: "5");

            migrationBuilder.DeleteData(
                table: "Tour",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: "4");

            migrationBuilder.DeleteData(
                table: "Sight",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Transport",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "City",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: "1");
        }
    }
}
