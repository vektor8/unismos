using Microsoft.AspNetCore.Mvc;
using unismos.Common.Extensions;
using unismos.Common.ViewModels;
using unismos.Interfaces.IProfessor;

namespace unismos.API.Controllers;

[ApiController]
[Route("/api/professors")]
public class ProfessorController : ControllerBase
{
    private readonly IProfessorService _professorService;

    public ProfessorController(IProfessorService professorService)
    {
        _professorService = professorService;
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] NewProfessorViewModel model)
    {
        var professor = (await _professorService.AddAsync(model.ToDto())).ToViewModel();
        return professor is NullProfessorViewModel
            ? BadRequest()
            : Created($"/api/professors/{professor.Id}", professor);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var professors = await _professorService.GetAllAsync();
        return Ok(professors.Select(e => e.ToViewModel()));
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        var professor = (await _professorService.GetByIdAsync(id)).ToViewModel();
        return professor is NullProfessorViewModel ? NotFound() : Ok(professor);
    }
}