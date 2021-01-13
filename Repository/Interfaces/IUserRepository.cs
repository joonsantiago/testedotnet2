using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositorys.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Buscar um usuário
        /// </summary>
        /// <param name="login">Login do usuário</param>
        /// <param name="password">Password do usuário</param>
        /// <returns></returns>
        User FindUser(string login, string password);
    }
}
