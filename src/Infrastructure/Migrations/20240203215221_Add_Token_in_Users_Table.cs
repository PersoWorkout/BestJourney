using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Token_in_Users_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Journeys",
                keyColumn: "Id",
                keyValue: new Guid("422e75cc-5581-41ac-a886-a5a8acc6309b"));

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Journeys",
                columns: new[] { "Id", "City", "Country", "CreatedAt", "Description", "IsActive", "Name", "Price", "UpdatedAt" },
                values: new object[] { new Guid("2ad03f9d-c658-4729-86c8-6a3a45864505"), "Istanbul", "Turkey", new DateTime(2024, 2, 3, 22, 52, 20, 921, DateTimeKind.Local).AddTicks(5697), "Welcome to Istanbul! Visit and discover the history of the most beautiful city in the world", true, "Discover Istanbul", 650m, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Journeys",
                keyColumn: "Id",
                keyValue: new Guid("2ad03f9d-c658-4729-86c8-6a3a45864505"));

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Journeys",
                columns: new[] { "Id", "City", "Country", "CreatedAt", "Description", "IsActive", "Name", "Price", "UpdatedAt" },
                values: new object[] { new Guid("422e75cc-5581-41ac-a886-a5a8acc6309b"), "Istanbul", "Turkey", new DateTime(2024, 1, 30, 23, 56, 2, 606, DateTimeKind.Local).AddTicks(5145), "Welcome to Istanbul! Visit and discover the history of the most beautiful city in the world", true, "Discover Istanbul", 650m, null });
        }
    }
}
