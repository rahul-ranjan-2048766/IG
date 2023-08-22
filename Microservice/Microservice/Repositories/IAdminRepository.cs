using Microservice.Models;

namespace Microservice.Repositories
{
    public interface IAdminRepository
    {
        public Task<bool> Add(Admin admin);
        public Task<bool> Delete(int id);
        public Task<bool> DeleteAll();
        public Task<Admin?> Get(int id);
        public Task<List<Admin>> GetAll();
        public Task<bool> Update(Admin admin);
    }
}
