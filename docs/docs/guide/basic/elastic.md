---
outline: deep
---

# ElasticSearch

## 安装

- 添加以下 NuGet 包到你的项目
  - Lion.AbpPro.ElasticSearch
- 添加 [DependsOn(typeof(AbpProElasticSearchModule))] 到你的项目模块类.

## 配置

```json
{
  "ElasticSearch": {
    "Host": "http://localhost:9200",
    "UserName": "admin",
    "Password": "1q2w3E*"
  }
}
```

## 示例

- 实现 Student 的增删查改

### 定义学生类

- 实现 IElasticSearchEntity 接口,此接口有主键 Id,创建时间字段,也是泛型约束。

```csharp
public class Student : IElasticSearchEntity
{
    public Guid Id { get; set; }

    public DateTime CreationTime { get; set; }

    public double Price { get; set; }

    public string Name { get; set; }

    public int Age { get; set; }
}
```

### 定义接口

```csharp
public interface IStudentElasticSearchRepository : IBasicElasticSearchRepository<Student>
{
}
```

### 实现接口

```csharp
public class StudentElasticSearchRepository : ElasticSearchRepository<Student>, IStudentElasticSearchRepository, ITransientDependency
{
    public StudentElasticSearchRepository(IElasticsearchProvider elasticsearchProvider) : base(elasticsearchProvider)
    {
    }

    // index 只能是小写
    protected override string IndexName => "Students".ToLower();
}
```

### CURD

```csharp
// 注入
private readonly IStudentElasticSearchRepository _studentElasticSearchRepository;
public StudentElasticSearchRepositoryTests(IStudentElasticSearchRepository studentElasticSearchRepository)
{
    _studentElasticSearchRepository = studentElasticSearchRepository;
}

// var student = new Student
//          {
//              Id = Guid.NewGuid(),
//              Name = "韩立",
//              Age = 10,
//              CreationTime = DateTime.Now,
//              Price = 100.3,
//          };

// 根据主键Id查询
var result = await _studentElasticSearchRepository.FindAsync(student.Id);

// 新增
await _studentElasticSearchRepository.InsertAsync(student);

// 批量新增
await _studentElasticSearchRepository.InsertManyAsync(students);

// 更新
await _studentElasticSearchRepository.UpdateAsync(student);

// 删除
await _studentElasticSearchRepository.DeleteAsync(student.Id);

// 分页查询
var mustFilters = new List<Func<QueryContainerDescriptor<Student>, QueryContainer>>();
mustFilters.Add(e => e.Term(f => f.Field(b => b.Name.Suffix("keyword")).Value("韩立")));
var result = await _studentElasticSearchRepository.PageAsync(mustFilters);
```

### 其它操作

- 在 StudentElasticSearchRepository 中使用 Client 即可获取到 es 原生 api 对象。
- 更多示例请查看单元测试(StudentElasticSearchRepositoryTests.cs)
