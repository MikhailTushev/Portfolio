using Portfolio.Domain.Dto;

namespace Portfolio.Common
{
    public interface IUserService
    {
        UserDto CurrentUserDto { get; }
    }
}