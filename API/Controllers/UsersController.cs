using API.DTOs;
using API.Models;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IMapper _mapper;
        protected ResponseAPI _responseAPI;
        private readonly IUserRepository _usRepo;

        public UsersController(IUserRepository usRep, IMapper mapper)
        {
            _mapper = mapper;
            this._responseAPI = new();
            _usRepo = usRep;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult GetUsers()
        {
            var listUsers = _usRepo.GetUsers();
            var listUsersDTO = new List<UserDTO>();

            foreach(var user in listUsers)
            {
                listUsersDTO.Add(_mapper.Map<UserDTO>(user));
            }

            return Ok(listUsersDTO);
        }

        [AllowAnonymous]
        [HttpGet("{Id:int}", Name = "GetUser")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCategoria(int userId)
        {
            var user = _usRepo.GetUser(userId);

            if (user == null)
            {
                return NotFound();
            }

            var userDto = _mapper.Map<UserDTO>(user);
            return Ok(userDto);
        }

        //[Authorize(Roles = "admin")]
        [HttpPost("registry")]
        [ProducesResponseType(201, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UserRegistry([FromBody] UserRegistryDTO addUser)
        {
            bool validateUserNameUnique = _usRepo.IsUniqueUser(addUser.UserName);
            if (!validateUserNameUnique)
            {
                _responseAPI.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _responseAPI.isSucces = false;
                _responseAPI.ErrorMessage.Add("User already exist");

                return BadRequest(_responseAPI);
            }

            var user = await _usRepo.Registry(addUser);
            if(user == null)
            {
                _responseAPI.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _responseAPI.isSucces = false;
                _responseAPI.ErrorMessage.Add("It was an error when registering the user.");

                return BadRequest(_responseAPI);
            }

            _responseAPI.StatusCode = System.Net.HttpStatusCode.OK;
            _responseAPI.isSucces = true;

            return Ok(_responseAPI);
        }

        [HttpPost("login")]
        [ProducesResponseType(201, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO userLogin)
        {
            var responseLogin = await _usRepo.Login(userLogin);

            if (responseLogin.User == null || string.IsNullOrEmpty(responseLogin.Token))
            {
                _responseAPI.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _responseAPI.isSucces = false;
                _responseAPI.ErrorMessage.Add("User name or password is incorrect.");

                return BadRequest(_responseAPI);
            }

            _responseAPI.StatusCode = System.Net.HttpStatusCode.OK;
            _responseAPI.isSucces = true;
            _responseAPI.Resut = responseLogin;

            return Ok(_responseAPI);
        }
    }
}
