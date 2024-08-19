using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WepAPICoreTasks.Models;

namespace WepAPICoreTasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly MyDbContext _db;
        public ProductsController(MyDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var products = _db.Products.Include(t=>t.Category).ToList();
            return Ok(products);
        }

        [HttpGet("id")]
        public IActionResult GetById(int id)
        {
            var productById = _db.Products.Include(t => t.Category).Where(a => a.ProductId == id);
            return Ok(productById);
        }
    }
}
