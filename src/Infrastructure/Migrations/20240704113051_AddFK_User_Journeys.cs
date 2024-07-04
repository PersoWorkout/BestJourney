using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFK_User_Journeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "Journeys",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CompanyName", "CreatedAt", "Email", "Firstname", "Lastname", "Password", "Phone", "Role", "UpdatedAt", "WebsiteName", "WebsiteUrl" },
                values: new object[] { new Guid("b1d75552-cba7-4057-b689-ba46ce6a2064"), null, new DateTime(2024, 7, 4, 13, 30, 49, 419, DateTimeKind.Local).AddTicks(6393), "yasin.karakus@example.com", "Yasin", "Karakus", "$2a$11$1RBCh9ZEiW5EsihXcYSoNeWxblafJrEyuWNkor8.htJOMlW/wPvNe", "0606060606", 0, null, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email_Role",
                table: "Users",
                columns: new[] { "Email", "Role" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Journeys_CreatorId",
                table: "Journeys",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Journeys_Users_CreatorId",
                table: "Journeys",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journeys_Users_CreatorId",
                table: "Journeys");

            migrationBuilder.DropIndex(
                name: "IX_Users_Email_Role",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Journeys_CreatorId",
                table: "Journeys");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b1d75552-cba7-4057-b689-ba46ce6a2064"));

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Journeys");

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
    }
}
