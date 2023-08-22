using Microservice.Models;
using Microservice.Repositories;

namespace Microservice.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository repository;
        public CommentService(ICommentRepository repository) => this.repository = repository;

        public async Task<bool> Add(Comment comment) => await repository.Add(comment);

        public async Task<bool> Delete(int id) => await repository.Delete(id);

        public async Task<bool> DeleteAll() => await repository.DeleteAll();

        public async Task<Comment?> Get(int id) => await repository.Get(id);

        public async Task<List<Comment>> GetAll() => await repository.GetAll();

        public async Task<bool> Update(Comment comment) => await repository.Update(comment);
    }
}
