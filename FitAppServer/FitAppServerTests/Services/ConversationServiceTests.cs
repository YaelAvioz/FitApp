using AutoMapper;
using FitAppServer.DTO;
using FitAppServer.Model;
using FitAppServer.Services;
using MongoDB.Driver;
using Xunit;


namespace FitAppServerTests.Services
{
    public class ConversationServiceTests
    {
        private ConversationService _conversationService;
        private IMapper _mapper;
        private IMongoCollection<Conversation> _collection;

        public ConversationServiceTests()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Conversation, ConversationDTO>();
            });
            _mapper = mapperConfiguration.CreateMapper();

            var mongoClient = new MongoClient();
            var database = mongoClient.GetDatabase("test_database");
            _collection = database.GetCollection<Conversation>("conversations");

            _conversationService = new ConversationService(_mapper, _collection);
        }

        [Fact]
        public async Task GetConversation_WithExistingUserId_ReturnsConversation()
        {
            // Arrange
            string userId = "6474f4b2eb0a3c43e1f4caa0";
            var conversation = new Conversation { UserId = userId, Messages = new List<string>() };
            await _collection.InsertOneAsync(conversation);

            // Act
            var result = await _conversationService.GetConversation(userId);

            // Assert
            Assert.Equal(conversation, result);
        }

        [Fact]
        public async Task GetConversation_WithNonexistentUserId_ReturnsNull()
        {
            // Arrange
            string userId = "6472104a8211738251914562";

            // Act
            var result = await _conversationService.GetConversation(userId);

            // Assert
            Assert.Null(result);
        }
    }
}
