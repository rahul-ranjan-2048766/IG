using Instagram.Models;

namespace Instagram.Services
{
    public interface IQueryService
    {
        public Task<string?> Add(Query query);
        public Task<List<Query>> GetAll(string token);
        public Task<string?> Delete(int id, string token);
        public Task<string?> DeleteAll(string token);
    }
}


