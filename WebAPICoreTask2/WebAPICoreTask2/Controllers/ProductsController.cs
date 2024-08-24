using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebAPICoreTask2.Models;

namespace WebAPICoreTask2.Controllers
{
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly MyDbContext _db;
        public ProductsController(MyDbContext db)
        {
            _db = db;
        }

        [HttpGet ("Products/All")]
        public IActionResult GetAllProducts()
        {
            var products = _db.Products
                                 .Select(p => new
                                 {
                                     p.ProductId,
                                     p.ProductName,
                                     p.Description,
                                     p.Price,
                                     p.ProductImage,
                                     Category = new
                                     {
                                         p.Category.CategoryId,
                                         p.Category.CategoryName,
                                         p.Category.CategoryImage
                                     }
                                 })
                                 .ToList();
            return Ok(products);
        }

        [HttpGet ("Products/id")]
        public IActionResult GetProductById(int id)
        {
            var product = _db.Products.Where(p => p.ProductId == id).Select(p => new
            {
                p.ProductId,
                p.ProductName,
                p.Description,
                p.Price,
                p.ProductImage,
                Category = new
                {
                    p.Category.CategoryId,
                    p.Category.CategoryName,
                    p.Category.CategoryImage,
                }
            }).FirstOrDefault();

            if (product == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(product);
            }
        }


        [HttpGet("Product/name")]
        public IActionResult GetProductByName(int id, string name)
        {
            var product = _db.Products.Where(p => p.ProductName == name && p.ProductId == id).
                Select(p => new
                {
                    p.ProductId,
                    p.ProductName,
                    p.Description,
                    p.Price,
                    p.ProductImage,
                    Category = new
                    {
                        p.Category.CategoryId,
                        p.Category.CategoryName,
                        p.Category.CategoryImage,
                    }
                }).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }
            if (id > 10)
            {
                return BadRequest("Max value allowed for id is 10");
            }
            else
            {
                return Ok(product);
            }
             
        }

        [HttpDelete ("Products/delete")]
        public void DeleteProduct(int id)
        {
            var product=_db.Products.First(p => p.ProductId == id);
            _db.Products.Remove(product);
            _db.SaveChanges();
        }
    }
}
