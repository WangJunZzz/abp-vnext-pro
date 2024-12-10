using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lion.AbpPro.Migrations
{
    /// <inheritdoc />
    public partial class _102 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpEntityModelProperties_AbpEntityModels_EntityModelId",
                table: "AbpEntityModelProperties");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpEnumTypeProperty_AbpEnumType_EnumTypeId",
                table: "AbpEnumTypeProperty");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpTemplateDetails_AbpTemplates_TemplateId",
                table: "AbpTemplateDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpTemplates",
                table: "AbpTemplates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpTemplateDetails",
                table: "AbpTemplateDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpProjects",
                table: "AbpProjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpEnumTypeProperty",
                table: "AbpEnumTypeProperty");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpEnumType",
                table: "AbpEnumType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpEntityModels",
                table: "AbpEntityModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpEntityModelProperties",
                table: "AbpEntityModelProperties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpDataType",
                table: "AbpDataType");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AbpProjects");

            migrationBuilder.RenameTable(
                name: "AbpTemplates",
                newName: "AbpProTemplates");

            migrationBuilder.RenameTable(
                name: "AbpTemplateDetails",
                newName: "AbpProTemplateDetails");

            migrationBuilder.RenameTable(
                name: "AbpProjects",
                newName: "AbpProProjects");

            migrationBuilder.RenameTable(
                name: "AbpEnumTypeProperty",
                newName: "AbpProEnumTypeProperty");

            migrationBuilder.RenameTable(
                name: "AbpEnumType",
                newName: "AbpProEnumType");

            migrationBuilder.RenameTable(
                name: "AbpEntityModels",
                newName: "AbpProEntityModels");

            migrationBuilder.RenameTable(
                name: "AbpEntityModelProperties",
                newName: "AbpProEntityModelProperties");

            migrationBuilder.RenameTable(
                name: "AbpDataType",
                newName: "AbpProDataType");

            migrationBuilder.RenameIndex(
                name: "IX_AbpTemplateDetails_TemplateId",
                table: "AbpProTemplateDetails",
                newName: "IX_AbpProTemplateDetails_TemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpEnumTypeProperty_EnumTypeId",
                table: "AbpProEnumTypeProperty",
                newName: "IX_AbpProEnumTypeProperty_EnumTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpEnumTypeProperty_Code",
                table: "AbpProEnumTypeProperty",
                newName: "IX_AbpProEnumTypeProperty_Code");

            migrationBuilder.RenameIndex(
                name: "IX_AbpEnumType_Code",
                table: "AbpProEnumType",
                newName: "IX_AbpProEnumType_Code");

            migrationBuilder.RenameIndex(
                name: "IX_AbpEntityModels_Code",
                table: "AbpProEntityModels",
                newName: "IX_AbpProEntityModels_Code");

            migrationBuilder.RenameIndex(
                name: "IX_AbpEntityModelProperties_EntityModelId",
                table: "AbpProEntityModelProperties",
                newName: "IX_AbpProEntityModelProperties_EntityModelId");

            migrationBuilder.AddColumn<bool>(
                name: "AllowAdd",
                table: "AbpProEntityModelProperties",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AllowEdit",
                table: "AbpProEntityModelProperties",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AllowSearch",
                table: "AbpProEntityModelProperties",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpProTemplates",
                table: "AbpProTemplates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpProTemplateDetails",
                table: "AbpProTemplateDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpProProjects",
                table: "AbpProProjects",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpProEnumTypeProperty",
                table: "AbpProEnumTypeProperty",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpProEnumType",
                table: "AbpProEnumType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpProEntityModels",
                table: "AbpProEntityModels",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpProEntityModelProperties",
                table: "AbpProEntityModelProperties",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpProDataType",
                table: "AbpProDataType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpProEntityModelProperties_AbpProEntityModels_EntityModelId",
                table: "AbpProEntityModelProperties",
                column: "EntityModelId",
                principalTable: "AbpProEntityModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AbpProEnumTypeProperty_AbpProEnumType_EnumTypeId",
                table: "AbpProEnumTypeProperty",
                column: "EnumTypeId",
                principalTable: "AbpProEnumType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AbpProTemplateDetails_AbpProTemplates_TemplateId",
                table: "AbpProTemplateDetails",
                column: "TemplateId",
                principalTable: "AbpProTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpProEntityModelProperties_AbpProEntityModels_EntityModelId",
                table: "AbpProEntityModelProperties");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpProEnumTypeProperty_AbpProEnumType_EnumTypeId",
                table: "AbpProEnumTypeProperty");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpProTemplateDetails_AbpProTemplates_TemplateId",
                table: "AbpProTemplateDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpProTemplates",
                table: "AbpProTemplates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpProTemplateDetails",
                table: "AbpProTemplateDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpProProjects",
                table: "AbpProProjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpProEnumTypeProperty",
                table: "AbpProEnumTypeProperty");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpProEnumType",
                table: "AbpProEnumType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpProEntityModels",
                table: "AbpProEntityModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpProEntityModelProperties",
                table: "AbpProEntityModelProperties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpProDataType",
                table: "AbpProDataType");

            migrationBuilder.DropColumn(
                name: "AllowAdd",
                table: "AbpProEntityModelProperties");

            migrationBuilder.DropColumn(
                name: "AllowEdit",
                table: "AbpProEntityModelProperties");

            migrationBuilder.DropColumn(
                name: "AllowSearch",
                table: "AbpProEntityModelProperties");

            migrationBuilder.RenameTable(
                name: "AbpProTemplates",
                newName: "AbpTemplates");

            migrationBuilder.RenameTable(
                name: "AbpProTemplateDetails",
                newName: "AbpTemplateDetails");

            migrationBuilder.RenameTable(
                name: "AbpProProjects",
                newName: "AbpProjects");

            migrationBuilder.RenameTable(
                name: "AbpProEnumTypeProperty",
                newName: "AbpEnumTypeProperty");

            migrationBuilder.RenameTable(
                name: "AbpProEnumType",
                newName: "AbpEnumType");

            migrationBuilder.RenameTable(
                name: "AbpProEntityModels",
                newName: "AbpEntityModels");

            migrationBuilder.RenameTable(
                name: "AbpProEntityModelProperties",
                newName: "AbpEntityModelProperties");

            migrationBuilder.RenameTable(
                name: "AbpProDataType",
                newName: "AbpDataType");

            migrationBuilder.RenameIndex(
                name: "IX_AbpProTemplateDetails_TemplateId",
                table: "AbpTemplateDetails",
                newName: "IX_AbpTemplateDetails_TemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpProEnumTypeProperty_EnumTypeId",
                table: "AbpEnumTypeProperty",
                newName: "IX_AbpEnumTypeProperty_EnumTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpProEnumTypeProperty_Code",
                table: "AbpEnumTypeProperty",
                newName: "IX_AbpEnumTypeProperty_Code");

            migrationBuilder.RenameIndex(
                name: "IX_AbpProEnumType_Code",
                table: "AbpEnumType",
                newName: "IX_AbpEnumType_Code");

            migrationBuilder.RenameIndex(
                name: "IX_AbpProEntityModels_Code",
                table: "AbpEntityModels",
                newName: "IX_AbpEntityModels_Code");

            migrationBuilder.RenameIndex(
                name: "IX_AbpProEntityModelProperties_EntityModelId",
                table: "AbpEntityModelProperties",
                newName: "IX_AbpEntityModelProperties_EntityModelId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AbpProjects",
                type: "varchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpTemplates",
                table: "AbpTemplates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpTemplateDetails",
                table: "AbpTemplateDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpProjects",
                table: "AbpProjects",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpEnumTypeProperty",
                table: "AbpEnumTypeProperty",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpEnumType",
                table: "AbpEnumType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpEntityModels",
                table: "AbpEntityModels",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpEntityModelProperties",
                table: "AbpEntityModelProperties",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpDataType",
                table: "AbpDataType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpEntityModelProperties_AbpEntityModels_EntityModelId",
                table: "AbpEntityModelProperties",
                column: "EntityModelId",
                principalTable: "AbpEntityModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AbpEnumTypeProperty_AbpEnumType_EnumTypeId",
                table: "AbpEnumTypeProperty",
                column: "EnumTypeId",
                principalTable: "AbpEnumType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AbpTemplateDetails_AbpTemplates_TemplateId",
                table: "AbpTemplateDetails",
                column: "TemplateId",
                principalTable: "AbpTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
