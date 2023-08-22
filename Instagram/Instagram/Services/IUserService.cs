using Instagram.Models;

namespace Instagram.Services
{
    public interface IUserService
    {
        public Task<string?> Add(User user);
        public Task<string?> Delete(int id, string token);
        public Task<string?> DeleteAll(string token);
        public Task<User?> Get(int id, string token);
        public Task<List<User>> GetAll();
        public Task<string?> Update(User user, string token);
    }
}
