using Microservice.Models;

namespace Microservice.Services
{
    public interface IPostService
    {
        public Task<bool> Add(Post data);
        public Task<bool> Delete(int id);
        public Task<bool> DeleteAll();
        public Task<Post?> Get(int id);
        public Task<List<Post>> GetAll();
        public Task<bool> Update(Post data);
    }
}
