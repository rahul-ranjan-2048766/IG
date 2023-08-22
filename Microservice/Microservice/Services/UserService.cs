using Microservice.Models;
using Microservice.Repositories;

namespace Microservice.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository repository;
        public UserService(IUserRepository repository) =>
            this.repository = repository;

        public async Task<bool> Add(User user) => await repository.Add(user);

        public async Task<bool> Delete(int id) => await repository.Delete(id);

        public async Task<bool> DeleteAll() => await repository.DeleteAll();

        public async Task<User?> Get(int id) => await repository.Get(id);

        public async Task<List<User>> GetAll() => await repository.GetAll();

        public async Task<bool> Update(User user) => await repository.Update(user);
    }
}
