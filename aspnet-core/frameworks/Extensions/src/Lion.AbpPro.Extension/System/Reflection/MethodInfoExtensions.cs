namespace Lion.AbpPro.Extension.System.Reflection
{
    /// <summary>
    /// 方法<see cref="MethodInfo"/>的扩展辅助操作方法
    /// </summary>
    public static class MethodInfoExtensions
    {
        /// <summary>
        /// 方法是否是异步
        /// </summary>
        public static bool IsAsync(this MethodInfo method)
        {
            return (method.ReturnType == typeof(Task<>)
                   || method.ReturnType.IsGenericType 
                   && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>)) 
                   || method.ReturnType == typeof(Task);
        }

        /// <summary>
        /// 返回当前方法信息是否是重写方法
        /// </summary>
        /// <param name="method">要判断的方法信息</param>
        /// <returns>是否是重写方法</returns>
        public static bool IsOverridden(this MethodInfo method)
        {
            return method.GetBaseDefinition().DeclaringType != method.DeclaringType;
        }
    }
}