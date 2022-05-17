using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using unismos.Common.Extensions;
using unismos.Common.ViewModels;
using unismos.Interfaces.ISubject;

namespace unismos.API.Controllers;

[Authorize]
[ApiController]
[Route("/api/subjects")]
public class SubjectController : ControllerBase
{
    private readonly ISubjectService _subjectService;

    public SubjectController(ISubjectService subjectService)
    {
        _subjectService = subjectService;
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] NewSubjectViewModel model)
    {
        var subject = (await _subjectService.AddAsync(model.ToDto())).ToViewModel();
        return subject is NullSubjectViewModel ? BadRequest() : Created("", subject);
    }

    // [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var currentUser = HttpContext.User;
        Console.WriteLine(currentUser.Claims);
        Log.Information("Hello, Serilog!");
        var subjects = await _subjectService.GetAllAsync();
        return Ok(subjects.Select(e => e.ToViewModel()).ToList());
    }
}