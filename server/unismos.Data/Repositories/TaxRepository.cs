using Microsoft.EntityFrameworkCore;
using unismos.Common.Dtos;
using unismos.Common.Entities;
using unismos.Common.ViewModels.Tax;
using unismos.Interfaces.ITax;

namespace unismos.Data.Repositories;

public class TaxRepository : ITaxRepository
{
    private readonly DataContext _context;

    public TaxRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Tax> AddAsync(Tax entity)
    {
        await _context.Taxes.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<List<Tax>> GetAllAsync()
    {
        return _context.Taxes.ToList();
    }

    public async Task<Tax> GetByIdAsync(Guid id)
    {
        var tax = await _context.Taxes
            .Include(e => e.Payers)
            .FirstOrDefaultAsync(e => e.Id == id) ?? new NullTax();
        return tax;
    }

    public async Task<List<Tax>?> GetByStudentIdAsync(Guid studentId)
    {
        var student = await _context.Users.FirstOrDefaultAsync(e => e.Id == studentId) ?? new NullStudent();
        if (student is NullStudent || student is not Student) return new List<Tax>();
        return (student as Student)?.TaxesPaid;
    }
}