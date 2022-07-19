namespace Lion.AbpPro.ElasticSearches.Dto
{
    /// <summary>
    ///   Dto为什么在Service层
    ///  因为NEST 类库的坑 PropertyName必须用这个
    ///  不想在契约层添加NEST 包引用
    /// </summary>
    [Serializable]
    public class PagingElasticSearchLogDto
    {
        /// <summary>
        /// 日志级别
        /// </summary>
        public string Level { get; set; }


        /// <summary>
        /// 日志内容
        /// </summary>
        public string Message { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary>
        [PropertyName("@timestamp")]
        public DateTime CreationTime { get; set; }
    }
}