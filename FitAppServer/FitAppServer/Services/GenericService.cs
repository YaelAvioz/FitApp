using AutoMapper;
using FitAppServer.DTO;
using FitAppServer.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;

namespace FitAppServer.Services
{
    public class GenericService<T, TDTO> where T : GenericEntity where TDTO : GenericEntity
    {
        protected readonly IMongoCollection<T> _collection;
        protected readonly IMongoDatabase _db;
        protected readonly IMapper _mapper;
        protected readonly string connectionString = "mongodb+srv://FitApp:FitAppYaelCoral@cluster0.hsylfut.mongodb.net/?retryWrites=true&w=majority";
        protected readonly string databaseName = "fitapp";

        public GenericService(IMapper mapper)
        {
            var client = new MongoClient(connectionString);
            _db = client.GetDatabase(databaseName);
            string s = typeof(T).ToString();
            string collectionName = s.Substring(s.LastIndexOf('.') + 1).ToLower();
            _collection = _db.GetCollection<T>(collectionName);
            _mapper = mapper;
        }

        public GenericService(IMapper mapper, IMongoCollection<T> collection)
        {
            var client = new MongoClient(connectionString);
            _db = client.GetDatabase(databaseName);
            string s = typeof(T).ToString();
            _collection = collection;
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
            var entities = await _collection.Find(_ => true).ToListAsync();
            return _mapper.Map<List<TDTO>>(entities);
        }

        public async Task<List<TDTO>> GetNext(int skip)
        {
            var entities = await _collection.Find(_ => true)
                                             .Skip(skip)
                                             .Limit(15)
                                             .ToListAsync();
            return _mapper.Map<List<TDTO>>(entities);
        }

        public async Task<TDTO> Get(string id)
        {
            var entity = await _collection.Find(x => x.Id.Equals(id)).FirstOrDefaultAsync();
            return _mapper.Map<TDTO>(entity);
        }

        public async Task<TDTO> Create(T entity)
        {
            await _collection.InsertOneAsync(entity);
            return _mapper.Map<TDTO>(entity);
        }

        public async Task<TDTO> Update(string id, T newEntity)
        {
            newEntity.Id = id;
            await _collection.ReplaceOneAsync(x => x.Id.Equals(id), newEntity);
            return _mapper.Map<TDTO>(newEntity);
        }

        public async Task Delete(string id)
        {
            await _collection.DeleteOneAsync(x => x.Id.Equals(id));
        }

        public async Task<T> UpdateId(TDTO newEntity)
        {
            T e = _mapper.Map<T>(newEntity);
            await _collection.ReplaceOneAsync(x => x.Id.Equals(newEntity.Id), e);
            return e;
        }
    }


/*
{
  "username": "matansha",
  "password": "51234",
  "firstname": "matan",
  "lastname": "shamir",
  "age": 30,
  "height": 180,
  "weight": 70,
  "gender": "male",
  "goal": "FIT",
  "mentor": "",
  "tags": [
    "Calorie-counting","Meal plans","Guidance","Lose weight","Women's health", "Vegetarianism"
  ]
}
*/
}
