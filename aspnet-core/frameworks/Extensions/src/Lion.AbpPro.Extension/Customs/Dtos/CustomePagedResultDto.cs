namespace Lion.AbpPro.Extension.Customs.Dtos
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