using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebAPICoreTask2.DTO_folder;
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
            var products = _db.Products.OrderByDescending(p => p.Price)
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
            }).ToList();

            if (product == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(product);
            }
        }




        [HttpGet("Products/CategoryId")]
        public IActionResult GetProductByCategoryId(int id)
        {
            var product = _db.Products.Where(p => p.Category.CategoryId == id).Select(p => new
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
            }).ToList();

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



        [Route("Product/post")]
        [HttpPost]
        public IActionResult CreateProductToCategpry([FromForm] productRequestDTO productt)
        {
            var data = new Product
            {
                ProductName = productt.ProductName,
                Description= productt.Description,
                Price = productt.Price,
                CategoryId = productt.CategoryId,
                ProductImage = productt.ProductImage.FileName,
            };

            var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads"); 
            if (!Directory.Exists(uploadsFolderPath)) 
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }
            var filePath = Path.Combine(uploadsFolderPath, productt.ProductImage.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create)) // عشان تظهر الصورة جوا ال vs واقدر افتحها
            {
                productt.ProductImage.CopyToAsync(stream);
            }

            _db.Products.Add(data);
            _db.SaveChanges();

            return Ok();
        }



        [HttpPut("Products/Put/{id}")]
        public IActionResult EditProduct(int id, [FromForm] productRequestDTO product)
        {
            var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }
            var filePath = Path.Combine(uploadsFolderPath, product.ProductImage.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                product.ProductImage.CopyToAsync(stream);
            }

            var pro = _db.Products.Find(id);
            if (pro == null)
            {
                return NotFound();
            };

            pro.ProductName = product.ProductName;
            pro.Description = product.Description;
            pro.Price = product.Price;
            pro.CategoryId = product.CategoryId;
            pro.ProductImage = product.ProductImage.FileName;

            _db.Products.Update(pro);
            _db.SaveChanges();
            return Ok();
        }




    }
}
