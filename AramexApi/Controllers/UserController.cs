using AramexApi.Data;
using AramexApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AramexApi.Controllers
{
    [ApiController]
    [Route ("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly AramexContext dbContext;

        public UserController(AramexContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetUsers() {


            return Ok(dbContext.Users.ToList());
           
        }

        [HttpPost]
        public IActionResult login(AddUser model)
        {
            if (model == null)
            {
                return BadRequest("Employee null here");
            }
            else
            {
                var userpassword = dbContext.Users.FirstOrDefault(u => u.Password == model.Password && u.Email==model.Email.ToLower());
                if (userpassword != null) {
                    return Ok(userpassword);
                }
                else
                {
                    return BadRequest();
                }

            }
            return Ok();
                
        }

        [HttpPost]
        public IActionResult AddUsers(Registration adduser)
        {
           if( dbContext.Users.Any(u => u.Email == adduser.Email))
            {
                return BadRequest();
            }
            else { 
                
              var  user = new User
            {

                UserName = adduser.UserName,
                Password = adduser.Password,
                Email = adduser.Email,
            };


            dbContext.Users.Add(user);
            dbContext.SaveChanges();
            return Ok(user);
                }
        }

    }
}
