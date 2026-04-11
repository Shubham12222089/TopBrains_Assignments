using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapShop.AuthService.Migrations
{
    /// <inheritdoc />
    public partial class AdminAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FullName", "Password", "Role" },
                values: new object[] { 1, "admin@capshop.local", "Admin User", "6G94qKPK8LYNjnTllCqm2G3BUM08AzOK7yW30tfjrMc=", "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
