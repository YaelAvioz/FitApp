using FitAppServer.Model;
using AutoMapper;
using FitAppServer.DTO;
using MongoDB.Driver;

namespace FitAppServer.Services
{
    public class MentorService : GenericService<Mentor, MentorDTO>
    {
        public MentorService(IMapper mapper) : base(mapper) {  }

        public async Task<Mentor> GetMentorInfo(string mentorName)
        {
            var filter = Builders<Mentor>.Filter.Eq(x => x.name, mentorName);
            var mentors = await _collection.Find(filter).ToListAsync();
            return mentors.FirstOrDefault();
        }

        public async Task<List<MentorDTO>> GetThreeMentors()
        {
            var mentors = await _collection.Find(Builders<Mentor>.Filter.Empty).Limit(3).ToListAsync();
            return _mapper.Map<List<MentorDTO>>(mentors);
        }

        public async Task<Dictionary<string, List<string>>> GetMappingInfo()
        {
            var mentors = await base.GetAll();
            var dict = new Dictionary<string, List<string>>();
            foreach (MentorDTO mentor in mentors)
            {
                dict.Add(mentor.name, mentor.expertise);
            }
            return dict;
        }
        
    }
}