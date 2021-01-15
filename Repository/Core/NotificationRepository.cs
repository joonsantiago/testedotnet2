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
    public class NotificationRepository : INotificationRepository
    {

        private readonly DatabaseContext databaseContext;
        public NotificationRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public Notification Save(Notification item)
        {
            using (var transaction = databaseContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                if (item.Id == 0)
                {
                    databaseContext.Notification.Add(item);
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

        public List<Notification> GetList()
        {
            return databaseContext.Notification.ToList();
        }

        public List<Notification> GetList(int skip, int size)
        {
            return databaseContext.Notification.OrderByDescending(x => x.Id).Skip(skip).Take(size).ToList();
        }

        public Notification GetById(int id)
        {
            return databaseContext.Notification.Where(x => x.Id == id).FirstOrDefault();
        }

        public void Delete(Notification item)
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
            return databaseContext.Notification.Count();
        }
    }
}
