using SimpleWallet.Application.DTOs.Base;
using SimpleWallet.Domain.Entities.Base;

namespace SimpleWallet.Application.Interfaces.Base
{
    public interface IBaseService<TEntity, TDto> where TEntity : BaseEntity where TDto : BaseDto
    {
        Task<TDto> GetByIdAsync(int id);
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto> CreateAsync(TEntity entity);
        Task<TDto> UpdateAsync(TEntity entity);
        Task DeleteAsync(int id);
    }
}