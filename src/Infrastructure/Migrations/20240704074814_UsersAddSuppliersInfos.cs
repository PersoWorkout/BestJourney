using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UsersAddSuppliersInfos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "WebsiteName",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebsiteUrl",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Journeys",
                columns: new[] { "Id", "City", "Country", "CreatedAt", "Description", "IsActive", "Name", "Price", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("d1b864fe-67b6-4002-8799-36acf286aaf0"), "Alanya", "Turkey", new DateTime(2024, 7, 4, 9, 48, 11, 93, DateTimeKind.Local).AddTicks(7924), "Welcome to Alanya! Visit and discover the different parts of this beatiful country from the beach to the forest without forgetting the ancient ruins.", true, "Travel to Alanya", 400m, null },
                    { new Guid("fff14995-9819-4226-b03d-6d1d4bce7a33"), "Istanbul", "Turkey", new DateTime(2024, 7, 4, 9, 48, 11, 93, DateTimeKind.Local).AddTicks(7838), "Welcome to Istanbul! Visit and discover the history of the most beautiful city in the world", true, "Discover Istanbul", 650m, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CompanyName", "CreatedAt", "Email", "Firstname", "Lastname", "Password", "Phone", "Role", "UpdatedAt", "WebsiteName", "WebsiteUrl" },
                values: new object[] { new Guid("561114a4-7af4-4a60-81de-77f95fd51ada"), null, new DateTime(2024, 7, 4, 9, 48, 11, 310, DateTimeKind.Local).AddTicks(1652), "yasin.karakus@example.com", "Yasin", "Karakus", "$2a$11$0eJ2RezEyCU9CBizHSJTKenOyNFcBs7hEf0yInl4BPgB5Um1.Fizu", "0606060606", 0, null, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Journeys",
                keyColumn: "Id",
                keyValue: new Guid("d1b864fe-67b6-4002-8799-36acf286aaf0"));

            migrationBuilder.DeleteData(
                table: "Journeys",
                keyColumn: "Id",
                keyValue: new Guid("fff14995-9819-4226-b03d-6d1d4bce7a33"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("561114a4-7af4-4a60-81de-77f95fd51ada"));

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "WebsiteName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "WebsiteUrl",
                table: "Users");

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
    }
}
