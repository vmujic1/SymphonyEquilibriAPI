using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SymphonyEquilibriAPI.Migrations
{
    /// <inheritdoc />
    public partial class UserModelTypoFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswrodHash",
                table: "Users",
                newName: "PasswordHash");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Users",
                newName: "PasswrodHash");
        }
    }
}
