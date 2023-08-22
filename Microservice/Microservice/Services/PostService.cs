using Microservice.Models;
using Microservice.Repositories;

namespace Microservice.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository repository;
        public PostService(IPostRepository repository) => this.repository = repository;

        public async Task<bool> Add(Post data) => await repository.Add(data);

        public async Task<bool> Delete(int id) => await repository.Delete(id);

        public async Task<bool> DeleteAll() => await repository.DeleteAll();

        public async Task<Post?> Get(int id) => await repository.Get(id);

        public async Task<List<Post>> GetAll() => await repository.GetAll();

        public async Task<bool> Update(Post data) => await repository.Update(data);
    }
}

