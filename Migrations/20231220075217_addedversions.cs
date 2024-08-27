using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wales_Online_Bank.Migrations
{
    /// <inheritdoc />
    public partial class addedversions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StatusOfAccount",
                table: "AspNetUsers",
                newName: "Version");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Version",
                table: "AspNetUsers",
                newName: "StatusOfAccount");
        }
    }
}
