using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Repositorys.Interfaces;
using Models;
using Microsoft.AspNetCore.Authorization;
using Models.Dto;
using LubyBackend.Utils;

namespace LubyBackend.Controllers
{
    [ApiController]
    [Route("v1/projects")]
    [Authorize]
    public class ProjectController : Controller
    {
        IProjectRepository projectRepository;

        public ProjectController(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        [HttpGet]
        [Route("{id}")]
        public Project GetById(int id)
        {
            return projectRepository.GetById(id);
        }

        [HttpGet]
        [Route("")]
        public Pagination<Project> GetAll([FromQuery(Name = "page")] int page = 0, [FromQuery(Name = "size")] int sizePage = 15)
        {
            int skip = page * sizePage;

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
    }
}
