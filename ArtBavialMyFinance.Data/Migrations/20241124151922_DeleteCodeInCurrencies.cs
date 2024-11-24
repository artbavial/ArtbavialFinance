using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtBavialMyFinance.Data.Migrations
{
    /// <inheritdoc />
    public partial class DeleteCodeInCurrencies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Currencies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Currencies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
