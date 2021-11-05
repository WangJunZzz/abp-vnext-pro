using System;
using System.Collections.Generic;

namespace Lion.Abp.Extension
{
    [Serializable]
    public class CustomePagedResultDto<T> : CustomeListResultDto<T>
    {
        public long TotalCount { get; set; }

        public CustomePagedResultDto()
        {
        }

        public CustomePagedResultDto(long totalCount, IReadOnlyList<T> items)
            : base(items)
        {
            TotalCount = totalCount;
        }
    }
}