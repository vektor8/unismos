using unismos.Common.Dtos;
using unismos.Common.Entities;
using unismos.Common.ViewModels;

namespace unismos.Common.Extensions;

public static class TeachingExtensions
{
    public static NewTeachingDto ToDto(this NewTeachingViewModel model) => new()
    {
        ProfessorId = model.ProfessorId,
        SubjectId = model.SubjectId
    };

    public static TeachingViewModel ToViewModel(this TeachingDto dto) =>
        dto is NullTeachingDto
            ? new NullTeachingViewModel()
            : new TeachingViewModel
            {
                Id = dto.Id,
                Professor = dto.Professor.ToViewModel(),
                Subject = dto.Subject.ToViewModel(),
                ExamDate = dto.ExamDate,
            };

    public static TeachingDto ToDto(this Teaching entity) => entity is NullTeaching
        ? new NullTeachingDto()
        : new TeachingDto
        {
            ExamDate = entity.ExamDate,
            Id = entity.Id,
            Professor = entity.Professor.ToDto(),
            Subject = entity.Subject.ToDto(),
        };
}