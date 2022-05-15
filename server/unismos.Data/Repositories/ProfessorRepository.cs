using Microsoft.EntityFrameworkCore;
using unismos.Common.Entities;
using unismos.Interfaces.IProfessor;

namespace unismos.Data.Repositories;

public class ProfessorRepository : IProfessorRepository
{
    private readonly DataContext _context;

    public ProfessorRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Professor> AddAsync(Professor entity)
    {
        await _context.Users.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Professor> GetByIdAsync(Guid id)
    {
        var professor =
            await _context.Users.FirstOrDefaultAsync(e => e.Id == id) ??
            new NullProfessor();
        if (professor is NullProfessor or not Professor) return new NullProfessor();
        var prof = professor as Professor;
        return prof;
    }

    public async Task<List<Professor>> GetAllAsync()
    {
        var professors = _context.Users.Where(e => e is Professor).Select(e => e as Professor).ToList();
        return professors;
    }
}