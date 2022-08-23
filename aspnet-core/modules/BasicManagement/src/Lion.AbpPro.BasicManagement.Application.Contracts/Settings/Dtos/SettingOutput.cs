namespace Lion.AbpPro.BasicManagement.Settings.Dtos
{
    public class SettingOutput
    {
        /// <summary>
        /// 分组
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// 分组显示名称
        /// </summary>
        public string GroupDisplayName { get; set; }

        public List<SettingItemOutput> SettingItemOutput { get; set; }

        public SettingOutput()
        {
            SettingItemOutput = new List<SettingItemOutput>();
        }
    }

    public class SettingItemOutput
    {
        public SettingItemOutput(string name, string displayName, string value, string type,string description)
        {
            Name = name;
            DisplayName = displayName;
            Value = value;
            Type = type ?? "Text";
            Description = description;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 前端控件类型
        /// </summary>
        public string Type { get; set; }
    }
}