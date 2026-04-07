using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoGuard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexToAssets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Assets_Symbol",
                table: "Assets",
                column: "Symbol",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Assets_Symbol",
                table: "Assets");
        }
    }
}
