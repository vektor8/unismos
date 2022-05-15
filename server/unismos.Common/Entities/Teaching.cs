namespace unismos.Common.Entities;

public class Teaching
{
    public Guid Id { get; set; }
    public Professor Professor { get; set; }
    public Subject Subject { get; set; }
    public long ExamDate { get; set; }
}