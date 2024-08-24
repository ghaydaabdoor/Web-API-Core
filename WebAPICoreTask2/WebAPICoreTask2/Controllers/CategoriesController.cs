using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPICoreTask2.Models;

namespace WebAPICoreTask2.Controllers
{
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly MyDbContext _db;
        public CategoriesController(MyDbContext db)
        {
            _db = db;
        }

        [Route("Categories/All")]
        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var categories = _db.Categories.ToList();
            return Ok(categories);
        }


        [Route("Categories/id/{id:int:min(5)}")]
        [HttpGet]
        public IActionResult GetCategoryById(int id)
        {
            var category = _db.Categories.FirstOrDefault(a => a.CategoryId == id);
            return Ok(category);
        }

        [Route("Categories/name/{name}")]
        [HttpGet]
        public IActionResult GetCategoryByName(string name)
        {
            var category = _db.Categories.FirstOrDefault(b => b.CategoryName == name);
            return Ok(category);
        }

        [Route("Categories/delete")]
        [HttpDelete]
        public void DeleteCategoryById(int id)
        {
            var category = _db.Categories.FirstOrDefault(a => a.CategoryId == id);
            if (category != null)
            {
                _db.Categories.Remove(category);
                _db.SaveChanges();
            }

        }


    }
}
