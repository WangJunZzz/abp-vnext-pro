namespace Lion.AbpPro.EntityFrameworkCore
{
    public class DbProp
    {
        public ValueConverter? ValueConverter { get; set; }
        public string ColumnName { get; set; }

        public Func<object, object?> GetValueFunc;
        public Type PropertyType { get; set; }
    }
}
