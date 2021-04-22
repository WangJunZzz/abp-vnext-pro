using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Zzz.Dic
{
    public interface IDataDicManager
    {
        Task<string> FindAsync(string name, string label);
    }
}
