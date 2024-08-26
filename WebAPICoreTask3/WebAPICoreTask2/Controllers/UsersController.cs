using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPICoreTask2.DTO_folder;
using WebAPICoreTask2.Models;

namespace WebAPICoreTask2.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyDbContext _db;
        public UsersController(MyDbContext db)
        {
            _db = db;
        }

        [Route("Users/All")]
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _db.Users.ToList();
            return Ok(users);
        }

        [Route("Users/id/{id}")]
        [HttpGet]
        public IActionResult GetUsersById(int id)
        {
            var user = _db.Users.FirstOrDefault(a => a.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(user);
            }
        }

        [Route("Users/name/{name}")]
        [HttpGet]
        public IActionResult GetUserByName(string name)
        {
            var user = _db.Users.FirstOrDefault(p => p.Username == name);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(user);
            }
        }

        [Route("Users/delete/{id}")]
        [HttpDelete]
        public IActionResult DeleteUser(int id)
        {
            var user= _db.Users.FirstOrDefault(a=>a.UserId == id);
            if (user == null)
            {
                return BadRequest("UserID does not exist");
            }
            else
            {
                _db.Users.Remove(user);
                _db.SaveChanges();
                return Ok();
            }
        }

        [Route("Users/Post")]
        [HttpPost]
        public IActionResult AddUser([FromForm] userRequestDTO newUser)
        {
            var data = new User
            {
                Username = newUser.Username,
                Email = newUser.Email,
                Password = newUser.Password,
            };

            _db.Users.Add(data);
            _db.SaveChanges();
            return Ok();
        }

        [Route("Users/Put/{id}")]
        [HttpPut]
        public IActionResult EditUser(int id, [FromForm] userRequestDTO modifyUser)
        {
            var user=_db.Users.FirstOrDefault(t => t.UserId == id);
            user.Username = modifyUser.Username;
            user.Email = modifyUser.Email;
            user.Password = modifyUser.Password;

            _db.Users.Update(user);
            _db.SaveChanges();

            return Ok();
        }


    }
}
