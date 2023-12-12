using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wales_Online_Bank.Migrations
{
    /// <inheritdoc />
    public partial class introducenewpptintransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TransactionDescription",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionDescription",
                table: "Transactions");
        }
    }
}
