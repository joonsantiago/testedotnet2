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
    [Route("v1/projects")]
    public class ProjectController : BaseController
    {
        IProjectRepository projectRepository;
        IUserRepository userRepository;
        IConfiguration configuration;


        public ProjectController(IUserRepository userRepository, IConfiguration configuration,
                                IProjectRepository projectRepository) : base(configuration)
        {
            this.userRepository = userRepository;

            this.projectRepository = projectRepository;
            this.configuration = configuration;
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<ActionResult<dynamic>> GetById(int id)
        {
            if (id <= 0)
            {                
                return BadRequest(new { success = false, data = new { }, messages = "Send Id project it's required" });
            }

            try
            {
                Project data_project = projectRepository.GetById(id);
                if (data_project == null)
                {
                    return Ok(new { success = false, data = new { }, messages = "No having project with the Id" });
                }
                return Ok(new { success = true, data = data_project, messages = "Item successfull finded" });
            }
            catch (Exception ex)
            {
                return CatchError(ex, "Find project by Id");
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
                int total = projectRepository.Count();
                List<Project> list = projectRepository.GetList(skip, sizePage);

                Pagination<Project> dataPagination = new Pagination<Project>
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
                return CatchError(ex, "Find all projects");
            }            
        }

        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<dynamic>> SaveProject([FromBody] Project project)
        {

            if(string.IsNullOrEmpty(project.Name))
            {
                return BadRequest(new { success = false, data= new { }, messages = "Project name is required" });
            }


            try
            {
                Project data_project = projectRepository.Save(project);
                return Ok(new { success = true, data = data_project, messages = "Item successfull created" });
            }
            catch (Exception ex)
            {
                return CatchError(ex, "Save new project");
            }
        }

        [HttpPatch]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<dynamic>> UpdateProject([FromBody] Project project)
        {
            List<string> validations_erro = new List<string>();
            if (string.IsNullOrEmpty(project.Name))
            {
                validations_erro.Add("Project name is required");
            }

            if(project.Id <= 0)
            {
                validations_erro.Add("Project id is required");
            }

            if(validations_erro.Count() > 0)
            {
                return BadRequest(new { success = false, data = new { }, messages = validations_erro });
            }

            try
            {
                Project data_project = projectRepository.Save(project);
                return Ok(new { success = true, data = data_project, messages = "Item successfull updated" });
            }
            catch (Exception ex)
            {
                return CatchError(ex, string.Format("Update project id({0})", project.Id));
            }

        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public async Task<ActionResult<dynamic>> DeleteProject(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { success = false, data = new { }, messages = "Send Id project" });
            }

            try
            {
                Project data_project = projectRepository.GetById(id);
                if (data_project == null)
                {
                    return Ok(new { success = false, data = new { }, messages = "No having project with the Id" });
                }
                projectRepository.Delete(data_project);
                return Ok(new { success = true, data = id, messages = "Item successfull deleted" });
            }
            catch (Exception ex)
            {
                return CatchError(ex, string.Format("Delete project id ({0})", id));
            }
        }

    }
}
