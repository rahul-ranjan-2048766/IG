using Microsoft.EntityFrameworkCore;
using Microservice.Models;

namespace Microservice.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context) => this._context = context;


        public async Task<bool> Add(User user)
        {
            user.Id = 0; 
            user.BANNED = false;
            user.DateTimeOfRegistration = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, 
                TimeZoneInfo.FindSystemTimeZoneById("Asia/Kolkata"));
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            User? data = await _context.Users.FirstOrDefaultAsync(option => option.Id == id);
            if (data == null)
                return false;
            _context.Users.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAll()
        {
            List<User> users = await _context.Users.ToListAsync();
            foreach (User user in users)
                _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User?> Get(int id) =>
            await _context.Users.FirstOrDefaultAsync(option => option.Id == id);

        public async Task<List<User>> GetAll() =>
            await _context.Users.ToListAsync();

        public async Task<bool> Update(User user)
        {
            try
            {
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }
    }
}
