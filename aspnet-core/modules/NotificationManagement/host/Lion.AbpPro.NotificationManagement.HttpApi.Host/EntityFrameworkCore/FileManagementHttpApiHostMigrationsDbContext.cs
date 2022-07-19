namespace Lion.AbpPro.NotificationManagement.EntityFrameworkCore;

public class NotificationManagementHttpApiHostMigrationsDbContext : AbpDbContext<NotificationManagementHttpApiHostMigrationsDbContext>
{
    public NotificationManagementHttpApiHostMigrationsDbContext(DbContextOptions<NotificationManagementHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureNotificationManagement();
    }
}
