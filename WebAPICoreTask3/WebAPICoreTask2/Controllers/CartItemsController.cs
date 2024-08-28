using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPICoreTask2.DTO_folder;
using WebAPICoreTask2.Models;

namespace WebAPICoreTask2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
       private readonly MyDbContext _db;

        public CartItemsController(MyDbContext db)
        {
            _db= db;
        }

        [HttpGet]
        public IActionResult GetCartItems()
        {
            var cartItems = _db.CartItems.Select(p => new CartItemResponseDTO
            {
                CartItemId = p.CartItemId,
                CartId = p.CartId,
                Quantity = p.Quantity,
                Product = new productDTO
                {
                    ProductId=p.Product.ProductId,
                    ProductName=p.Product.ProductName,
                    Price = p.Product.Price,
                }
            });
            return Ok(cartItems);
        }


        [HttpPost]
        public IActionResult AddtoCart([FromBody] AddCartItemRequestDTO add)
        {

            var data = new CartItem
            {
                CartId = add.CartId,
                ProductId = add.ProductId,
                Quantity = add.Quantity,
            };
            _db.CartItems.Add(data);
            _db.SaveChanges();
            return Ok(data);
        }
    }
}
