using Instagram.Models;

namespace Instagram.Services
{
    public interface IChatService
    {
        public Task<string?> Add(Chat chat, string token);
        public Task<string?> Delete(int id, string token);
        public Task<string?> DeleteAll(string token);
        public Task<Chat?> Get(int id, string token);
        public Task<List<Chat>> GetAll(string token);
        public Task<string?> Update(Chat chat, string token);
    }
}
