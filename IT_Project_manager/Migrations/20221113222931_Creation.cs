using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ITProjectmanager.Migrations
{
    /// <inheritdoc />
    public partial class Creation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Surname = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "DateOfBirth", "Email", "Name", "Surname" },
                values: new object[,]
                {
                    { 5, new DateTime(1, 1, 1), "krzysiek.palonek@gmail.com", "Krzysztof", "Palonek" },
                    { 6, new DateTime(1, 1, 1), "marz.koł@gmail.com", "Marzena", "Kołodziej" },
                    { 7, new DateTime(1, 1, 1), "jan.kow@gmail.com", "Jan", "Kowalski" },
                    { 8, new DateTime(1, 1, 1), "Nat.uro@gmail.com", "Natalia", "Urodek" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Members");
        }
    }
}
