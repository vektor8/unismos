using unismos.Common.Dtos;
using unismos.Common.Dtos.Professor;
using unismos.Common.Entities;
using unismos.Common.ViewModels;
using unismos.Common.ViewModels.Professor;

namespace unismos.Common.Extensions;

public static class ProfessorExtensions
{
    public static NewProfessorDto ToDto(this NewProfessorViewModel model) => new()
    {
        FirstName = model.FirstName,
        LastName = model.LastName,
        Password = model.Password,
        Username = model.Username,
    };

    public static ProfessorViewModel ToViewModel(this ProfessorDto dto) =>
        dto is NullProfessorDto
            ? new NullProfessorViewModel()
            : new ProfessorViewModel
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Username = dto.Username,
                // Teachings = dto.Teachings.Select(e => new ProfessorTeachingViewModel
                // {
                //     ExamDate = e.ExamDate,
                //     Id = e.Id,
                //     Subject = e.Subject.ToViewModel(),
                //     Enrollments = e.Enrollments.Select(f => new ProfessorEnrollmentViewModel
                //     {
                //         Id = f.Id,
                //         Grade = f.Grade,
                //         Review = f.Review,
                //         Student = new EnrollmentStudentViewModel
                //         {
                //             FirstName = f.Student.FirstName,
                //             Id = f.Student.Id,
                //             LastName = f.Student.LastName,
                //         }
                //     }).ToList()
                // }).ToList()
            };

    public static ProfessorDto ToDto(this Professor entity) => entity is NullProfessor
        ? new NullProfessorDto()
        : new ProfessorDto
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Username = entity.Username,
        };
}