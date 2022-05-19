using unismos.Common.Dtos;
using unismos.Common.Entities;
using unismos.Common.ViewModels;

namespace unismos.Common.Extensions;

public static class UserExtensions
{
    public static UserDto ToDto(this User entity)
    {
        switch (entity)
        {
            case Student student:
                return student.ToDto();
            case Professor professor:
                return professor.ToDto();
            case Secretary secretary:
                return secretary.ToDto();
        }

        return new NullUserDto();
    }

    public static LoginUserDto ToDto(this LoginViewModel model) => new()
    {
        Username = model.Username,
        Password = model.Password
    };

    public static LoggedInViewModel ToViewModel(this LoggedInUserDto dto) => dto is NullLoggedInUserDto
        ? new NullLoggedInUserViewModel()
        : new LoggedInViewModel
        {
            Id = dto.Id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Token = dto.Token,
            Username = dto.Username,
            Type = dto.Type
        };
}