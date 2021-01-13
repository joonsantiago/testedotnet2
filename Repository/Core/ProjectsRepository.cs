using Microsoft.EntityFrameworkCore;
using Models;
using Repositorys.Context;
using Repositorys.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;

namespace Repositorys.Core
{
    public class ProjectsRepository: IBaseRepository<Project>
    {

        private readonly DatabaseContext databaseContext;
        public ProjectsRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        /// <summary>
        /// Save or Update data User
        /// </summary>
        /// <param name="user">Object User for saver or update</param>
        public void Save(Project user)
        {
            using (var transaction = databaseContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                if (user.Id == 0)
                {
                    databaseContext.Project.Add(user);
                }
                else
                {
                    databaseContext.Entry(user).State = EntityState.Modified;
                }
                databaseContext.SaveChanges();
                transaction.Commit();
            }
        }

        /// <summary>
        /// List all Projects
        /// </summary>
        /// <returns></returns>
        public List<Project> GetList()
        {
            using (databaseContext)
            {
                return databaseContext.Project.ToList();
            }
        }

        /// <summary>
        /// Find Project by Id
        /// </summary>
        /// <param name="id">Value of Id for search</param>
        /// <returns></returns>
        public Project GetById(int id)
        {
            using (databaseContext)
            {
                return databaseContext.Project.Where(x => x.Id == id).FirstOrDefault();
            }
        }

        /// <summary>
        /// Delete Project
        /// </summary>
        /// <param name="item">Object Project for delete</param>
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
    }
}
