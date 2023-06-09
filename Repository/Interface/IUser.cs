using DataModels.Models;
using DataModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IUser
    {
        public UserViewModel Login(AddUser model);

        public User Register(Registration model);

    }


}
