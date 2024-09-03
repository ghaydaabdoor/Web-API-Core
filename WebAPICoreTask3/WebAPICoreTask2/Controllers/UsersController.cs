using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebAPICoreTask2.DTO_folder;
using WebAPICoreTask2.Models;

namespace WebAPICoreTask2.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyDbContext _db;
        private readonly TokenGenerator _tokenGenerator;
        public UsersController(MyDbContext db, TokenGenerator tokenGenerator)
        {
            _db = db;
            _tokenGenerator = tokenGenerator;
        }

        //[Route("Users/All")]
        //[HttpGet]
        //public IActionResult GetAllUsers()
        //{
        //    var users = _db.Users.ToList();
        //    return Ok(users);
        //}

        //[Route("Users/id/{id}")]
        //[HttpGet]
        //public IActionResult GetUsersById(int id)
        //{
        //    var user = _db.Users.FirstOrDefault(a => a.UserId == id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        return Ok(user);
        //    }
        //}

        //[Route("Users/name/{name}")]
        //[HttpGet]
        //public IActionResult GetUserByName(string name)
        //{
        //    var user = _db.Users.FirstOrDefault(p => p.Username == name);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        return Ok(user);
        //    }
        //}

        //[Route("Users/delete/{id}")]
        //[HttpDelete]
        //public IActionResult DeleteUser(int id)
        //{
        //    var user= _db.Users.FirstOrDefault(a=>a.UserId == id);
        //    if (user == null)
        //    {
        //        return BadRequest("UserID does not exist");
        //    }
        //    else
        //    {
        //        _db.Users.Remove(user);
        //        _db.SaveChanges();
        //        return Ok();
        //    }
        //}

        //[Route("Users/Post")]
        //[HttpPost]
        //public IActionResult AddUser([FromForm] userRequestDTO newUser)
        //{
        //    var data = new User
        //    {
        //        Username = newUser.Username,
        //        Email = newUser.Email,
        //        Password = newUser.Password,
        //    };

        //    _db.Users.Add(data);
        //    _db.SaveChanges();
        //    return Ok();
        //}

        //[Route("Users/Put/{id}")]
        //[HttpPut]
        //public IActionResult EditUser(int id, [FromForm] userRequestDTO modifyUser)
        //{
        //    var user=_db.Users.FirstOrDefault(t => t.UserId == id);
        //    user.Username = modifyUser.Username;
        //    user.Email = modifyUser.Email;
        //    user.Password = modifyUser.Password;

        //    _db.Users.Update(user);
        //    _db.SaveChanges();

        //    return Ok();
        //}

        //[Route("calc")]
        //[HttpGet]
        //public IActionResult calc(string x)
        //{
        //    var calcToInt = x.Split(' ');
        //    var num1 = Convert.ToDouble(calcToInt[0]);
        //    var num2= Convert.ToDouble(calcToInt[2]);
        //    var operation = calcToInt[1];
        //    double result = 0;
        //    switch (operation)
        //    {
        //        case "+":
        //            result=num1+num2;
        //            break;
        //        case "-":
        //            result=num1-num2;
        //            break;
        //        default:
        //            return BadRequest("Invalid operation");
        //    }
        //    return Ok(result);

        //}



        //[Route("Problem2/{num1}/{num2}")]
        //[HttpGet]
        //public IActionResult problem2(int num1, int num2)
        //{
        //    if(num1==30 || num2==30 || num1+num2==30)
        //    {
        //        return Ok("true");
        //    }
        //    else
        //    {
        //        return Ok("false");
        //    }
        //}


        //[Route("Problem3")]
        //[HttpGet]
        //public IActionResult problem3(int numb)
        //{
        //    if(numb>0 && (numb%3==0 || numb%7==0))
        //    {
        //        return Ok("true");
        //    }
        //    else
        //    {
        //        return Ok("false");
        //    }
        //}

        [Route("Register")]
        [HttpPost]
        public IActionResult addUser([FromForm] userRequestDTO userDTO)
        {
            byte[] passwordHash;
            byte[] salt;

            PasswordHasherNew.createPasswordHash(userDTO.Password, out passwordHash, out salt); // must be declared above to pass here

            var user = new User
            {
                Password = userDTO.Password,
                PasswordHash = passwordHash, // from line 166
                PasswordSalt = salt, // from line 166
                Username = userDTO.Username,
                Email = userDTO.Email,
            };


            _db.Users.Add(user);
            _db.SaveChanges();
            return Ok(user);
        }


        [HttpPost("login")] // why post not get? more secure to send data in the body of the form not in url
        public IActionResult Login([FromForm] userRequestDTO model)
        {
            var user = _db.Users.FirstOrDefault(x => x.Email == model.Email); // better to get record by email not UserName since it's unique!
            if (user == null || !PasswordHasher.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Unauthorized("Invalid email or password."); // 401 Unauthorized 
            }
            var roles = _db.UserRoles.Where(x => x.UserId == user.UserId).Select(ur => ur.Role).ToList();
            var token = _tokenGenerator.GenerateToken(user.Username, roles);

            return Ok(new { Token = token, UserId1= user.UserId }); // now product controller and make authorization,, it will generate token that will be used in front end
        }


        [Route("Users/id/{id}")]
        [HttpGet]
        public IActionResult GetUsersById(int id)
        {
            var user = _db.Users.FirstOrDefault(a => a.UserId == id);
            if (user == null)
            {
                return NotFound("User does not exist");
            }
            else
            {
                return Ok(user);
            }
        }

    }
}
