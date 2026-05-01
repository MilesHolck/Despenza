using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DespenzaLib.Migrations
{
    /// <inheritdoc />
    public partial class RecipeScale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Antal",
                table: "Recipes",
                newName: "QuantityOfProduct");

            migrationBuilder.AddColumn<double>(
                name: "RecipeScale",
                table: "Recipes",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecipeScale",
                table: "Recipes");

            migrationBuilder.RenameColumn(
                name: "QuantityOfProduct",
                table: "Recipes",
                newName: "Antal");
        }
    }
}
