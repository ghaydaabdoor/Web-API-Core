using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WepAPICoreTasks.Models;

namespace WepAPICoreTasks.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly MyDbContext _db;
        public ProductsController(MyDbContext db)
        {
            _db = db;
        }

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    var products = _db.Products.Include(t => t.Category).ToList();
        //    return Ok(products);
        //}

        //[HttpGet("id")]
        //public IActionResult GetById(int id)
        //{
        //    var productById = _db.Products.Include(t => t.Category).Where(a => a.ProductId == id);
        //    return Ok(productById);
        //}

        //[HttpGet("name")]
        //public IActionResult GetByName(string name)
        //{
        //    var productByName = _db.Products.Include(t => t.Category).Where(a => a.ProductName == name);
        //    return Ok(productByName);
        //}


        [Route("Products/CategoryID/{CategoryID}/Price/{price}")]
        [HttpGet]
        public IActionResult GetProductByCatAndPrice(int CategoryID, int price)
        {
            return Ok($"the product with CategoryID of {CategoryID} and Price {price} is as follows");
        }

        [Route("Products/CategoryID")]
        [HttpGet]
        public IActionResult GetProductById(int ProductID)
        {
            var product= _db.Products.FirstOrDefault(b=>b.ProductId==ProductID);
            return Ok(product);
        }



        //[HttpGet("{id}/{price}")]
        //public IActionResult GetSpecific(int id, int price)
        //{
        //    var productById = _db.Products.Include(t => t.Category).Where(t => t.CategoryId == id && t.Price  >price);
        //    int count = productById.Count();
        //    return Ok(count);
        //}


        //[HttpDelete]
        //public IActionResult Delete(int id)
        //{

        //    var del = _db.Products.FirstOrDefault(e => e.ProductId == id);
        //    if (del == null)
        //    {
        //        return NotFound();
        //    }
        //    _db.Products.Remove(del);
        //    _db.SaveChanges();
        //    return Ok();

        //}






    }
}
