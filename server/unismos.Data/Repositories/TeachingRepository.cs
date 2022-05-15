using Microsoft.EntityFrameworkCore;
using unismos.Common.Entities;
using unismos.Interfaces.ITeaching;

namespace unismos.Data.Repositories;

public class TeachingRepository : ITeachingRepository
{
    private readonly DataContext _context;

    public TeachingRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Teaching> GetByIdAsync(Guid id)
    {
        var teaching =
            await _context.Teachings
                .Include(e => e.Professor)
                .Include(e => e.Subject)
                .FirstOrDefaultAsync(e => e.Id == id) ?? new NullTeaching();
        return teaching;
    }

    public async Task<Teaching> AddAsync(Teaching entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<List<Teaching>> GetAllAsync()
    {
        var teachings = _context.Teachings
            .Include(e => e.Professor)
            .Include(e => e.Subject).ToList();
        return teachings;
    }

    public async Task<List<Teaching>> GetByProfessorIdAsync(Guid professorId)
    {
        var teachings = _context.Teachings
            .Include(e => e.Professor)
            .Include(e => e.Subject)
            .Where(e => e.Professor.Id == professorId).ToList();
        return teachings;
    }

    public async Task<List<Teaching>> GetBySubjectIdAsync(Guid subjectId)
    {
        var teachings = _context.Teachings
            .Include(e => e.Professor)
            .Include(e => e.Subject)
            .Where(e => e.Subject.Id == subjectId).ToList();
        return teachings;
    }
}