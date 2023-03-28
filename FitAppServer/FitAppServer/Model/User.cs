namespace FitAppServer.Model
{
    public class User : GenericEntity
    {
        public virtual Guid Id { get; set; }
        public virtual string username { get; set; }
        public virtual byte[] passwordHash { get; set; }
        public virtual byte[] passwordSalt { get; set; }
    }
}