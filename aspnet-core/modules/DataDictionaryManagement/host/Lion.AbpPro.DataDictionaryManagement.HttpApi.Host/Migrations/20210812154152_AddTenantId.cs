using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lion.AbpPro.DataDictionaryManagement.Migrations
{
    public partial class AddTenantId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "DataDictionary",
                type: "char(36)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "DataDictionary");
        }
    }
}
