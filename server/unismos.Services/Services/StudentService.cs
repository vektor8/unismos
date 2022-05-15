using unismos.Common.Dtos;
using unismos.Common.Entities;
using unismos.Common.Extensions;
using unismos.Common.ViewModels.Student;
using unismos.Interfaces.IStudent;

namespace unismos.Services.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;

    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<StudentDto> GetByIdAsync(Guid id)
    {
        var student = (await _studentRepository.GetByIdAsync(id)).ToDto();
        return student;
    }

    public async Task<StudentDto> AddAsync(NewStudentDto dto)
    {
        var student = new Student
        {
            Id = new Guid(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Enrollments = new List<Enrollment>(),
            Password = dto.Password,
            TaxesPaid = new List<Tax>(),
            Username = dto.Username
        };
        await _studentRepository.AddAsync(student);
        return student.ToDto();
    }
}