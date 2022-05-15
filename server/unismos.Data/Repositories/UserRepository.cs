using Microsoft.EntityFrameworkCore;
using unismos.Common.Entities;
using unismos.Interfaces.IUser;

namespace unismos.Data.Repositories;

public class UserRepository: IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        var user = await _context.Users
                       .Include(student => ((student as Student)!).TaxesPaid)
                       .Include(student => ((student as Student)!).Enrollments)
                       .FirstOrDefaultAsync(e => e.Id == id) ?? new NullUser();
        return user;
    }

    public async Task<User> GetByUsername(string username)
    {
        var user = await _context.Users
            .Include(student => ((student as Student)!).TaxesPaid)
            .Include(student => ((student as Student)!).Enrollments)
            .FirstOrDefaultAsync(e => e.Username == username) ?? new NullUser();
        return user;
    }
}