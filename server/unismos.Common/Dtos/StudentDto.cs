namespace unismos.Common.Dtos;

public class StudentDto : UserDto
{
    public List<TaxDto> TaxesPaid { get; set; }
    public List<EnrollmentDto> Enrollments { get; set; }
}