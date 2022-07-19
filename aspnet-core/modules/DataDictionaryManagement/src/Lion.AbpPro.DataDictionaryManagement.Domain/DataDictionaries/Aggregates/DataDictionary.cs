namespace Lion.AbpPro.DataDictionaryManagement.DataDictionaries.Aggregates
{
    /// <summary>
    /// 数据字典
    /// </summary>
    public class DataDictionary : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 租户id
        /// </summary>
        public Guid? TenantId { get; private set; }

        /// <summary>
        /// 字典编码
        /// </summary>
        [Required]
        [MaxLength(DataDictionaryMaxLengths.Code)]
        public string Code { get; private set; }

        /// <summary>
        /// 显示名
        /// </summary>
        [Required]
        [MaxLength(DataDictionaryMaxLengths.DisplayText)]
        public string DisplayText { get; private set; }


        /// <summary>
        /// 描述
        /// </summary>
        [Required]
        [MaxLength(DataDictionaryMaxLengths.Description)]
        public string Description { get; private set; }

        /// <summary>
        /// 字典明细集合
        /// </summary>
        public List<DataDictionaryDetail> Details { get; private set; }

        private DataDictionary()
        {
            Details = new List<DataDictionaryDetail>();
        }

        public DataDictionary(
            Guid id,
            string code,
            string displayText,
            string description = null,
            Guid? tenantId = null) : base(id)
        {
            SetProperties(code, displayText, description, tenantId);
            Details = new List<DataDictionaryDetail>();
        }

        private void SetProperties(string code, string displayText, string description, Guid? tenantId)
        {
            SetCode(code);
            SetDisplayText(displayText);
            SetDescription(description);
            SetTenantId(tenantId);
        }

        public void SetTenantId(Guid? tenantId)
        {
            TenantId = tenantId;
        }

        public void SetCode(string code)
        {
            Guard.NotNullOrWhiteSpace(code, nameof(code), DataDictionaryMaxLengths.Code);
            Code = code;
        }

        private void SetDisplayText(string displayText)
        {
            Guard.NotNullOrWhiteSpace(displayText, nameof(displayText), DataDictionaryMaxLengths.DisplayText);
            DisplayText = displayText;
        }

        private void SetDescription(string description)
        {
            Guard.Length(description, nameof(description), DataDictionaryMaxLengths.Description);
            Description = description ?? string.Empty;
        }

        public void AddDetail(Guid dataDictionayDetailId, string code, string displayText, int order = 1,
            string description = "", bool isEnabled = true)
        {
            if (Details.Any(e => e.Code == code.Trim()))
            {
                throw new DataDictionaryDomainException(message: "数据字典项已存在");
            }

            Details.Add(new DataDictionaryDetail(dataDictionayDetailId, Id, code, displayText, order, isEnabled,
                description));
        }

        public void RemoveDetail(string detailCode)
        {
            var detail = Details.FirstOrDefault(item => item.Code == detailCode);
            if (null == detail)
            {
                throw new DataDictionaryDomainException(message: "数据字典项不存在");
            }

            Details.Remove(detail);
        }

        public void Update(Guid dataDictionayDetailId,string displayText,string description)
        {
            SetDescription(description);
            SetDisplayText(displayText);
        }
    }
}