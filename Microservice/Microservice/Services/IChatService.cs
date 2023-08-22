using Microservice.Models;

namespace Microservice.Services
{
    public interface IChatService
    {
        public Task<bool> Add(Chat chat);
        public Task<bool> Delete(int id);
        public Task<bool> DeleteAll();
        public Task<Chat?> Get(int id);
        public Task<List<Chat>> GetAll();
        public Task<bool> Update(Chat chat);
    }
}
