using FitAppServer.Services;
using FitAppServer.Model;
using AutoMapper;
using FitAppServer.DTO;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        [Route("{mentorName}")]
        public async Task<ActionResult<Food>> GetFood([FromRoute] string mentorName)
        {
            Mentor mentor = await _mentorService.GetMentorInfo(mentorName);
            if (mentor == null)
            {
                return NotFound();
            }
            return Ok(mentor);
        }
    }
}