using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;

namespace CompanyName.ProjectName.Users
{
    public class UserDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IdentityUserManager _userManager;
        private readonly IdentityRoleManager _identityRoleManager;

        public UserDataSeedContributor(
            IdentityUserManager userManager,
            IdentityRoleManager identityRoleManager)
        {
            _userManager = userManager;
            _identityRoleManager = identityRoleManager;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            const string adminUserName = "admin";
            var adminUser = await _userManager.FindByNameAsync(adminUserName);
            if (adminUser != null)
            {
                await _userManager.SetLockoutEndDateAsync(adminUser, DateTimeOffset.UtcNow.AddDays(-1));
            }

            var role = await _identityRoleManager.FindByNameAsync(adminUserName);
            if (role != null)
            {
                role.IsDefault = true;
                await _identityRoleManager.UpdateAsync(role);
            }
        }
    }
}