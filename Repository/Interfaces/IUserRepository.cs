using Models.Core;
using Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositorys.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Find user with login and password
        /// </summary>
        /// <param name="login">Login of User</param>
        /// <param name="password">Password of User</param>
        /// <returns></returns>
        User GetUser(string login, string password);

        /// <summary>
        /// Save or Update data User
        /// </summary>
        /// <param name="user">Object User for saver or update</param>
        /// <param name="uptadePassword">Option for update password</param>
        User Save(User item, bool uptadePassword = false);

        /// <summary>
        /// List all Users
        /// </summary>
        /// <returns></returns>
        List<UserDto> GetList();

        /// <summary>
        /// List all Users with pagination
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        List<UserDto> GetList(int skip, int size);

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

        /// <summary>
        /// Count all User
        /// </summary>
        int Count();

        /// <summary>
        /// Find user with email
        /// </summary>
        /// <param name="cpf">CPF of User</param>
        /// <returns></returns>
        UserDto GetUser(string cpf);

        /// <summary>
        /// Find user with email
        /// </summary>
        /// <param name="login">Login of User</param>
        /// <returns></returns>
        User GetUserLogin(string login);

        /// <summary>
        /// Return one object user with Id
        /// </summary>
        /// <param name="id">Id of User</param>
        /// <returns></returns>
        User GetUser(int id);
    }
}
