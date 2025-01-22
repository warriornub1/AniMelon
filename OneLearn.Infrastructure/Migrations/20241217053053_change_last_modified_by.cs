using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneLearn.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class change_last_modified_by : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "last_modified_by",
                table: "Tests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "last_modified_by",
                table: "Passages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "last_modified_by",
                table: "Language",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Tests",
                keyColumn: "id",
                keyValue: 1,
                column: "last_modified_by",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tests",
                keyColumn: "id",
                keyValue: 2,
                column: "last_modified_by",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tests",
                keyColumn: "id",
                keyValue: 3,
                column: "last_modified_by",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tests",
                keyColumn: "id",
                keyValue: 4,
                column: "last_modified_by",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tests",
                keyColumn: "id",
                keyValue: 5,
                column: "last_modified_by",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tests",
                keyColumn: "id",
                keyValue: 6,
                column: "last_modified_by",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tests",
                keyColumn: "id",
                keyValue: 7,
                column: "last_modified_by",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tests",
                keyColumn: "id",
                keyValue: 8,
                column: "last_modified_by",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tests",
                keyColumn: "id",
                keyValue: 9,
                column: "last_modified_by",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tests",
                keyColumn: "id",
                keyValue: 10,
                column: "last_modified_by",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "last_modified_by",
                table: "Tests",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_modified_by",
                table: "Passages",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "last_modified_by",
                table: "Language",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Tests",
                keyColumn: "id",
                keyValue: 1,
                column: "last_modified_by",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tests",
                keyColumn: "id",
                keyValue: 2,
                column: "last_modified_by",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tests",
                keyColumn: "id",
                keyValue: 3,
                column: "last_modified_by",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tests",
                keyColumn: "id",
                keyValue: 4,
                column: "last_modified_by",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tests",
                keyColumn: "id",
                keyValue: 5,
                column: "last_modified_by",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tests",
                keyColumn: "id",
                keyValue: 6,
                column: "last_modified_by",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tests",
                keyColumn: "id",
                keyValue: 7,
                column: "last_modified_by",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tests",
                keyColumn: "id",
                keyValue: 8,
                column: "last_modified_by",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tests",
                keyColumn: "id",
                keyValue: 9,
                column: "last_modified_by",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tests",
                keyColumn: "id",
                keyValue: 10,
                column: "last_modified_by",
                value: null);
        }
    }
}
