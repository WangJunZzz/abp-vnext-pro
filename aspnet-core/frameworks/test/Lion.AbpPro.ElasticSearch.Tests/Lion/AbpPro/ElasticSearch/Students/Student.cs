namespace Lion.AbpPro.ElasticSearch.Students;

public class Student : IElasticSearchEntity
{
    public Guid Id { get; set; }

    public DateTime CreationTime { get; set; }

    public double Price { get; set; }

    public string Name { get; set; }

    public int Age { get; set; }

    public Gender Gender { get; set; }
}