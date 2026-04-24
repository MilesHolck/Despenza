using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DespenzaLib.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeLine_Recipes_RecipeId",
                table: "RecipeLine");

            migrationBuilder.DropColumn(
                name: "SalePrice",
                table: "Wares");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "Product_UserId",
                table: "Wares",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Wares",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "RecipeId",
                table: "RecipeLine",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wares_Product_UserId",
                table: "Wares",
                column: "Product_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Wares_UserId",
                table: "Wares",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeLine_Recipes_RecipeId",
                table: "RecipeLine",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wares_Users_Product_UserId",
                table: "Wares",
                column: "Product_UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wares_Users_UserId",
                table: "Wares",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeLine_Recipes_RecipeId",
                table: "RecipeLine");

            migrationBuilder.DropForeignKey(
                name: "FK_Wares_Users_Product_UserId",
                table: "Wares");

            migrationBuilder.DropForeignKey(
                name: "FK_Wares_Users_UserId",
                table: "Wares");

            migrationBuilder.DropIndex(
                name: "IX_Wares_Product_UserId",
                table: "Wares");

            migrationBuilder.DropIndex(
                name: "IX_Wares_UserId",
                table: "Wares");

            migrationBuilder.DropColumn(
                name: "Product_UserId",
                table: "Wares");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Wares");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Users");

            migrationBuilder.AddColumn<decimal>(
                name: "SalePrice",
                table: "Wares",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Users",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "RecipeId",
                table: "RecipeLine",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeLine_Recipes_RecipeId",
                table: "RecipeLine",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id");
        }
    }
}
