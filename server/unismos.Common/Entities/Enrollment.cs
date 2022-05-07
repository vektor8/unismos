namespace unismos.Common.Entities;

public class Enrollment
{
    public Guid Id { get; set; }
    public Student Student { get; set; }
    public Teaching Teaching { get; set; }
    public int Grade { get; set; }
    public string Review { get; set; }
}