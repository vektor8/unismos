using unismos.Common.Dtos.Payment;
using unismos.Common.Entities;
using unismos.Common.Extensions;
using unismos.Data;
using unismos.Interfaces.IPayment;
using unismos.Interfaces.IStudent;
using unismos.Interfaces.ITax;

namespace unismos.Services.Services;

public class PaymentService : IPaymentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly ITaxRepository _taxRepository;
    private readonly DataContext _context;

    public PaymentService(IStudentRepository studentRepository, ITaxRepository taxRepository, DataContext context)
    {
        _studentRepository = studentRepository;
        _taxRepository = taxRepository;
        _context = context;
    }

    public async Task<PaymentDto> AddAsync(NewPaymentDto dto)
    {
        var student = await _studentRepository.GetByIdAsync(dto.StudentId);
        if (student is NullStudent) return new NullPaymentDto();

        var tax = await _taxRepository.GetByIdAsync(dto.TaxId);
        if (tax is NullTax) return new NullPaymentDto();

        student.TaxesPaid.Add(tax);
        tax.Payers.Add(student);
        await _context.SaveChangesAsync();
        return new PaymentDto
        {
            Student = student.ToDto(),
            Tax = tax.ToDto()
        };
    }
}