using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DespenzaLib.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "Email", "Id", "Name", "Password", "Phone", "Role", "UserType" },
                values: new object[] { 1, "", "Admin@Despenza.dk", 0, "Admin", "1234", 0, "Admin", "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);
        }
    }
}
