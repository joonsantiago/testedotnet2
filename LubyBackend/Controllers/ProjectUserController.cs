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

namespace LubyBackend.Controllers
{
    [ApiController]
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
            //this.userLoged = this.userRepository.GetUser(email);

            this.projectUserRepository = projectUserRepository;
            this.configuration = configuration;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<dynamic>> GetById(int id)
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
                return Ok(new { success = true, data = data_projectUser, messages = "Item successfull finded" });
            }
            catch (Exception ex)
            {
                return CatchError(ex, "Find projectUser by Id");
            }
        }

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

        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<dynamic>> SaveProjectUser([FromBody] ProjectUser projectUser)
        {
            
            List<string> validations_erro = new List<string>();

            if (projectUser.projectId <= 0)
            {
                validations_erro.Add("ProjectUser project id is required");
            }

            if (projectUser.userId <= 0)
            {
                validations_erro.Add("ProjectUser user id is required");
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

            if (projectUser.projectId <= 0)
            {
                validations_erro.Add("ProjectUser project id is required");
            }

            if (projectUser.userId <= 0)
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
