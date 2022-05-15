using unismos.Common.Dtos;
using unismos.Common.Dtos.Tax;
using unismos.Common.Entities;
using unismos.Common.Extensions;
using unismos.Interfaces.ITax;

namespace unismos.Services.Services;

public class TaxService : ITaxService
{
    private readonly ITaxRepository _taxRepository;

    public TaxService(ITaxRepository taxRepository)
    {
        _taxRepository = taxRepository;
    }

    public async Task<TaxDto> AddAsync(NewTaxDto dto)
    {
        var tax = new Tax
        {
            Id = new Guid(),
            Name = dto.Name,
            Amount = dto.Amount,
            Payers = new List<Student>()
        };
        await _taxRepository.AddAsync(tax);
        return tax.ToDto();
    }

    public async Task<List<TaxDto>> GetAllAsync()
    {
        return (await _taxRepository.GetAllAsync()).Select(e => e.ToDto()).ToList();
    }

    public async Task<List<TaxDto>> GetByStudentIdAsync(Guid studentId)
    {
        return (await _taxRepository.GetByStudentIdAsync(studentId)).Select(e => e.ToDto()).ToList();
    }
}