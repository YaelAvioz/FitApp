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
        protected readonly MessageService _messageService;
        protected readonly IMongoDatabase _db;
        protected readonly IMapper _mapper;
        protected readonly string connectionString = "mongodb+srv://FitApp:FitAppYaelCoral@cluster0.hsylfut.mongodb.net/?retryWrites=true&w=majority";
        protected readonly string databaseName = "fitapp";
        protected readonly string collectionName = "user";
        readonly string fakeId = "aaaaaaaaaaaaaaaaaaaaaaaa";

        public AccountService(IMapper mapper)
        {
            _mapper = mapper;
            var client = new MongoClient(connectionString);
            _db = client.GetDatabase(databaseName);
            _collection = _db.GetCollection<User>(collectionName);
            _conversationService = new ConversationService(_mapper);
            _messageService = new MessageService(_mapper);
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

            // add the user to the db
            await _collection.InsertOneAsync(newUser);

            // get the user from the db to get its Id
            var user = await UserExists(userDTO.username);

            // when registering a new user: create a conversation + send him the first msg.
            Conversation conversation = new Conversation
            {
                Messages = new List<string>(),
                UserId = user.Id
            };

            // create the first message and add it to the db
            Message message = new Message
            {
                Content = user.FirstMsg(),
                IsUser = false,
                Timestamp = DateTime.Now,
                ConversationId = fakeId
            };
            var msg = await _messageService.Create(message);

            // add the message to the conversation
            conversation.Messages.Add(msg.Id);

            // new conversation for the new user + update the Id
            var conv = await _conversationService.Create(conversation);
            msg.ConversationId = conv.Id;
            await _messageService.UpdateId(msg);
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
            User e = _mapper.Map<User>(userDTO);
            await _collection.ReplaceOneAsync(x => x.username.Equals(userDTO.username), e);
        }

    }
}

