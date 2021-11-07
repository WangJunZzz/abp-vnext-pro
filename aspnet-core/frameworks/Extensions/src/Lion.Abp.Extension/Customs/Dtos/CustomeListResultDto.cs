using System;
using System.Collections.Generic;

namespace Lion.Abp.Extension
{
    [Serializable]
    public class CustomeListResultDto<T> 
    {
        public IReadOnlyList<T> Items
        {
            get { return _items ??= new List<T>(); }
            set => _items = value;
        }

        private IReadOnlyList<T> _items;

        public CustomeListResultDto()
        {
        }

        public CustomeListResultDto(IReadOnlyList<T> items)
        {
            Items = items;
        }
    }
}