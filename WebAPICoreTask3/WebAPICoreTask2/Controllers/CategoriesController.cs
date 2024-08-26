using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPICoreTask2.DTO_folder;
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



        [Route("Categories/post")]
        [HttpPost]
        public IActionResult CreateCategory([FromForm] categoryRequestDTO category)
        {
            var data = new Category
            {
                CategoryName = category.CategoryName,
                CategoryImage = category.CategoryImage.FileName,
            };

            var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads"); //جيبلي الباث مع اسم الفولدر اللي عملته، بركبهم جنب بعض
            if (!Directory.Exists(uploadsFolderPath)) // اذا الفولدر مش موجود بعملي واحد جديد
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }
            var filePath = Path.Combine(uploadsFolderPath, category.CategoryImage.FileName); // من باث الفولدر واسم الصورة :: بتجيبلي باث الصورة

            using (var stream = new FileStream(filePath, FileMode.Create)) // عشان تظهر الصورة جوا ال vs واقدر افتحها
            {
                category.CategoryImage.CopyToAsync(stream);
            }

            _db.Categories.Add(data);
            _db.SaveChanges();

            //var response = new
            //{
            //    Success = true,
            //    Message = $"Category {category.CategoryName} created successfully!",
            //    Code = StatusCodes.Status200OK
            //};

            return Ok();
        }


        [HttpPut("Categories/put/{id}")]
        public IActionResult EditCategory(int id, [FromForm] categoryRequestDTO category)
        {
            var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }
            var filePath = Path.Combine(uploadsFolderPath, category.CategoryImage.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                category.CategoryImage.CopyToAsync(stream);
            }

            var cat = _db.Categories.Find(id);
            if (cat == null) return NotFound();

            cat.CategoryName = category.CategoryName;
            cat.CategoryImage = category.CategoryImage.FileName;

            _db.Categories.Update(cat);
            _db.SaveChanges();
            return Ok();
        }


    }
}
