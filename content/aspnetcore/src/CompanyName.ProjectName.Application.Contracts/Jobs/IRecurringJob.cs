using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace CompanyNameProjectName.Jobs
{
    public interface IRecurringJob : ITransientDependency
    {
        /// <summary>
        /// 执行任务
        /// </summary>
        /// <returns></returns>
        Task ExecuteAsync();
    }
}
