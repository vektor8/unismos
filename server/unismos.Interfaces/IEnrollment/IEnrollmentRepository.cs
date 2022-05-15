using unismos.Common.Dtos;
using unismos.Common.Entities;

namespace unismos.Interfaces.IEnrollment;

public interface IEnrollmentRepository
{
    public Task<Enrollment> AddAsync(Enrollment entity);

    public Task<List<Enrollment>> GetByStudentIdAsync(Guid studentId);
    public Task<List<Enrollment>> GetByTeachingIdAsync(Guid teachingId);
    public Task<Enrollment> GetByIdAsync(Guid id);
}