namespace Lion.AbpPro.Cli.Utils;

public static class ReplacePackageReferenceExtensions
{
    public static string ReplacePackageReferenceCore(this string content)
    {
        return content
                .Replace("<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\frameworks\\src\\Lion.AbpPro.Core\\Lion.AbpPro.Core.csproj\"/>",
                    "<PackageReference Include=\"Lion.AbpPro.Core\"/>")
                .Replace("<ProjectReference Include=\"..\\..\\..\\..\\aspnet-core\\frameworks\\src\\Lion.AbpPro.Core\\Lion.AbpPro.Core.csproj\"/>",
                    "<PackageReference Include=\"Lion.AbpPro.Core\"/>")
                .Replace("<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\shared\\Lion.AbpPro.Shared.Hosting.Microservices\\Lion.AbpPro.Shared.Hosting.Microservices.csproj\"/>",
                    "<PackageReference Include=\"Lion.AbpPro.Shared.Hosting.Microservices\"/>")
                .Replace("<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\shared\\Lion.AbpPro.Shared.Hosting.Gateways\\Lion.AbpPro.Shared.Hosting.Gateways.csproj\"/>",
                    "<PackageReference Include=\"Lion.AbpPro.Shared.Hosting.Gateways\"/>")
                .Replace("<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\frameworks\\src\\Lion.AbpPro.AspNetCore\\Lion.AbpPro.AspNetCore.csproj\"/>",
                    "<PackageReference Include=\"Lion.AbpPro.AspNetCore\"/>")
            ;
    }

    public static string ReplacePackageReferenceBasicManagement(this string content)
    {
        return content
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\BasicManagement\\src\\Lion.AbpPro.BasicManagement.Application\\Lion.AbpPro.BasicManagement.Application.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.BasicManagement.Application\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\BasicManagement\\src\\Lion.AbpPro.BasicManagement.Application.Contracts\\Lion.AbpPro.BasicManagement.Application.Contracts.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.BasicManagement.Application.Contracts\"/>")
            .Replace("<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\BasicManagement\\src\\Lion.AbpPro.BasicManagement.Domain\\Lion.AbpPro.BasicManagement.Domain.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.BasicManagement.Domain\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\BasicManagement\\src\\Lion.AbpPro.BasicManagement.Domain.Shared\\Lion.AbpPro.BasicManagement.Domain.Shared.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.BasicManagement.Domain.Shared\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\BasicManagement\\src\\Lion.AbpPro.BasicManagement.EntityFrameworkCore\\Lion.AbpPro.BasicManagement.EntityFrameworkCore.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.BasicManagement.EntityFrameworkCore\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\BasicManagement\\src\\Lion.AbpPro.BasicManagement.FreeSqlRepository\\Lion.AbpPro.BasicManagement.FreeSqlRepository.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FreeSqlRepository\"/>")
            .Replace("<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\BasicManagement\\src\\Lion.AbpPro.BasicManagement.HttpApi\\Lion.AbpPro.BasicManagement.HttpApi.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.BasicManagement.HttpApi\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\BasicManagement\\src\\Lion.AbpPro.BasicManagement.HttpApi.Client\\Lion.AbpPro.BasicManagement.HttpApi.Client.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.BasicManagement.HttpApi.Client\"/>");
    }

    public static string ReplacePackageReferenceDataDictionaryManagement(this string content)
    {
        return content
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\DataDictionaryManagement\\src\\Lion.AbpPro.DataDictionaryManagement.Application\\Lion.AbpPro.DataDictionaryManagement.Application.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.DataDictionaryManagement.Application\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\DataDictionaryManagement\\src\\Lion.AbpPro.DataDictionaryManagement.Application.Contracts\\Lion.AbpPro.DataDictionaryManagement.Application.Contracts.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.DataDictionaryManagement.Application.Contracts\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\DataDictionaryManagement\\src\\Lion.AbpPro.DataDictionaryManagement.Domain\\Lion.AbpPro.DataDictionaryManagement.Domain.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.DataDictionaryManagement.Domain\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\DataDictionaryManagement\\src\\Lion.AbpPro.DataDictionaryManagement.Domain.Shared\\Lion.AbpPro.DataDictionaryManagement.Domain.Shared.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.DataDictionaryManagement.Domain.Shared\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\DataDictionaryManagement\\src\\Lion.AbpPro.DataDictionaryManagement.EntityFrameworkCore\\Lion.AbpPro.DataDictionaryManagement.EntityFrameworkCore.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.DataDictionaryManagement.EntityFrameworkCore\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\DataDictionaryManagement\\src\\Lion.AbpPro.DataDictionaryManagement.FreeSqlRepository\\Lion.AbpPro.DataDictionaryManagement.FreeSqlRepository.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FreeSqlRepository\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\DataDictionaryManagement\\src\\Lion.AbpPro.DataDictionaryManagement.HttpApi\\Lion.AbpPro.DataDictionaryManagement.HttpApi.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.DataDictionaryManagement.HttpApi\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\DataDictionaryManagement\\src\\Lion.AbpPro.DataDictionaryManagement.HttpApi.Client\\Lion.AbpPro.DataDictionaryManagement.HttpApi.Client.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.DataDictionaryManagement.HttpApi.Client\"/>");
    }

    public static string ReplacePackageReferenceFileManagement(this string content)
    {
        return content
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\FileManagement\\src\\Lion.AbpPro.FileManagement.Application\\Lion.AbpPro.FileManagement.Application.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FileManagement.Application\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\FileManagement\\src\\Lion.AbpPro.FileManagement.Application.Contracts\\Lion.AbpPro.FileManagement.Application.Contracts.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FileManagement.Application.Contracts\"/>")
            .Replace("<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\FileManagement\\src\\Lion.AbpPro.FileManagement.Domain\\Lion.AbpPro.FileManagement.Domain.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FileManagement.Domain\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\FileManagement\\src\\Lion.AbpPro.FileManagement.Domain.Shared\\Lion.AbpPro.FileManagement.Domain.Shared.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FileManagement.Domain.Shared\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\FileManagement\\src\\Lion.AbpPro.FileManagement.EntityFrameworkCore\\Lion.AbpPro.FileManagement.EntityFrameworkCore.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FileManagement.EntityFrameworkCore\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\FileManagement\\src\\Lion.AbpPro.FileManagement.FreeSqlRepository\\Lion.AbpPro.FileManagement.FreeSqlRepository.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FreeSqlRepository\"/>")
            .Replace("<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\FileManagement\\src\\Lion.AbpPro.FileManagement.HttpApi\\Lion.AbpPro.FileManagement.HttpApi.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FileManagement.HttpApi\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\FileManagement\\src\\Lion.AbpPro.FileManagement.HttpApi.Client\\Lion.AbpPro.FileManagement.HttpApi.Client.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FileManagement.HttpApi.Client\"/>");
    }

    public static string ReplacePackageReferenceLanguageManagement(this string content)
    {
        return content
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\LanguageManagement\\src\\Lion.AbpPro.LanguageManagement.Application\\Lion.AbpPro.LanguageManagement.Application.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.LanguageManagement.Application\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\LanguageManagement\\src\\Lion.AbpPro.LanguageManagement.Application.Contracts\\Lion.AbpPro.LanguageManagement.Application.Contracts.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.LanguageManagement.Application.Contracts\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\LanguageManagement\\src\\Lion.AbpPro.LanguageManagement.Domain\\Lion.AbpPro.LanguageManagement.Domain.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.LanguageManagement.Domain\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\LanguageManagement\\src\\Lion.AbpPro.LanguageManagement.Domain.Shared\\Lion.AbpPro.LanguageManagement.Domain.Shared.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.LanguageManagement.Domain.Shared\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\LanguageManagement\\src\\Lion.AbpPro.LanguageManagement.EntityFrameworkCore\\Lion.AbpPro.LanguageManagement.EntityFrameworkCore.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.LanguageManagement.EntityFrameworkCore\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\LanguageManagement\\src\\Lion.AbpPro.LanguageManagement.FreeSqlRepository\\Lion.AbpPro.LanguageManagement.FreeSqlRepository.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FreeSqlRepository\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\LanguageManagement\\src\\Lion.AbpPro.LanguageManagement.HttpApi\\Lion.AbpPro.LanguageManagement.HttpApi.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.LanguageManagement.HttpApi\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\LanguageManagement\\src\\Lion.AbpPro.LanguageManagement.HttpApi.Client\\Lion.AbpPro.LanguageManagement.HttpApi.Client.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.LanguageManagement.HttpApi.Client\"/>");
    }

    public static string ReplacePackageReferenceNotificationManagement(this string content)
    {
        return content
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\NotificationManagement\\src\\Lion.AbpPro.NotificationManagement.Application\\Lion.AbpPro.NotificationManagement.Application.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.NotificationManagement.Application\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\NotificationManagement\\src\\Lion.AbpPro.NotificationManagement.Application.Contracts\\Lion.AbpPro.NotificationManagement.Application.Contracts.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.NotificationManagement.Application.Contracts\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\NotificationManagement\\src\\Lion.AbpPro.NotificationManagement.Domain\\Lion.AbpPro.NotificationManagement.Domain.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.NotificationManagement.Domain\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\NotificationManagement\\src\\Lion.AbpPro.NotificationManagement.Domain.Shared\\Lion.AbpPro.NotificationManagement.Domain.Shared.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.NotificationManagement.Domain.Shared\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\NotificationManagement\\src\\Lion.AbpPro.NotificationManagement.EntityFrameworkCore\\Lion.AbpPro.NotificationManagement.EntityFrameworkCore.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.NotificationManagement.EntityFrameworkCore\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\NotificationManagement\\src\\Lion.AbpPro.NotificationManagement.FreeSqlRepository\\Lion.AbpPro.NotificationManagement.FreeSqlRepository.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.FreeSqlRepository\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\NotificationManagement\\src\\Lion.AbpPro.NotificationManagement.HttpApi\\Lion.AbpPro.NotificationManagement.HttpApi.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.NotificationManagement.HttpApi\"/>")
            .Replace(
                "<ProjectReference Include=\"..\\..\\..\\..\\..\\aspnet-core\\modules\\NotificationManagement\\src\\Lion.AbpPro.NotificationManagement.HttpApi.Client\\Lion.AbpPro.NotificationManagement.HttpApi.Client.csproj\"/>",
                "<PackageReference Include=\"Lion.AbpPro.NotificationManagement.HttpApi.Client\"/>");
    }

    public static string ReplaceLionPackageVersion(this string context, string version)
    {
        return context.Replace("MyVersion", version);
    }
}