using unismos.Common.Dtos;
using unismos.Common.Entities;

namespace unismos.Interfaces.IProfessor;

public interface IProfessorRepository
{
    public Task<Professor> AddAsync(Professor entity);
    public Task<Professor> GetByIdAsync(Guid id);
    public Task<List<Professor>> GetAllAsync();
}