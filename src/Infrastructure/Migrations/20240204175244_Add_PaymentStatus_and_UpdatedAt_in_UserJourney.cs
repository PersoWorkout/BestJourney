using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_PaymentStatus_and_UpdatedAt_in_UserJourney : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Journeys",
                keyColumn: "Id",
                keyValue: new Guid("2ad03f9d-c658-4729-86c8-6a3a45864505"));

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "UsersJourneys",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "UsersJourneys",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Journeys",
                columns: new[] { "Id", "City", "Country", "CreatedAt", "Description", "IsActive", "Name", "Price", "UpdatedAt" },
                values: new object[] { new Guid("fb103731-e84e-4822-94de-11a6806680a6"), "Istanbul", "Turkey", new DateTime(2024, 2, 4, 18, 52, 43, 715, DateTimeKind.Local).AddTicks(7901), "Welcome to Istanbul! Visit and discover the history of the most beautiful city in the world", true, "Discover Istanbul", 650m, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Journeys",
                keyColumn: "Id",
                keyValue: new Guid("fb103731-e84e-4822-94de-11a6806680a6"));

            migrationBuilder.DropColumn(
                name: "Status",
                table: "UsersJourneys");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "UsersJourneys");

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
    }
}
