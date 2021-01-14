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
    public class WorkHourRepository 
    {

        private readonly DatabaseContext databaseContext;
        public WorkHourRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        /// <summary>
        /// Save or Update data User
        /// </summary>
        /// <param name="item">Object User for saver or update</param>
        public void Save(WorkHour item)
        {
            using (var transaction = databaseContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                if (item.Id == 0)
                {
                    databaseContext.WorkHour.Add(item);
                }
                else
                {
                    databaseContext.Entry(item).State = EntityState.Modified;
                }
                databaseContext.SaveChanges();
                transaction.Commit();
            }
        }

        /// <summary>
        /// List all WorkHours
        /// </summary>
        /// <returns></returns>
        public List<WorkHour> GetList()
        {
            using (databaseContext)
            {
                return databaseContext.WorkHour.ToList();
            }
        }

        /// <summary>
        /// Find WorkHour by Id
        /// </summary>
        /// <param name="id">Value of Id for search</param>
        /// <returns></returns>
        public WorkHour GetById(int id)
        {
            using (databaseContext)
            {
                return databaseContext.WorkHour.Where(x => x.Id == id).FirstOrDefault();
            }
        }

        /// <summary>
        /// Delete WorkHour
        /// </summary>
        /// <param name="item">Object WorkHour for delete</param>
        public void Delete(WorkHour item)
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
