using Microsoft.EntityFrameworkCore;
using Microservice.Models;

namespace Microservice.Repositories
{
    public class ProfilePicRepository : IProfilePicRepository
    {
        private readonly DataContext _context;
        public ProfilePicRepository(DataContext context) => this._context = context;

        public async Task<bool> Add(ProfilePic data)
        {
            data.Id = 0;
            data.DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow,
                TimeZoneInfo.FindSystemTimeZoneById("Asia/Kolkata"));
            await _context.ProfilePics.AddAsync(data);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            ProfilePic? data = await _context.ProfilePics.FirstOrDefaultAsync(option => 
            option.Id == id);

            if (data != null)
            {
                _context.ProfilePics.Remove(data);
                await _context.SaveChangesAsync();
            }
            return true;
        }

        public async Task<bool> DeleteAll()
        {
            List<ProfilePic> pics = await _context.ProfilePics.ToListAsync();
            foreach (ProfilePic pic in pics)
                _context.ProfilePics.Remove(pic);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ProfilePic?> Get(int id) =>
            await _context.ProfilePics.FirstOrDefaultAsync(option => option.Id == id);

        public async Task<List<ProfilePic>> GetAll() =>
            await _context.ProfilePics.ToListAsync();

        public async Task<bool> Update(ProfilePic data)
        {
            try
            {
                _context.Entry(data).State = EntityState.Modified;
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
