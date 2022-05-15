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

    public async Task<List<SubjectDto>> GetAllAsync()
    {
        var subjects = await _subjectRepository.GetAllAsync();
        return subjects.Select(e => e.ToDto()).ToList();
    }
}