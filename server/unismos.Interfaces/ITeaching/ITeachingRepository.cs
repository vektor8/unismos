using unismos.Common.Entities;

namespace unismos.Interfaces.ITeaching;

public interface ITeachingRepository
{
    public Task<Teaching> GetByIdAsync(Guid id);
    public Task<Teaching> AddAsync(Teaching entity);
    public Task<List<Teaching>> GetAllAsync();
    public Task<List<Teaching>> GetByProfessorIdAsync(Guid professorId);
    public Task<List<Teaching>> GetBySubjectIdAsync(Guid subjectId);
}