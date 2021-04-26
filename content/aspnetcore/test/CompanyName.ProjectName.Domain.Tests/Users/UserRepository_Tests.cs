using System.Threading.Tasks;
using Xunit;
using CompanyNameProjectName.Repository;
using Shouldly;

namespace CompanyNameProjectName.Users
{
    public class UserRepository_Tests: CompanyNameProjectNameDomainTestBase
    {
        private readonly IUserDapperRepository _userDapperRepository;
        public UserRepository_Tests()
        {
            _userDapperRepository = GetService<IUserDapperRepository>();
        }

        [Fact]
        public async Task Shuold_Get_User_Return_Ok()
        {
            await WithUnitOfWorkAsync(async () =>
            {
                var result = await _userDapperRepository.GetAllUserNameListAsync();
                result.Count.ShouldBe(1);
            });
        }
    }
}
