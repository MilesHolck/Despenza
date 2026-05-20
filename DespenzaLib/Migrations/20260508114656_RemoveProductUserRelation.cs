using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DespenzaLib.Migrations
{
    /// <inheritdoc />
    public partial class RemoveProductUserRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wares_Users_Product_UserId",
                table: "Wares");

            migrationBuilder.AddForeignKey(
                name: "FK_Wares_Users_Product_UserId",
                table: "Wares",
                column: "Product_UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                 name: "FK_Wares_Users_Product_UserId",
                 table: "Wares");

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
