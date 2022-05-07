namespace unismos.Common.Entities;

public class Professor : User
{
    public List<Teaching> Teachings { get; set; }
}