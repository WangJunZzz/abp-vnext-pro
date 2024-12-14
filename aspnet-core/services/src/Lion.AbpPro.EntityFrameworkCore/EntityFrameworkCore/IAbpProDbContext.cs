using Lion.AbpPro.Books;

namespace Lion.AbpPro.EntityFrameworkCore
{
    [ConnectionStringName("Default")]
    public interface IAbpProDbContext : IEfCoreDbContext
    {
        DbSet<Book> Books { get; set; }
    }
}