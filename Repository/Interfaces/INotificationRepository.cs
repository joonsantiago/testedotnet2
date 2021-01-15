using Models.Core;
using Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositorys.Interfaces
{
    public interface INotificationRepository
    {
        /// <summary>
        /// Save or Update data Notification
        /// </summary>
        /// <param name="user">Object Notification for saver or update</param>
        Notification Save(Notification item);

        /// <summary>
        /// List all Notifications
        /// </summary>
        /// <returns></returns>
        List<Notification> GetList();

        /// <summary>
        /// List all Notifications with pagination
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        List<Notification> GetList(int skip, int size);

        /// <summary>
        /// Find Notification by Id
        /// </summary>
        /// <param name="id">Value of Id for search</param>
        /// <returns></returns>
        Notification GetById(int id);

        /// <summary>
        /// Delete Notification
        /// </summary>
        /// <param name="item">Object Notification for delete</param>
        void Delete(Notification item);

        /// <summary>
        /// Count all Notification
        /// </summary>
        int Count();
    }
}
