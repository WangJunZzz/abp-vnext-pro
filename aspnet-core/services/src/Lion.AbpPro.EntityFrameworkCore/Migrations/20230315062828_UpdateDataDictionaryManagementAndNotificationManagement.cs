using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lion.AbpPro.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDataDictionaryManagementAndNotificationManagement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationSubscription_Notification_NotificationId",
                table: "NotificationSubscription");

            migrationBuilder.AlterColumn<Guid>(
                name: "NotificationId",
                table: "NotificationSubscription",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationSubscription_Notification_NotificationId",
                table: "NotificationSubscription",
                column: "NotificationId",
                principalTable: "Notification",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationSubscription_Notification_NotificationId",
                table: "NotificationSubscription");

            migrationBuilder.AlterColumn<Guid>(
                name: "NotificationId",
                table: "NotificationSubscription",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationSubscription_Notification_NotificationId",
                table: "NotificationSubscription",
                column: "NotificationId",
                principalTable: "Notification",
                principalColumn: "Id");
        }
    }
}
