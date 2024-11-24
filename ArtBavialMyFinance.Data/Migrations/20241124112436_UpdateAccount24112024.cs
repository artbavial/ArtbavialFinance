using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtBavialMyFinance.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAccount24112024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPrimaryAccount",
                table: "Accounts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPrimaryAccount",
                table: "Accounts");
        }
    }
}
