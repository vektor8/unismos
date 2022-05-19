using Microsoft.AspNetCore.Mvc;
using unismos.Common.Extensions;
using unismos.Common.ViewModels;
using unismos.Interfaces.IEnrollment;

namespace unismos.API.Controllers;

[ApiController]
[Route("/api/enrollments")]
public class EnrollmentController : ControllerBase
{
    private readonly IEnrollmentService _enrollmentService;

    public EnrollmentController(IEnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] NewEnrollmentViewModel model)
    {
        var enrollment = (await _enrollmentService.AddAsync(model.ToDto())).ToViewModel();
        return enrollment is NullEnrollmentViewModel ? BadRequest() : Created("", enrollment);
    }

    [HttpGet]
    [Route("student/{studentId}")]
    public async Task<IActionResult> GetByStudentIdAsync([FromRoute] Guid studentId)
    {
        var enrollments = (await _enrollmentService.GetByStudentIdAsync(studentId)).Select(e => e.ToViewModel());
        return Ok(enrollments);
    }

    [HttpGet]
    [Route("teaching/{teachingId}")]
    public async Task<IActionResult> GetByTeachingIdAsync([FromRoute] Guid teachingId)
    {
        var enrollments = (await _enrollmentService.GetByTeachingIdAsync(teachingId)).Select(e => e.ToViewModel());
        return Ok(enrollments);
    }

    [HttpPut]
    [Route("grade/{id}")]
    public async Task<IActionResult> GradeEnrollment([FromRoute] Guid id, [FromBody] int grade)
    {
        var enrollment = (await _enrollmentService.GradeAsync(id, grade)).ToViewModel();
        return enrollment is NullEnrollmentViewModel ? BadRequest() : Ok(enrollment);
    }

    [HttpPut]
    [Route("review/{id}")]
    public async Task<IActionResult> GradeEnrollment([FromRoute] Guid id, [FromBody] ReviewViewModel model)
    {
        var enrollment = (await _enrollmentService.ReviewAsync(id, model.Review)).ToViewModel();
        return enrollment is NullEnrollmentViewModel ? BadRequest() : Ok(enrollment);
    }
}

public class ReviewViewModel
{
    public string Review { get; set; }
}