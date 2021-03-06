namespace unismos.Common.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public virtual string Type => "user";
}