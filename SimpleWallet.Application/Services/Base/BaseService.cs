using AutoMapper;
using SimpleWallet.Application.DTOs.Base;
using SimpleWallet.Application.Interfaces.Base;
using SimpleWallet.Domain.Entities.Base;
using SimpleWallet.Infraestructure.Repositories.Interfaces.Base;

namespace SimpleWallet.Application.Services.Base;

public abstract class BaseService<TEntity, TDto>(IMapper mapper, IBaseRepository<TEntity> repository) : IBaseService<TEntity, TDto>
    where TEntity : BaseEntity
    where TDto : BaseDto
{
    protected readonly IMapper _mapper = mapper;
    protected readonly IBaseRepository<TEntity> _repository = repository;

    public async Task<TDto> CreateAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        var createdEntity = await _repository.AddAsync(entity);
        await _repository.SaveChangesAsync();
        if (createdEntity == null)
        {
            throw new InvalidOperationException("Failed to create entity.");
        }
        return _mapper.Map<TDto>(createdEntity);
    }

    public async Task DeleteAsync(int id)
    {
        ArgumentNullException.ThrowIfNull(id);

        var entity = await _repository.GetByIdAsync(id);

        if (entity == null)
        {
            throw new KeyNotFoundException($"Entity with ID {id} not found.");
        }

        await _repository.DeleteAsync(entity);
        await _repository.SaveChangesAsync();
        return;
    }

    public async Task<IEnumerable<TDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return await Task.FromResult(_mapper.Map<IEnumerable<TDto>>(entities));
    }

    public async Task<TDto> GetByIdAsync(int id)
    {
        ArgumentNullException.ThrowIfNull(id);

        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new KeyNotFoundException($"Entity with ID {id} not found.");
        }

        return await Task.FromResult(_mapper.Map<TDto>(entity));
    }

    public async Task<TDto> UpdateAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        var existingEntity = await _repository.GetByIdAsync(entity.Id);
        if (existingEntity == null)
        {
            throw new KeyNotFoundException($"Entity with ID {entity.Id} not found.");
        }

        var updatedEntity = await _repository.UpdateAsync(entity);
        await _repository.SaveChangesAsync();

        return await Task.FromResult(_mapper.Map<TDto>(updatedEntity));
    }
}