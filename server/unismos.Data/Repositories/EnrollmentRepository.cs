using Microsoft.EntityFrameworkCore;
using unismos.Common.Entities;
using unismos.Interfaces.IEnrollment;

namespace unismos.Data.Repositories;

public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly DataContext _context;

    public EnrollmentRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Enrollment> AddAsync(Enrollment entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<List<Enrollment>> GetByStudentIdAsync(Guid studentId)
    {
        var enrollments = _context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Teaching)
            .Where(e => e.Student.Id == studentId)
            .ToList();
        return enrollments;
    }

    public async Task<List<Enrollment>> GetByTeachingIdAsync(Guid teachingId)
    {
        var enrollments = _context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Teaching)
            .Where(e => e.Teaching.Id == teachingId)
            .ToList();
        return enrollments;
    }

    public async Task<Enrollment> GetByIdAsync(Guid id)
    {
        var enrollment = await _context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Teaching)
            .FirstOrDefaultAsync(e => e.Id == id) ?? new NullEnrollment();
        return enrollment;
    }
}