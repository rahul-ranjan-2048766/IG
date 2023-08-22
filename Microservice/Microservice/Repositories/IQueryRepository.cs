using Microservice.Models;

namespace Microservice.Repositories
{
    public interface IQueryRepository
    {
        public Task<bool> Add(Query query);
        public Task<bool> Delete(int id);
        public Task<bool> DeleteAll();
        public Task<Query?> Get(int id);
        public Task<List<Query>> GetAll();
        public Task<bool> Update(Query query);
    }
}
