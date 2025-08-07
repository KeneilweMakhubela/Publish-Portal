using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Store.Data;
using Online_Store.Models.Domain;
using Online_Store.Models.DTO;
using Online_Store.Repository.Interface;

namespace Online_Store.Controllers
{
    // https:localhost:xxxx/api/category
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICreateCatRepo categoryRepo;

        //injecting the dbcontext inside the controller
        /*private readonly AppDbContext dbContext;

        public CategoriesController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }*/

        public CategoriesController(ICreateCatRepo categoryRepo)
        {
            this.categoryRepo = categoryRepo;
        }

        //
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCatReqDto request)
        {
            //convert dto to domain model
            var category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };

            //Using injected Dbcontext class to create 
            //save data to the database
            /*
            await dbContext.CategoryC.AddAsync(category);
            await dbContext.SaveChangesAsync();  */

            await categoryRepo.CreateAsync(category);

            //Convert domain to dto

            var response = new CatRespDto
            {
                Id = category.Id,
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };

            return Ok(response);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var category = await categoryRepo.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var response = new CatRespDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return Ok(response);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await categoryRepo.GetAllAsync();

            var response = categories.Select(c => new CatRespDto
            {
                Id = c.Id,
                Name = c.Name,
                UrlHandle = c.UrlHandle
            });

            return Ok(response);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, CreateCatReqDto request)
        {
            // Convert DTO to domain model
            var updatedCategory = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };

            // Call the repository to update
            var result = await categoryRepo.UpdateAsync(id, updatedCategory);

            if (result == null)
            {
                return NotFound();
            }

            // Convert domain model to response DTO
            var response = new CatRespDto
            {
                Id = result.Id,
                Name = result.Name,
                UrlHandle = result.UrlHandle
            };

            return Ok(response);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var deleted = await categoryRepo.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent(); // 204 No Content
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteAllCategories()
        {
            await categoryRepo.DeleteAllAsync();
            return NoContent(); // 204 No Content
        }


    }
}
