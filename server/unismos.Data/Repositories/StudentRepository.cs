using Microsoft.EntityFrameworkCore;
using unismos.Common.Entities;
using unismos.Interfaces.IStudent;

namespace unismos.Data.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly DataContext _context;

    public StudentRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Student> GetByIdAsync(Guid id)
    {
        var student = await _context.Users.FirstOrDefaultAsync(e => e.Id == id) ?? new NullStudent();
        return student is Student ? student as Student : new NullStudent();
    }

    public async Task<Student> AddAsync(Student entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}