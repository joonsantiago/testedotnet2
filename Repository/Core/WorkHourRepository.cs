using Microsoft.EntityFrameworkCore;
using Models.Core;
using Repositorys.Context;
using Repositorys.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using Models.Dto;

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

        public List<WorkHour> GetList(int skip, int size, int userId = 0)
        {
            var entity = databaseContext.WorkHour
                .OrderByDescending(x => x.Id)
                .Skip(skip).Take(size);

            entity = userId > 0 ? entity.Where(x => x.UserId == userId) : entity;
            return entity.ToList();
        }

        public WorkHour GetById(int id, int userId = 0)
        {
            var entity = databaseContext.WorkHour.Where(x => x.Id == id);

            entity = userId > 0 ? entity.Where(x => x.UserId == userId) : entity;
            return entity.FirstOrDefault();
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

        public int Count(int userId = 0)
        {
            if(userId > 0)
            {
                return databaseContext.WorkHour.Where(x => x.UserId == userId).Count();
            }
            else
            {
                return databaseContext.WorkHour.Count();
            }
        }

        public List<DevTop5WeekDto> DevTop5Week()
        {
            List<WorkHour> userList = new List<WorkHour>();
            List<DevTop5WeekDto> dataReturn = new List<DevTop5WeekDto>();

            var dateWeek = DateTime.Now.AddDays(-7);
            var listWorks = databaseContext.WorkHour.Where(wh => wh.FinishedAt != null && wh.CreatedAt >= dateWeek && wh.FinishedAt <= DateTime.Now).ToList();

            foreach (var lw in listWorks)
            {
                var lwUser = userList.Where(x => x.UserId == lw.UserId).FirstOrDefault();
                if(lwUser == null)
                {
                    lwUser = new WorkHour
                    {
                        UserId = lw.UserId,
                        TotalTime = 0
                    };

                    lwUser.TotalTime += lw.TotalTime;
                    userList.Add(lwUser);
                }
                else
                {
                    lwUser.TotalTime += lw.TotalTime;
                }
            }

            userList = userList.OrderByDescending(x => x.TotalTime).Take(5).ToList();
            List<int> ids = userList.Select(s => s.UserId).ToList();

            var users = databaseContext.User.Where(x => ids.Contains(x.Id) ).ToList();

            foreach (var u in users)
            {
                DevTop5WeekDto dt5 = new DevTop5WeekDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    TotalHours = userList.Where(x => x.UserId == u.Id).FirstOrDefault().TotalTime
                };
                dataReturn.Add(dt5);
            }

            return dataReturn;
        }
    }
}
