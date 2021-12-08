using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Microsoft.AspNetCore.Builder;


namespace Lion.AbpPro.Localization
{
    public static class LocalizationHelper
    {
        private static IServiceProvider ServiceProvider { get; set; }

        private static IAbpLazyServiceProvider _lazyServiceProvider;
        private static IAbpLazyServiceProvider LazyServiceProvider
        {
            get
            {
                return _lazyServiceProvider ??= ServiceProvider.GetRequiredService<IAbpLazyServiceProvider>();
            }
        }

        private static IStringLocalizerFactory StringLocalizerFactory => LazyServiceProvider.LazyGetRequiredService<IStringLocalizerFactory>();

        private static IStringLocalizer _localizer;

        private static Type _localizationResource = typeof(AbpProResource);

        public static IStringLocalizer L => _localizer ??= CreateLocalizer();

        public static Type LocalizationResource
        {
            get => _localizationResource;
            set
            {
                _localizationResource = value;
                _localizer = null;
            }
        }

        private static IStringLocalizer CreateLocalizer()
        {
            if (LocalizationResource != null)
            {
                return StringLocalizerFactory.Create(LocalizationResource);
            }

            return StringLocalizerFactory.CreateDefaultOrNull() ??
                   throw new AbpException(message: "Localizer is null");
        }

     
        public static void InitializeLocalization(this IApplicationBuilder app)
        {
            ServiceProvider = app.ApplicationServices;
        }
        
        
        public static void InitializeLocalization(this IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}