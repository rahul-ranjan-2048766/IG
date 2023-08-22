using Microservice.Models;

namespace Microservice.Services
{
    public interface ICommentService
    {
        public Task<bool> Add(Comment coment);
        public Task<bool> Delete(int id);
        public Task<bool> DeleteAll();
        public Task<Comment?> Get(int id);
        public Task<List<Comment>> GetAll();
        public Task<bool> Update(Comment comment);
    }
}
