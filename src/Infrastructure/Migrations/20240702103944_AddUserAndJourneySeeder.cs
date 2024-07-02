using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserAndJourneySeeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Journeys",
                keyColumn: "Id",
                keyValue: new Guid("9028a981-7fab-4038-8939-2931b2755840"));

            migrationBuilder.DeleteData(
                table: "Journeys",
                keyColumn: "Id",
                keyValue: new Guid("b8bc5c1f-7d32-49bb-8eba-b7c9506051aa"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("80b469e1-e11a-4b69-9565-4c42e64a79a1"));

            migrationBuilder.InsertData(
                table: "Journeys",
                columns: new[] { "Id", "City", "Country", "CreatedAt", "Description", "IsActive", "Name", "Price", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("5b4097df-ac52-4683-82ac-27435e0d2790"), "Istanbul", "Turkey", new DateTime(2024, 7, 2, 12, 39, 43, 370, DateTimeKind.Local).AddTicks(4483), "Welcome to Istanbul! Visit and discover the history of the most beautiful city in the world", true, "Discover Istanbul", 650m, null },
                    { new Guid("7e3b20eb-9b9e-401d-9bf6-9ce162b18828"), "Alanya", "Turkey", new DateTime(2024, 7, 2, 12, 39, 43, 370, DateTimeKind.Local).AddTicks(4552), "Welcome to Alanya! Visit and discover the different parts of this beatiful country from the beach to the forest without forgetting the ancient ruins.", true, "Travel to Alanya", 400m, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Firstname", "Lastname", "Password", "UpdatedAt" },
                values: new object[] { new Guid("73ff5a11-ebfc-4f09-a7d0-09902fd5f319"), new DateTime(2024, 7, 2, 12, 39, 43, 840, DateTimeKind.Local).AddTicks(3491), "yasin.karakus@example.com", "Yasin", "Karakus", "$2a$11$AefNbmF5rMuNxLtfwcyTWe.cCSMBaxYWbNBCiYRoTeNMx7lnfcgge", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Journeys",
                keyColumn: "Id",
                keyValue: new Guid("5b4097df-ac52-4683-82ac-27435e0d2790"));

            migrationBuilder.DeleteData(
                table: "Journeys",
                keyColumn: "Id",
                keyValue: new Guid("7e3b20eb-9b9e-401d-9bf6-9ce162b18828"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("73ff5a11-ebfc-4f09-a7d0-09902fd5f319"));

            migrationBuilder.InsertData(
                table: "Journeys",
                columns: new[] { "Id", "City", "Country", "CreatedAt", "Description", "IsActive", "Name", "Price", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("9028a981-7fab-4038-8939-2931b2755840"), "Alanya", "Turkey", new DateTime(2024, 7, 2, 12, 34, 9, 7, DateTimeKind.Local).AddTicks(1037), "Welcome to Alanya! Visit and discover the different parts of this beatiful country from the beach to the forest without forgetting the ancient ruins.", true, "Travel to Alanya", 400m, null },
                    { new Guid("b8bc5c1f-7d32-49bb-8eba-b7c9506051aa"), "Istanbul", "Turkey", new DateTime(2024, 7, 2, 12, 34, 9, 7, DateTimeKind.Local).AddTicks(978), "Welcome to Istanbul! Visit and discover the history of the most beautiful city in the world", true, "Discover Istanbul", 650m, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Firstname", "Lastname", "Password", "UpdatedAt" },
                values: new object[] { new Guid("80b469e1-e11a-4b69-9565-4c42e64a79a1"), new DateTime(2024, 7, 2, 12, 34, 9, 7, DateTimeKind.Local).AddTicks(1328), "yasin.karakus@example.com", "Yasin", "Karakus", "Password123!", null });
        }
    }
}
