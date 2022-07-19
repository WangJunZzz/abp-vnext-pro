namespace Lion.AbpPro.Extension.System.Reflection
{
    /// <summary>
    /// 属性<see cref="MethodInfo"/>的扩展辅助操作方法
    /// </summary>
    public static class PropertyInfoExtensions
    {
        /// <summary>
        /// 返回当前属性信息是否为virtual
        /// </summary>
        public static bool IsVirtual(this PropertyInfo property)
        {
            var accessor = property.GetAccessors().FirstOrDefault();
            if (accessor == null)
            {
                return false;
            }

            return accessor.IsVirtual && !accessor.IsFinal;
        }
    }
}