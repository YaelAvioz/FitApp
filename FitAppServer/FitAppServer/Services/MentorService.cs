using FitAppServer.Model;
using AutoMapper;
using FitAppServer.DTO;
using MongoDB.Driver;

namespace FitAppServer.Services
{
    public class MentorService : GenericService<Mentor, MentorDTO>
    {
        private readonly IMongoCollection<Mentor> _collection;

        public MentorService(IMapper mapper) : base(mapper, "mentors") {  }

        public async Task<Mentor> GetMentorInfo(string mentorName)
        {
            var filter = Builders<Mentor>.Filter.Eq(x => x.name, mentorName);
            var mentors = await _collection.Find(filter).ToListAsync();
            return mentors.FirstOrDefault();
        }

    }
}