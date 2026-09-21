using Microsoft.EntityFrameworkCore;
using QuanLiSach.Models;

namespace QuanLiSach.Repositories
{
    public class EFCategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public EFCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }
        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories
                                 .Include(c => c.Books)
                                 .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task AddAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> ExistsByNameAsync(string name, int? excludeId = null)
        {
            var nameClean = name.Trim().ToLower();

            return await _context.Categories
                .AnyAsync(c => c.Name.ToLower() == nameClean && (excludeId == null || c.Id != excludeId));
        }
    }
}
