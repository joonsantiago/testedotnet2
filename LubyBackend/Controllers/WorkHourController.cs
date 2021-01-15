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
    [Route("v1/work-hours")]
    public class WorkHourController : BaseController
    {
        IWorkHourRepository workHourRepository;
        IUserRepository userRepository;
        IConfiguration configuration;


        public WorkHourController(IUserRepository userRepository, IConfiguration configuration,
                                IWorkHourRepository workHourRepository) : base(configuration)
        {
            this.userRepository = userRepository;
            //this.userLoged = this.userRepository.GetUser(email);

            this.workHourRepository = workHourRepository;
            this.configuration = configuration;
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<ActionResult<dynamic>> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { success = false, data = new { }, messages = "Send Id workHour it's required" });
            }

            try
            {
                WorkHour data_workHour = workHourRepository.GetById(id);
                if (data_workHour == null)
                {
                    return Ok(new { success = false, data = new { }, messages = "No having workHour with the Id" });
                }
                return Ok(new { success = true, data = data_workHour, messages = "Item successfull finded" });
            }
            catch (Exception ex)
            {
                return CatchError(ex, "Find workHour by Id");
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
                int total = workHourRepository.Count();
                List<WorkHour> list = workHourRepository.GetList(skip, sizePage);

                Pagination<WorkHour> dataPagination = new Pagination<WorkHour>
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
                return CatchError(ex, "Find all workHours");
            }
        }

        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<dynamic>> SaveWorkHour([FromBody] WorkHour workHour)
        {

            List<string> validations_erro = new List<string>();
            if (workHour.CreatedAt == null)
            {
                validations_erro.Add("WorkHour created date is required");
            }

            if (workHour.ProjectId <= 0)
            {
                validations_erro.Add("WorkHour project id is required");
            }

            if (workHour.UserId <= 0)
            {
                validations_erro.Add("WorkHour user id is required");
            }

            if (validations_erro.Count() > 0)
            {
                return BadRequest(new { success = false, data = new { }, messages = validations_erro });
            }

            try
            {
                WorkHour data_workHour = workHourRepository.Save(workHour);
                return Ok(new { success = true, data = data_workHour, messages = "Item successfull created" });
            }
            catch (Exception ex)
            {
                return CatchError(ex, "Save new workHour");
            }
        }

        [HttpPatch]
        [Route("")]
        [Authorize]
        public async Task<ActionResult<dynamic>> UpdateWorkHour([FromBody] WorkHour workHour)
        {
            List<string> validations_erro = new List<string>();
            if (workHour.Id <= 0)
            {
                validations_erro.Add("WorkHour id is required");
            }

            if (workHour.CreatedAt == null)
            {
                validations_erro.Add("WorkHour created date is required");
            }

            if (workHour.ProjectId <= 0)
            {
                validations_erro.Add("WorkHour project id is required");
            }

            if (workHour.UserId <= 0)
            {
                validations_erro.Add("WorkHour user id is required");
            }

            if (validations_erro.Count() > 0)
            {
                return BadRequest(new { success = false, data = new { }, messages = validations_erro });
            }

            try
            {
                WorkHour data_workHour = workHourRepository.Save(workHour);
                return Ok(new { success = true, data = data_workHour, messages = "Item successfull updated" });
            }
            catch (Exception ex)
            {
                return CatchError(ex, string.Format("Update workHour id({0})", workHour.Id));
            }

        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public async Task<ActionResult<dynamic>> DeleteWorkHour(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { success = false, data = new { }, messages = "Send Id workHour" });
            }

            try
            {
                WorkHour data_workHour = workHourRepository.GetById(id);
                if (data_workHour == null)
                {
                    return Ok(new { success = false, data = new { }, messages = "No having workHour with the Id" });
                }
                workHourRepository.Delete(data_workHour);
                return Ok(new { success = true, data = id, messages = "Item successfull deleted" });
            }
            catch (Exception ex)
            {
                return CatchError(ex, string.Format("Delete workHour id ({0})", id));
            }
        }

    }
}
