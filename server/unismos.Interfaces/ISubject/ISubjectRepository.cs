using unismos.Common.Entities;

namespace unismos.Interfaces.ISubject;

public interface ISubjectRepository
{
    public Task<Subject> AddAsync(Subject entity);
    public Task<Subject> GetByIdAsync(Guid id);
    public Task<List<Subject>> GetAllAsync();
}