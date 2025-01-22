using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneLearn.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_foreign_key_to_Passage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "RolePrivileges",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Passages_langauge_id",
                table: "Passages",
                column: "langauge_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Passages_Language_langauge_id",
                table: "Passages",
                column: "langauge_id",
                principalTable: "Language",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Passages_Language_langauge_id",
                table: "Passages");

            migrationBuilder.DropIndex(
                name: "IX_Passages_langauge_id",
                table: "Passages");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "RolePrivileges",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
