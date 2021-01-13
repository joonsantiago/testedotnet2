using Microsoft.EntityFrameworkCore;
using Models;
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
    public class UserRepository: IUserRepository
    {
        private readonly DatabaseContext databaseContext;
        public UserRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public void Save(User user)
        {
            using(var transaction = databaseContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                if(user.Id == 0)
                {
                    databaseContext.User.Add(user);
                }
                else
                {
                    databaseContext.Entry(user).State = EntityState.Modified;
                }
                databaseContext.SaveChanges();
                transaction.Commit();
            }
        }

        public List<UserDto> GetList()
        {
            using(databaseContext)
            {
                var query = from u in databaseContext.User
                            select new UserDto
                            {
                                Id = u.Id,
                                Login = u.Login,
                                Email = u.Email,
                                Name = u.Name,
                                Role = u.Role
                            };
                return query.ToList();
            }
        }

        public UserDto GetById(int id)
        {
            using (databaseContext)
            {
                var query = from u in databaseContext.User
                            where u.Id == id
                            select new UserDto
                            {
                                Id = u.Id,
                                Login = u.Login,
                                Email = u.Email,
                                Name = u.Name,
                                Role = u.Role
                            };
                return query.FirstOrDefault();
            }
        }

        public void Delete(User item)
        {
            if(item.Id > 0)
            {
                using (var transaction = databaseContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    databaseContext.Entry(item).State = EntityState.Deleted;
                    databaseContext.SaveChanges();
                    transaction.Commit();
                }
            }
        }

        public User FindUser(string login, string password)
        {
            using (databaseContext)
            {
                return databaseContext.User.Where(x => x.Login == login && x.Password == password).FirstOrDefault();
            }
        }

    }
}
