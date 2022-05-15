namespace unismos.Common.Dtos;

public class EnrollmentDto
{
    public Guid Id { get; set; }
    public StudentDto Student { get; set; }
    public TeachingDto Teaching { get; set; }
    public int Grade { get; set; }
    public string Review { get; set; }
}