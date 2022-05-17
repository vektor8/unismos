using unismos.Common.Dtos;
using unismos.Common.Entities;

namespace unismos.Common.Extensions;

public static class SecretaryExtensions
{
    public static SecretaryDto ToDto(this Secretary entity) => entity is NullSecretary
        ? new NullSecretaryDto()
        : new SecretaryDto
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Username = entity.Username
        };

}