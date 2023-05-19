using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lion.AbpPro.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLanguageManagementFlagIconIsNotRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FlagIcon",
                table: "AbpLanguages",
                type: "varchar(128)",
                maxLength: 128,
                nullable: true,
                comment: "图标",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldComment: "图标")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AbpLanguages",
                keyColumn: "FlagIcon",
                keyValue: null,
                column: "FlagIcon",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "FlagIcon",
                table: "AbpLanguages",
                type: "longtext",
                nullable: false,
                comment: "图标",
                oldClrType: typeof(string),
                oldType: "varchar(128)",
                oldMaxLength: 128,
                oldNullable: true,
                oldComment: "图标")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
