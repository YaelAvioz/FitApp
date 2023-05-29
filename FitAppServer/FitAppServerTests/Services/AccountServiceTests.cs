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
    public class AccountServiceTests
    {
        private readonly AccountService _accountService;
        private readonly IMongoCollection<User> _collection;

        public AccountServiceTests()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RegisterDTO, User>();
                cfg.CreateMap<LoginDTO, User>();
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<UserDTO, User>();
            });
            var _mapper = mapperConfig.CreateMapper();

            var connectionString = "mongodb+srv://FitApp:FitAppYaelCoral@cluster0.hsylfut.mongodb.net/?retryWrites=true&w=majority";
            var databaseName = "fitapp";
            var collectionName = "user";

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _collection = database.GetCollection<User>(collectionName);

            _accountService = new AccountService(_mapper, _collection);
        }

        [Fact]
        public async Task RegisterUser_WithExistingUser_ReturnsNull()
        {
            // Arrange
            var testUsername = "existingUserForTest";
            var existingUser = new User { username = testUsername };
            await _collection.InsertOneAsync(existingUser);

            var registerDTO = new RegisterDTO { username = testUsername };

            try
            {
                // Act
                var result = await _accountService.RegisterUser(registerDTO);

                // Assert
                Assert.Null(result);
            }
            finally
            {
                // Cleanup
                await _collection.DeleteOneAsync(u => u.username == testUsername);
            }
        }

        [Fact]
        public async Task RegisterUser_WithNewUser_RegistersUser()
        {
            // Arrange
            var registerDTO = new RegisterDTO
            {
                username = "johnidoe",
                password = "132456789",
                age = 25,
                firstname = "John",
                lastname = "Doe",
                gender = "Male",
                height = 180,
                tags = new List<string> { "Build strength", "Feel healthy" },
                goal = "lose weight",
                mentor = null
            };

            try
            {
                // Act
                var result = await _accountService.RegisterUser(registerDTO);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(registerDTO.username.ToLower(), result.username);
                Assert.True(VerifyPasswordHash(registerDTO.password, result.passwordHash, result.passwordSalt));
            }
            finally
            {
                // Cleanup
                await _collection.DeleteOneAsync(u => u.username == "johnidoe");
            }
        }

        [Fact]
        public async Task RegisterUser_EnsuresMentorIsNotNull()
        {
            // Arrange
            var registerDTO = new RegisterDTO
            {
                username = "johnidoe",
                password = "132456789",
                age = 25,
                firstname = "John",
                lastname = "Doe",
                gender = "Male",
                height = 180,
                tags = new List<string> { "Build strength", "Feel healthy" },
                goal = "lose weight",
                mentor = null
            };

            try
            {
                // Act
                var result = await _accountService.RegisterUser(registerDTO);

                // Assert
                Assert.NotNull(result.mentor);
            }
            finally
            {
                // Cleanup
                await _collection.DeleteOneAsync(u => u.username == "johnidoe");
            }
        }

        [Fact]
        public async Task RegisterUser_CreatesNewConversationWithMessageFromMentor()
        {
            // Arrange
            var registerDTO = new RegisterDTO
            {
                username = "johnidoe",
                password = "132456789",
                age = 25,
                firstname = "John",
                lastname = "Doe",
                gender = "Male",
                height = 180,
                tags = new List<string> { "Build strength", "Feel healthy" },
                goal = "lose weight",
                mentor = null
            };

            try
            {
                // Act
                User result = await _accountService.RegisterUser(registerDTO);

                // Assert
                Assert.NotNull(result);

                Assert.NotNull(await _accountService._conversationService.GetConversation(result.Id));
                var conversation = await _accountService._conversationService.GetConversation(result.Id);

                Assert.NotNull(await _accountService._messageService.GetConvMsgs(conversation.Id));
                var messages = await _accountService._messageService.GetConvMsgs(conversation.Id);

                Assert.NotNull(messages[0]);
            }
            finally
            {
                // Cleanup
                await _collection.DeleteOneAsync(u => u.username == "johnidoe");
            }
        }

        [Fact]
        public async Task Login_WithExistingUserAndCorrectPassword_ReturnsUser()
        {
             // Arrange
            var user = new RegisterDTO
            {
                username = "johnidoe",
                password = "132456789",
                age = 25,
                firstname = "John",
                lastname = "Doe",
                gender = "Male",
                height = 180,
                tags = new List<string> { "Build strength", "Feel healthy" },
                goal = "lose weight",
                mentor = null
            };

            await _accountService.RegisterUser(user);

            var loginDTO = new LoginDTO { username = "johnidoe", password = "132456789" };

            try
            {
                // Act
                var result = await _accountService.Login(loginDTO);

                // Assert
                Assert.NotNull(result);
                Assert.Equal("johnidoe", result.username);
            }
            finally
            {
                // Cleanup
                await _collection.DeleteOneAsync(u => u.username == "johnidoe");
            }

        }

        [Fact]
        public async Task Login_WithNonexistentUser_ReturnsNull()
        {
            // Arrange
            var loginDTO = new LoginDTO { username = "nonexistentUser" };

            // Act
            var result = await _accountService.Login(loginDTO);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Login_WithExistingUserAndIncorrectPassword_ThrowsException()
        {
            // Arrange
            var user = new RegisterDTO
            {
                username = "johnidoe",
                password = "132456789",
                age = 25,
                firstname = "John",
                lastname = "Doe",
                gender = "Male",
                height = 180,
                tags = new List<string> { "Build strength", "Feel healthy" },
                goal = "lose weight",
                mentor = null
            };

            await _accountService.RegisterUser(user);

            try
            {                
                // Act & Assert
                var loginDTO = new LoginDTO { username = "johnidoe", password = "notsamepass" };
                await Assert.ThrowsAsync<Exception>(() => _accountService.Login(loginDTO));
            }
            finally
            {
                // Cleanup
                await _collection.DeleteOneAsync(u => u.username == "johnidoe");
            }

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
