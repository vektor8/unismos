using unismos.Common.Dtos;
using unismos.Common.Entities;
using unismos.Common.ViewModels.Student;

namespace unismos.Interfaces.IStudent;

public interface IStudentService
{
    public Task<StudentDto> GetByIdAsync(Guid id);

    public Task<StudentDto> AddAsync(NewStudentDto dto);
    
}