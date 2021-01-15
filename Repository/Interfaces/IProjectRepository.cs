using Models;
using Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositorys.Interfaces
{
    public interface IProjectRepository
    {

        /// <summary>
        /// Save or Update data Project
        /// </summary>
        /// <param name="user">Object Project for saver or update</param>
        Project Save(Project item);

        /// <summary>
        /// List all Projects
        /// </summary>
        /// <returns></returns>
        List<Project> GetList();

        /// <summary>
        /// List all Projects with pagination
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        List<Project> GetList(int skip, int size);

        /// <summary>
        /// Find Project by Id
        /// </summary>
        /// <param name="id">Value of Id for search</param>
        /// <returns></returns>
        Project GetById(int id);

        /// <summary>
        /// Delete Project
        /// </summary>
        /// <param name="item">Object Project for delete</param>
        void Delete(Project item);

        /// <summary>
        /// Count all Project
        /// </summary>
        int Count();
    }
}
