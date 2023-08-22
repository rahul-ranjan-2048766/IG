using Microservice.Models;

namespace Microservice.Repositories
{
    public interface ICommentRepository
    {
        public Task<bool> Add(Comment comment);
        public Task<bool> Delete(int id);
        public Task<bool> DeleteAll();
        public Task<Comment?> Get(int id);
        public Task<List<Comment>> GetAll();
        public Task<bool> Update(Comment comment);
    }
}
