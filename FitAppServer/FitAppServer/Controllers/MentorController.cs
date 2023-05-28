using FitAppServer.Services;
using FitAppServer.Model;
using AutoMapper;
using FitAppServer.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FitAppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MentorController : GenericController<Mentor, MentorDTO>
    {
        private static MentorService _mentorService;

        public MentorController(IMapper mapper) : base(mapper)
        {
            _mentorService = new MentorService(mapper);
        }

        [HttpGet("getThreeMentors")]
        public async Task<ActionResult<List<MentorDTO>>> GetThreeMentors()
        {
            try
            {
                var mentors = await _mentorService.GetThreeMentors();
                return Ok(mentors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("getMentorByName/{name}")]
        public async Task<ActionResult<MentorDTO>> getMentorByName(string name)
        {
            try
            {
                var mentor = await _mentorService.GetMentorByName(name);
                return Ok(mentor);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
