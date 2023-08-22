using Microservice.Models;
using Microservice.Repositories;

namespace Microservice.Services
{
    public class QueryService : IQueryService
    {
        private readonly IQueryRepository repository;
        public QueryService(IQueryRepository repository) => this.repository = repository;

        public async Task<bool> Add(Query query) => await repository.Add(query);

        public async Task<bool> Delete(int id) => await repository.Delete(id);

        public async Task<bool> DeleteAll() => await repository.DeleteAll();

        public async Task<Query?> Get(int id) => await repository.Get(id);

        public async Task<List<Query>> GetAll() => await repository.GetAll();

        public async Task<bool> Update(Query query) => await repository.Update(query);
    }
}
