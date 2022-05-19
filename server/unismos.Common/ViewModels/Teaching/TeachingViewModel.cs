using unismos.Common.ViewModels.Professor;

namespace unismos.Common.ViewModels;

public class TeachingViewModel
{
    public Guid Id { get; set; }
    public ProfessorViewModel Professor { get; set; }
    public SubjectViewModel Subject { get; set; }
    public long ExamDate { get; set; }
}