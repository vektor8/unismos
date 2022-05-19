using Serilog;
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

    /// <summary>
    /// add a new enrollment based on a given student id and teaching id
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<EnrollmentDto> AddAsync(NewEnrollmentDto dto)
    {
        var student = await _studentRepository.GetByIdAsync(dto.StudentId);
        if (student is NullStudent)
        {
            Log.Error("Cannot enroll unexisting student {id}", dto.StudentId);
            return new NullEnrollmentDto();
        }

        var teaching = await _teachingRepository.GetByIdAsync(dto.TeachingId);
        if (teaching is NullTeaching)
        {
            Log.Error("Cannot enroll student in unexisting teaching {id}", dto.TeachingId);
            return new NullEnrollmentDto();
        }


        var enrollment = (await _enrollmentRepository
                .GetByStudentIdAsync(student.Id))
            .FirstOrDefault(e =>
                e.Teaching.Subject.Id == teaching.Subject.Id) ?? new NullEnrollment();

        if (enrollment is NullEnrollment)
        {
            Log.Error("Student is already enrolled");
            return new NullEnrollmentDto();
        }

        if (enrollment.Teaching.ExamDate != 0 &&
            enrollment.Teaching.ExamDate < new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds())
        {
            Log.Error("Cannot enroll if exam took place already");
            return new NullEnrollmentDto();
        }

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

    /// <summary>
    /// get all enrollments for a given student
    /// </summary>
    /// <param name="studentId"></param>
    /// <returns></returns>
    public async Task<List<EnrollmentDto>> GetByStudentIdAsync(Guid studentId)
    {
        var enrollments = (await _enrollmentRepository.GetByStudentIdAsync(studentId)).Select(e => e.ToDto()).ToList();
        Log.Information("Getting all enrollments by student {id}", studentId);
        return enrollments;
    }

    /// <summary>
    /// get all enrollments for a teaching
    /// </summary>
    /// <param name="teachingId"></param>
    /// <returns></returns>
    public async Task<List<EnrollmentDto>> GetByTeachingIdAsync(Guid teachingId)
    {
        var enrollments = (await _enrollmentRepository.GetByTeachingIdAsync(teachingId)).Select(e => e.ToDto())
            .ToList();
        Log.Information("Getting all enrollments by teaching {id}", teachingId);
        return enrollments;
    }

    /// <summary>
    /// validate parameter grade and update the enrollment accordingly
    /// </summary>
    /// <param name="id"></param>
    /// <param name="grade"></param>
    /// <returns></returns>
    public async Task<EnrollmentDto> GradeAsync(Guid id, int grade)
    {
        var enrollment = await _enrollmentRepository.GetByIdAsync(id);
        if (enrollment is NullEnrollment || enrollment.Grade != 0 || grade <= 0 || grade > 10)
        {
            Log.Error("Invalid enrollment id or invalid grade");
            return new NullEnrollmentDto();
        }

        enrollment.Grade = grade;
        await _context.SaveChangesAsync();
        return enrollment.ToDto();
    }

    /// <summary>
    /// validate review and update enrollment accordingly
    /// </summary>
    /// <param name="id"></param>
    /// <param name="review"></param>
    /// <returns></returns>
    public async Task<EnrollmentDto> ReviewAsync(Guid id, string review)
    {
        if (string.IsNullOrWhiteSpace(review))
        {
            Log.Error("Empty review");
            return new NullEnrollmentDto();
        }

        var enrollment = await _enrollmentRepository.GetByIdAsync(id);
        if (enrollment is NullEnrollment || enrollment.Grade != 0)
        {
            Log.Error("Unexisting enrollment or grade, or grade already there");
            return new NullEnrollmentDto();
        }

        enrollment.Review = review;
        await _context.SaveChangesAsync();
        return enrollment.ToDto();
    }
}