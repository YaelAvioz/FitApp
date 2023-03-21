namespace FitAppServer.Model
{
    public class Mentor : GenericEntity
    {
        public virtual string name { get; set; }
        public virtual Array tags { get; set; }
        public virtual string picture { get; set; }
        public virtual string description { get; set; }

    }
}
