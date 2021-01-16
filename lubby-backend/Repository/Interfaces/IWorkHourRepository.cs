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
        /// <param name="item">Object WorkHour for saver or update</param>
        WorkHour Save(WorkHour item);

        /// <summary>
        /// List all WorkHours
        /// </summary>
        /// <returns></returns>
        List<WorkHour> GetList();

        /// <summary>
        /// List all WorkHours with pagination
        /// </summary>
        /// <param name="skip">Line for search start</param>
        /// <param name="size">Size list data</param>
        /// <param name="userId">User if for seachr</param>
        /// <returns></returns>
        List<WorkHour> GetList(int skip, int size, int userId = 0);

        /// <summary>
        /// Find WorkHour by Id
        /// </summary>
        /// <param name="id">Value of Id for search</param>
        /// <param name="userId">User if for seachr</param>
        /// <param name="userId">User if for seachr</param>
        /// <returns></returns>
        WorkHour GetById(int id, int userId = 0);

        /// <summary>
        /// Delete WorkHour
        /// </summary>
        /// <param name="item">Object WorkHour for delete</param>
        void Delete(WorkHour item);

        /// <summary>
        /// Count all WorkHour
        /// </summary>
        /// <param name="userId">User if for seachr</param>
        int Count(int userId = 0);

        /// <summary>
        /// Return top 5 developers with has many hour work
        /// </summary>
        /// <returns></returns>
        List<DevTop5WeekDto> DevTop5Week();
    }
}
