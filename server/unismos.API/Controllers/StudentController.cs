using Microsoft.AspNetCore.Mvc;
using unismos.Common.Extensions;
using unismos.Common.ViewModels;
using unismos.Common.ViewModels.Student;
using unismos.Interfaces.IStudent;

namespace unismos.API.Controllers;

[ApiController]
[Route("/api/students")]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] NewStudentViewModel model)
    {
        var student = (await _studentService.AddAsync(model.ToDto())).ToViewModel();
        return student is NullStudentViewModel ? BadRequest() : Created($"/api/students/{student.Id}", student);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var student = (await _studentService.GetByIdAsync(id)).ToViewModel();
        return student is NullStudentViewModel ? NotFound() : Ok(student);
    }
}