using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lion.AbpPro.Migrations
{
    /// <inheritdoc />
    public partial class _103 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bytes",
                table: "AbpFileObjects");

            migrationBuilder.DropColumn(
                name: "FileExtension",
                table: "AbpFileObjects");

            migrationBuilder.DropColumn(
                name: "ProviderKey",
                table: "AbpFileObjects");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Bytes",
                table: "AbpFileObjects",
                type: "longblob",
                nullable: true,
                comment: "二进制数据");

            migrationBuilder.AddColumn<string>(
                name: "FileExtension",
                table: "AbpFileObjects",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "",
                comment: "文件扩展名")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ProviderKey",
                table: "AbpFileObjects",
                type: "varchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "",
                comment: "文件Provider")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
