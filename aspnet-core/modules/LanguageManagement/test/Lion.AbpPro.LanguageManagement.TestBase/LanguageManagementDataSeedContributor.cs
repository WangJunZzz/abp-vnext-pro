namespace Lion.AbpPro.LanguageManagement
{
    public class LanguageManagementDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly ICurrentTenant _currentTenant;


        public LanguageManagementDataSeedContributor(
            IGuidGenerator guidGenerator,
            ICurrentTenant currentTenant)
        {
            _guidGenerator = guidGenerator;
            _currentTenant = currentTenant;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            /* Instead of returning the Task.CompletedTask, you can insert your test data
             * at this point!
             */
            await  Task.CompletedTask;
        }
    }
}