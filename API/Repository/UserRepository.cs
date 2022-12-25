using API.Data;
using API.Repository.IRepository;
using API.Models;
using API.DTOs;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private string token;

        public UserRepository(ApplicationDbContext db, IConfiguration config)
        {
            _db = db;
            token = config.GetValue<string>("AppSettings:Token");
        }
        public bool UserExist(string userName)
        {
            bool value = _db.User.Any(c => c.UserName.ToLower().Trim() == userName.ToLower().Trim());
            return value;
        }

        public bool UserExist(int id)
        {
            return _db.User.Any(c => c.Id == id);
        }

        public User GetUser(int userId)
        {
            return _db.User.FirstOrDefault(c => c.Id == userId);
        }

        public ICollection<User> GetUsers()
        {
            return _db.User.OrderBy(c => c.UserName).ToList();
        }

        public bool IsUniqueUser(string user)
        {
            var userdb = _db.User.FirstOrDefault(u => u.UserName == user);
            if (userdb == null)
            {
                return true;
            }

            return false;
        }

        public async Task<UserLoginResponseDTO> Login(UserLoginDTO userLoginDTO)
        {
            var passwordEncription = createHMAC(userLoginDTO.Password);

            var user = _db.User.FirstOrDefault(u => u.UserName.ToLower() == userLoginDTO.UserName.ToLower() && u.Password == passwordEncription);

            if (user == null)
            {
                return new UserLoginResponseDTO()
                {
                    Token = "",
                    User = null
                };
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(token);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var t = tokenHandler.CreateToken(tokenDescriptor);

            UserLoginResponseDTO userLoginResponseDTO = new UserLoginResponseDTO()
            {
                Token = tokenHandler.WriteToken(t),
                User = user
            };
            return userLoginResponseDTO;
        }

        public async Task<User> Registry(UserRegistryDTO userRegistryDTO)
        {
            var passEncript = createHMAC(userRegistryDTO.Password);

            User user = new User()
            {
                UserName = userRegistryDTO.UserName,
                Password = userRegistryDTO.Password,
                FirstName = userRegistryDTO.FirstName,
                LastName = userRegistryDTO.LastName,
                Email = userRegistryDTO.Email,
                Role = userRegistryDTO.Role,
            };
            user.Password = passEncript;

            _db.User.Add(user);
            await _db.SaveChangesAsync();
            

            return user;
        }

        public static string createHMAC(string password)
        {
            MD5CryptoServiceProvider pass = new();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(password);
            data= pass.ComputeHash(data);

            string resp = "";

            for (int i = 0; i < data.Length; i++)
                resp += data[i].ToString("x2").ToLower();
            return resp;
        }
    }
}
