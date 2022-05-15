using unismos.Common.Entities;

namespace unismos.Interfaces.ITax;

public interface ITaxRepository
{
    public Task<Tax> AddAsync(Tax entity);
    public Task<List<Tax>> GetAllAsync();
    public Task<Tax> GetByIdAsync(Guid id);
    public Task<List<Tax>?> GetByStudentIdAsync(Guid studentId);
}