using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using unismos.Common.Extensions;
using unismos.Common.ViewModels;
using unismos.Interfaces.ISubject;

namespace unismos.API.Controllers;

[Authorize(Roles = "secretary")]
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

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var currentUser = HttpContext.User;
        Console.WriteLine(currentUser.Claims.FirstOrDefault(e => e.Type == JwtRegisteredClaimNames.Jti)?.Value);
        var subjects = await _subjectService.GetAllAsync();
        return Ok(subjects.Select(e => e.ToViewModel()).ToList());
    }
}