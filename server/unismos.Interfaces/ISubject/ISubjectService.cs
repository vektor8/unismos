using unismos.Common.Dtos;

namespace unismos.Interfaces.ISubject;

public interface ISubjectService
{
    public Task<SubjectDto> AddAsync(NewSubjectDto dto);
    public Task<List<SubjectDto>> GetAllAsync();
}