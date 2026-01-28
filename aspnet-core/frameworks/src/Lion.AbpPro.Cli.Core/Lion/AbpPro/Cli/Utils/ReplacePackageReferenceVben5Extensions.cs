namespace Lion.AbpPro.Cli.Utils;

public static class ReplacePackageReferenceVben5Extensions
{
    public static string Vben5ReplacePackageReferenceCore(this string content)
    {
        return content
                .Replace("<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\frameworks\\src\\Lion.AbpPro.Core\\Lion.AbpPro.Core.csproj\"/>",
                    "<PackageReference Include=\"Lion.AbpPro.Core\"/>")
                .Replace("<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\frameworks\\src\\Lion.AbpPro.Core\\Lion.AbpPro.Core.csproj\"/>",
                    "<PackageReference Include=\"Lion.AbpPro.Core\"/>")
                .Replace("<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\shared\\Lion.AbpPro.Shared.Hosting.Microservices\\Lion.AbpPro.Shared.Hosting.Microservices.csproj\"/>",
                    "<PackageReference Include=\"Lion.AbpPro.Shared.Hosting.Microservices\"/>")
                .Replace("<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\shared\\Lion.AbpPro.Shared.Hosting.Gateways\\Lion.AbpPro.Shared.Hosting.Gateways.csproj\"/>",
                    "<PackageReference Include=\"Lion.AbpPro.Shared.Hosting.Gateways\"/>")
                .Replace("<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\frameworks\\src\\Lion.AbpPro.AspNetCore\\Lion.AbpPro.AspNetCore.csproj\"/>",
                    "<PackageReference Include=\"Lion.AbpPro.AspNetCore\"/>")
            ;
    }

    public static string Vben5ReplacePackageReferenceBasicManagement(this string content)
    {
        return content
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\BasicManagement\\src\\Lion.AbpPro.BasicManagement.Application\\Lion.AbpPro.BasicManagement.Application.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.BasicManagement.Application\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\BasicManagement\\src\\Lion.AbpPro.BasicManagement.Application.Contracts\\Lion.AbpPro.BasicManagement.Application.Contracts.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.BasicManagement.Application.Contracts\"/>")
            .Replace("<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\BasicManagement\\src\\Lion.AbpPro.BasicManagement.Domain\\Lion.AbpPro.BasicManagement.Domain.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.BasicManagement.Domain\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\BasicManagement\\src\\Lion.AbpPro.BasicManagement.Domain.Shared\\Lion.AbpPro.BasicManagement.Domain.Shared.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.BasicManagement.Domain.Shared\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\BasicManagement\\src\\Lion.AbpPro.BasicManagement.EntityFrameworkCore\\Lion.AbpPro.BasicManagement.EntityFrameworkCore.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.BasicManagement.EntityFrameworkCore\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\BasicManagement\\src\\Lion.AbpPro.BasicManagement.FreeSqlRepository\\Lion.AbpPro.BasicManagement.FreeSqlRepository.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FreeSqlRepository\"/>")
            .Replace("<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\BasicManagement\\src\\Lion.AbpPro.BasicManagement.HttpApi\\Lion.AbpPro.BasicManagement.HttpApi.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.BasicManagement.HttpApi\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\BasicManagement\\src\\Lion.AbpPro.BasicManagement.HttpApi.Client\\Lion.AbpPro.BasicManagement.HttpApi.Client.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.BasicManagement.HttpApi.Client\"/>");
    }

    public static string Vben5ReplacePackageReferenceDataDictionaryManagement(this string content)
    {
        return content
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\DataDictionaryManagement\\src\\Lion.AbpPro.DataDictionaryManagement.Application\\Lion.AbpPro.DataDictionaryManagement.Application.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.DataDictionaryManagement.Application\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\DataDictionaryManagement\\src\\Lion.AbpPro.DataDictionaryManagement.Application.Contracts\\Lion.AbpPro.DataDictionaryManagement.Application.Contracts.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.DataDictionaryManagement.Application.Contracts\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\DataDictionaryManagement\\src\\Lion.AbpPro.DataDictionaryManagement.Domain\\Lion.AbpPro.DataDictionaryManagement.Domain.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.DataDictionaryManagement.Domain\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\DataDictionaryManagement\\src\\Lion.AbpPro.DataDictionaryManagement.Domain.Shared\\Lion.AbpPro.DataDictionaryManagement.Domain.Shared.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.DataDictionaryManagement.Domain.Shared\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\DataDictionaryManagement\\src\\Lion.AbpPro.DataDictionaryManagement.EntityFrameworkCore\\Lion.AbpPro.DataDictionaryManagement.EntityFrameworkCore.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.DataDictionaryManagement.EntityFrameworkCore\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\DataDictionaryManagement\\src\\Lion.AbpPro.DataDictionaryManagement.FreeSqlRepository\\Lion.AbpPro.DataDictionaryManagement.FreeSqlRepository.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FreeSqlRepository\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\DataDictionaryManagement\\src\\Lion.AbpPro.DataDictionaryManagement.HttpApi\\Lion.AbpPro.DataDictionaryManagement.HttpApi.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.DataDictionaryManagement.HttpApi\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\DataDictionaryManagement\\src\\Lion.AbpPro.DataDictionaryManagement.HttpApi.Client\\Lion.AbpPro.DataDictionaryManagement.HttpApi.Client.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.DataDictionaryManagement.HttpApi.Client\"/>");
    }

    public static string Vben5ReplacePackageReferenceFileManagement(this string content)
    {
        return content
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\FileManagement\\src\\Lion.AbpPro.FileManagement.Application\\Lion.AbpPro.FileManagement.Application.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FileManagement.Application\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\FileManagement\\src\\Lion.AbpPro.FileManagement.Application.Contracts\\Lion.AbpPro.FileManagement.Application.Contracts.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FileManagement.Application.Contracts\"/>")
            .Replace("<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\FileManagement\\src\\Lion.AbpPro.FileManagement.Domain\\Lion.AbpPro.FileManagement.Domain.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FileManagement.Domain\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\FileManagement\\src\\Lion.AbpPro.FileManagement.Domain.Shared\\Lion.AbpPro.FileManagement.Domain.Shared.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FileManagement.Domain.Shared\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\FileManagement\\src\\Lion.AbpPro.FileManagement.EntityFrameworkCore\\Lion.AbpPro.FileManagement.EntityFrameworkCore.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FileManagement.EntityFrameworkCore\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\FileManagement\\src\\Lion.AbpPro.FileManagement.FreeSqlRepository\\Lion.AbpPro.FileManagement.FreeSqlRepository.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FreeSqlRepository\"/>")
            .Replace("<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\FileManagement\\src\\Lion.AbpPro.FileManagement.HttpApi\\Lion.AbpPro.FileManagement.HttpApi.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FileManagement.HttpApi\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\FileManagement\\src\\Lion.AbpPro.FileManagement.HttpApi.Client\\Lion.AbpPro.FileManagement.HttpApi.Client.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FileManagement.HttpApi.Client\"/>");
    }

    public static string Vben5ReplacePackageReferenceLanguageManagement(this string content)
    {
        return content
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\LanguageManagement\\src\\Lion.AbpPro.LanguageManagement.Application\\Lion.AbpPro.LanguageManagement.Application.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.LanguageManagement.Application\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\LanguageManagement\\src\\Lion.AbpPro.LanguageManagement.Application.Contracts\\Lion.AbpPro.LanguageManagement.Application.Contracts.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.LanguageManagement.Application.Contracts\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\LanguageManagement\\src\\Lion.AbpPro.LanguageManagement.Domain\\Lion.AbpPro.LanguageManagement.Domain.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.LanguageManagement.Domain\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\LanguageManagement\\src\\Lion.AbpPro.LanguageManagement.Domain.Shared\\Lion.AbpPro.LanguageManagement.Domain.Shared.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.LanguageManagement.Domain.Shared\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\LanguageManagement\\src\\Lion.AbpPro.LanguageManagement.EntityFrameworkCore\\Lion.AbpPro.LanguageManagement.EntityFrameworkCore.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.LanguageManagement.EntityFrameworkCore\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\LanguageManagement\\src\\Lion.AbpPro.LanguageManagement.FreeSqlRepository\\Lion.AbpPro.LanguageManagement.FreeSqlRepository.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FreeSqlRepository\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\LanguageManagement\\src\\Lion.AbpPro.LanguageManagement.HttpApi\\Lion.AbpPro.LanguageManagement.HttpApi.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.LanguageManagement.HttpApi\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\LanguageManagement\\src\\Lion.AbpPro.LanguageManagement.HttpApi.Client\\Lion.AbpPro.LanguageManagement.HttpApi.Client.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.LanguageManagement.HttpApi.Client\"/>");
    }

    public static string Vben5ReplacePackageReferenceNotificationManagement(this string content)
    {
        return content
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\NotificationManagement\\src\\Lion.AbpPro.NotificationManagement.Application\\Lion.AbpPro.NotificationManagement.Application.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.NotificationManagement.Application\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\NotificationManagement\\src\\Lion.AbpPro.NotificationManagement.Application.Contracts\\Lion.AbpPro.NotificationManagement.Application.Contracts.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.NotificationManagement.Application.Contracts\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\NotificationManagement\\src\\Lion.AbpPro.NotificationManagement.Domain\\Lion.AbpPro.NotificationManagement.Domain.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.NotificationManagement.Domain\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\NotificationManagement\\src\\Lion.AbpPro.NotificationManagement.Domain.Shared\\Lion.AbpPro.NotificationManagement.Domain.Shared.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.NotificationManagement.Domain.Shared\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\NotificationManagement\\src\\Lion.AbpPro.NotificationManagement.EntityFrameworkCore\\Lion.AbpPro.NotificationManagement.EntityFrameworkCore.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.NotificationManagement.EntityFrameworkCore\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\NotificationManagement\\src\\Lion.AbpPro.NotificationManagement.FreeSqlRepository\\Lion.AbpPro.NotificationManagement.FreeSqlRepository.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FreeSqlRepository\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\NotificationManagement\\src\\Lion.AbpPro.NotificationManagement.HttpApi\\Lion.AbpPro.NotificationManagement.HttpApi.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.NotificationManagement.HttpApi\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\NotificationManagement\\src\\Lion.AbpPro.NotificationManagement.HttpApi.Client\\Lion.AbpPro.NotificationManagement.HttpApi.Client.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.NotificationManagement.HttpApi.Client\"/>");
    }

    public static string Vben5ReplacePackageReferenceCodeManagement(this string content)
    {
        return content
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\CodeManagement\\src\\Lion.AbpPro.CodeManagement.Application\\Lion.AbpPro.CodeManagement.Application.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.CodeManagement.Application\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\CodeManagement\\src\\Lion.AbpPro.CodeManagement.Application.Contracts\\Lion.AbpPro.CodeManagement.Application.Contracts.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.CodeManagement.Application.Contracts\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\CodeManagement\\src\\Lion.AbpPro.CodeManagement.Domain\\Lion.AbpPro.CodeManagement.Domain.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.CodeManagement.Domain\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\CodeManagement\\src\\Lion.AbpPro.CodeManagement.Domain.Shared\\Lion.AbpPro.CodeManagement.Domain.Shared.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.CodeManagement.Domain.Shared\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\CodeManagement\\src\\Lion.AbpPro.CodeManagement.EntityFrameworkCore\\Lion.AbpPro.CodeManagement.EntityFrameworkCore.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.CodeManagement.EntityFrameworkCore\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\CodeManagement\\src\\Lion.AbpPro.CodeManagement.FreeSqlRepository\\Lion.AbpPro.CodeManagement.FreeSqlRepository.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FreeSqlRepository\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\CodeManagement\\src\\Lion.AbpPro.CodeManagement.HttpApi\\Lion.AbpPro.CodeManagement.HttpApi.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.CodeManagement.HttpApi\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\CodeManagement\\src\\Lion.AbpPro.CodeManagement.HttpApi.Client\\Lion.AbpPro.CodeManagement.HttpApi.Client.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.CodeManagement.HttpApi.Client\"/>");
    }

    public static string Vben5ReplacePackageReferenceTemplateManagement(this string content)
    {
        return content
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\TemplateManagement\\src\\Lion.AbpPro.TemplateManagement.Application\\Lion.AbpPro.TemplateManagement.Application.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.TemplateManagement.Application\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\TemplateManagement\\src\\Lion.AbpPro.TemplateManagement.Application.Contracts\\Lion.AbpPro.TemplateManagement.Application.Contracts.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.TemplateManagement.Application.Contracts\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\TemplateManagement\\src\\Lion.AbpPro.TemplateManagement.Domain\\Lion.AbpPro.TemplateManagement.Domain.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.TemplateManagement.Domain\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\TemplateManagement\\src\\Lion.AbpPro.TemplateManagement.Domain.Shared\\Lion.AbpPro.TemplateManagement.Domain.Shared.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.TemplateManagement.Domain.Shared\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\TemplateManagement\\src\\Lion.AbpPro.TemplateManagement.EntityFrameworkCore\\Lion.AbpPro.TemplateManagement.EntityFrameworkCore.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.TemplateManagement.EntityFrameworkCore\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\TemplateManagement\\src\\Lion.AbpPro.TemplateManagement.FreeSqlRepository\\Lion.AbpPro.TemplateManagement.FreeSqlRepository.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FreeSqlRepository\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\TemplateManagement\\src\\Lion.AbpPro.TemplateManagement.HttpApi\\Lion.AbpPro.TemplateManagement.HttpApi.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.TemplateManagement.HttpApi\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\TemplateManagement\\src\\Lion.AbpPro.TemplateManagement.HttpApi.Client\\Lion.AbpPro.TemplateManagement.HttpApi.Client.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.TemplateManagement.HttpApi.Client\"/>");
    }

    public static string Vben5ReplacePackageReferenceDynamicMenuManagement(this string content)
    {
        return content
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\DynamicMenuManagement\\src\\Lion.AbpPro.DynamicMenuManagement.Application\\Lion.AbpPro.DynamicMenuManagement.Application.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.DynamicMenuManagement.Application\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\DynamicMenuManagement\\src\\Lion.AbpPro.DynamicMenuManagement.Application.Contracts\\Lion.AbpPro.DynamicMenuManagement.Application.Contracts.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.DynamicMenuManagement.Application.Contracts\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\DynamicMenuManagement\\src\\Lion.AbpPro.DynamicMenuManagement.Domain\\Lion.AbpPro.DynamicMenuManagement.Domain.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.DynamicMenuManagement.Domain\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\DynamicMenuManagement\\src\\Lion.AbpPro.DynamicMenuManagement.Domain.Shared\\Lion.AbpPro.DynamicMenuManagement.Domain.Shared.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.DynamicMenuManagement.Domain.Shared\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\DynamicMenuManagement\\src\\Lion.AbpPro.DynamicMenuManagement.EntityFrameworkCore\\Lion.AbpPro.DynamicMenuManagement.EntityFrameworkCore.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.DynamicMenuManagement.EntityFrameworkCore\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\DynamicMenuManagement\\src\\Lion.AbpPro.DynamicMenuManagement.FreeSqlRepository\\Lion.AbpPro.DynamicMenuManagement.FreeSqlRepository.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FreeSqlRepository\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\DynamicMenuManagement\\src\\Lion.AbpPro.DynamicMenuManagement.HttpApi\\Lion.AbpPro.DynamicMenuManagement.HttpApi.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.DynamicMenuManagement.HttpApi\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\DynamicMenuManagement\\src\\Lion.AbpPro.DynamicMenuManagement.HttpApi.Client\\Lion.AbpPro.DynamicMenuManagement.HttpApi.Client.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.DynamicMenuManagement.HttpApi.Client\"/>");
    }

    public static string Vben5ReplacePackageReferenceImportExportManagement(this string content)
    {
        return content
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\ImportExportManagement\\src\\Lion.AbpPro.ImportExportManagement.Application\\Lion.AbpPro.ImportExportManagement.Application.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.ImportExportManagement.Application\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\ImportExportManagement\\src\\Lion.AbpPro.ImportExportManagement.Application.Contracts\\Lion.AbpPro.ImportExportManagement.Application.Contracts.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.ImportExportManagement.Application.Contracts\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\ImportExportManagement\\src\\Lion.AbpPro.ImportExportManagement.Domain\\Lion.AbpPro.ImportExportManagement.Domain.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.ImportExportManagement.Domain\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\ImportExportManagement\\src\\Lion.AbpPro.ImportExportManagement.Domain.Shared\\Lion.AbpPro.ImportExportManagement.Domain.Shared.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.ImportExportManagement.Domain.Shared\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\ImportExportManagement\\src\\Lion.AbpPro.ImportExportManagement.EntityFrameworkCore\\Lion.AbpPro.ImportExportManagement.EntityFrameworkCore.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.ImportExportManagement.EntityFrameworkCore\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\ImportExportManagement\\src\\Lion.AbpPro.ImportExportManagement.FreeSqlRepository\\Lion.AbpPro.ImportExportManagement.FreeSqlRepository.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FreeSqlRepository\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\ImportExportManagement\\src\\Lion.AbpPro.ImportExportManagement.HttpApi\\Lion.AbpPro.ImportExportManagement.HttpApi.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.ImportExportManagement.HttpApi\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\ImportExportManagement\\src\\Lion.AbpPro.ImportExportManagement.HttpApi.Client\\Lion.AbpPro.ImportExportManagement.HttpApi.Client.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.ImportExportManagement.HttpApi.Client\"/>");
    }

    public static string Vben5ReplacePackageReferenceCacheManagement(this string content)
    {
        return content
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\CacheManagement\\src\\Lion.AbpPro.CacheManagement.Application\\Lion.AbpPro.CacheManagement.Application.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.CacheManagement.Application\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\CacheManagement\\src\\Lion.AbpPro.CacheManagement.Application.Contracts\\Lion.AbpPro.CacheManagement.Application.Contracts.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.CacheManagement.Application.Contracts\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\CacheManagement\\src\\Lion.AbpPro.CacheManagement.Domain\\Lion.AbpPro.CacheManagement.Domain.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.CacheManagement.Domain\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\CacheManagement\\src\\Lion.AbpPro.CacheManagement.Domain.Shared\\Lion.AbpPro.CacheManagement.Domain.Shared.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.CacheManagement.Domain.Shared\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\CacheManagement\\src\\Lion.AbpPro.CacheManagement.EntityFrameworkCore\\Lion.AbpPro.CacheManagement.EntityFrameworkCore.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.CacheManagement.EntityFrameworkCore\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\CacheManagement\\src\\Lion.AbpPro.CacheManagement.FreeSqlRepository\\Lion.AbpPro.CacheManagement.FreeSqlRepository.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FreeSqlRepository\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\CacheManagement\\src\\Lion.AbpPro.CacheManagement.HttpApi\\Lion.AbpPro.CacheManagement.HttpApi.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.CacheManagement.HttpApi\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\CacheManagement\\src\\Lion.AbpPro.CacheManagement.HttpApi.Client\\Lion.AbpPro.CacheManagement.HttpApi.Client.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.CacheManagement.HttpApi.Client\"/>");
    }

    public static string Vben5ReplacePackageReferenceMasterDataManagement(this string content)
    {
        return content
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\MasterDataManagement\\src\\Lion.AbpPro.MasterDataManagement.Application\\Lion.AbpPro.MasterDataManagement.Application.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.MasterDataManagement.Application\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\MasterDataManagement\\src\\Lion.AbpPro.MasterDataManagement.Application.Contracts\\Lion.AbpPro.MasterDataManagement.Application.Contracts.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.MasterDataManagement.Application.Contracts\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\MasterDataManagement\\src\\Lion.AbpPro.MasterDataManagement.Domain\\Lion.AbpPro.MasterDataManagement.Domain.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.MasterDataManagement.Domain\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\MasterDataManagement\\src\\Lion.AbpPro.MasterDataManagement.Domain.Shared\\Lion.AbpPro.MasterDataManagement.Domain.Shared.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.MasterDataManagement.Domain.Shared\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\MasterDataManagement\\src\\Lion.AbpPro.MasterDataManagement.EntityFrameworkCore\\Lion.AbpPro.MasterDataManagement.EntityFrameworkCore.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.MasterDataManagement.EntityFrameworkCore\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\MasterDataManagement\\src\\Lion.AbpPro.MasterDataManagement.FreeSqlRepository\\Lion.AbpPro.MasterDataManagement.FreeSqlRepository.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FreeSqlRepository\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\MasterDataManagement\\src\\Lion.AbpPro.MasterDataManagement.HttpApi\\Lion.AbpPro.MasterDataManagement.HttpApi.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.MasterDataManagement.HttpApi\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\modules\\MasterDataManagement\\src\\Lion.AbpPro.MasterDataManagement.HttpApi.Client\\Lion.AbpPro.MasterDataManagement.HttpApi.Client.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.MasterDataManagement.HttpApi.Client\"/>");
    }

    public static string Vben5ReplaceLionPackageVersion(this string context, string version)
    {
        return context.Replace("MyVersion", version);
    }
}