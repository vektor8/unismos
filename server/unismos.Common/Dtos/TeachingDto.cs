using unismos.Common.Dtos.Professor;

namespace unismos.Common.Dtos;

public class TeachingDto
{
    public Guid Id { get; set; }
    public ProfessorDto Professor { get; set; }
    public SubjectDto Subject { get; set; }
    public long ExamDate { get; set; }
}