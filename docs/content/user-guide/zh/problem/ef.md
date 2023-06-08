# EFCore

## 创建时间,更新时间,删除时间

- 当实体继承了这三个属性得时候,只有在新增才会又创建时间,如果想要也有更新时间如何处理呢？

- 解决方式:重写 DbContext 一下方法

```csharp
namespace Lion.AbpPro.EntityFrameworkCore
{
    [ConnectionStringName("Default")]
    public class AbpProDbContext :
        AbpDbContext<AbpProDbContext>,
        IAbpProDbContext
    {
        protected override void SetCreationAuditProperties(EntityEntry entry)
        {
            SetModificationAuditProperties(entry);
            base.SetCreationAuditProperties(entry);
        }

        protected override void SetDeletionAuditProperties(EntityEntry entry)
        {
            SetModificationAuditProperties(entry);
            base.SetDeletionAuditProperties(entry);
        }
    }
}
```

## 设置数据库字符集格式

```csharp
namespace Lion.AbpPro.EntityFrameworkCore
{
    [ConnectionStringName("Default")]
    public class AbpProDbContext :
        AbpDbContext<AbpProDbContext>,
        IAbpProDbContext
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.UseCollation("utf8mb4_unicode_ci");
            builder.UseGuidCollation("utf8mb4_unicode_ci");
            base.OnModelCreating(builder);
        }
    }
}
```

## 全局设置字符串长度

- 当数据类型是 string 时,需要给每个字段指定长度很麻烦,以下提供统一处理方式。

```csharp
namespace Lion.Pro.EntityFrameworkCore;

/// <summary>
/// ef迁移全局设置
/// </summary>
public static class LionDbContextGlobalSettingExtensions
{
    private const string Remark = "Remark";
    private const string Description = "Description";
    private const string CreationTime = "CreationTime";
    private const string IndexPrefix = "IX_Default_";

    public static void ConfigureGlobalSetting(this ModelBuilder builder)
    {
        ConfigureDefaultMaxLength(builder);
        ConfigureIndexForCreationTime(builder);
    }

    /// <summary>
    /// 配置默认字符串长度
    /// </summary>
    private static void ConfigureDefaultMaxLength(ModelBuilder builder)
    {
        var rules = ConfigureEntityMaxLengthOptions.Configure();
        // 如果是abp表不全局修改长度
        foreach (var property in builder.Model
                     .GetEntityTypes()
                     .Where(e => !e.GetTableName().StartsWith(AbpCommonDbProperties.DbTablePrefix))
                     .SelectMany(t => t.GetProperties())
                     .Where(e => e.ClrType == typeof(string)))
        {
            // 默认设置128长度
           if (property.GetMaxLength() == null)
            {
                property.SetMaxLength(128);
            }

        }
    }

    /// <summary>
    /// 创建时间添加索引
    /// </summary>
    private static void ConfigureIndexForCreationTime(ModelBuilder builder)
    {
        foreach (var property in builder.Model
                     .GetEntityTypes()
                     .Where(e => !e.GetTableName().StartsWith(AbpCommonDbProperties.DbTablePrefix))
                     .SelectMany(t => t.GetProperties())
                     .Where(e => e.Name == CreationTime))
        {
            var entityType = builder.Model.GetEntityTypes().Where(e => e.ClrType == property.DeclaringEntityType.ClrType).ToList().FirstOrDefault();
            var indexName = IndexPrefix + entityType.GetTableName() + "_" + property.Name;
            if (entityType.FindIndex(indexName) == null)
            {
                entityType?.AddIndex(property, indexName);
            }
        }
    }
}
```

```csharp
namespace Lion.AbpPro.EntityFrameworkCore
{
    [ConnectionStringName("Default")]
    public class AbpProDbContext :
        AbpDbContext<AbpProDbContext>,
        IAbpProDbContext
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ConfigureGlobalSetting();
            base.OnModelCreating(builder);
        }
    }
}
```
