using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUsersAndJourneysSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Journeys",
                keyColumn: "Id",
                keyValue: new Guid("1044bb21-b38c-4937-af6c-15d1ecb19495"));

            migrationBuilder.DeleteData(
                table: "Journeys",
                keyColumn: "Id",
                keyValue: new Guid("e219dfc4-2200-494c-9d04-0f883efeff50"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("de4d787a-92ec-450a-822b-aa686699c535"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7ed93e45-5c14-421e-9a81-ac946a3e1e34"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CompanyName", "CreatedAt", "Email", "Firstname", "Lastname", "Password", "Phone", "Role", "UpdatedAt", "WebsiteName", "WebsiteUrl" },
                values: new object[,]
                {
                    { new Guid("88d43f6f-309d-4c8a-a95a-7ba3ec8f92da"), "Trivago", new DateTime(2024, 7, 4, 15, 42, 38, 574, DateTimeKind.Local).AddTicks(2806), "supplier@example.com", "John", "Doe", "$2a$11$iSHHD/NUGOUdtgEOrcOyQu4TmMzfGVpXoRsLvtF0DMSVA5shlRvTu", "0602020202", 1, null, "Trivago.fr", "https://www.trivago.fr" },
                    { new Guid("b9217759-7274-477b-8772-284803c86a34"), null, new DateTime(2024, 7, 4, 15, 42, 38, 574, DateTimeKind.Local).AddTicks(2692), "customer@example.com", "Tom", "Doe", "$2a$11$iSHHD/NUGOUdtgEOrcOyQu4TmMzfGVpXoRsLvtF0DMSVA5shlRvTu", "0601010101", 0, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Journeys",
                columns: new[] { "Id", "City", "Country", "CreatedAt", "CreatorId", "Description", "IsActive", "Name", "Price", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("bbe9ad9a-d9dd-4025-9a2d-6c383e705364"), "Alanya", "Turkey", new DateTime(2024, 7, 4, 15, 42, 38, 574, DateTimeKind.Local).AddTicks(4785), new Guid("88d43f6f-309d-4c8a-a95a-7ba3ec8f92da"), "Welcome to Alanya! Visit and discover the different parts of this beatiful country from the beach to the forest without forgetting the ancient ruins.", true, "Travel to Alanya", 400m, null },
                    { new Guid("d543e25e-6341-4fa9-834f-36b6d2f1ea7f"), "Istanbul", "Turkey", new DateTime(2024, 7, 4, 15, 42, 38, 574, DateTimeKind.Local).AddTicks(4732), new Guid("88d43f6f-309d-4c8a-a95a-7ba3ec8f92da"), "Welcome to Istanbul! Visit and discover the history of the most beautiful city in the world", true, "Discover Istanbul", 650m, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Journeys",
                keyColumn: "Id",
                keyValue: new Guid("bbe9ad9a-d9dd-4025-9a2d-6c383e705364"));

            migrationBuilder.DeleteData(
                table: "Journeys",
                keyColumn: "Id",
                keyValue: new Guid("d543e25e-6341-4fa9-834f-36b6d2f1ea7f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b9217759-7274-477b-8772-284803c86a34"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("88d43f6f-309d-4c8a-a95a-7ba3ec8f92da"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CompanyName", "CreatedAt", "Email", "Firstname", "Lastname", "Password", "Phone", "Role", "UpdatedAt", "WebsiteName", "WebsiteUrl" },
                values: new object[,]
                {
                    { new Guid("7ed93e45-5c14-421e-9a81-ac946a3e1e34"), "Trivago", new DateTime(2024, 7, 4, 15, 38, 31, 26, DateTimeKind.Local).AddTicks(1994), "supplier@example.com", "John", "Doe", "Password123!", "0602020202", 1, null, "Trivago.fr", "https://www.trivago.fr" },
                    { new Guid("de4d787a-92ec-450a-822b-aa686699c535"), null, new DateTime(2024, 7, 4, 15, 38, 31, 26, DateTimeKind.Local).AddTicks(1911), "customer@example.com", "Tom", "Doe", "Password123!", "0601010101", 0, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Journeys",
                columns: new[] { "Id", "City", "Country", "CreatedAt", "CreatorId", "Description", "IsActive", "Name", "Price", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("1044bb21-b38c-4937-af6c-15d1ecb19495"), "Istanbul", "Turkey", new DateTime(2024, 7, 4, 15, 38, 31, 26, DateTimeKind.Local).AddTicks(2714), new Guid("7ed93e45-5c14-421e-9a81-ac946a3e1e34"), "Welcome to Istanbul! Visit and discover the history of the most beautiful city in the world", true, "Discover Istanbul", 650m, null },
                    { new Guid("e219dfc4-2200-494c-9d04-0f883efeff50"), "Alanya", "Turkey", new DateTime(2024, 7, 4, 15, 38, 31, 26, DateTimeKind.Local).AddTicks(2737), new Guid("7ed93e45-5c14-421e-9a81-ac946a3e1e34"), "Welcome to Alanya! Visit and discover the different parts of this beatiful country from the beach to the forest without forgetting the ancient ruins.", true, "Travel to Alanya", 400m, null }
                });
        }
    }
}
