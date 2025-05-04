using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoxikaBlogApp.Migrations
{
    /// <inheritdoc />
    public partial class SeedingInitialData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Author", "CategoryId", "Content", "FeatureImagePath", "PublishedDate", "Title" },
                values: new object[] { 4, "Kiana Mancholds", 3, "Content of Lifestyle Post 2", "lifestyle_image.jpg", new DateTime(2025, 5, 4, 15, 44, 48, 405, DateTimeKind.Local).AddTicks(8667), "Lifestyle Post 2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
