using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using unismos.Common.Extensions;
using unismos.Common.ViewModels;
using unismos.Interfaces.IEnrollment;

namespace unismos.API.Controllers;

[Authorize]
[ApiController]
[Route("/api/enrollments")]
public class EnrollmentController : ControllerBase
{
    private readonly IEnrollmentService _enrollmentService;

    public EnrollmentController(IEnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }

    [Authorize(Roles = "student")]
    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] NewEnrollmentViewModel model)
    {
        var enrollment = (await _enrollmentService.AddAsync(model.ToDto())).ToViewModel();
        return enrollment is NullEnrollmentViewModel ? BadRequest() : Created("", enrollment);
    }

    [Authorize(Roles = "student")]
    [HttpGet]
    [Route("student/{studentId}")]
    public async Task<IActionResult> GetByStudentIdAsync([FromRoute] Guid studentId)
    {
        var userId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(e => e.Type == JwtRegisteredClaimNames.Jti)
            ?.Value);
        if (userId != studentId) return Forbid();
        
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

    [Authorize(Roles = "professor")]
    [HttpPut]
    [Route("grade/{id}")]
    public async Task<IActionResult> GradeEnrollment([FromRoute] Guid id, [FromBody] GradeViewModel model)
    {
        var enrollment = (await _enrollmentService.GradeAsync(id, model.Grade)).ToViewModel();
        return enrollment is NullEnrollmentViewModel ? BadRequest() : Ok(enrollment);
    }

    [Authorize(Roles = "student")]
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

public class GradeViewModel
{
    public int Grade { get; set; }
}