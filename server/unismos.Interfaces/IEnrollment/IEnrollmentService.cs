using unismos.Common.Dtos;

namespace unismos.Interfaces.IEnrollment;

public interface IEnrollmentService
{
    public Task<EnrollmentDto> AddAsync(NewEnrollmentDto dto);
    public Task<List<EnrollmentDto>> GetByStudentIdAsync(Guid studentId);
    public Task<List<EnrollmentDto>> GetByTeachingIdAsync(Guid teachingId);
    public Task<EnrollmentDto> GradeAsync(Guid id, int grade);
    public Task<EnrollmentDto> ReviewAsync(Guid id, string review);
}