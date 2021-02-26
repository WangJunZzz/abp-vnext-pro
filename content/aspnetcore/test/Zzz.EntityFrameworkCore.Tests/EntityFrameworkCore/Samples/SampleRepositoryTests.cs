using Microsoft.EntityFrameworkCore;
using Zzz.Users;
using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;
using Zzz.Repository;

namespace Zzz.EntityFrameworkCore.Samples
{
    /* This is just an example test class.
     * Normally, you don't test ABP framework code
     * (like default AppUser repository IRepository<AppUser, Guid> here).
     * Only test your custom repository methods.
     */
    public class SampleRepositoryTests : ZzzEntityFrameworkCoreTestBase
    {
        private readonly IRepository<AppUser, Guid> _appUserRepository;
        private readonly IUserDapperRepository _userDapperRepository;
        public SampleRepositoryTests()
        {
            _appUserRepository = GetRequiredService<IRepository<AppUser, Guid>>();
            _userDapperRepository = GetRequiredService<IUserDapperRepository>();
        }

        [Fact]
        public async Task Should_Query_AppUser()
        {
            /* Need to manually start Unit Of Work because
             * FirstOrDefaultAsync should be executed while db connection / context is available.
             */
            await WithUnitOfWorkAsync(async () =>
            {
                //Act
                var adminUser = await _appUserRepository
                    .Where(u => u.UserName == "admin")
                    .FirstOrDefaultAsync();

                //Assert
                adminUser.ShouldNotBeNull();
            });
        }

        [Fact]
        public async Task Should_Dapper_Query_User()
        {
           var result = await  _userDapperRepository.GetAllUserNameListAsync();
            result.Count.ShouldBe(1);
        }
    }
}
