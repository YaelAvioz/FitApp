using AutoMapper;
using FitAppServer.DTO;
using FitAppServer.Model;
using FitAppServer.Services;
using MongoDB.Driver;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace FitAppServer.Tests.Services
{
    public class UserServiceTests
    {
        private readonly UserService _userService;
        private readonly IMongoCollection<User> _userCollection;
        private readonly IMongoCollection<Conversation> _conversationCollection;
        private readonly IMongoCollection<Message> _messageCollection;
        private readonly RegisterDTO _registerDTO;
        private readonly string _fakeUsername = "jhondoe2023";
        private readonly string _fakePassword = "123456789";
        private readonly string _fakeWrongPassword = "abcdefghj";


        public UserServiceTests()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RegisterDTO, User>();
                cfg.CreateMap<LoginDTO, User>();
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<UserDTO, User>();
                cfg.CreateMap<Message, MessageDTO>();
                cfg.CreateMap<MessageDTO, Message>();
                cfg.CreateMap<Mentor, MentorDTO>();
                cfg.CreateMap<MentorDTO, Mentor>();
                cfg.CreateMap<Conversation, ConversationDTO>();
                cfg.CreateMap<ConversationDTO, Conversation>();
            });
            var _mapper = mapperConfig.CreateMapper();

            var connectionString = "mongodb+srv://FitApp:FitAppYaelCoral@cluster0.hsylfut.mongodb.net/?retryWrites=true&w=majority";
            var databaseName = "fitapp";

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _userCollection = database.GetCollection<User>("user");
            _conversationCollection = database.GetCollection<Conversation>("conversation");
            _messageCollection = database.GetCollection<Message>("message");

            _userService = new UserService(_mapper, _userCollection);

            _registerDTO = new RegisterDTO
            {
                username = _fakeUsername,
                password = _fakePassword,
                firstname = "jhon",
                lastname = "doe",
                age = 30,
                height = 180,
                weight = 70,
                gender = "male",
                goal = "FIT",
                mentor = "",
                tags = new List<string> { "Calorie-counting", "Meal plans", "Guidance", "Lose weight", "Women's health", "Vegetarianism" }
            };
        }

        [Fact]
        public async Task RegisterExistingUsername_ReturnsNull()
        {
            // Arrange
            var existingUser = new User { username = _fakeUsername };
            await _userCollection.InsertOneAsync(existingUser);

            var registerDTO = new RegisterDTO { username = _fakeUsername };

            try
            {
                // Act
                var result = await _userService.RegisterUser(registerDTO);

                // Assert
                Assert.Null(result);
            }
            finally
            {
                // Cleanup
                await _userCollection.DeleteOneAsync(u => u.username == _fakeUsername);
            }
        }

        [Fact]
        public async Task RegisterNewUser_RegistersUser()
        {
            // Arrange
            var registerDTO = _registerDTO;

            try
            {
                // Act
                var result = await _userService.RegisterUser(registerDTO);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(registerDTO.username.ToLower(), result.username);
                Assert.True(VerifyPasswordHash(registerDTO.password, result.passwordHash, result.passwordSalt));
            }
            finally
            {
                // Cleanup
                await Cleanup(_fakeUsername);

            }
        }

        [Fact]
        public async Task RegisterUser_EnsuresMentorIsNotNull()
        {
            // Arrange
            var registerDTO = _registerDTO;

            try
            {
                // Act
                var result = await _userService.RegisterUser(registerDTO);

                // Assert
                Assert.NotNull(result.mentor);
            }
            finally
            {
                // Cleanup
                await Cleanup(_fakeUsername);
            }
        }

        [Fact]
        public async Task RegisterUser_CreatesNewConversationWithMessageFromMentor()
        {
            // Arrange
            var registerDTO = _registerDTO;

            try
            {
                // Act
                User result = await _userService.RegisterUser(registerDTO);

                // Assert
                Assert.NotNull(result);

                Assert.NotNull(await _userService._conversationService.GetConversation(result.Id));
                var conversation = await _userService._conversationService.GetConversation(result.Id);

                Assert.NotNull(await _userService._messageService.GetConvMsgs(conversation.Id));
                var messages = await _userService._messageService.GetConvMsgs(conversation.Id);

                Assert.NotNull(messages[0]);
            }
            finally
            {
                // Cleanup
                await Cleanup(_fakeUsername);
            }
        }

        [Fact]
        public async Task Login_WithExistingUserAndCorrectPassword_ReturnsUser()
        {
            // Arrange
            var user = _registerDTO;

            await _userService.RegisterUser(user);

            var loginDTO = new LoginDTO { username = _fakeUsername, password = _fakePassword };

            try
            {
                // Act
                var result = await _userService.Login(loginDTO);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(_fakeUsername, result.username);
            }
            finally
            {
                // Cleanup
                await Cleanup(_fakeUsername);
            }

        }

        [Fact]
        public async Task Login_WithNonexistentUser_ReturnsNull()
        {
            // Arrange
            var loginDTO = new LoginDTO { username = _fakeUsername };

            // Act
            var result = await _userService.Login(loginDTO);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Login_WithExistingUserAndIncorrectPassword_ThrowsException()
        {
            // Arrange
            var user = _registerDTO;

            await _userService.RegisterUser(user);

            try
            {                
                // Act & Assert
                var loginDTO = new LoginDTO { username = _fakeUsername, password = _fakeWrongPassword };
                await Assert.ThrowsAsync<Exception>(() => _userService.Login(loginDTO));
            }
            finally
            {
                // Cleanup
                await Cleanup(_fakeUsername);
            }

        }

        private async Task Cleanup(string username)
        {
            var userId = (await _userCollection.Find(u => u.username.Equals(username)).FirstOrDefaultAsync()).Id;
            await _userCollection.DeleteOneAsync(u => u.Id == userId);
            var convId = (await _conversationCollection.Find(c => c.UserId.Equals(userId)).FirstOrDefaultAsync()).Id;
            await _messageCollection.DeleteOneAsync(m => m.ConversationId == convId);
            await _conversationCollection.DeleteOneAsync(c => c.UserId == userId);
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != passwordHash[i])
                    return false;
            }

            return true;
        }
    }
}
