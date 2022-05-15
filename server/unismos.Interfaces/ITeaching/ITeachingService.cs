using unismos.Common.Dtos;

namespace unismos.Interfaces.ITeaching;

public interface ITeachingService
{
    public Task<List<TeachingDto>> GetAllAsync();
    public Task<TeachingDto> AddAsync(NewTeachingDto dto);
    public Task<List<TeachingDto>> GetByProfessorIdAsync(Guid professorId);
    public Task<List<TeachingDto>> GetBySubjectIdAsync(Guid subjectId);
    public Task<TeachingDto> ScheduleExam(Guid id, long examDate);
}