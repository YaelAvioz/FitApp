using AutoMapper;
using FitAppServer.DTO;
using FitAppServer.Interfaces;
using FitAppServer.Model;
using FitAppServer.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
            var user = await _accountService.RegisterUser(registerUserDTO);
            if (user == null) return BadRequest("username taken");

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

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(LoginDTO loginDTO)
        {
            var user = await _accountService.Login(loginDTO);

            if (user == null) return BadRequest("unauothorized user");

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
                foods = user.foods,
            };
            return Ok(userDTO);
        }

        [HttpGet("{id}/weight")]
        public async Task<ActionResult<List<Tuple<double, DateTime>>>> WeightCharts(string id)
        {
            List<Tuple<double, DateTime>> res = await _accountService.WeightCharts(id);
            if (res != null && res.Count > 0) return Ok(res);

            return BadRequest();
        }

        [HttpPost("{id}/weight")]
        public async Task<ActionResult<object>> UpdateWeight(string id, [FromBody] double newWeight)
        {
            List<Tuple<double, DateTime>> res = await _accountService.UpdateWeight(id, newWeight);
            if (res != null && res.Count > 0) return Ok(res);

            return BadRequest();
        }

        [HttpGet("{id}/water")]
        public async Task<ActionResult<List<bool>>> GetWater(string id)
        {
            List<bool> res = await _accountService.GetWater(id);
            if (res != null && res.Count > 0) return Ok(res);

            return BadRequest();
        }

        [HttpPost("{id}/water")]
        public async Task<ActionResult<List<bool>>> UpdateWater(string id, [FromBody] int cupsToAdd)
        {
            List<bool> res = await _accountService.UpdateWater(id, cupsToAdd);
            if (res != null && res.Count > 0) return Ok(res);

            return BadRequest();
        }

        [HttpPost("{id}/food")]
        public async Task<ActionResult<object>> AddFood(string id, [FromBody] AddFoodDTO addFoodDto)
        {
            string foodId = addFoodDto.FoodId;
            double amount = addFoodDto.Amount;
            var res = await _accountService.AddFood(id, foodId, amount);
            if (res != null) return Ok();
 
            return BadRequest();
        }
    }
}
