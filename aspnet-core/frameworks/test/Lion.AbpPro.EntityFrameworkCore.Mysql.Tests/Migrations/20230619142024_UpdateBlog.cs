using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lion.AbpPro.EntityFrameworkCore.Tests.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBlog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BlogType",
                table: "Blog",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsShow",
                table: "Blog",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Blog",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<float>(
                name: "PriceOne",
                table: "Blog",
                type: "float",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlogType",
                table: "Blog");

            migrationBuilder.DropColumn(
                name: "IsShow",
                table: "Blog");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Blog");

            migrationBuilder.DropColumn(
                name: "PriceOne",
                table: "Blog");
        }
    }
}
