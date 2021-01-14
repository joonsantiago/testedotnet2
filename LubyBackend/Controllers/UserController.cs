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
    [Route("v1/users")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserRepository userRepository;
        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        [Route("{id}")]
        public UserDto GetById(int id)
        {
            return userRepository.GetById(id);
        }

        [HttpGet]
        [Route("")]
        public Pagination<UserDto> GetAll([FromQuery(Name = "page")] int page = 0, [FromQuery(Name = "size")] int sizePage = 15)
        {
            int skip = page * sizePage;

            int total = userRepository.Count();
            List<UserDto> list = userRepository.GetList(skip, sizePage);

            Pagination<UserDto> dataPagination = new Pagination<UserDto>
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
