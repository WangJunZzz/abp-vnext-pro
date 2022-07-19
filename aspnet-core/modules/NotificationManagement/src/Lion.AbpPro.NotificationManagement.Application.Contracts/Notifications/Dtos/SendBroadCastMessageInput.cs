namespace Lion.AbpPro.NotificationManagement.Notifications.Dtos;

public class SendBroadCastMessageInput:IValidatableObject
{
    /// <summary>
    /// 消息标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 消息内容
    /// </summary>
    public string Content { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var localization = validationContext.GetRequiredService<IStringLocalizer<NotificationManagementResource>>();

        if (Title.IsNullOrWhiteSpace())
        {
            yield return new ValidationResult(localization[NotificationManagementErrorCodes.MessageTitle], new[] {nameof(Title)});
        }
        if (Content.IsNullOrWhiteSpace())
        {
            yield return new ValidationResult(localization[NotificationManagementErrorCodes.MessageContent], new[] {nameof(Content)});
        }
    }
}