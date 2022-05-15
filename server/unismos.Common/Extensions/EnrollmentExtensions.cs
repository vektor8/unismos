using unismos.Common.Dtos;
using unismos.Common.Entities;
using unismos.Common.ViewModels;

namespace unismos.Common.Extensions;

public static class EnrollmentExtensions
{
    public static EnrollmentDto ToDto(this Enrollment model) => model is NullEnrollment
        ? new NullEnrollmentDto()
        : new EnrollmentDto
        {
            Id = model.Id,
            Grade = model.Grade,
            Review = model.Review,
            Student = model.Student.ToDto(),
            Teaching = model.Teaching.ToDto()
        };


    public static NewEnrollmentDto ToDto(this NewEnrollmentViewModel model) => new()
    {
        StudentId = model.StudentId,
        TeachingId = model.TeachingId
    };

    public static EnrollmentViewModel ToViewModel(this EnrollmentDto dto) => dto is NullEnrollmentDto
        ? new NullEnrollmentViewModel()
        : new EnrollmentViewModel
        {
            Id = dto.Id,
            Grade = dto.Grade,
            Student = dto.Student.ToViewModel(),
            Review = dto.Review,
            Teaching = dto.Teaching.ToViewModel()
        };
}