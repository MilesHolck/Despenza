using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DespenzaLib.Migrations
{
    /// <inheritdoc />
    public partial class DateTimeRecipe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateSaved",
                table: "Recipes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateSaved",
                table: "Recipes");
        }
    }
}
