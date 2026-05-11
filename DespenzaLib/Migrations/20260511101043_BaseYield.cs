using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DespenzaLib.Migrations
{
    /// <inheritdoc />
    public partial class BaseYield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "BaseYield",
                table: "Recipes",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseYield",
                table: "Recipes");
        }
    }
}
