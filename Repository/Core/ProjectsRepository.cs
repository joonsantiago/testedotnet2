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
    public class ProjectsRepository: IProjectRepository
    {

        private readonly DatabaseContext databaseContext;
        public ProjectsRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public Project Save(Project item)
        {
            using (var transaction = databaseContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                if (item.Id == 0)
                {
                    databaseContext.Project.Add(item);
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

        public List<Project> GetList()
        {
            return databaseContext.Project.ToList();
        }

        public List<Project> GetList(int skip, int size)
        {
            return databaseContext.Project.OrderByDescending(x => x.Id).Skip(skip).Take(size).ToList();
        }

        public Project GetById(int id)
        {
            return databaseContext.Project.Where(x => x.Id == id).FirstOrDefault();
        }

        public void Delete(Project item)
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
            return databaseContext.Project.Count();
        }
    }
}
