using System.Collections.Generic;

namespace Lion.AbpPro.Settings
{
    public class UpdateSettingInput
    {
        public Dictionary<string, string> Values { get; set; }

        public UpdateSettingInput()
        {
            Values = new Dictionary<string, string>();
        }
    }
}