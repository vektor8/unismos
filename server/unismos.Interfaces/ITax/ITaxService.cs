using unismos.Common.Dtos;
using unismos.Common.Dtos.Tax;

namespace unismos.Interfaces.ITax;

public interface ITaxService
{
    public Task<TaxDto> AddAsync(NewTaxDto dto);
    public Task<List<TaxDto>> GetAllAsync();
    public Task<List<TaxDto>> GetByStudentIdAsync(Guid studentId);
}