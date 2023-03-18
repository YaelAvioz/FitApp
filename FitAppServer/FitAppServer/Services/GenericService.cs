using AutoMapper;
using fitappserver.Model;
using MongoDB.Driver;

namespace fitappserver.Services
{
    public class GenericService<T, TDTO> where T : GenericEntity where TDTO : GenericEntity
    {
        private readonly IMongoCollection<T> _collection;
        private readonly IMongoDatabase _db;
        private readonly IMapper _mapper;
        private readonly String connectionString = "mongodb+srv://<fitapp>:<FitAppYaelCoral>@cluster0.hsylfut.mongodb.net/?retryWrites=true&w=majority";
        private readonly String databaseName = "fitapp";

        public GenericService(IMapper mapper)
        {
            var client = new MongoClient(connectionString);
            _db = client.GetDatabase(databaseName);
            _mapper = mapper;
        }

        public GenericService(IMapper mapper, string connectionString, string databaseName, string collectionName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _collection = database.GetCollection<T>(collectionName);
            _mapper = mapper;
        }

        public GenericService(IMapper mapper, string collectionName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _collection = database.GetCollection<T>(collectionName);
            _mapper = mapper;
        }

        public async Task<List<TDTO>> GetAll()
        {
            var entities = await _collection.Find(x => true).ToListAsync();
            return _mapper.Map<List<TDTO>>(entities);
        }

        public async Task<TDTO> Get(int id)
        {
            var entity = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<TDTO>(entity);
        }

        public async Task<TDTO> Create(T entity)
        {
            await _collection.InsertOneAsync(entity);
            return _mapper.Map<TDTO>(entity);
        }

        public async Task<TDTO> Update(int id, T newEntity)
        {
            newEntity.Id = id;
            await _collection.ReplaceOneAsync(x => x.Id == id, newEntity);
            return _mapper.Map<TDTO>(newEntity);
        }

        public async void Delete(int id)
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
