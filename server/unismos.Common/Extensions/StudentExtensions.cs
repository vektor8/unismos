using unismos.Common.Dtos;
using unismos.Common.Entities;
using unismos.Common.ViewModels;
using unismos.Common.ViewModels.Student;

namespace unismos.Common.Extensions;

public static class StudentExtensions
{

    public static NewStudentDto ToDto(this NewStudentViewModel model) => new()
    {
        FirstName = model.FirstName,
        LastName = model.LastName,
        Username = model.Username,
        Password = model.Password
    };
    public static StudentDto ToDto(this Student model) => new()
    {
        Id = model.Id,
        // Enrollments = model.Enrollments.Select(e => e.ToDto()).ToList(),
        FirstName = model.FirstName,
        LastName = model.LastName,
        Username = model.Username,
        // TaxesPaid = model.TaxesPaid.Select(e => e.ToDto()).ToList()
    };

    public static StudentViewModel ToViewModel(this StudentDto dto) => dto is NullStudentDto
        ? new NullStudentViewModel()
        : new StudentViewModel
        {
            Id = dto.Id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Username = dto.Username
        };
}