namespace unismos.Common.Entities;

public class Subject
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Teaching> Teachings { get; set; }
}