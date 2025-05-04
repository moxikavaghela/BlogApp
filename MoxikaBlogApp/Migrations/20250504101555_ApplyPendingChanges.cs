using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoxikaBlogApp.Migrations
{
    /// <inheritdoc />
    public partial class ApplyPendingChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 4,
                column: "PublishedDate",
                value: new DateTime(2025, 5, 4, 15, 45, 55, 223, DateTimeKind.Local).AddTicks(5731));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 4,
                column: "PublishedDate",
                value: new DateTime(2025, 5, 4, 15, 44, 48, 405, DateTimeKind.Local).AddTicks(8667));
        }
    }
}
