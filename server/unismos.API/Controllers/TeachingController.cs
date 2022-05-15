using Microsoft.AspNetCore.Mvc;
using unismos.Common.Extensions;
using unismos.Common.ViewModels;
using unismos.Interfaces.ITeaching;

namespace unismos.API.Controllers;

[ApiController]
[Route("/api/teachings")]
public class TeachingController : ControllerBase
{
    private readonly ITeachingService _teachingService;

    public TeachingController(ITeachingService teachingService)
    {
        _teachingService = teachingService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var teachings = (await _teachingService.GetAllAsync()).Select(e => e.ToViewModel()).ToList();
        return Ok(teachings);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] NewTeachingViewModel model)
    {
        var teaching = (await _teachingService.AddAsync(model.ToDto())).ToViewModel();
        return teaching is NullTeachingViewModel ? BadRequest() : Created("", teaching);
    }

    [HttpGet]
    [Route("professor/{professorId}")]
    public async Task<IActionResult> GetByProfessorIdAsync([FromRoute] Guid professorId)
    {
        var teachings = (await _teachingService.GetByProfessorIdAsync(professorId))
            .Select(e => e.ToViewModel())
            .ToList();
        return Ok(teachings);
    }
    
    [HttpGet]
    [Route("subject/{subjectId}")]
    public async Task<IActionResult> GetBySubjectIdAsync([FromRoute] Guid subjectId)
    {
        var teachings = (await _teachingService.GetBySubjectIdAsync(subjectId))
            .Select(e => e.ToViewModel())
            .ToList();
        return Ok(teachings);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> ScheduleExam([FromRoute] Guid id, [FromBody] long examDate)
    {
        var teaching = (await _teachingService.ScheduleExam(id, examDate)).ToViewModel();
        return teaching is NullTeachingViewModel ? BadRequest() : Ok(teaching);
    }
}