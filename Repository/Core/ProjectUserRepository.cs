using Microsoft.EntityFrameworkCore;
using Models.Core;
using Repositorys.Context;
using Repositorys.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;

namespace Repositorys.Core
{
    public class ProjectUserRepository : IProjectUserRepository
    {
        private readonly DatabaseContext databaseContext;
        public ProjectUserRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public ProjectUser Save(ProjectUser item)
        {
            using (var transaction = databaseContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                if (item.Id == 0)
                {
                    databaseContext.ProjectUser.Add(item);
                }
                else
                {
                    databaseContext.Entry(item).State = EntityState.Modified;
                }
                databaseContext.SaveChanges();
                transaction.Commit();
            }
            return item;
        }

        public List<ProjectUser> GetList()
        {
            return databaseContext.ProjectUser.ToList();
        }

        public List<ProjectUser> GetList(int skip, int size)
        {
            return databaseContext.ProjectUser.OrderByDescending(x => x.Id).Skip(skip).Take(size).ToList();
        }

        public ProjectUser GetById(int id)
        {
            return databaseContext.ProjectUser.Where(x => x.Id == id).FirstOrDefault();
        }

        public void Delete(ProjectUser item)
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

        public int Count()
        {
            return databaseContext.ProjectUser.Count();
        }

        public List<ProjectUser> ListByUser(int userId, int projectId = 0)
        {
            var entity = databaseContext.ProjectUser.Where(x => x.UserId == userId);

            if(projectId > 0)
            {
                entity = entity.Where(x => x.ProjectId == projectId);
            }

            return entity.ToList();
        }

        public List<ProjectUser> FindByProjectOrUser(int userId = 0, int projectId = 0)
        {
            var entity = from p in databaseContext.ProjectUser
                         select new ProjectUser
                         {
                             Project = p.Project,
                             ProjectId = p.ProjectId,
                             UserId = p.UserId,
                             User = new User
                             {
                                 Id = p.User.Id,
                                 Name = p.User.Name,
                                 Email = p.User.Email,
                                 Login = p.User.Login,
                                 Role = p.User.Role,
                                 CPF = p.User.CPF
                             }
                         };

            if (projectId > 0)
            {
                entity = entity.Where(x => x.ProjectId == projectId);
            }

            if (userId > 0)
            {
                entity = entity.Where(x => x.UserId == userId);
            }

            return entity.ToList();
        }

        public List<User> FindByProjecthabledUser(int projectId = 0)
        {
            List<int> list_ids = databaseContext.ProjectUser.Where(x => x.ProjectId == projectId).Select(p => p.UserId).ToList();

            var entity = from p in databaseContext.User
                         where p.Id > 0
                         select new User
                         {
                             Id = p.Id,
                             Name = p.Name,
                             Email = p.Email,
                             Login = p.Login,
                             Role = p.Role,
                             CPF = p.CPF
                         };

            return entity.Where(x => !list_ids.Contains(x.Id)).ToList();
        }
    }
}
