using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniLiveBank.Api.Migrations
{
    /// <inheritdoc />
    public partial class Added_seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Advisors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Alice Johnson" },
                    { 2, "Bob Brown" }
                });

            migrationBuilder.InsertData(
                table: "Sessions",
                columns: new[] { "Id", "AcceptedAt", "AdvisorId", "CreatedAt", "CustomerName", "EndedAt", "Status" },
                values: new object[,]
                {
                    { 1, null, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "John Doe", null, "Waiting" },
                    { 2, null, null, new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Jane Smith", null, "Waiting" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Advisors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Advisors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sessions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sessions",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
