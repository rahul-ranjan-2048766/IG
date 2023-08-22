using Microservice.Models;

namespace Microservice.Repositories
{
    public interface IMessageRepository
    {
        public Task<bool> Add(Community data);
        public Task<bool> Delete(int id);
        public Task<bool> DeleteAll();
        public Task<Community?> Get(int id);
        public Task<List<Community>> GetAll();
        public Task<bool> Update(Community data);
    }
}
