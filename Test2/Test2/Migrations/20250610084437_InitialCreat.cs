using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test2.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Author",
                columns: new[] { "IdAuthor", "FirstName", "LastName" },
                values: new object[] { 1, "Ana", "Brown" });

            migrationBuilder.InsertData(
                table: "Genre",
                columns: new[] { "IdGenre", "Name" },
                values: new object[] { 1, "Horror" });

            migrationBuilder.InsertData(
                table: "PublishingHouse",
                columns: new[] { "IdPublishingHouse", "City", "Country", "Name" },
                values: new object[] { 1, "Warsaw", "Poland", "gra" });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "IdBook", "IdPublishingHouse", "Name", "ReleaseDate" },
                values: new object[] { 1, 1, "Book", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "BookAuthor",
                columns: new[] { "IdAuthor", "IdBook" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "BookGenre",
                columns: new[] { "IdBook", "IdGenre" },
                values: new object[] { 1, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BookAuthor",
                keyColumns: new[] { "IdAuthor", "IdBook" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "BookGenre",
                keyColumns: new[] { "IdBook", "IdGenre" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Author",
                keyColumn: "IdAuthor",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "IdBook",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "IdGenre",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PublishingHouse",
                keyColumn: "IdPublishingHouse",
                keyValue: 1);
        }
    }
}
