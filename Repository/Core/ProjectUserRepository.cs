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
            return databaseContext.ProjectUser.Skip(skip).Take(size).ToList();
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
    }
}
