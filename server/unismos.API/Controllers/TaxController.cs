using Microsoft.AspNetCore.Mvc;
using unismos.Common.Extensions;
using unismos.Common.ViewModels.Tax;
using unismos.Interfaces.ITax;

namespace unismos.API.Controllers;

[ApiController]
[Route("/api/taxes")]
public class TaxController : ControllerBase
{
    private readonly ITaxService _taxService;

    public TaxController(ITaxService taxService)
    {
        _taxService = taxService;
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] NewTaxVIewModel model)
    {
        var tax = (await _taxService.AddAsync(model.ToDto())).ToViewModel();
        return tax is NullTaxViewModel ? BadRequest() : Ok(tax);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var taxes = (await _taxService.GetAllAsync()).Select(e => e.ToViewModel()).ToList();
        return Ok(taxes);
    }

    [HttpGet]
    [Route("student/{studentId}")]
    public async Task<IActionResult> GetByStudentId([FromRoute] Guid studentId)
    {
        var taxes = (await _taxService.GetByStudentIdAsync(studentId)).Select(e => e.ToViewModel()).ToList();
        return Ok(taxes);
    }
}