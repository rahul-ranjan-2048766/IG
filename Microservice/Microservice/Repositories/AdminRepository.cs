using Microservice.Models;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly DataContext _context;
        public AdminRepository(DataContext context) => _context = context;

        public async Task<bool> Add(Admin admin)
        {
            admin.Id = 0;
            admin.DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow,
                TimeZoneInfo.FindSystemTimeZoneById("Asia/Kolkata"));
            await _context.Admins.AddAsync(admin);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            Admin? admin = await _context.Admins.FirstOrDefaultAsync(option => 
                            option.Id == id);

            if (admin == null) return false;

            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAll()
        {
            List<Admin> Admins = await _context.Admins.ToListAsync();
            foreach (Admin admin in Admins)
                _context.Admins.Remove(admin);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Admin?> Get(int id) => 
            await _context.Admins.FirstOrDefaultAsync(option => option.Id == id);

        public async Task<List<Admin>> GetAll() =>
            await _context.Admins.ToListAsync();

        public async Task<bool> Update(Admin admin)
        {
            try
            {
                _context.Entry(admin).State = EntityState.Modified;
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
