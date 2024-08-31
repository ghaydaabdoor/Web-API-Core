using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPICoreTask2.DTO_folder;
using WebAPICoreTask2.Models;

namespace WebAPICoreTask2.Controllers
{
    [ApiController]
    public class CartItemsController : ControllerBase
    {
       private readonly MyDbContext _db;

        public CartItemsController(MyDbContext db)
        {
            _db= db;
        }

        [Route("CartItems/Get")]
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

        [Route("CartItems/Post")]
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

        [Route("CartItems/Edit/{id}")]
        [HttpPut()]
        public IActionResult editCartItem(int id, [FromBody] CartItemRequestDTO cartitem)
        {
            var specificItem = _db.CartItems.FirstOrDefault(p=>p.Product.ProductId==id);

            specificItem.Quantity=cartitem.Quantity;

            _db.CartItems.Update(specificItem);
            _db.SaveChanges();
            return Ok(specificItem);
        }

        [Route("CartItems/Delete/{id}")]
        [HttpDelete()]
        public IActionResult deleteCartItem(int id)
        {
            var specificItem = _db.CartItems.FirstOrDefault(p => p.Product.ProductId == id);
            _db.CartItems.Remove(specificItem);
            _db.SaveChanges();
            return Ok(specificItem);
        }

    }
}
