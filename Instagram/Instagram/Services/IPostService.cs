using Instagram.Models;

namespace Instagram.Services
{
    public interface IPostService
    {
        public Task<string?> Add(Post data, string token);
        public Task<string?> Delete(int id, string token);
        public Task<string?> DeleteAll(string token);
        public Task<Post?> Get(int id, string token);
        public Task<List<Post>> GetAll(string token);
        public Task<string?> Update(Post data, string token);
    }
}
