using Serilog;
using unismos.Common.Dtos;
using unismos.Common.Entities;
using unismos.Common.Extensions;
using unismos.Interfaces.IStudent;
using unismos.Interfaces.IUser;

namespace unismos.Services.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly IUserRepository _userRepository;

    public StudentService(IStudentRepository studentRepository, IUserRepository userRepository)
    {
        _studentRepository = studentRepository;
        _userRepository = userRepository;
    }

    /// <summary>
    /// get student by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<StudentDto> GetByIdAsync(Guid id)
    {
        var student = (await _studentRepository.GetByIdAsync(id)).ToDto();
        Log.Information("Getting student with id {id}", id);
        return student;
    }

    /// <summary>
    /// validate username and add student
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<StudentDto> AddAsync(NewStudentDto dto)
    {
        var user = await _userRepository.GetByUsername(dto.Username);
        if (user is not NullUser)
        {
            Log.Error("Username taken");
            return new NullStudentDto();
        }
        
        var student = new Student
        {
            Id = new Guid(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Enrollments = new List<Enrollment>(),
            Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            TaxesPaid = new List<Tax>(),
            Username = dto.Username
        };
        await _studentRepository.AddAsync(student);
        return student.ToDto();
    }
}