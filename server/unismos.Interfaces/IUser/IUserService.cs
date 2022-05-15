using unismos.Common.Dtos;

namespace unismos.Interfaces.IUser;

public interface IUserService
{
    public Task<UserDto> GetByIdAsync(Guid id);
}