using JobBoardAPI.Models;

namespace JobBoardAPI.ServicesInterfaces
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
        public List<UserDto> GetUsers();
        string Login(LoginUserDto dto);
    }
}