namespace Lion.AbpPro.NotificationManagement
{
    public static class NotificationManagementErrorCodes
    {
        public const string ReceiverNotNull =NotificationManagementConsts.NameSpace+ ":100001";
        public const string MessageNotExist =NotificationManagementConsts.NameSpace+ ":100002";
        public const string MessageTypeUnknown =NotificationManagementConsts.NameSpace+ ":100003";
        public const string UserUnSubscription =NotificationManagementConsts.NameSpace+ ":100004";
    }
}
