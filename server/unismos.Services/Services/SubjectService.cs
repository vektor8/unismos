using Serilog;
using unismos.Common.Dtos;
using unismos.Common.Entities;
using unismos.Common.Extensions;
using unismos.Interfaces.ISubject;

namespace unismos.Services.Services;

public class SubjectService : ISubjectService
{
    private readonly ISubjectRepository _subjectRepository;

    public SubjectService(ISubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository;
    }

    /// <summary>
    /// add new subject
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<SubjectDto> AddAsync(NewSubjectDto dto)
    {
        var entity = new Subject
        {
            Id = new Guid(),
            Description = dto.Description,
            Name = dto.Name,
        };
        var response = await _subjectRepository.AddAsync(entity);
        return response.ToDto();
    }

    /// <summary>
    /// get all subjects
    /// </summary>
    /// <returns></returns>
    public async Task<List<SubjectDto>> GetAllAsync()
    {
        var subjects = await _subjectRepository.GetAllAsync();
        Log.Information("Getting all subjects");
        return subjects.Select(e => e.ToDto()).ToList();
    }
}