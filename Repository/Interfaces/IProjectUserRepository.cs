using Models.Core;
using Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositorys.Interfaces
{
    public interface IProjectUserRepository
    {

        /// <summary>
        /// Save or Update data ProjectUser
        /// </summary>
        /// <param name="user">Object ProjectUser for saver or update</param>
        ProjectUser Save(ProjectUser item);

        /// <summary>
        /// List all ProjectUsers
        /// </summary>
        /// <returns></returns>
        List<ProjectUser> GetList();

        /// <summary>
        /// List all ProjectUsers with pagination
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        List<ProjectUser> GetList(int skip, int size);

        /// <summary>
        /// Find ProjectUser by Id
        /// </summary>
        /// <param name="id">Value of Id for search</param>
        /// <returns></returns>
        ProjectUser GetById(int id);

        /// <summary>
        /// Delete ProjectUser
        /// </summary>
        /// <param name="item">Object ProjectUser for delete</param>
        void Delete(ProjectUser item);

        /// <summary>
        /// Count all ProjectUser
        /// </summary>
        int Count();

        /// <summary>
        /// List all ProjectUsers for one user
        /// </summary>
        /// <param name="userId">Get projects user</param>
        /// <returns></returns>
        List<ProjectUser> ListByUser(int userId, int projectId = 0);

        /// <summary>
        /// List all project users
        /// </summary>
        /// <param name="userId">Filter user id</param>
        /// <param name="projectId">Filter project id</param>
        /// <returns></returns>
        List<ProjectUser> FindByProjectOrUser(int userId = 0, int projectId = 0);

        /// <summary>
        /// List user habled for vinculate at projecct
        /// </summary>
        /// <param name="projectId">Id project</param>
        /// <returns></returns>
        List<User> FindByProjecthabledUser(int projectId = 0);
    }
}
