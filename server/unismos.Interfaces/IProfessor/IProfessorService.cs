using unismos.Common.Dtos;
using unismos.Common.Entities;

namespace unismos.Interfaces.IProfessor;

public interface IProfessorService
{
    public Task<ProfessorDto> AddAsync(NewProfessorDto dto);
    public Task<ProfessorDto> GetByIdAsync(Guid id);
    public Task<List<ProfessorDto>> GetAllAsync();
}