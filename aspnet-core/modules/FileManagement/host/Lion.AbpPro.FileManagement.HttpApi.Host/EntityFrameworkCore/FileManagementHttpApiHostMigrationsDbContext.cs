namespace Lion.AbpPro.FileManagement.EntityFrameworkCore;

public class FileManagementHttpApiHostMigrationsDbContext : AbpDbContext<FileManagementHttpApiHostMigrationsDbContext>
{
    public FileManagementHttpApiHostMigrationsDbContext(DbContextOptions<FileManagementHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureFileManagement();
    }
}
