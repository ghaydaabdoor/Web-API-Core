using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WepAPICoreTasks.Models;

namespace WepAPICoreTasks.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly MyDbContext _db;
        public CategoriesController(MyDbContext db)
        {
            _db = db;
        }

        [HttpGet ("Cat/All")]
        public IActionResult Get()
        {
            var categories = _db.Categories.ToList();
            return Ok(categories);
        }

        [Route("Cat/All/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            var categoryById=_db.Categories.Find(id);
            return Ok(categoryById);
        }
    }
}
