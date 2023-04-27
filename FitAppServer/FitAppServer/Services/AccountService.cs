using AutoMapper;
using FitAppServer.DTO;
using FitAppServer.Interfaces;
using FitAppServer.Model;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Text;

namespace FitAppServer.Services
{
    public class AccountService
    {
        protected readonly IMongoCollection<User> _collection;
        protected readonly ConversationService _conversationService;
        protected readonly IMongoDatabase _db;
        protected readonly IMapper _mapper;
        protected readonly string connectionString = "mongodb+srv://FitApp:FitAppYaelCoral@cluster0.hsylfut.mongodb.net/?retryWrites=true&w=majority";
        protected readonly string databaseName = "fitapp";
        protected readonly string collectionName = "user";

        public AccountService()
        {
            var client = new MongoClient(connectionString);
            _db = client.GetDatabase(databaseName);
            _collection = _db.GetCollection<User>(collectionName);
            _conversationService = new ConversationService(_mapper);
        }

        public async Task<User> RegisterUser(RegisterDTO userDTO)
        {
            var isUserExist = await UserExists(userDTO.username);
            if (isUserExist != null)
            {
                throw new Exception("User exists!");
            }
            using var hmac = new HMACSHA512();
            var newUser = new User
            {
                username = userDTO.username.ToLower(),
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.password)),
                passwordSalt = hmac.Key,

                age = userDTO.age,
                firstname = userDTO.firstname,
                lastname = userDTO.lastname,
                gender = userDTO.gender,
                height = userDTO.height,
                foods = new List<string>(),
                weight = new List<Tuple<double, DateTime>>(),
                tags = userDTO.tags,
                goal = userDTO.goal,
                mentor = userDTO.mentor,
            };

            newUser.weight.Add(Tuple.Create(userDTO.weight, DateTime.Now));
            newUser.bmi = newUser.getBmi(userDTO.weight);

            await _collection.InsertOneAsync(newUser);

            // get the user from the db to get its Id
            var user = await UserExists(userDTO.username);
            Conversation conv = new Conversation
            {
                Messages = new List<string>(),
                UserId = user.Id
            };
            // when registering a new user: create a conversation + send him the first msg.
            conv.Messages.Add(user.FirstMsg());

            // new conversation for the new user
            await _conversationService.Create(conv);           
            return newUser;
        }

        public async Task<User> Login(LoginDTO userDTO)
        {
            var userExists = await UserExists(userDTO.username);
            if (userExists == null) throw new Exception("Unauthorized newUser!");
            using var hmac = new HMACSHA512(userExists.passwordSalt);
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.password));
            for (int i = 0; i < computeHash.Length; i++)
            {
                if (computeHash[i] != userExists.passwordHash[i]) throw new Exception("Unauthorized newUser!");
            }
            return userExists;
        }

        private async Task<User> UserExists(string username)
        {
            var user = await _collection.Find(x => x.username.ToLower().Equals(username.ToLower())).FirstOrDefaultAsync();
            return user;
        }

        public async Task<User> GetUser(string id)
        {
            var user = await _collection.Find(x => x.Id.Equals(id)).FirstOrDefaultAsync();
            return user;
        }

        public async Task UpdateToken(UserDTO userDTO)
        {
            var filter = Builders<User>.Filter.Eq(x => x.username.ToLower(), userDTO.username.ToLower());
            var update = Builders<User>.Update.Set(x => x.token, userDTO.token);

            await _collection.UpdateOneAsync(filter, update);
        }

    }
}

