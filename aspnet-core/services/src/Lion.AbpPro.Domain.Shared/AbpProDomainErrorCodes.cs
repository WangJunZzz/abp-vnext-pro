namespace Lion.AbpPro
{
    public static class AbpProDomainErrorCodes
    {
        public const string OrganizationUnitNotExist =AbpProDomainSharedConsts.NameSpace+ ":100001";
        public const string UserLockedOut =AbpProDomainSharedConsts.NameSpace+ ":100002";
        public const string UserOrPasswordMismatch =AbpProDomainSharedConsts.NameSpace+ ":100003";
        public const string ApiResourceNotExist =AbpProDomainSharedConsts.NameSpace+ ":100004";
        public const string ApiResourceExist =AbpProDomainSharedConsts.NameSpace+ ":100005";
        public const string ApiScopeNotExist =AbpProDomainSharedConsts.NameSpace+ ":100006";
        public const string ApiScopeExist =AbpProDomainSharedConsts.NameSpace+ ":100007";
        public const string ApiClientNotExist =AbpProDomainSharedConsts.NameSpace+ ":100008";
        public const string ApiClientExist =AbpProDomainSharedConsts.NameSpace+ ":100009";
        public const string IdentityResourceNotExist =AbpProDomainSharedConsts.NameSpace+ ":100010";
        public const string IdentityResourceExist =AbpProDomainSharedConsts.NameSpace+ ":100011";
    }
}
