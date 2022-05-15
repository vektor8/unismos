using unismos.Common.Dtos.Payment;

namespace unismos.Interfaces.IPayment;

public interface IPaymentService
{
    public Task<PaymentDto> AddAsync(NewPaymentDto dto);
}