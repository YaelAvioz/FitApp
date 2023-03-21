﻿using AutoMapper;
using FitAppServer.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;

namespace FitAppServer.Services
{
    public class GenericService<T, TDTO> where T : GenericEntity where TDTO : GenericEntity
    {
        private readonly IMongoCollection<T> _collection;
        private readonly IMongoDatabase _db;
        private readonly IMapper _mapper;
        private readonly string connectionString = "mongodb+srv://FitApp:FitAppYaelCoral@cluster0.hsylfut.mongodb.net/?retryWrites=true&w=majority";
        private readonly string databaseName = "fitapp";

        public GenericService(IMapper mapper)
        {
            var client = new MongoClient(connectionString);
            _db = client.GetDatabase(databaseName);
            string s = typeof(T).ToString();
            _collection = _db.GetCollection<T>(s.Substring(s.LastIndexOf('.') + 1).ToLower());
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

        public async Task<TDTO> Get(string id)
        {
            var objectId = new ObjectId(id);
            var entity = await _collection.Find(x => x.Id.Equals(objectId)).FirstOrDefaultAsync();
            return _mapper.Map<TDTO>(entity);
        }

        public async Task<TDTO> Create(T entity)
        {
            await _collection.InsertOneAsync(entity);
            return _mapper.Map<TDTO>(entity);
        }

        public async Task<TDTO> Update(string id, T newEntity)
        {
            var objectId = new ObjectId(id);
            newEntity.Id = objectId.ToString();
            await _collection.ReplaceOneAsync(x => x.Id.Equals(objectId), newEntity);
            return _mapper.Map<TDTO>(newEntity);
        }

        public async Task Delete(string id)
        {
            var objectId = new ObjectId(id);
            await _collection.DeleteOneAsync(x => x.Id.Equals(objectId));
        }
    }
}