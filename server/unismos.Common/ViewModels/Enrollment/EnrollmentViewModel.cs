namespace unismos.Common.ViewModels;

public class EnrollmentViewModel
{
    public Guid Id { get; set; }
    public StudentViewModel Student { get; set; }
    public TeachingViewModel Teaching { get; set; }
    public int Grade { get; set; }
    public string Review { get; set; }
}