using unismos.Common.Dtos;
using unismos.Common.Entities;
using unismos.Common.ViewModels;

namespace unismos.Common.Extensions;

public static class SubjectExtensions
{
    public static SubjectViewModel ToViewModel(this SubjectDto dto) => new()
    {
        Id = dto.Id,
        Subject = dto.Subject,
        Description = dto.Description
    };

    public static NewSubjectDto ToDto(this NewSubjectViewModel model) => new()
    {
        Name = model.Name,
        Description = model.Description
    };

    public static SubjectDto ToDto(this Subject entity) => entity is NullSubject
        ? new NullSubjectDto()
        : new SubjectDto
        {
            Id = entity.Id,
            Subject = entity.Name,
            Description = entity.Description
        };
}