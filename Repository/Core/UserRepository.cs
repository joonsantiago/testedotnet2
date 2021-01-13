using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repositorys.Core
{
    public class UserRepository
    {
        public static User GetUser(string login, string password)
        {

            var dataUser = new List<User>();
            dataUser.Add(new User { Id = 2, Email = "batman@batman.com", Login = "batman", Name = "batman", Password = "batman", Role = 1 });

            return dataUser.Where(x => x.Login.ToLower() == login.ToLower() && x.Password.ToLower() == password.ToLower()).FirstOrDefault();
        }

    }
}
