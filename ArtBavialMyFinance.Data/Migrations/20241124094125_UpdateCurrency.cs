using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtBavialMyFinance.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCurrency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodeCurrency",
                table: "Currencies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsBaseCurrency",
                table: "Currencies",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeCurrency",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "IsBaseCurrency",
                table: "Currencies");
        }
    }
}
