namespace FitAppServer.Model
{
    public class User : GenericEntity
    {
        public virtual Guid Id { get; set; }
        public virtual string Username { get; set; }
        public virtual byte[] PasswordHash { get; set; }
        public virtual byte[] PasswordSalt { get; set; }
    }
}