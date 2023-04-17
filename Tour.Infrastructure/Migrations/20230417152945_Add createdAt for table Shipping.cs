using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tour.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddcreatedAtfortableShipping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "createdAt",
                table: "Shipping",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Tour",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 4, 17, 15, 29, 45, 669, DateTimeKind.Utc).AddTicks(6593), new DateTime(2023, 4, 17, 15, 29, 45, 669, DateTimeKind.Utc).AddTicks(6588) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "createdAt",
                table: "Shipping");

            migrationBuilder.UpdateData(
                table: "Tour",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 4, 17, 9, 2, 2, 916, DateTimeKind.Utc).AddTicks(4644), new DateTime(2023, 4, 17, 9, 2, 2, 916, DateTimeKind.Utc).AddTicks(4640) });
        }
    }
}
