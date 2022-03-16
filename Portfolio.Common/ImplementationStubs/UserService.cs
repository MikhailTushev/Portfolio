using Portfolio.Domain.Dto;

namespace Portfolio.Common.ImplementationStubs
{
    public class UserService : IUserService
    {
        private static UserDto _userDto = new() {Id = 1L, Name = "Arnold"};

        public UserService()
        {
            CurrentUserDto = _userDto;
        }

        public UserDto CurrentUserDto { get; }
    }
}