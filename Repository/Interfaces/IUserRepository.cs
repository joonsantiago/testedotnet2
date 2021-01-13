using Models;
using Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositorys.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Find user with logiin and password
        /// </summary>
        /// <param name="login">Login of User</param>
        /// <param name="password">Password of User</param>
        /// <returns></returns>
        User FindUser(string login, string password);

        /// <summary>
        /// Save or Update data User
        /// </summary>
        /// <param name="user">Object User for saver or update</param>
        void Save(User item);

        /// <summary>
        /// List all Users
        /// </summary>
        /// <returns></returns>
        List<UserDto> GetList();

        /// <summary>
        /// Find User by Id
        /// </summary>
        /// <param name="id">Value of Id for search</param>
        /// <returns></returns>
        UserDto GetById(int id);

        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="item">Object User for delete</param>
        void Delete(User item);
    }
}
