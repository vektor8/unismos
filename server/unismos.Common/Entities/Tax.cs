namespace unismos.Common.Entities;

public class Tax
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Amount { get; set; }
    public List<Student> Payers { get; set; }
}