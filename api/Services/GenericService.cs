using api.Model;
using AutoMapper;
using NHibernate;
using NHibernate.Helper;
using NHibernate.Linq;

namespace api.Services
{
    public class GenericService<T, TDTO> where T : GenericEntity where TDTO : GenericEntity
    {
        protected readonly List<T> _entities = GenericDB<T>.Entities;
        protected readonly ISessionFactory _sessionFactory;
        protected readonly IMapper _mapper;
        public GenericService(IMapper mapper)
        {
            _sessionFactory = NHibernateManager.GetSession();
            _mapper = mapper;
        }

        public async Task<List<TDTO>> GetAll()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var entities= await session.Query<T>().ToListAsync();
                return _mapper.Map<List<TDTO>>(entities);  
            }
        }

        public async Task<TDTO> Get(int id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var entity = await session.GetAsync<T>(id);
                return _mapper.Map<TDTO>(entity);
            }
        }

        public async Task<TDTO> Create(T entity)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var id =await session.SaveAsync(entity);
                    transaction.Commit();
                    entity.Id = (int)id;
                    return _mapper.Map<TDTO>(entity);
                }
            }
        }

        public async Task<TDTO> Update(int id, T newEntity)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var entityExists = await session.LoadAsync<T>(id);
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        newEntity.Id = entityExists.Id;
                        await session.MergeAsync(newEntity);
                        transaction.Commit();
                    }
                }
                return _mapper.Map<TDTO>(newEntity);
            }
        }

        public async void Delete(int id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var entity = await session.LoadAsync<T>(id);
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        await session.DeleteAsync(entity);
                        transaction.Commit();
                    }
                }
            }
        }

        public static class GenericDB<T>
        {
            public static List<T> Entities { get; set; } = new List<T>();
        }
    }
}
