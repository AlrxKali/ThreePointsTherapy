using API.Models;
using API.DTOs;

namespace API.Repository.IRepository
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(int userId);
        bool IsUniqueUser(string user);
        bool UserExist(string nombre);
        bool UserExist(int id);
        Task<UserLoginResponseDTO> Login(UserLoginDTO userLoginDTO);
        Task<User> Registry(UserRegistryDTO userRegistryDTO);
    }
}
