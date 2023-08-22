using Instagram.Models;

namespace Instagram.Services
{
    public interface ICommentService
    {
        public Task<string?> Add(Comment comment, string token);
        public Task<string?> Delete(int id, string token);
        public Task<string?> DeleteAll(string token);
        public Task<Comment?> Get(int id, string token);
        public Task<List<Comment>> GetAll(string token);
        public Task<string?> Update(Comment comment, string token);
    }
}
