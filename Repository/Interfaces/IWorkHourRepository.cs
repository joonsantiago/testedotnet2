using Models.Core;
using Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositorys.Interfaces
{
    public interface IWorkHourRepository
    {
        /// <summary>
        /// Save or Update data WorkHour
        /// </summary>
        /// <param name="user">Object WorkHour for saver or update</param>
        WorkHour Save(WorkHour item);

        /// <summary>
        /// List all WorkHours
        /// </summary>
        /// <returns></returns>
        List<WorkHour> GetList();

        /// <summary>
        /// List all WorkHours with pagination
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        List<WorkHour> GetList(int skip, int size);

        /// <summary>
        /// Find WorkHour by Id
        /// </summary>
        /// <param name="id">Value of Id for search</param>
        /// <returns></returns>
        WorkHour GetById(int id);

        /// <summary>
        /// Delete WorkHour
        /// </summary>
        /// <param name="item">Object WorkHour for delete</param>
        void Delete(WorkHour item);

        /// <summary>
        /// Count all WorkHour
        /// </summary>
        int Count();
    }
}
