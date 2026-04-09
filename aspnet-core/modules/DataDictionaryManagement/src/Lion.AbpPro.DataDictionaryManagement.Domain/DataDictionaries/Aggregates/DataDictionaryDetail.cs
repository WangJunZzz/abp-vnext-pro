namespace Lion.AbpPro.DataDictionaryManagement.DataDictionaries.Aggregates
{
    public class DataDictionaryDetail : AuditedEntity<Guid>
    {
        /// <summary>
        /// 所属字典Id
        /// </summary>
        public Guid DataDictionaryId { get; private set; }

        /// <summary>
        /// 字典明细编码
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// 展现列表时排序用
        /// </summary>
        public int Order { get; private set; }

        /// <summary>
        /// 英文显示名
        /// </summary>
        public string DisplayText { get; private set; }


        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// 启/停用(默认启用)
        /// </summary>
        public bool IsEnabled { get; private set; }

        private DataDictionaryDetail()
        {
        }


        public DataDictionaryDetail(Guid id, Guid dataDictionaryId, string code, string displayText, int order,
            bool isEnabled = true, string description = null)
        {
            DataDictionaryId = dataDictionaryId;
            Id = id;
            SetProperties(code, displayText, order, isEnabled, description);
        }


        public void SetProperties(string code, string displayText, int order, bool isEnabled = true,
            string description = null)
        {
            SetCode(code);
            SetOrder(order);
            SetDisplayText(displayText);
            SetIsEnabled(isEnabled);
            SetDescription(description);
        }

        private void SetCode(string code)
        {
            Guard.NotNullOrWhiteSpace(code, nameof(code), DataDictionaryMaxLengths.Code);
            Code = code;
        }

        private void SetOrder(int order)
        {
            Order = order;
        }

        private void SetDisplayText(string displayText)
        {
            Guard.NotNullOrWhiteSpace(displayText, nameof(displayText), DataDictionaryMaxLengths.DisplayText);
            DisplayText = displayText;
        }

        private void SetDescription(string description)
        {
            Guard.Length(description, nameof(description), DataDictionaryMaxLengths.Description);
            Description = Description = description ?? string.Empty;
        }

        public void SetIsEnabled(bool isEnabled)
        {
            IsEnabled = isEnabled;
        }

        public void UpdateDetail(
            Guid dataDictionayDetailId,
            string displayText,
            string description,
            int order)
        {
            SetDescription(description);
            SetDisplayText(displayText);
            SetOrder(order);
        }
    }
}