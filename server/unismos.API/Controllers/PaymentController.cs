using Microsoft.AspNetCore.Mvc;
using unismos.Common.Extensions;
using unismos.Common.ViewModels;
using unismos.Common.ViewModels.Payment;
using unismos.Interfaces.IPayment;

namespace unismos.API.Controllers;

[ApiController]
[Route("/api/payments")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] NewPaymentViewModel model)
    {
        var payment = (await _paymentService.AddAsync(model.ToDto())).ToViewModel();
        return payment is NullPaymentViewModel ? BadRequest() : Created("", payment);
    }
}