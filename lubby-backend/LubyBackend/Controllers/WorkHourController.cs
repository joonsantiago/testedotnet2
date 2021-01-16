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
using LubyBackend.Services;

namespace LubyBackend.Controllers
{

    [Route("v1/work-hours")]
    public class WorkHourController : BaseController
    {
        IWorkHourRepository workHourRepository;
        IUserRepository userRepository;
        IConfiguration configuration;
        INotificationRepository notificationRepository;
        IProjectUserRepository projectUserRepository;

        public WorkHourController(IUserRepository userRepository, IConfiguration configuration,
                                IWorkHourRepository workHourRepository, INotificationRepository notificationRepository,
                                IProjectUserRepository projectUserRepository) 
            : base(configuration)
        {
            this.userRepository = userRepository;
            this.configuration = configuration;

            this.workHourRepository = workHourRepository;
            this.notificationRepository = notificationRepository;
            this.projectUserRepository = projectUserRepository;
        }

        /// <summary>
        /// Get one work user by Id
        /// </summary>
        /// <param name="id">Id work user for return</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "1,2")]
        public async Task<ActionResult<dynamic>> GetById(int id)
        {
            int userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            int roleId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value);

            if(roleId == (int)EnumRole.Administrator)
            {
                userId = 0;
            }

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

        /// <summary>
        /// Get the list of work user
        /// </summary>
        /// <param name="page">Page for find in pagination</param>
        /// <param name="sizePage">Size of data in page</param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [Authorize(Roles = "1,2")]
        public async Task<ActionResult<dynamic>> GetAll([FromQuery(Name = "page")] int page = 0, [FromQuery(Name = "size")] int sizePage = 15)
        {
            int skip = page * sizePage;
            int userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            int roleId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value);

            if (roleId == (int)EnumRole.Administrator)
            {
                userId = 0;
            }

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

        /// <summary>
        /// Create a new work user
        /// </summary>
        /// <param name="workHour">The work user object</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [Authorize(Roles = "1,2")]
        public async Task<ActionResult<dynamic>> SaveWorkHour([FromBody] WorkHour workHour)
        {
            List<string> validations_erro = new List<string>();
            WorkHour data_workHour = new WorkHour();

            Notification notification = new Notification
            {
                Status = (int)EnumNotificationStatus.Sucesso
            };

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

            var projectUser = projectUserRepository.ListByUser(workHour.UserId, workHour.ProjectId);

            if (projectUser.Count() == 0)
            {
                validations_erro.Add("User don't has vinculate to project");
            }

            if (validations_erro.Count() > 0)
            {
                return BadRequest(new { success = false, data = new { }, messages = validations_erro });
            }

            try
            {
                if(workHour.FinishedAt != null)
                {
                    var d1 = DateTime.Parse(workHour.CreatedAt.ToString());
                    var d2 = DateTime.Parse(workHour.FinishedAt.ToString());
                    var totalMinutes = float.Parse((d2 - d1).Minutes.ToString());
                    var totalHours = float.Parse(((d2 - d1).Hours * 60).ToString());
                    decimal totalTime = Decimal.Parse(((totalHours + totalMinutes)/ 60).ToString());
                    workHour.TotalTime = totalTime;
                }

                data_workHour = workHourRepository.Save(workHour);
                notification.WorkHourId = data_workHour.Id;
                notification = notificationRepository.Save(notification);
                return Ok(new { success = true, data = data_workHour, messages = "Item successfull created" });
            }
            catch (Exception ex)
            {
                notification.Status = (int)EnumNotificationStatus.ErroAplicacao;
                notification = notificationRepository.Save(notification);
                return CatchError(ex, "Save new workHour");
            }
            finally
            {
                try
                {

                    if(data_workHour.Id > 0)
                    {
                        var urlSendNotification = configuration["external_links:send_notification"];
                        var responseSendNotification = HttpService.Get(urlSendNotification + "/" + data_workHour.Id.ToString());

                        if(responseSendNotification.httpStatusCode != 200 || responseSendNotification.message != "Enviado")
                        {
                            notification.Status = (int)EnumNotificationStatus.ServidorIndisponivel;
                            notification = notificationRepository.Save(notification);
                        }
                    }
                }
                catch (Exception ex1)
                {

                    notification.Status = (int)EnumNotificationStatus.ServidorIndisponivel;
                    notification = notificationRepository.Save(notification);
                    CatchError(ex1, "Send notification Work Hour");
                }
            }
        }

        /// <summary>
        /// Update one work user
        /// </summary>
        /// <param name="workHour">The work user object</param>
        /// <returns></returns>
        [HttpPatch]
        [Route("")]
        [Authorize(Roles = "1,2")]
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

            var projectUser = projectUserRepository.ListByUser(workHour.UserId, workHour.ProjectId);

            if (projectUser.Count() == 0)
            {
                validations_erro.Add("User don't has vinculate to project");
            }

            if (validations_erro.Count() > 0)
            {
                return BadRequest(new { success = false, data = new { }, messages = validations_erro });
            }

            try
            {
                if (workHour.FinishedAt != null)
                {
                    var d1 = DateTime.Parse(workHour.CreatedAt.ToString());
                    var d2 = DateTime.Parse(workHour.FinishedAt.ToString());
                    var totalMinutes = float.Parse((d2 - d1).Minutes.ToString());
                    var totalHours = float.Parse(((d2 - d1).Hours * 60).ToString());
                    decimal totalTime = Decimal.Parse(((totalHours + totalMinutes) / 60).ToString());
                    workHour.TotalTime = totalTime;
                }

                WorkHour data_workHour = workHourRepository.Save(workHour);
                return Ok(new { success = true, data = data_workHour, messages = "Item successfull updated" });
            }
            catch (Exception ex)
            {
                return CatchError(ex, string.Format("Update workHour id({0})", workHour.Id));
            }

        }

        /// <summary>
        /// Delete one work user
        /// </summary>
        /// <param name="id">Id work user for delete</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "1,2")]
        public async Task<ActionResult<dynamic>> DeleteWorkHour(int id)
        {
            int userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            int roleId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value);

            if (roleId == (int)EnumRole.Administrator)
            {
                userId = 0;
            }

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

        /// <summary>
        /// Get top 5 developers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("dev-top5-week")]
        [Authorize]
        public async Task<ActionResult<dynamic>> DevTop5Week()
        {
            var data = workHourRepository.DevTop5Week().OrderByDescending(x => x.TotalHours).ToList();
            return Ok(new { success = true, data = data, messages = "Item successfull calculate" });
        }

    }
}
