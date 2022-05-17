using unismos.Common.Dtos;
using unismos.Common.Dtos.Professor;
using unismos.Common.Entities;
using unismos.Common.Extensions;
using unismos.Data;
using unismos.Interfaces.IProfessor;
using unismos.Interfaces.IUser;

namespace unismos.Services.Services;

public class ProfessorService : IProfessorService
{
    private readonly IUserRepository _userRepository;
    private readonly IProfessorRepository _professorRepository;
    private readonly DataContext _context; 

    public ProfessorService(IUserRepository userRepository, IProfessorRepository professorRepository, DataContext context)
    {
        _userRepository = userRepository;
        _professorRepository = professorRepository;
        _context = context;
    }

    public async Task<ProfessorDto> AddAsync(NewProfessorDto dto)
    {
        var user = await _userRepository.GetByUsername(dto.Username);
        if (user is not NullUser) return new NullProfessorDto();
        
        var entity = new Professor
        {
            Id = new Guid(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Username = dto.Username,
            Teachings = new List<Teaching>()
        };
        
        var response = await _professorRepository.AddAsync(entity);
        return response.ToDto();
    }

    public async Task<ProfessorDto> GetByIdAsync(Guid id)
    {
        var entity = await _professorRepository.GetByIdAsync(id);
        var result = entity.ToDto();
        // result.Teachings = _context.Teachings.Where(e => e.Professor.Id == entity.Id).Select(e =>
        //     new ProfessorTeachingDto
        //     {
        //         Id = e.Id,
        //         ExamDate = e.ExamDate,
        //         Subject = e.Subject.ToDto(),
        //         Enrollments = _context.Enrollments.Where(f => f.Teaching.Id == e.Id).Select(f =>
        //             new ProfessorEnrollmentDto
        //             {
        //                 Id = f.Id,
        //                 Grade = f.Grade,
        //                 Review = f.Review,
        //                 Student = new EnrollmentStudentDto
        //                 {
        //                     FirstName = f.Student.FirstName,
        //                     LastName = f.Student.LastName,
        //                     Id = f.Student.Id
        //                 }
        //             }).ToList()
        //     }).ToList();
        return result;
    }

    public async Task<List<ProfessorDto>> GetAllAsync()
    {  
        var professors = (await _professorRepository.GetAllAsync()).Select(e => e.ToDto()).ToList();
        return professors;
    }
}