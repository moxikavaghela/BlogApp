﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MoxikaBlogApp.Migrations
{
    /// <inheritdoc />
    public partial class SeedingInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "All about technology", "Technology" },
                    { 2, "All about Food-Recipies", "Food" },
                    { 3, "All about Consulting", "Consultancy" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Author", "CategoryId", "Content", "FeatureImagePath", "PublishedDate", "Title" },
                values: new object[,]
                {
                    { 1, "John Doe", 1, "Content of Tech Post 1", "tech_image.jpg", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tech Post 1" },
                    { 2, "Jane Doe", 2, "Content of Health Post 1", "health_image.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Health Post 1" },
                    { 3, "Alex Smith", 3, "Content of Lifestyle Post 1", "lifestyle_image.jpg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lifestyle Post 1" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
