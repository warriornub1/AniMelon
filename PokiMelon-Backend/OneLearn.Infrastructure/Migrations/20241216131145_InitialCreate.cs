using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneLearn.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    a = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    b = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    c = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    d = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    e = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    f = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    g = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    h = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    i = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    j = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    k = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    l = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    m = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    n = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    created_on = table.Column<DateTime>(type: "datetime2", nullable: false),
                    last_modified_on = table.Column<DateTime>(type: "datetime2", nullable: true),
                    last_modified_by = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tests");
        }
    }
}
