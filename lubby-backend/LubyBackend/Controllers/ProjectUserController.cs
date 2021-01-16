using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Repositorys.Interfaces;
using Models.Core;
using Microsoft.AspNetCore.Authorization;
using Models.Dto;
using LubyBackend.Utils;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Models.Constantes;
using Swashbuckle.AspNetCore.Filters;

namespace LubyBackend.Controllers
{
    [Route("v1/projects-user")]
    public class ProjectUserController : BaseController
    {

        IProjectUserRepository projectUserRepository;
        IUserRepository userRepository;
        IConfiguration configuration;


        public ProjectUserController(IUserRepository userRepository, IConfiguration configuration,
                                IProjectUserRepository projectUserRepository) : base(configuration)
        {
            this.userRepository = userRepository;

            this.projectUserRepository = projectUserRepository;
            this.configuration = configuration;
        }

        /// <summary>
        /// Get one project user by Id
        /// </summary>
        /// <param name="id">Id project for return</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<ActionResult<dynamic>> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { success = false, data = new { }, messages = "Send Id projectUser it's required" });
            }

            try
            {
                ProjectUser data_projectUser = projectUserRepository.GetById(id);
                if (data_projectUser == null)
                {
                    return Ok(new { success = false, data = new { }, messages = "No having projectUser with the Id" });
                }
                return Ok(new { success = true, data = data_projectUser, messages = "Item successfull finded" });
            }
            catch (Exception ex)
            {
                return CatchError(ex, "Find projectUser by Id");
            }
        }

        /// <summary>
        /// Get users for one project user
        /// </summary>
        /// <param name="projectId">Id project for found</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{projectId}/users")]
        [Authorize]
        public async Task<ActionResult<dynamic>> GetProjectsUser(int projectId)
        {
            if (projectId <= 0)
            {
                return BadRequest(new { success = false, data = new { }, messages = "Send Id projectUser it's required" });
            }

            try
            {
                List<ProjectUser> data_projectUser = projectUserRepository.FindByProjectOrUser(0, projectId);
                if (data_projectUser == null)
                {
                    return Ok(new { success = false, data = new { }, messages = "No having projectUser with the Id" });
                }
                return Ok(new { success = true, data = data_projectUser, messages = "Item successfull finded" });
            }
            catch (Exception ex)
            {
                return CatchError(ex, "Find projectUser by Id");
            }
        }

        /// <summary>
        /// Get users not relation a one project
        /// </summary>
        /// <param name="projectId">Id project for found</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{projectId}/habled-users")]
        [Authorize]
        public async Task<ActionResult<dynamic>> GetProjectsHabledUser(int projectId)
        {
            if (projectId <= 0)
            {
                return BadRequest(new { success = false, data = new { }, messages = "Send Id projectUser it's required" });
            }

            try
            {
                List<User> data_users = projectUserRepository.FindByProjecthabledUser(projectId);
                if (data_users == null)
                {
                    return Ok(new { success = false, data = new { }, messages = "No having projectUser with the Id" });
                }
                return Ok(new { success = true, data = data_users, messages = "Item successfull finded" });
            }
            catch (Exception ex)
            {
                return CatchError(ex, "Find projectUser by Id");
            }
        }

        /// <summary>
        /// Get projects with relation a user
        /// </summary>
        /// <param name="userId">Id user for found</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{userId}/projects")]
        [Authorize]
        public async Task<ActionResult<dynamic>> GetUserProjects(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest(new { success = false, data = new { }, messages = "Send Id projectUser it's required" });
            }

            try
            {
                List<ProjectUser> data_projectUser = projectUserRepository.FindByProjectOrUser(userId, 0);
                if (data_projectUser == null)
                {
                    return Ok(new { success = false, data = new { }, messages = "No having projectUser with the Id" });
                }
                return Ok(new { success = true, data = data_projectUser, messages = "Item successfull finded" });
            }
            catch (Exception ex)
            {
                return CatchError(ex, "Find projectUser by Id");
            }
        }

        /// <summary>
        /// Get the list of projects
        /// </summary>
        /// <param name="page">Page for find in pagination</param>
        /// <param name="sizePage">Size of data in page</param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<dynamic>> GetAll([FromQuery(Name = "page")] int page = 0, [FromQuery(Name = "size")] int sizePage = 15)
        {
            int skip = page * sizePage;

            string id = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value;


            try
            {
                int total = projectUserRepository.Count();
                List<ProjectUser> list = projectUserRepository.GetList(skip, sizePage);

                Pagination<ProjectUser> dataPagination = new Pagination<ProjectUser>
                {
                    Page = page,
                    TotalCount = total,
                    SizePage = sizePage,
                    Items = list
                };

                return dataPagination;
            }
            catch (Exception ex)
            {
                return CatchError(ex, "Find all projectUsers");
            }
        }

        /// <summary>
        /// Create a new projeto user
        /// </summary>
        /// <param name="projectUser">The project user object</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<dynamic>> SaveProjectUser([FromBody] ProjectUser projectUser)
        {
            
            List<string> validations_erro = new List<string>();

            if (projectUser.ProjectId <= 0)
            {
                validations_erro.Add("ProjectUser project id is required");
            }

            if (projectUser.UserId <= 0)
            {
                validations_erro.Add("ProjectUser user id is required");
            }

            int userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            int roleId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value);

            if (roleId != (int)EnumRole.Administrator)
            {
                var projectUserOlder = projectUserRepository.ListByUser(projectUser.UserId, projectUser.ProjectId);

                if (projectUserOlder.Count >= 1)
                {
                    validations_erro.Add("User has vinculation to project");
                }
            }


            if (validations_erro.Count() > 0)
            {
                return BadRequest(new { success = false, data = new { }, messages = validations_erro });
            }

            try
            {
                ProjectUser data_projectUser = projectUserRepository.Save(projectUser);
                return Ok(new { success = true, data = data_projectUser, messages = "Item successfull created" });
            }
            catch (Exception ex)
            {
                return CatchError(ex, "Save new projectUser");
            }
        }

        /// <summary>
        /// Update one project user
        /// </summary>
        /// <param name="projectUser">The project user object</param>
        /// <returns></returns>
        [HttpPatch]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<dynamic>> UpdateProjectUser([FromBody] ProjectUser projectUser)
        {
            List<string> validations_erro = new List<string>();

            if (projectUser.Id <= 0)
            {
                validations_erro.Add("ProjectUser id is required");
            }

            if (projectUser.ProjectId <= 0)
            {
                validations_erro.Add("ProjectUser project id is required");
            }

            if (projectUser.UserId <= 0)
            {
                validations_erro.Add("ProjectUser user id is required");
            }


            if (validations_erro.Count() > 0)
            {
                return BadRequest(new { success = false, data = new { }, messages = validations_erro });
            }

            try
            {
                ProjectUser data_projectUser = projectUserRepository.Save(projectUser);
                return Ok(new { success = true, data = data_projectUser, messages = "Item successfull updated" });
            }
            catch (Exception ex)
            {
                return CatchError(ex, string.Format("Update projectUser id({0})", projectUser.Id));
            }

        }

        /// <summary>
        /// Delete one projetct user
        /// </summary>
        /// <param name="id">Id project for delete</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public async Task<ActionResult<dynamic>> DeleteProjectUser(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { success = false, data = new { }, messages = "Send Id projectUser" });
            }

            try
            {
                ProjectUser data_projectUser = projectUserRepository.GetById(id);
                if (data_projectUser == null)
                {
                    return Ok(new { success = false, data = new { }, messages = "No having projectUser with the Id" });
                }
                projectUserRepository.Delete(data_projectUser);
                return Ok(new { success = true, data = id, messages = "Item successfull deleted" });
            }
            catch (Exception ex)
            {
                return CatchError(ex, string.Format("Delete projectUser id ({0})", id));
            }
        }

    }
}
