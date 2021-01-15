using Microsoft.EntityFrameworkCore;
using Models.Core;
using Models.Dto;
using Repositorys.Context;
using Repositorys.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Repositorys.Core
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext databaseContext;
        public UserRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public User Save(User user, bool uptadePassword = false)
        {
            using (var transaction = databaseContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                if (user.Id == 0)
                {
                    databaseContext.User.Add(user);
                }
                else
                {
                    databaseContext.Entry(user).State = EntityState.Modified;
                    if(!uptadePassword)
                    {
                        databaseContext.Entry(user).Property(x => x.Password).IsModified = false;
                        databaseContext.Entry(user).Property(x => x.Login).IsModified = false;
                    }
                }
                databaseContext.SaveChanges();
                transaction.Commit();

                return user;
            }
        }

        public List<UserDto> GetList()
        {
            var query = from u in databaseContext.User
                        select new UserDto(u.Id, u.CPF, u.Name, u.Email, u.Login, u.Role);
            return query.ToList();
        }

        public List<UserDto> GetList(int skip, int size)
        {
            var query = from u in databaseContext.User
                        select new UserDto(u.Id, u.CPF, u.Name, u.Email, u.Login, u.Role); ;
            return query.Skip(skip).Take(size).ToList();
        }

        public UserDto GetById(int id)
        {
            var query = from u in databaseContext.User
                        where u.Id == id
                        select new UserDto(u.Id, u.CPF, u.Name, u.Email, u.Login, u.Role);
            return query.FirstOrDefault();
        }

        public void Delete(User item)
        {
            if (item.Id > 0)
            {
                using (var transaction = databaseContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    databaseContext.Entry(item).State = EntityState.Deleted;
                    databaseContext.SaveChanges();
                    transaction.Commit();
                }
            }
        }

        public User GetUser(string login, string password)
        {
            return databaseContext.User.Where(x => x.Login == login && x.Password == password).FirstOrDefault();
        }

        public int Count()
        {
            return databaseContext.User.Count();

        }
        public UserDto GetUser(string cpf)
        {
            var query = from u in databaseContext.User
                        where u.CPF == cpf
                        select new UserDto(u.Id, u.CPF, u.Name, u.Email, u.Login, u.Role);

            return query.FirstOrDefault();
        }

        public User GetUser(int id)
        {
            var query = from u in databaseContext.User
                        where u.Id == id
                        select u;
            return query.FirstOrDefault();
        }
    }
}
