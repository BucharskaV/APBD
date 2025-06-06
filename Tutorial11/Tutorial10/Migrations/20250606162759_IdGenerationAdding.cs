﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tutorial10.Migrations
{
    /// <inheritdoc />
    public partial class IdGenerationAdding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Login",
                table: "Users",
                newName: "Username");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "Login");
        }
    }
}
