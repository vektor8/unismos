namespace unismos.Common.Dtos;

public class LoggedInUserDto
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Token { get; set; }
    public string Type { get; set; }
}