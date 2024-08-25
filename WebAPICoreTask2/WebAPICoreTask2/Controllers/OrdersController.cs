using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPICoreTask2.Models;
using Microsoft.EntityFrameworkCore;


namespace WebAPICoreTask2.Controllers
{
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly MyDbContext _db;
        public OrdersController(MyDbContext db)
        {
            _db = db;
        }

        [HttpGet("Orders/All")]
        public IActionResult GetAllOrders()
        {
            var orders = _db.Orders.Select(h => new
            {
                h.OrderId,
                h.OrderDate,
                User = new
                {
                    h.User.UserId,
                    h.User.Username,
                    h.User.Email,
                }
            });
            return Ok (orders);
        }

        [HttpGet("Order/id/{id:int:min(1)}")]
        public IActionResult GetOrdersById(int id)
        {
            var order = _db.Orders.Where(a=>a.OrderId == id).Select(h=> new
            {
                h.OrderId,
                h.OrderDate,
                User = new
                {
                    h.User.UserId,
                    h.User.Username,
                    h.User.Email,
                }
            }).FirstOrDefault();
            if (order == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(order);
            }
        }


        [HttpGet("Order/name/{name}")]
        public IActionResult GetOrdersByName(string name)
        {
            var order = _db.Orders.Where(a => a.User.Username == name).Select(h => new
            {
                h.OrderId,
                h.OrderDate,
                User = new
                {
                    h.User.UserId,
                    h.User.Username,
                    h.User.Email,
                }
            }).FirstOrDefault();
            if (order == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(order);
            }
        }

        [HttpDelete ("Orders/delete/{id:int:min(1)}")]
        public IActionResult Delete(int id)
        {
            var order=_db.Orders.FirstOrDefault(a=>a.OrderId == id);
            if (order == null)
            {
                return BadRequest("id is not exist");
            }
            else
            {
                _db.Orders.Remove(order);
                _db.SaveChanges();
                return Ok();
            }
        }

    }
}
