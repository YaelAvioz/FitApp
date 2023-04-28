using AutoMapper;
using FitAppServer.DTO;
using FitAppServer.Interfaces;
using FitAppServer.Model;
using FitAppServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitAppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly ITokenService _tokenService;

        public AccountController(AccountService accountService, ITokenService tokenService, IMapper mapper)
        {
            _accountService = accountService;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDTO registerUserDTO)
        {
            try
            {
                var user = await _accountService.RegisterUser(registerUserDTO);
                var userDTO = new UserDTO
                {
                    username = user.username,
                    token = _tokenService.CreateToken(user),
                    firstname = registerUserDTO.firstname,
                    lastname = registerUserDTO.lastname,
                    age = registerUserDTO.age,
                    height = registerUserDTO.height,
                    gender = registerUserDTO.gender,
                    tags = registerUserDTO.tags,
                    bmi = user.bmi,
                    goal = user.goal,
                    mentor = user.mentor,
                    weight = user.weight,
                    foods = user.foods,
                    firstMsg = user.FirstMsg(),
                };

                await _accountService.UpdateToken(userDTO);
                return Ok(userDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(LoginDTO loginDTO)
        {
            try
            {
                var user = await _accountService.Login(loginDTO);
                var userDTO = new UserDTO
                {
                    username = user.username,
                    token = _tokenService.CreateToken(user),
                    firstname = user.firstname,
                    lastname = user.lastname,
                    age = user.age,
                    height = user.height,
                    weight = user.weight,
                    gender = user.gender,
                    bmi = user.bmi,
                    goal = user.goal,
                    mentor = user.mentor,
                    tags = user.tags,
                    foods= user.foods,
                };
                return Ok(userDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
