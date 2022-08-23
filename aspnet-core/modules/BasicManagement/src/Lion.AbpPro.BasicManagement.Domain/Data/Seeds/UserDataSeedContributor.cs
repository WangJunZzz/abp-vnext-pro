using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace Lion.AbpPro.BasicManagement.Data.Seeds
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
            // abp 默认会锁定当前用户
            const string adminUserName = "admin";
            var adminUser = await _userManager.FindByNameAsync(adminUserName);
            if (adminUser != null)
            {
                await _userManager.SetLockoutEnabledAsync(adminUser, false);
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