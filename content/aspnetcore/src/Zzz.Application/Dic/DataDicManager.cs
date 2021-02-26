using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;

namespace Zzz.Dic
{
    public class DataDicManager : IDataDicManager
    {
        private readonly IRepository<DataDictionary, Guid> _dataDictionaryRepository;

        public DataDicManager(IRepository<DataDictionary, Guid> dataDictionaryRepository)
        {
            _dataDictionaryRepository = dataDictionaryRepository;
        }

        public async Task<string> FindAsync(string name, string label)
        {
            Check.NotNullOrEmpty(name, nameof(name));
            Check.NotNullOrEmpty(label, nameof(label));
            var entity = await _dataDictionaryRepository.Include(e => e.DataDictionaryDetails).Where(e => e.Name == name.Trim()).FirstOrDefaultAsync();
            if (entity == null) throw new BusinessException("Dic is Null");
            var entityDetail= entity.DataDictionaryDetails.Find(e => e.Label == label.Trim());
            if(entityDetail==null) throw new BusinessException("DicDetail is Null");
            return entityDetail.Value;
        }
    }
}
