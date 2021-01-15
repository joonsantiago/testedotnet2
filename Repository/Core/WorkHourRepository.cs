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
    public class WorkHourRepository : IWorkHourRepository
    {

        private readonly DatabaseContext databaseContext;
        public WorkHourRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public WorkHour Save(WorkHour item)
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
            return item;
        }

        public List<WorkHour> GetList()
        {
            return databaseContext.WorkHour.ToList();
        }

        public List<WorkHour> GetList(int skip, int size)
        {
            return databaseContext.WorkHour.Skip(skip).Take(size).ToList();
        }

        public WorkHour GetById(int id)
        {
            return databaseContext.WorkHour.Where(x => x.Id == id).FirstOrDefault();
        }

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

        public int Count()
        {
            return databaseContext.WorkHour.Count();
        }
    }
}
