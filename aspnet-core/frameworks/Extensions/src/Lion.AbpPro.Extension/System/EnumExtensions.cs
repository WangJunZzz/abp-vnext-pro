namespace Lion.AbpPro.Extension.System
{
    /// <summary>
    /// 枚举<see cref="Enum"/>的扩展辅助操作方法
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取枚举项上的<see cref="DescriptionAttribute"/>特性的文字描述
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToDescription(this Enum value)
        {
            var type = value.GetType();
            var member = type.GetMember(value.ToString()).FirstOrDefault();

            return member != null ? member.GetDescription() : value.ToString();
        }

        /// <summary>
        /// 枚举遍历，返回枚举的名称、值、特性
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <param name="action">回调函数</param>
        private static void Each(this Type enumType, Action<string, string, string, object> action)
        {
            if (enumType.BaseType != typeof(Enum))
            {
                return;
            }
            var arr = Enum.GetValues(enumType);
            foreach (var name in arr)
            {
                var currentEnum = Enum.Parse(enumType, name.ToString());
                var value = Convert.ToInt32(Enum.Parse(enumType, name.ToString()));
                var fieldInfo = enumType.GetField(name.ToString());
                var description = "";
                if (fieldInfo != null)
                {
                    var attr = Attribute.GetCustomAttribute(fieldInfo,
                        typeof(DescriptionAttribute), false) as DescriptionAttribute;
                    if (attr != null)
                    {
                        description = attr.Description;
                    }
                }
                action(name.ToString(), value.ToString(), description, currentEnum);
            }
        }

        /// <summary>
        /// 根据枚举类型值返回枚举定义Description属性
        /// </summary>
        /// <param name="value"></param>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static string ToEnumDescriptionString(this short value, Type enumType)
        {
            var nvc = new NameValueCollection();
            var typeDescription = typeof(DescriptionAttribute);
            var fields = enumType.GetFields();
            foreach (var field in fields)
            {
                if (field.FieldType.IsEnum)
                {
                    var strValue = ((int)enumType.InvokeMember(field.Name, BindingFlags.GetField, null, null, null)).ToString();
                    var arr = field.GetCustomAttributes(typeDescription, true);
                    string strText;
                    if (arr.Length > 0)
                    {
                        var aa = (DescriptionAttribute)arr[0];
                        strText = aa.Description;
                    }
                    else
                    {
                        strText = "";
                    }
                    nvc.Add(strValue, strText);
                }
            }
            return nvc[value.ToString()];
        }

        /// <summary>
        /// 将指定枚举转换为字典.
        /// 枚举的Description为字典的Key,枚举的Value为字典的Value
        /// </summary>
        /// <typeparam name="T">指定枚举</typeparam>
        private static List<KeyValuePair<string, string>> GetEnumTypeValueList<T>()
        {
            var items = new List<KeyValuePair<string, string>>();
            typeof(T).Each((name, value, description, enumObj) =>
                items.Add(new KeyValuePair<string, string>(description, value)));
            return items;
        }

        /// <summary>
        /// 将指定枚举转换为字典.
        /// 枚举的Description为字典的Key,枚举为字典的Value
        /// </summary>
        /// <typeparam name="T">指定枚举</typeparam>
        private static List<KeyValuePair<string, T>> GetEnumTypeList<T>()
        {
            var items = new List<KeyValuePair<string, T>>();
            typeof(T).Each((name, value, description, enumObj) =>
                items.Add(new KeyValuePair<string, T>(description, (T)enumObj)));
            return items;
        }

        /// <summary>
        /// 将指定枚举转换为字典.
        /// 枚举的Description为字典的Key,枚举的Name为字典的Value
        /// </summary>
        /// <typeparam name="T">指定枚举</typeparam>
        public static List<KeyValuePair<string, string>> GetEnumTypeDescriptionNameList<T>()
        {
            var items = new List<KeyValuePair<string, string>>();
            typeof(T).Each((name, value, description, enumObj) => items.Add(new KeyValuePair<string, string>(description, name)));
            return items;
        }

        /// <summary>
        /// 将指定枚举转换为字典.
        /// 枚举的Name为字典的Key,枚举的Description为字典的Value
        /// </summary>
        /// <typeparam name="T">指定枚举</typeparam>
        public static List<KeyValuePair<string, string>> GetEnumTypeValueNameList<T>()
        {
            var items = new List<KeyValuePair<string, string>>();
            typeof(T).Each((name, value, description, enumObj) => items.Add(new KeyValuePair<string, string>(name, description)));
            return items;
        }

        /// <summary>
        /// 将指定枚举转换为字典.
        /// 枚举的Name为字典的Key,枚举的Description为字典的Value
        /// </summary>
        /// <typeparam name="TModel">指定枚举</typeparam>
        public static List<KeyValuePair<string, string>> GetStringKeyValueList<TModel>() where TModel : Enum
        {
            var keyValuePairList = new List<KeyValuePair<string, string>>();
            var values = Enum.GetValues(typeof(TModel));
            var modelArray = new TModel[values.Length];
            values.CopyTo(modelArray, 0);
            foreach (TModel model in modelArray)
                keyValuePairList.Add(new KeyValuePair<string, string>(model.ToString(), model.ToString()));
            return keyValuePairList;
        }

        /// <summary>
        /// 将指定枚举转换为字典.
        /// 枚举的Description为字典的Key,枚举为字典的Value
        /// </summary>
        /// <typeparam name="TModel">指定枚举</typeparam>
        public static List<KeyValuePair<string, TModel>> GetEnumKeyValueList<TModel>() where TModel : Enum
        {
            var enumTypeList = GetEnumTypeList<TModel>();
            var keyValuePairList = new List<KeyValuePair<string, TModel>>();
            foreach (KeyValuePair<string, TModel> keyValuePair in enumTypeList)
                keyValuePairList.Add(new KeyValuePair<string, TModel>(keyValuePair.Key, keyValuePair.Value));
            return keyValuePairList;
        }

        public static List<KeyValuePair<string, string>> GetEntityDoubleStringKeyValueList<TModel>()
        {
            var enumTypeList = GetEnumTypeValueList<TModel>();
            var keyValuePairList = new List<KeyValuePair<string, string>>();
            foreach (KeyValuePair<string, string> keyValuePair in enumTypeList)
                keyValuePairList.Add(new KeyValuePair<string, string>(keyValuePair.Key, keyValuePair.Value));
            return keyValuePairList;
        }

        public static List<KeyValuePair<string, int>> GetEntityStringIntKeyValueList<TModel>()
        {
            List<KeyValuePair<string, string>> enumTypeList = GetEnumTypeValueList<TModel>();
            List<KeyValuePair<string, int>> keyValuePairList = new List<KeyValuePair<string, int>>();
            foreach (KeyValuePair<string, string> keyValuePair in enumTypeList)
                keyValuePairList.Add(new KeyValuePair<string, int>(keyValuePair.Key, Convert.ToInt32(keyValuePair.Value)));
            return keyValuePairList;
        }

        public static List<KeyValuePair<int, int>> GetEntityDoubleIntKeyValueList<TModel>()
        {
            List<KeyValuePair<string, string>> enumTypeList = GetEnumTypeValueList<TModel>();
            List<KeyValuePair<int, int>> keyValuePairList = new List<KeyValuePair<int, int>>();
            foreach (KeyValuePair<string, string> keyValuePair in enumTypeList)
                keyValuePairList.Add(new KeyValuePair<int, int>(Convert.ToInt32(keyValuePair.Key), Convert.ToInt32(keyValuePair.Value)));
            return keyValuePairList;
        }
    }
}