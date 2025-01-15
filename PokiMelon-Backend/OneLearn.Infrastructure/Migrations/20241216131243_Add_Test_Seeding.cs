using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OneLearn.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Test_Seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tests",
                columns: new[] { "id", "a", "b", "c", "created_by", "created_on", "d", "e", "f", "g", "h", "i", "j", "k", "l", "last_modified_by", "last_modified_on", "m", "n" },
                values: new object[,]
                {
                    { 1, "a", "b", "c", "asd", new DateTime(2024, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "d", "e", "f", "g", "h", "i", "j", "k", "l", null, null, "m", "n" },
                    { 2, "a", "b", "c", "asd", new DateTime(2024, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "d", "e", "f", "g", "h", "i", "j", "k", "l", null, null, "m", "n" },
                    { 3, "a", "b", "c", "asd", new DateTime(2024, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "d", "e", "f", "g", "h", "i", "j", "k", "l", null, null, "m", "n" },
                    { 4, "a", "b", "c", "asd", new DateTime(2024, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "d", "e", "f", "g", "h", "i", "j", "k", "l", null, null, "m", "n" },
                    { 5, "a", "b", "c", "asd", new DateTime(2024, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "d", "e", "f", "g", "h", "i", "j", "k", "l", null, null, "m", "n" },
                    { 6, "a", "b", "c", "asd", new DateTime(2024, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "d", "e", "f", "g", "h", "i", "j", "k", "l", null, null, "m", "n" },
                    { 7, "a", "b", "c", "asd", new DateTime(2024, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "d", "e", "f", "g", "h", "i", "j", "k", "l", null, null, "m", "n" },
                    { 8, "a", "b", "c", "asd", new DateTime(2024, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "d", "e", "f", "g", "h", "i", "j", "k", "l", null, null, "m", "n" },
                    { 9, "a", "b", "c", "asd", new DateTime(2024, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "d", "e", "f", "g", "h", "i", "j", "k", "l", null, null, "m", "n" },
                    { 10, "a", "b", "c", "asd", new DateTime(2024, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "d", "e", "f", "g", "h", "i", "j", "k", "l", null, null, "m", "n" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tests",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tests",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tests",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tests",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tests",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tests",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Tests",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Tests",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Tests",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Tests",
                keyColumn: "id",
                keyValue: 10);
        }
    }
}
