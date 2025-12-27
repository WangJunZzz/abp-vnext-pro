using Lion.AbpPro.Ddd.Application.Contracts;
using Mapster;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Lion.AbpPro.Ddd.Application
{
    /// <summary>
    /// CRUD应用服务基类 
    /// </summary>
    public abstract class AbpProCrudAppService<TEntity, TEntityDto, TKey>
        : AbpProCrudAppService<TEntity, TEntityDto, TKey, PagedAndSortedResultRequestDto>
        where TEntity : class, IEntity<TKey>
        where TEntityDto : IEntityDto<TKey>
    {
        protected AbpProCrudAppService(IRepository<TEntity, TKey> repository)
            : base(repository)
        {
        }
    }

    /// <summary>
    /// CRUD应用服务基类
    /// </summary>
    public abstract class AbpProCrudAppService<TEntity, TEntityDto, TKey, TGetListInput>
        : AbpProCrudAppService<TEntity, TEntityDto, TKey, TGetListInput, TEntityDto>
        where TEntity : class, IEntity<TKey>
        where TEntityDto : IEntityDto<TKey>
    {
        protected AbpProCrudAppService(IRepository<TEntity, TKey> repository)
            : base(repository)
        {
        }
    }

    /// <summary>
    /// CRUD应用服务基类
    /// </summary>
    public abstract class AbpProCrudAppService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput>
        : AbpProCrudAppService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput, TCreateInput>
        where TEntity : class, IEntity<TKey>
        where TEntityDto : IEntityDto<TKey>
    {
        protected AbpProCrudAppService(IRepository<TEntity, TKey> repository)
            : base(repository)
        {
        }
    }

    /// <summary>
    /// CRUD应用服务基类
    /// </summary>
    public abstract class AbpProCrudAppService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
        : AbpProCrudAppService<TEntity, TEntityDto, TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
        where TEntity : class, IEntity<TKey>
        where TEntityDto : IEntityDto<TKey>
    {
        protected AbpProCrudAppService(IRepository<TEntity, TKey> repository)
            : base(repository)
        {
        }
    }

    /// <summary>
    /// CRUD应用服务基类
    /// </summary>
    public abstract class AbpProCrudAppService<TEntity, TGetOutputDto, TGetListOutputDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
        : CrudAppService<TEntity, TGetOutputDto, TGetListOutputDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
        where TEntity : class, IEntity<TKey>
        where TGetOutputDto : IEntityDto<TKey>
        where TGetListOutputDto : IEntityDto<TKey>
    {
        protected AbpProCrudAppService(IRepository<TEntity, TKey> repository)
            : base(repository)
        {
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="id">实体ID</param>
        /// <param name="input">更新输入</param>
        /// <returns>更新后的实体DTO</returns>
       // [HttpPost("Update")]
        public override async Task<TGetOutputDto> UpdateAsync(TKey id, TUpdateInput input)
        {
            // 检查更新权限
            await CheckUpdatePolicyAsync();

            // 获取并验证实体
            var entity = await GetEntityByIdAsync(id);

            // 检查更新输入
            await CheckUpdateInputDtoAsync(entity, input);

            // 映射并更新实体
            await MapToEntityAsync(input, entity);
            await Repository.UpdateAsync(entity, autoSave: true);

            return await MapToGetOutputDtoAsync(entity);
        }

        /// <summary>
        /// 检查更新输入数据的有效性
        /// </summary>
        protected virtual Task CheckUpdateInputDtoAsync(TEntity entity, TUpdateInput input)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 创建实体
        /// </summary>
        /// <param name="input">创建输入</param>
        /// <returns>创建后的实体DTO</returns>
       // [HttpPost("Create")]
        public override async Task<TGetOutputDto> CreateAsync(TCreateInput input)
        {
            // 检查创建权限
            await CheckCreatePolicyAsync();

            // 检查创建输入
            await CheckCreateInputDtoAsync(input);

            // 映射到实体
            var entity = await MapToEntityAsync(input);

            // 设置租户ID
            TryToSetTenantId(entity);

            // 插入实体
            await Repository.InsertAsync(entity, autoSave: true);

            return await MapToGetOutputDtoAsync(entity);
        }

        /// <summary>
        /// 检查创建输入数据的有效性
        /// </summary>
        protected virtual Task CheckCreateInputDtoAsync(TCreateInput input)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="input">查询输入</param>
        /// <returns>分页结果</returns>
       // [HttpPost("Page")]
        public override async Task<PagedResultDto<TGetListOutputDto>> GetListAsync(TGetListInput input)
        {
            List<TEntity> entities;

            // 根据输入类型决定查询方式
            if (input is IAbpProPagedInput pagedInput)
            {
                // 分页查询
                entities = await Repository.GetPagedListAsync(
                    pagedInput.SkipCount,
                    pagedInput.MaxResultCount,
                    pagedInput.Sorting.IsNullOrWhiteSpace() ? string.Empty : pagedInput.Sorting
                );
            }
            else
            {
                // 查询全部
                entities = await Repository.GetListAsync();
            }

            // 获取总数并映射结果
            var totalCount = await Repository.GetCountAsync();
            var dtos = await MapToGetListOutputDtosAsync(entities);

            return new PagedResultDto<TGetListOutputDto>(totalCount, dtos);
        }
        
        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <param name="ids">实体ID集合</param>
       // [HttpPost("BatchDelete")]
        public virtual async Task DeleteAsync(IEnumerable<TKey> ids)
        {
            await Repository.DeleteManyAsync(ids);
        }

        /// <summary>
        /// 单个删除实体
        /// </summary>
       // [HttpPost("Delete")]
        public override Task DeleteAsync(TKey id)
        {
            return base.DeleteAsync(id);
        }

        protected override TEntity MapToEntity(TCreateInput createInput)
        {
            return createInput.Adapt<TEntity>();
        }


        protected override void MapToEntity(TUpdateInput updateInput, TEntity entity)
        {
             updateInput.Adapt(entity);
        }

        protected override TGetOutputDto MapToGetOutputDto(TEntity entity)
        {
            return entity.Adapt<TGetOutputDto>();
        }

        protected override TGetListOutputDto MapToGetListOutputDto(TEntity entity)
        {
            return entity.Adapt<TGetListOutputDto>();
        }
        
    }
}