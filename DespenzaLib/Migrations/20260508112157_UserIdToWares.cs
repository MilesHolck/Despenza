using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DespenzaLib.Migrations
{
    /// <inheritdoc />
    public partial class UserIdToWares : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
            name: "FK_Wares_Users_Product_UserId",
                table: "Wares");

            migrationBuilder.DropIndex(
                name: "IX_Wares_Product_UserId",
                table: "Wares");

            migrationBuilder.DropColumn(
                name: "Product_UserId",
                table: "Wares");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Wares",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Wares",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Product_UserId",
                table: "Wares",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wares_Product_UserId",
                table: "Wares",
                column: "Product_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wares_Users_Product_UserId",
                table: "Wares",
                column: "Product_UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
