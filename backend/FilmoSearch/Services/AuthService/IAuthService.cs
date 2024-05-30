using FilmoSearch.DTOs.UserDtos;
using FilmoSearch.Models;

namespace FilmoSearch.Services.AuthService
{
    public interface IAuthService
    {
        Task<string> Register(CreateUserDto createUserDto);
        Task<string> Login(CreateUserDto createUserDto);
        Task<string> CheckToken();
        string GetName();
    }
}
