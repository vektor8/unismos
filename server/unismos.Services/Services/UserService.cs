using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using unismos.Common.Dtos;
using unismos.Common.Entities;
using unismos.Common.Extensions;
using unismos.Interfaces.IUser;

namespace unismos.Services.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public UserService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    /// <summary>
    /// get userById
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<UserDto> GetByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        switch (user)
        {
            case Student student:
                return student.ToDto();
            case Secretary secretary:
                return secretary.ToDto();
            case Professor professor:
                return professor.ToDto();
        }

        return new NullUserDto();
    }

    /// <summary>
    /// verify credentials and return user data with jwt
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<LoggedInUserDto> Authenticate(LoginUserDto dto)
    {
        var user = await _userRepository.GetByUsername(dto.Username);

        if (user is NullUser)
        {
            Log.Error("Nonexisting user");
            return new NullLoggedInUserDto();
        }

        if (BCrypt.Net.BCrypt.Verify(dto.Password, user.Password) == false)
        {
            Log.Error("Incorrect password");
            return new NullLoggedInUserDto();
        }

        var userDto = user.ToDto();

        return new LoggedInUserDto
        {
            Id = userDto.Id,
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Token = await GenerateJwtToken(userDto),
            Username = userDto.Username,
            Type = userDto.GetType().Name.ToLower()[..^3]
        };
    }


    /// <summary>
    /// generate jwt for given user
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    private async Task<string> GenerateJwtToken(UserDto user)
    {
        Log.Information("Generating jwt for {name}", user.Username);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Type)
        };

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(3),
            claims: claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}