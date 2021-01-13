using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Repositorys.Interfaces;
using Models;
using Microsoft.AspNetCore.Authorization;
using Models.Dto;

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

    }
}
