using Microsoft.EntityFrameworkCore;
using unismos.Common.Entities;
using unismos.Interfaces.ISubject;

namespace unismos.Data.Repositories;

public class SubjectRepository : ISubjectRepository
{
    private readonly DataContext _context;

    public SubjectRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Subject> AddAsync(Subject entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Subject> GetByIdAsync(Guid id)
    {
        var subject = await _context.Subjects.FirstOrDefaultAsync(e => e.Id == id) ?? new NullSubject();
        return subject;
    }

    public async Task<List<Subject>> GetAllAsync()
    {
        var subjects = _context.Subjects;
        return subjects.ToList();
    }
}