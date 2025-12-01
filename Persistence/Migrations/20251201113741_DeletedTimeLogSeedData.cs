using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DeletedTimeLogSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TimeLogs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TimeLogs",
                keyColumn: "Id",
                keyValue: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TimeLogs",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "EmployeeId", "IsDeleted", "LogTime", "LogType", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2004, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 1, false, new DateTime(2025, 10, 18, 9, 0, 0, 0, DateTimeKind.Utc), 1, null },
                    { 2, new DateTime(2004, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 1, false, new DateTime(2025, 10, 18, 18, 0, 0, 0, DateTimeKind.Utc), 2, null }
                });
        }
    }
}
