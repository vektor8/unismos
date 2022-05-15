using unismos.Common.Entities;

namespace unismos.Interfaces.IUser;

public interface IUserRepository
{
    public Task<User> GetByIdAsync(Guid id);
    public Task<User> GetByUsername(string username);

}