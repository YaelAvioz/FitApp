using FitAppServer.Model;
namespace FitAppServer.DTO
{
    public class MentorDTO : GenericEntity
    {
        public virtual string name { get; set; }
        public virtual Array tags { get; set; }
        public virtual string picture { get; set; }
        public virtual string description { get; set; }
    }
}