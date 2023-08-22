using Microservice.Models;
using Microservice.Repositories;

namespace Microservice.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository repository;
        public MessageService(IMessageRepository repository) => this.repository = repository;

        public async Task<bool> Add(Community data) => await repository.Add(data);

        public async Task<bool> Delete(int id) => await repository.Delete(id);

        public async Task<bool> DeleteAll() => await repository.DeleteAll();

        public async Task<Community?> Get(int id) => await repository.Get(id);

        public async Task<List<Community>> GetAll() => await repository.GetAll();

        public async Task<bool> Update(Community data) => await repository.Update(data);
    }
}
