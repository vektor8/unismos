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

    public async Task<List<TeachingDto>> GetAllAsync()
    {
        var teachings = (await _teachingRepository.GetAllAsync()).Select(e => e.ToDto()).ToList();
        return teachings;
    }

    public async Task<TeachingDto> AddAsync(NewTeachingDto dto)
    {
        var professor = await _professorRepository.GetByIdAsync(dto.ProfessorId);
        if (professor is NullProfessor) return new NullTeachingDto();

        var subject = await _subjectRepository.GetByIdAsync(dto.SubjectId);
        if (subject is NullSubject) return new NullTeachingDto();

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

    public async Task<List<TeachingDto>> GetByProfessorIdAsync(Guid professorId)
    {
        var teachings = (await _teachingRepository
            .GetByProfessorIdAsync(professorId)).Select(e => e.ToDto()).ToList();
        return teachings;
    }

    public async Task<List<TeachingDto>> GetBySubjectIdAsync(Guid subjectId)
    {
        var teachings = (await _teachingRepository
            .GetBySubjectIdAsync(subjectId)).Select(e => e.ToDto()).ToList();
        return teachings;
    }

    public async Task<TeachingDto> ScheduleExam(Guid id, long examDate)
    {
        var teaching = (await _teachingRepository.GetByIdAsync(id));
        if (teaching is NullTeaching) return new NullTeachingDto();
        teaching.ExamDate = examDate;
        await _context.SaveChangesAsync();
        return teaching.ToDto();
    }
}