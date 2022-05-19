using Serilog;
using unismos.Common.Dtos;
using unismos.Common.Entities;
using unismos.Common.Extensions;
using unismos.Data;
using unismos.Interfaces.IProfessor;
using unismos.Interfaces.ISubject;
using unismos.Interfaces.ITeaching;

namespace unismos.Services.Services;

public class TeachingService : ITeachingService
{
    private readonly IProfessorRepository _professorRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly ITeachingRepository _teachingRepository;
    private readonly DataContext _context;

    public TeachingService(IProfessorRepository professorRepository, ISubjectRepository subjectRepository,
        ITeachingRepository teachingRepository, DataContext context)
    {
        _professorRepository = professorRepository;
        _subjectRepository = subjectRepository;
        _teachingRepository = teachingRepository;
        _context = context;
    }

    /// <summary>
    /// get all teachings
    /// </summary>
    /// <returns></returns>
    public async Task<List<TeachingDto>> GetAllAsync()
    {
        var teachings = (await _teachingRepository.GetAllAsync()).Select(e => e.ToDto()).ToList();
        Log.Information("Getting all teachings");
        return teachings;
    }

    /// <summary>
    /// add new teaching if valid prof and subject are given
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<TeachingDto> AddAsync(NewTeachingDto dto)
    {
        var professor = await _professorRepository.GetByIdAsync(dto.ProfessorId);
        if (professor is NullProfessor)
        {
            Log.Error("Nonexisting professor id {id}", dto.ProfessorId);
            return new NullTeachingDto();
        }

        var subject = await _subjectRepository.GetByIdAsync(dto.SubjectId);
        if (subject is NullSubject)
        {
            Log.Error("Nonexisting subject id {id}", dto.SubjectId);
            return new NullTeachingDto();
        }

        var teachingExists =
            (await _teachingRepository.GetBySubjectIdAsync(subject.Id)).Any(e => e.Professor.Id == professor.Id);
        if (teachingExists)
        {
            Log.Error("Professor {name} already teaches {subj}", professor.LastName, subject.Name);
            return new NullTeachingDto();
        }
        
        var entity = new Teaching
        {
            ExamDate = 0,
            Id = new Guid(),
            Professor = professor,
            Subject = subject
        };
        await _teachingRepository.AddAsync(entity);
        return entity.ToDto();
    }

    /// <summary>
    /// get all teachings for a prof
    /// </summary>
    /// <param name="professorId"></param>
    /// <returns></returns>
    public async Task<List<TeachingDto>> GetByProfessorIdAsync(Guid professorId)
    {
        var teachings = (await _teachingRepository
            .GetByProfessorIdAsync(professorId)).Select(e => e.ToDto()).ToList();
        Log.Information("Getting teachings for prof with id {id}", professorId);
        return teachings;
    }

    /// <summary>
    /// get all teachings for a student
    /// </summary>
    /// <param name="subjectId"></param>
    /// <returns></returns>
    public async Task<List<TeachingDto>> GetBySubjectIdAsync(Guid subjectId)
    {
        var teachings = (await _teachingRepository
            .GetBySubjectIdAsync(subjectId)).Select(e => e.ToDto()).ToList();
        Log.Information("Getting teachings for subj with id {id}", subjectId);
        return teachings;
    }

    /// <summary>
    /// change examDate for a teaching
    /// </summary>
    /// <param name="id"></param>
    /// <param name="examDate"></param>
    /// <returns></returns>
    public async Task<TeachingDto> ScheduleExam(Guid id, long examDate)
    {
        var teaching = (await _teachingRepository.GetByIdAsync(id));
        if (teaching is NullTeaching)
        {
            Log.Error("Nonexisting teaching");
            return new NullTeachingDto();
        }

        if (examDate < new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds())
        {
            Log.Error("Cannot schedule exam in the past");
            return new NullTeachingDto();
        }
        
        teaching.ExamDate = examDate;
        await _context.SaveChangesAsync();
        return teaching.ToDto();
    }
}