namespace Lion.AbpPro.Extension.System.Reflection
{
    /// <summary>
    /// 成员<see cref="MemberInfo"/>的扩展辅助操作方法
    /// </summary>
    public static class MemberInfoExtensions
    {
        /// <summary>
        /// 获取成员元数据的Description特性描述信息。
        /// </summary>
        /// <param name="member">成员元数据对象。</param>
        /// <param name="inherit">是否搜索成员的继承链以查找描述特性。</param>
        /// <returns>返回Description特性描述信息，如不存在则返回成员的名称。</returns>
        public static string GetDescription(this MemberInfo member, bool inherit = true)
        {
            var desc = member.GetAttribute<DescriptionAttribute>(inherit);
            if (desc != null)
            {
                return desc.Description;
            }

            var displayName = member.GetAttribute<DisplayNameAttribute>(inherit);
            if (displayName != null)
            {
                return displayName.DisplayName;
            }

            var display = member.GetAttribute<DisplayAttribute>(inherit);
            return display != null ? display.Name : member.Name;
        }

        /// <summary>
        /// 检查指定指定类型成员中是否存在指定的Attribute特性。
        /// </summary>
        /// <typeparam name="T">要检查的Attribute特性类型。</typeparam>
        /// <param name="memberInfo">要检查的类型成员</param>
        /// <param name="inherit">是否从继承中查找</param>
        /// <returns>是否存在</returns>
        public static bool HasAttribute<T>(this MemberInfo memberInfo, bool inherit = true) where T : Attribute
        {
            return memberInfo.IsDefined(typeof(T), inherit);
        }

        /// <summary>
        /// 从类型成员获取指定Attribute特性
        /// </summary>
        /// <typeparam name="T">Attribute特性类型</typeparam>
        /// <param name="memberInfo">类型类型成员</param>
        /// <param name="inherit">是否从继承中查找</param>
        /// <returns>存在返回第一个，不存在返回null</returns>
        public static T GetAttribute<T>(this MemberInfo memberInfo, bool inherit = true) where T : Attribute
        {
            var attributes = memberInfo.GetCustomAttributes(typeof(T), inherit);
            return attributes.FirstOrDefault() as T;
        }

        /// <summary>
        /// 从类型成员获取指定Attribute特性。
        /// </summary>
        /// <typeparam name="T">Attribute特性类型。</typeparam>
        /// <param name="memberInfo">类型类型成员。</param>
        /// <param name="inherit">是否从继承中查找。</param>
        /// <returns>返回所有指定Attribute特性的数组。</returns>
        public static T[] GetAttributes<T>(this MemberInfo memberInfo, bool inherit = true) where T : Attribute
        {
            return memberInfo.GetCustomAttributes(typeof(T), inherit).Cast<T>().ToArray();
        }
    }
}