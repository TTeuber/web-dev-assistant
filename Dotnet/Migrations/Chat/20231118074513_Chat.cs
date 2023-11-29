using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Dotnet.Migrations.Chat
{
    /// <inheritdoc />
    public partial class Chat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ChatId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_Chat_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Chat",
                columns: new[] { "Id", "Title" },
                values: new object[] { new Guid("ed49ef44-a87e-4c90-a4d1-223910a05d9b"), "New Chat" });

            migrationBuilder.InsertData(
                table: "Message",
                columns: new[] { "Id", "ChatId", "Content", "Role" },
                values: new object[,]
                {
                    { new Guid("1e074283-1eda-443b-b304-e74de7f0b162"), new Guid("ed49ef44-a87e-4c90-a4d1-223910a05d9b"), "Hello Person!", "assistant" },
                    { new Guid("7e5f5e01-616b-4359-b3da-a27987ac4d19"), new Guid("ed49ef44-a87e-4c90-a4d1-223910a05d9b"), "Goodbye!", "user" },
                    { new Guid("97e3edd3-0a58-47d3-9084-34fb8f7e5aee"), new Guid("ed49ef44-a87e-4c90-a4d1-223910a05d9b"), "Hello World!", "user" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Message_ChatId",
                table: "Message",
                column: "ChatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "Chat");
        }
    }
}
