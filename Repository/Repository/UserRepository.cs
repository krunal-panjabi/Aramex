using DataModels.Data;
using DataModels.Models;
using DataModels.ViewModel;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class UserRepository : IUser
    {
        private readonly AramexContext _aramexContext;

        public UserRepository(AramexContext aramexContext)
        {
            _aramexContext = aramexContext;
        }

        public UserViewModel Login(AddUser model)
        {
            var userpassword = _aramexContext.Users.FirstOrDefault(u => u.Password == model.Password && u.Email == model.Email.ToLower());
            if (userpassword != null)
            {
                UserViewModel user = new UserViewModel
                {
                    Email = userpassword.Email,
                    UserId = userpassword.UserId,
                    UserName = userpassword.UserName,
                    Password = userpassword.Password,
                };
                return user;
            }
            else
            {
                return null;
            }
        }

        public User Register(Registration model)
        {
            if (_aramexContext.Users.Any(u => u.Email == model.Email))
            {
                return null;
            }
            else
            {

                var user = new User
                {

                    UserName = model.UserName,
                    Password = model.Password,
                    Email = model.Email,
                };


                _aramexContext.Users.Add(user);
                _aramexContext.SaveChanges();
                return (user);
            }
        }



    }
}
