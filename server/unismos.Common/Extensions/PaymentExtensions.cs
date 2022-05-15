using unismos.Common.Dtos.Payment;
using unismos.Common.ViewModels;
using unismos.Common.ViewModels.Payment;

namespace unismos.Common.Extensions;

public static class PaymentExtensions
{
    public static PaymentViewModel ToViewModel(this PaymentDto dto) => dto is NullPaymentDto
        ? new NullPaymentViewModel()
        : new PaymentViewModel
        {
            Student = dto.Student.ToViewModel(),
            Tax = dto.Tax.ToViewModel()
        };

    public static NewPaymentDto ToDto(this NewPaymentViewModel model) => new()
    {
        StudentId = model.StudentId,
        TaxId = model.TaxId
    };
}