using Microservice.Models;

namespace Microservice.Repositories
{
    public interface IUserRepository
    {
        public Task<bool> Add(User user);
        public Task<bool> Delete(int id);
        public Task<bool> DeleteAll();
        public Task<User?> Get(int id);
        public Task<List<User>> GetAll();
        public Task<bool> Update(User user);
    }
}
