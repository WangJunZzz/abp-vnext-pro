using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lion.AbpPro.Migrations
{
    /// <inheritdoc />
    public partial class RemoveLanguageIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AbpLanguages_CultureName",
                table: "AbpLanguages");

            migrationBuilder.CreateIndex(
                name: "IX_AbpLanguages_CultureName",
                table: "AbpLanguages",
                column: "CultureName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AbpLanguages_CultureName",
                table: "AbpLanguages");

            migrationBuilder.CreateIndex(
                name: "IX_AbpLanguages_CultureName",
                table: "AbpLanguages",
                column: "CultureName",
                unique: true);
        }
    }
}
