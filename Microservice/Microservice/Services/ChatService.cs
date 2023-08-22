using Microservice.Models;
using Microservice.Repositories;

namespace Microservice.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository repository;
        public ChatService(IChatRepository repository) => this.repository = repository;

        public async Task<bool> Add(Chat chat) => await repository.Add(chat);

        public async Task<bool> Delete(int id) => await repository.Delete(id);

        public async Task<bool> DeleteAll() => await repository.DeleteAll();

        public async Task<Chat?> Get(int id) => await repository.Get(id);

        public async Task<List<Chat>> GetAll() => await repository.GetAll();

        public async Task<bool> Update(Chat chat) => await repository.Update(chat);
    }
}
