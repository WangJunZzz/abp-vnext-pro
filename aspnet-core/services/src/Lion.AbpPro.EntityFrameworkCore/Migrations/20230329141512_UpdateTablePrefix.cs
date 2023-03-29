using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lion.AbpPro.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTablePrefix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataDictionaryDetail_DataDictionary_DataDictionaryId",
                table: "DataDictionaryDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationSubscription_Notification_NotificationId",
                table: "NotificationSubscription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationSubscription",
                table: "NotificationSubscription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notification",
                table: "Notification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataDictionaryDetail",
                table: "DataDictionaryDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataDictionary",
                table: "DataDictionary");

            migrationBuilder.RenameTable(
                name: "NotificationSubscription",
                newName: "AbpNotificationSubscriptions");

            migrationBuilder.RenameTable(
                name: "Notification",
                newName: "AbpNotifications");

            migrationBuilder.RenameTable(
                name: "DataDictionaryDetail",
                newName: "AbpDataDictionaryDetails");

            migrationBuilder.RenameTable(
                name: "DataDictionary",
                newName: "AbpDataDictionaries");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationSubscription_NotificationId",
                table: "AbpNotificationSubscriptions",
                newName: "IX_AbpNotificationSubscriptions_NotificationId");

            migrationBuilder.RenameIndex(
                name: "IX_DataDictionaryDetail_DataDictionaryId",
                table: "AbpDataDictionaryDetails",
                newName: "IX_AbpDataDictionaryDetails_DataDictionaryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpNotificationSubscriptions",
                table: "AbpNotificationSubscriptions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpNotifications",
                table: "AbpNotifications",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpDataDictionaryDetails",
                table: "AbpDataDictionaryDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpDataDictionaries",
                table: "AbpDataDictionaries",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AbpLanguages_CultureName",
                table: "AbpLanguages",
                column: "CultureName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AbpDataDictionaryDetails_AbpDataDictionaries_DataDictionaryId",
                table: "AbpDataDictionaryDetails",
                column: "DataDictionaryId",
                principalTable: "AbpDataDictionaries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AbpNotificationSubscriptions_AbpNotifications_NotificationId",
                table: "AbpNotificationSubscriptions",
                column: "NotificationId",
                principalTable: "AbpNotifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpDataDictionaryDetails_AbpDataDictionaries_DataDictionaryId",
                table: "AbpDataDictionaryDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpNotificationSubscriptions_AbpNotifications_NotificationId",
                table: "AbpNotificationSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_AbpLanguages_CultureName",
                table: "AbpLanguages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpNotificationSubscriptions",
                table: "AbpNotificationSubscriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpNotifications",
                table: "AbpNotifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpDataDictionaryDetails",
                table: "AbpDataDictionaryDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpDataDictionaries",
                table: "AbpDataDictionaries");

            migrationBuilder.RenameTable(
                name: "AbpNotificationSubscriptions",
                newName: "NotificationSubscription");

            migrationBuilder.RenameTable(
                name: "AbpNotifications",
                newName: "Notification");

            migrationBuilder.RenameTable(
                name: "AbpDataDictionaryDetails",
                newName: "DataDictionaryDetail");

            migrationBuilder.RenameTable(
                name: "AbpDataDictionaries",
                newName: "DataDictionary");

            migrationBuilder.RenameIndex(
                name: "IX_AbpNotificationSubscriptions_NotificationId",
                table: "NotificationSubscription",
                newName: "IX_NotificationSubscription_NotificationId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpDataDictionaryDetails_DataDictionaryId",
                table: "DataDictionaryDetail",
                newName: "IX_DataDictionaryDetail_DataDictionaryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationSubscription",
                table: "NotificationSubscription",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notification",
                table: "Notification",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataDictionaryDetail",
                table: "DataDictionaryDetail",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataDictionary",
                table: "DataDictionary",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataDictionaryDetail_DataDictionary_DataDictionaryId",
                table: "DataDictionaryDetail",
                column: "DataDictionaryId",
                principalTable: "DataDictionary",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationSubscription_Notification_NotificationId",
                table: "NotificationSubscription",
                column: "NotificationId",
                principalTable: "Notification",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
