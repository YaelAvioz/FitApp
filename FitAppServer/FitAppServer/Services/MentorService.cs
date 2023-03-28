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
    }
}