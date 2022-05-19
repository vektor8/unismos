using Serilog;
using unismos.Common.Dtos;
using unismos.Common.Dtos.Professor;
using unismos.Common.Entities;
using unismos.Common.Extensions;
using unismos.Interfaces.IProfessor;
using unismos.Interfaces.IUser;

namespace unismos.Services.Services;

public class ProfessorService : IProfessorService
{
    private readonly IUserRepository _userRepository;
    private readonly IProfessorRepository _professorRepository;

    public ProfessorService(IUserRepository userRepository, IProfessorRepository professorRepository)
    {
        _userRepository = userRepository;
        _professorRepository = professorRepository;
    }
    
    /// <summary>
    /// validate username and add prof
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<ProfessorDto> AddAsync(NewProfessorDto dto)
    {
        var user = await _userRepository.GetByUsername(dto.Username);
        if (user is not NullUser)
        {
            Log.Error("Username taken");
            return new NullProfessorDto();
        }
        
        var entity = new Professor
        {
            Id = new Guid(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Username = dto.Username,
            Teachings = new List<Teaching>()
        };
        
        var response = await _professorRepository.AddAsync(entity);
        return response.ToDto();
    }

    /// <summary>
    /// get prof by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<ProfessorDto> GetByIdAsync(Guid id)
    {
        var entity = await _professorRepository.GetByIdAsync(id);
        var result = entity.ToDto();
        Log.Information("Getting professor with id {id}", id);
        return result;
    }

    /// <summary>
    /// get all profs from db
    /// </summary>
    /// <returns></returns>
    public async Task<List<ProfessorDto>> GetAllAsync()
    {
        var professors = (await _professorRepository.GetAllAsync()).Select(e => e.ToDto()).ToList();
        Log.Information("Getting all professors");
        return professors;
    }
}