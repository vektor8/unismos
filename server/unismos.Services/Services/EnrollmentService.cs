using unismos.Common.Dtos;
using unismos.Common.Entities;
using unismos.Common.Extensions;
using unismos.Data;
using unismos.Interfaces.IEnrollment;
using unismos.Interfaces.IStudent;
using unismos.Interfaces.ITeaching;

namespace unismos.Services.Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly ITeachingRepository _teachingRepository;
    private readonly DataContext _context;

    public EnrollmentService(IEnrollmentRepository enrollmentRepository, IStudentRepository studentRepository,
        ITeachingRepository teachingRepository, DataContext context)
    {
        _enrollmentRepository = enrollmentRepository;
        _studentRepository = studentRepository;
        _teachingRepository = teachingRepository;
        _context = context;
    }

    public async Task<EnrollmentDto> AddAsync(NewEnrollmentDto dto)
    {
        var student = await _studentRepository.GetByIdAsync(dto.StudentId);
        if (student is NullStudent) return new NullEnrollmentDto();

        var teaching = await _teachingRepository.GetByIdAsync(dto.TeachingId);
        if (teaching is NullTeaching) return new NullEnrollmentDto();

        var entity = new Enrollment
        {
            Grade = 0,
            Id = new Guid(),
            Review = "",
            Student = student,
            Teaching = teaching
        };
        var response = await _enrollmentRepository.AddAsync(entity);
        return response.ToDto();
    }

    public async Task<List<EnrollmentDto>> GetByStudentIdAsync(Guid studentId)
    {
        var enrollments = (await _enrollmentRepository.GetByStudentIdAsync(studentId)).Select(e => e.ToDto()).ToList();
        return enrollments;
    }

    public async Task<List<EnrollmentDto>> GetByTeachingIdAsync(Guid teachingId)
    {
        var enrollments = (await _enrollmentRepository.GetByTeachingIdAsync(teachingId)).Select(e => e.ToDto()).ToList();
        return enrollments;
    }

    public async Task<EnrollmentDto> GradeAsync(Guid id, int grade)
    {
        var enrollment = await _enrollmentRepository.GetByIdAsync(id);
        if (enrollment is NullEnrollment || enrollment.Grade != 0 || grade < 0) return new NullEnrollmentDto();
        enrollment.Grade = grade;
        await _context.SaveChangesAsync();
        return enrollment.ToDto();
    }

    public async Task<EnrollmentDto> ReviewAsync(Guid id, string review)
    {
        var enrollment = await _enrollmentRepository.GetByIdAsync(id);
        if (enrollment is NullEnrollment || enrollment.Grade != 0) return new NullEnrollmentDto();
        enrollment.Review = review;
        await _context.SaveChangesAsync();
        return enrollment.ToDto();
    }
}