namespace Lion.AbpPro
{
    public class AbpProApplicationAutoMapperProfile : Profile
    {
        public AbpProApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<IdentityUser, ExportIdentityUserOutput>()
                .ForMember(e => e.CreationTimeFormat, opt => opt.Ignore())
                .ForMember(e => e.Status, opt => opt.Ignore());
            CreateMap<OrganizationUnit, OrganizationUnitDto>();
            CreateMap<IdentityUser, GetOrganizationUnitUserOutput>();
            CreateMap<IdentityUser, GetUnAddUserOutput>();
            CreateMap<IdentityRole, GetOrganizationUnitRoleOutput>();
            CreateMap<IdentityRole, GetUnAddRoleOutput>();
        }
    }
}
