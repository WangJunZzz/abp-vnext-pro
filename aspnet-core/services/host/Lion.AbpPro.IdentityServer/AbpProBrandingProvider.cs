namespace Lion.AbpPro
{
    [Dependency(ReplaceServices = true)]
    public class AbpProBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "AbpPro";
    }
}
