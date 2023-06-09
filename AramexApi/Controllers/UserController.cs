
using DataModels.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;

namespace AramexApi.Controllers
{
    [ApiController]
    [Route ("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUser _userrepository;
        public UserController(IUser userrepository)
        {
           
            this._userrepository = userrepository;
        }

        [HttpGet]
        public IActionResult GetUsers() {

            return null;
          /*  return Ok(dbContext.Users.ToList());*/
           
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
                var response = _userrepository.Login(model);
                if(response== null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(response);
                }
              /*  var userpassword = dbContext.Users.FirstOrDefault(u => u.Password == model.Password && u.Email==model.Email.ToLower());
                if (userpassword != null) {
                    return Ok(userpassword);
                }
                else
                {
                    return BadRequest();
                }*/

            }
         
                
        }

        [HttpPost]
        public IActionResult AddUsers(Registration model)
        {
            var register = _userrepository.Register(model);

            if (register == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(register);
            }
            /* if( dbContext.Users.Any(u => u.Email == adduser.Email))
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
                  }*/
            
        }

    }
}
