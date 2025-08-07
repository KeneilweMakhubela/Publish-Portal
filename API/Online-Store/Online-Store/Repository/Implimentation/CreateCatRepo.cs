using Microsoft.EntityFrameworkCore;
using Online_Store.Data;
using Online_Store.Models.Domain;
using Online_Store.Repository.Interface;

namespace Online_Store.Repository.Implimentation
{
    public class CreateCatRepo : ICreateCatRepo
    {
        private readonly AppDbContext dbContext;

        public CreateCatRepo(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await dbContext.CategoryC.AddAsync(category);
            await dbContext.SaveChangesAsync();

            return category;
        }


        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await dbContext.CategoryC.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await dbContext.CategoryC.ToListAsync();
        }

        public async Task<Category?> UpdateAsync(Guid id, Category category)
        {
            var existingCategory = await dbContext.CategoryC.FirstOrDefaultAsync(c => c.Id == id);

            if (existingCategory == null)
            {
                return null;
            }

            existingCategory.Name = category.Name;
            existingCategory.UrlHandle = category.UrlHandle;

            await dbContext.SaveChangesAsync();

            return existingCategory;
        }


        public async Task<bool> DeleteAsync(Guid id)
        {
            var category = await dbContext.CategoryC.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return false;
            }

            dbContext.CategoryC.Remove(category);
            await dbContext.SaveChangesAsync();
            return true;
        }


        public async Task DeleteAllAsync()
        {
            var allCategories = await dbContext.CategoryC.ToListAsync();

            if (allCategories.Any())
            {
                dbContext.CategoryC.RemoveRange(allCategories);
                await dbContext.SaveChangesAsync();
            }
        }


    }
}
