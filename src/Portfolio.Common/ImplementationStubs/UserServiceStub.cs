using Portfolio.Domain.Dto;

namespace Portfolio.Common.ImplementationStubs
{
    public class UserServiceStub : IUserService
    {
        private static UserDto _userDto = new() {Id = 1L, Name = "Arnold"};

        public UserServiceStub()
        {
            CurrentUserDto = _userDto;
        }

        public UserDto CurrentUserDto { get; }
    }
}