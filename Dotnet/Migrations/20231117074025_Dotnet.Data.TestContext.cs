using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Dotnet.Migrations
{
    /// <inheritdoc />
    public partial class DotnetDataTestContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Test",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false),
                    Message = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Test",
                columns: new[] { "Id", "Message", "Role" },
                values: new object[,]
                {
                    { new Guid("68fb1957-95aa-4fc2-a892-0076b3901aa8"), "Goodbye!", "User" },
                    { new Guid("b6a5c921-4645-4156-9d29-260b4aad13ea"), "Hello Person!", "Assistant" },
                    { new Guid("bad3985d-b424-4525-a853-b57ee23dd338"), "Hello World!", "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Test");
        }
    }
}
