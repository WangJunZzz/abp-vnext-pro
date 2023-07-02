using Lion.AbpPro.ElasticSearch.Exceptions;
using Lion.AbpPro.ElasticSearch.Students;
using Nest;
using Shouldly;

namespace Lion.AbpPro.ElasticSearch
{
    public sealed class StudentElasticSearchRepositoryTests : AbpProElasticSearchTestBase
    {
        private readonly IStudentElasticSearchRepository _studentElasticSearchRepository;


        public StudentElasticSearchRepositoryTests()
        {
            _studentElasticSearchRepository = GetRequiredService<IStudentElasticSearchRepository>();
        }

        [Fact]
        public async Task FindAsync_Should_Find_Student()
        {
            // Arrange
            var student = new Student
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                Age = 10,
                CreationTime = DateTime.Now,
                Price = 100.3,
                Gender = Gender.Man
            };
            await _studentElasticSearchRepository.InsertAsync(student);

            // Act
            var result = await _studentElasticSearchRepository.FindAsync(student.Id);

            // Assert
            result.ShouldNotBeNull();
        }

        [Fact]
        public async Task GetAsync_Should_Exception()
        {
            await Should.ThrowAsync<AbpProElasticSearchEntityNotFoundException>(async () => { await _studentElasticSearchRepository.GetAsync(Guid.NewGuid()); });
        }

        [Fact]
        public async Task InsertAsync_Should_Insert_Student()
        {
            // Arrange
            var student = new Student
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                Age = 10,
                CreationTime = DateTime.Now,
                Price = 100.3,
                Gender = Gender.Man
            };

            // Act
            await _studentElasticSearchRepository.InsertAsync(student);

            // Assert
            var result = await _studentElasticSearchRepository.FindAsync(student.Id);
            result.ShouldNotBeNull();
        }

        [Fact]
        public async Task InsertAsync_Should_RepeatInsert_Student()
        {
            // Arrange
            var student = new Student
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                Age = 10,
                CreationTime = DateTime.Now,
                Price = 100.3,
                Gender = Gender.Man
            };

            // Act
            await _studentElasticSearchRepository.InsertAsync(student);

            // Assert
            var result = await _studentElasticSearchRepository.FindAsync(student.Id);
            result.ShouldNotBeNull();

            // Act
            student.Name = "abp";
            student.Age = 20;
            student.Gender = Gender.WoMan;
            await _studentElasticSearchRepository.InsertAsync(student);

            // Assert
            var result1 = await _studentElasticSearchRepository.FindAsync(student.Id);
            result1.ShouldNotBeNull();
            result1.Name.ShouldBe(student.Name);
            result1.Age.ShouldBe(student.Age);
            result1.Gender.ShouldBe(student.Gender);
            result1.Price.ShouldBe(student.Price);
        }

        [Fact]
        public async Task InsertManyAsync_Should_Insert_Student()
        {
            // Arrange
            var students = new List<Student>
            {
                new Student
                {
                    Id = Guid.NewGuid(),
                    Name = "John Doe",
                    Age = 10,
                    CreationTime = DateTime.Now,
                    Price = 100.3,
                    Gender = Gender.Man
                },
                new Student
                {
                    Id = Guid.NewGuid(),
                    Name = "John Wang",
                    Age = 10,
                    CreationTime = DateTime.Now,
                    Price = 100.3,
                    Gender = Gender.WoMan
                }
            };
            // Act
            await _studentElasticSearchRepository.InsertManyAsync(students);

            // Assert
            foreach (var student in students)
            {
                var result = await _studentElasticSearchRepository.FindAsync(student.Id);
                result.ShouldNotBeNull();
            }
        }

        [Fact]
        public async Task DeleteAsync_Should_Delete_Student()
        {
            // Arrange
            var student = new Student
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                Age = 10,
                CreationTime = DateTime.Now,
                Price = 100.3,
                Gender = Gender.Man
            };
            await _studentElasticSearchRepository.InsertAsync(student);

            // Act
            await _studentElasticSearchRepository.DeleteAsync(student.Id);

            // Assert
            var result = await _studentElasticSearchRepository.FindAsync(student.Id);
            result.ShouldBeNull();
        }

        [Fact]
        public async Task UpdateAsync_Should_Update_Student()
        {
            // Arrange
            var student = new Student
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                Age = 10,
                CreationTime = DateTime.Now,
                Price = 100.3,
                Gender = Gender.Man
            };
            await _studentElasticSearchRepository.InsertAsync(student);

            // Act
            student.Name = "update";
            student.Age = 20;
            await _studentElasticSearchRepository.UpdateAsync(student);

            // Assert
            var exiStudent = await _studentElasticSearchRepository.FindAsync(student.Id);
            exiStudent.ShouldNotBeNull();
            exiStudent.Name.ShouldBe(student.Name);
            exiStudent.Age.ShouldBe(student.Age);
        }

        [Fact(DisplayName = "DataRange时间范围查询")]
        public async Task PageAsync_Should_Return_Students()
        {
            // Arrange
            var students = new List<Student>
            {
                new Student
                {
                    Id = Guid.NewGuid(),
                    Name = "韩立",
                    Age = 10,
                    CreationTime = DateTime.Now,
                    Price = 100.1,
                    Gender = Gender.Man
                },
                new Student
                {
                    Id = Guid.NewGuid(),
                    Name = "南宫婉",
                    Age = 18,
                    CreationTime = DateTime.Now.AddDays(-1),
                    Price = 100.2,
                    Gender = Gender.WoMan
                },
                new Student
                {
                    Id = Guid.NewGuid(),
                    Name = "Test001",
                    Age = 19,
                    CreationTime = DateTime.Now,
                    Price = 100,
                    Gender = Gender.WoMan
                },
                new Student
                {
                    Id = Guid.NewGuid(),
                    Name = "Test",
                    Age = 10,
                    CreationTime = DateTime.Now.AddDays(-10),
                    Price = 100,
                    Gender = Gender.WoMan
                }
            };
            // Act
            await _studentElasticSearchRepository.InsertManyAsync(students);
            var TimeZone = "Asia/Shanghai";
            var mustFilters = new List<Func<QueryContainerDescriptor<Student>, QueryContainer>>();

            // // 查询日期
            // mustFilters.Add(e =>
            //     e.DateRange(f =>
            //         f.Field(fd => fd.CreationTime)
            //             .TimeZone(TimeZone)
            //             // 小于等于LessThanOrEquals
            //             // 大于等于GreaterThanOrEquals
            //             .GreaterThanOrEquals(DateTime.Now.AddDays(-1))));
            //
            // 查询日期区间  3天之前到现在
            mustFilters.Add(a => a
                .Bool(b => b
                    .Must(
                        m => m.DateRange(r => r.Field(f => f.CreationTime).TimeZone(TimeZone).GreaterThanOrEquals(DateTime.Now.AddDays(-3))),
                        m => m.DateRange(r => r.Field(f => f.CreationTime).TimeZone(TimeZone).LessThanOrEquals(DateTime.Now))
                    )
                )
            );

            var query = new QueryContainerDescriptor<Student>();
            // https://zhuanlan.zhihu.com/p/592767668

            // Act
            var result = await _studentElasticSearchRepository.PageAsync(mustFilters);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Item1 >= 0);
            Assert.NotNull(result.Item2);
        }

        [Fact(DisplayName = "Term精准查询")]
        public async Task PageAsync_Term_Should_OK()
        {
            // Arrange
            var students = new List<Student>
            {
                new Student
                {
                    Id = Guid.NewGuid(),
                    Name = "韩立",
                    Age = 10,
                    CreationTime = DateTime.Now,
                    Price = 100.1,
                    Gender = Gender.Man
                },
            };
            await _studentElasticSearchRepository.InsertManyAsync(students);
            // Act
            var mustFilters = new List<Func<QueryContainerDescriptor<Student>, QueryContainer>>();
            // 因为text类型会自动生成keyword类型,所以此时这样可以查询出来
            mustFilters.Add(e => e.Term(f => f.Field(b => b.Name.Suffix("keyword")).Value("韩立")));
            // Act
            var result = await _studentElasticSearchRepository.PageAsync(mustFilters);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Item1 >= 0);
            Assert.NotNull(result.Item2);

            // Act
            var mustFilters1 = new List<Func<QueryContainerDescriptor<Student>, QueryContainer>>();
            // 如果name是中文是无法查询到的，text类型会自动分词
            mustFilters1.Add(e => e.Term(f => f.Field(b => b.Name).Value("韩立")));
            // Act
            var result1 = await _studentElasticSearchRepository.PageAsync(mustFilters1);

            Assert.True(result1.Item1 == 0);
            Assert.True(result1.Item2.Count == 0);
        }

        [Fact(DisplayName = "Wildcard模糊查询")]
        public async Task PageAsync_Wildcard_Should_OK()
        {
            // Arrange
            var students = new List<Student>
            {
                new Student
                {
                    Id = Guid.NewGuid(),
                    Name = "Mock",
                    Age = 10,
                    CreationTime = DateTime.Now,
                    Price = 100.1,
                    Gender = Gender.Man
                },
            };
            await _studentElasticSearchRepository.InsertManyAsync(students);

            // Act
            var mustFilters = new List<Func<QueryContainerDescriptor<Student>, QueryContainer>>();
            // * 代表匹配多个字符
            // ？代表匹配单个字符
            mustFilters.Add(e => e.Wildcard(f => f.Field(b => b.Name).Value("Moc?")));
            // Act
            var result = await _studentElasticSearchRepository.PageAsync(mustFilters);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Item1 >= 0);
            Assert.NotNull(result.Item2);
        }

        [Fact(DisplayName = "Match查询")]
        public async Task PageAsync_Match_Should_OK()
        {
            // Arrange
            var students = new List<Student>
            {
                new Student
                {
                    Id = Guid.NewGuid(),
                    //Name = "杀人防火历飞雨，万人敬仰韩天尊",
                    Name = "Student1",
                    Age = 10,
                    CreationTime = DateTime.Now,
                    Price = 100.1,
                    Gender = Gender.Man
                },
            };
            await _studentElasticSearchRepository.InsertManyAsync(students);

            // Act
            var mustFilters = new List<Func<QueryContainerDescriptor<Student>, QueryContainer>>();

            // Student1 不会进行分词, 所以参数是Student 或者 1 都会查询不到数据
            mustFilters.Add(e => e.Match(f => f.Field(b => b.Name).Query("Student")));
            // Act
            var result = await _studentElasticSearchRepository.PageAsync(mustFilters);

            // Assert
            Assert.True(result.Item1 == 0);
            Assert.True(result.Item2.Count == 0);
        }

        [Fact(DisplayName = "数值区间查询")]
        public async Task PageAsync_Range_Should_OK()
        {
            // Arrange
            var students = new List<Student>
            {
                new Student
                {
                    Id = Guid.NewGuid(),
                    Name = "My Name Is Match",
                    Age = 10,
                    CreationTime = DateTime.Now,
                    Price = 100.1,
                    Gender = Gender.Man
                },
            };
            await _studentElasticSearchRepository.InsertManyAsync(students);

            // Act
            var mustFilters = new List<Func<QueryContainerDescriptor<Student>, QueryContainer>>();

            // 查询数值区间
            mustFilters.Add(a => a
                .Bool(b => b
                    .Must(
                        m => m.Range(r => r.Field(f => f.Age).GreaterThanOrEquals(0)),
                        m => m.Range(r => r.Field(f => f.Age).LessThanOrEquals(20))
                    )
                )
            );
            // Act
            var result = await _studentElasticSearchRepository.PageAsync(mustFilters);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Item1 >= 0);
            Assert.NotNull(result.Item2);
        }
    }
}