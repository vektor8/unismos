namespace unismos.Common.Entities;

public class Student : User
{
    public List<Tax>? TaxesPaid { get; set; }
    public List<Enrollment> Enrollments { get; set; }
    
}