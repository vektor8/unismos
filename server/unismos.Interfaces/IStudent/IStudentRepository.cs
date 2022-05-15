using unismos.Common.Entities;

namespace unismos.Interfaces.IStudent;

public interface IStudentRepository
{
    public Task<Student> GetByIdAsync(Guid id);

    public Task<Student> AddAsync(Student entity);
}