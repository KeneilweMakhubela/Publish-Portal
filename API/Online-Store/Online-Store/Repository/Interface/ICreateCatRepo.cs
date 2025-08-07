using Online_Store.Models.Domain;

namespace Online_Store.Repository.Interface
{
    public interface ICreateCatRepo
    {
        //Take a category insert to a db and return a created category
        Task<Category>CreateAsync(Category category);
        
        Task<Category?> GetByIdAsync(Guid id);


        Task<IEnumerable<Category>> GetAllAsync();

        Task<Category?> UpdateAsync(Guid id, Category category);

        Task<bool> DeleteAsync(Guid id);

        Task DeleteAllAsync();


    }
}
