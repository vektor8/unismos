using unismos.Common.Dtos;
using unismos.Common.Dtos.Tax;
using unismos.Common.Entities;
using unismos.Common.ViewModels.Tax;

namespace unismos.Common.Extensions;

public static class TaxExtensions
{
    public static TaxDto ToDto(this Tax entity) => new TaxDto
    {
        Id = entity.Id,
        Amount = entity.Amount,
        Name = entity.Name
    };

    public static NewTaxDto ToDto(this NewTaxVIewModel model) => new()
    {
        Amount = model.Amount,
        Name = model.Name
    };

    public static TaxViewModel ToViewModel(this TaxDto dto) => new()
    {
        Id = dto.Id,
        Amount = dto.Amount,
        Name = dto.Name
    };
}