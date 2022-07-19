namespace Lion.AbpPro.Extension.Customs
{
    /// <summary>
    /// 参数合法性检查类
    /// </summary>
    [DebuggerStepThrough]
    public static class Guard
    {
        /// <summary>
        /// 检查参数不能为空引用，
        /// </summary>
        /// <param name="value"></param>
        /// <param name="valueName">参数名称</param>
        public static T NotNull<T>(T value, string valueName)
        {
            if (null == value)
            {
                throw new ArgumentNullException(valueName);
            }

            return value;
        }

        /// <summary>
        /// 检查字符串不能为空引用或空字符串，
        /// </summary>
        /// <param name="value"></param>
        /// <param name="valueName">参数名称。</param>
        /// <param name="maxLength">字符串允许的最大长度。</param>
        /// <param name="minLength">字符串允许的最小长度。0表示不限制最小长度</param>
        public static string NotNullOrEmpty(string value, string valueName, int maxLength = int.MaxValue,
            int minLength = 0)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(valueName);
            }

            if (value.Length > maxLength)
            {
                throw new ArgumentOutOfRangeException(valueName);
            }

            if (minLength > 0 && value.Length < minLength)
            {
                throw new ArgumentOutOfRangeException(valueName);
            }

            return value;
        }

        /// <summary>
        /// 检查字符串不能为空引用或全部为空白，
        /// </summary>
        /// <param name="value">需检查的字符串</param>
        /// <param name="valueName">参数名称。</param>
        /// <param name="maxLength">字符串允许的最大长度。</param>
        /// <param name="minLength">字符串允许的最小长度。0表示不限制最小长度</param>
        public static string NotNullOrWhiteSpace(
            string value,
            string valueName,
            int maxLength = int.MaxValue,
            int minLength = 0)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(valueName);
            }

            if (value.Length > maxLength)
            {
                throw new ArgumentOutOfRangeException(valueName);
            }

            if (minLength > 0 && value.Length < minLength)
            {
                throw new ArgumentOutOfRangeException(valueName);
            }

            return value;
        }

        /// <summary>
        /// 检查字符串长度是否超过最大长度，或低于最小长度，
        /// </summary>
        /// <param name="value">需检查的字符串。</param>
        /// <param name="valueName">参数名称。</param>
        /// <param name="maxLength">字符串允许的最大长度。</param>
        /// <param name="minLength">字符串要求的最小长度。0表示不限制最小长度</param>
        public static string Length(string value, string valueName, int maxLength = int.MaxValue,
            int minLength = 0)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            if (value.Length > maxLength)
            {
                throw new ArgumentOutOfRangeException(valueName);
            }

            if (minLength > 0 && value.Length < minLength)
            {
                throw new ArgumentOutOfRangeException(valueName);
            }

            return value;
        }

        /// <summary>
        /// 检查Guid值不能为Guid.Empty
        /// </summary>
        /// <param name="value"></param>
        /// <param name="valueName">参数名称。</param>
        public static Guid NotEmpty(
            Guid value,
            string valueName)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentNullException(valueName);
            }

            return value;
        }

        /// <summary>
        /// 检查集合不能为空引用或空集合，
        /// </summary>
        /// <typeparam name="T">集合项的类型。</typeparam>
        /// <param name="list"></param>
        /// <param name="valueName">参数名称。</param>
        public static void NotNullOrEmpty<T>(
            IReadOnlyList<T> list,
            string valueName)
        {
            if (null == list || !list.Any())
            {
                throw new ArgumentNullException(valueName);
            }
        }

        /// <summary>
        /// 检查参数必须小于[或可等于，参数<paramref name="canEqual"/>]指定值，
        /// </summary>
        /// <typeparam name="T">参数类型。</typeparam>
        /// <param name="value"></param>
        /// <param name="valueName">参数名称。</param>
        /// <param name="target">要比较的值。</param>
        /// <param name="canEqual">是否可等于。</param>
        public static void LessThan<T>(
            T value,
            string valueName,
            T target,
            bool canEqual = false)
            where T : IComparable<T>
        {
            var flag = canEqual ? value.CompareTo(target) <= 0 : value.CompareTo(target) < 0;
            if (!flag)
            {
                throw new ArgumentOutOfRangeException(valueName);
            }
        }

        /// <summary>
        /// 检查参数必须大于[或可等于，参数<paramref name="canEqual"/>]指定值，
        /// </summary>
        /// <typeparam name="T">参数类型。</typeparam>
        /// <param name="value">需检查的参数。</param>
        /// <param name="valueName">参数名称。</param>
        /// <param name="target">要比较的值。</param>
        /// <param name="canEqual">是否可等于。</param>
        public static void GreaterThan<T>(
            T value,
            string valueName,
            T target,
            bool canEqual = false)
            where T : IComparable<T>
        {
            var flag = canEqual ? value.CompareTo(target) >= 0 : value.CompareTo(target) > 0;
            if (!flag)
            {
                throw new ArgumentOutOfRangeException(valueName);
            }
        }

        /// <summary>
        /// 检查参数必须在指定范围之间
        /// </summary>
        /// <typeparam name="T">参数类型。</typeparam>
        /// <param name="value">需检查的参数。</param>
        /// <param name="valueName">参数名称。</param>
        /// <param name="start">比较范围的起始值。</param>
        /// <param name="end">比较范围的结束值。</param>
        /// <param name="startEqual">是否可等于起始值</param>
        /// <param name="endEqual">是否可等于结束值</param>
        public static void Between<T>(
            T value,
            string valueName,
            T start,
            T end,
            bool startEqual = false,
            bool endEqual = false)
            where T : IComparable<T>
        {
            var flag = startEqual ? value.CompareTo(start) >= 0 : value.CompareTo(start) > 0;
            if (!flag)
            {
                throw new ArgumentOutOfRangeException(valueName);
            }

            flag = endEqual ? value.CompareTo(end) <= 0 : value.CompareTo(end) < 0;
            if (!flag)
            {
                throw new ArgumentOutOfRangeException(valueName);
            }
        }

        /// <summary>
        /// 检查指定路径的文件夹必须存在，
        /// </summary>
        /// <param name="directory">需检查的路径。</param>
        /// <param name="parameterName">参数名称。</param>
        public static string DirectoryExists(
            string directory,
            string parameterName)
        {
            if (string.IsNullOrWhiteSpace(directory))
            {
                throw new DirectoryNotFoundException(parameterName);
            }

            if (!Directory.Exists(directory))
            {
                throw new DirectoryNotFoundException(directory);
            }

            return directory;
        }

        /// <summary>
        /// 检查指定路径的文件必须存在，否则抛出
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="valueName">参数名称。</param>

        public static string FileExists(
            string filename,
            string valueName)
        {
            if (string.IsNullOrWhiteSpace(filename))
            {
                throw new ArgumentNullException(valueName);
            }

            if (!File.Exists(filename))
            {
                throw new FileNotFoundException(filename);
            }

            return filename;
        }
    }
}
